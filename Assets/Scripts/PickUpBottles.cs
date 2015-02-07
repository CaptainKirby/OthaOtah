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
	void Start () 
	{
		player = ReInput.players.GetPlayer(0);
	}

	void Update () 
	{
		pressY = player.GetButtonDown("Y");
		pressA = player.GetButtonDown("A");

		if(pressA && insidePickup)
		{
			for(int i = 0; i < bottlesToSpawn; i++)
			{
				Instantiate(bottles[Random.Range(0, bottles.Count)], new Vector3(tray.transform.position.x + Random.Range(0.1f, 0.8f), tray.transform.position.y + 1, tray.transform.position.z + Random.Range(0.1f, 0.8f)), Quaternion.identity);
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
