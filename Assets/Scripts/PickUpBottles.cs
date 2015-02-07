using UnityEngine;
using System.Collections;
using Rewired;
using System.Collections.Generic;
public class PickUpBottles : MonoBehaviour {
	bool insidePickup;
	Player player;

	private bool pressY;
	private bool pressA;

	public List<GameObject> bottles;
	public int bottlesToSpawn = 3;

	public GameObject tray;

	public List<Transform> spawnPoints;
	void Start () 
	{
		player = ReInput.players.GetPlayer(0);
		foreach(Transform tr in tray.transform)
		{
			spawnPoints.Add(tr);
		}
	}

	void Update () 
	{
		pressY = player.GetButtonDown("Y");
		pressA = player.GetButtonDown("A");

		if(pressA && insidePickup)
		{
			for(int i = 0; i < bottlesToSpawn; i++)
			{
				Instantiate(bottles[i], spawnPoints[i].position, Quaternion.identity);
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("pickup") && !insidePickup)
		{
			insidePickup = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.CompareTag("pickup") && insidePickup)
		{
			insidePickup = false;
		}
	}


}
