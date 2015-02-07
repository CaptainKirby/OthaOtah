using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BottleManager : MonoBehaviour {

	public List<GameObject> curBottles;

	public int destroyedBottles;
	public int deliveredBottles;
	private Objectives objectives;
	public int deliveries;
	public int successDeliveries;
	void Start () {
	
		objectives =GameObject.FindObjectOfType<Objectives>();
	}
	

	void Update () {
	
		if(destroyedBottles + deliveredBottles == 3)
		{
//			Debug.Log ("GENU");
			successDeliveries = successDeliveries + deliveredBottles ;
			destroyedBottles = 0;
			deliveredBottles = 0;
			objectives.deliverDrink = false;
			objectives.getDrink = true;
			objectives.spawnDrinkArrow = false;
			objectives.curClient++;
			curBottles.Clear();
			deliveries++;

			//			Debug.Log("ALL BOTTLES DONE");
		}

		if(deliveries == 3)
		{
			objectives.end = true;
		}
	}
}
