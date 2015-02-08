using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Objectives : MonoBehaviour {
	[HideInInspector]
	public bool getDrink;
	[HideInInspector]
	public bool deliverDrink;
	public GameObject arrow;
	public GameObject barLocation;
	public bool spawnDrinkArrow;
	public bool spawnDeliverArrow;
	private GameObject drinkArrow;
	private GameObject deliverArrow;

	public List<GameObject> clients;
	public int curClient;
	public GameObject curClientObj;
	public bool end;
	void Start () 
	{
		barLocation = GameObject.FindGameObjectWithTag("pickup");
		getDrink = true;
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("client"))
		{
			clients.Add(g);
		}
	}
	
	void Update () 
	{
		if(end)
		{

		}
		if(getDrink && !spawnDrinkArrow && !end)
		{
//			Debug.Log ("WAT");
			spawnDrinkArrow = true;
			drinkArrow = Instantiate(arrow, barLocation.transform.position, barLocation.transform.rotation) as GameObject;
			if(drinkArrow.activeSelf == false)
			{
				drinkArrow.SetActive(true);
			}
			if(deliverArrow != null)
			{
				deliverArrow.SetActive(false);
			}
		}

		if(!getDrink && !deliverDrink)
		{

			drinkArrow.SetActive(false);
			deliverDrink= true;
//			Debug.Log("Ghhhhh");

						spawnDeliverArrow = false;
		}

		if(deliverDrink && !spawnDeliverArrow)
		{
			Debug.Log ("WAT2");

			spawnDeliverArrow = true;
			if(deliverArrow == null)
			{
				deliverArrow = Instantiate(arrow, clients[curClient].transform.position, Quaternion.identity) as GameObject;
			}
			if(deliverArrow.activeSelf == false)
			{

				deliverArrow.SetActive(true);
				deliverArrow.transform.position = clients[curClient].transform.position;
			}
			curClientObj = clients[curClient].gameObject;
//			curClient ++;
		}

//		if(!deliverDrink && getDrink && spawnDrinkArrow)
//		{
//			spawnDrinkArrow = false;
//		}

	}

}
