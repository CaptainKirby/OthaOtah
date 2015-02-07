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

	private Objectives objectives;
	private BottleManager bottleMgr;

	void Start () 
	{
		bottleMgr = GameObject.FindObjectOfType<BottleManager>();
		objectives = GetComponent<Objectives>();
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

		if(pressA && insidePickup && objectives.getDrink)
		{
			objectives.getDrink = false;

			for(int i = 0; i < bottlesToSpawn; i++)
			{
				GameObject bottle = Instantiate(bottles[i], spawnPoints[i].position, Quaternion.identity) as GameObject;
				bottleMgr.curBottles.Add(bottles[i]);
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
