using UnityEngine;
using System.Collections;
using Rewired;

public class Power : MonoBehaviour {
	public float power = 100;
	bool inside;
	Player player;
	private bool pressA;
	private bool used;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(0);

	}
	
	// Update is called once per frame
	void Update () {
	
		power -= Time.deltaTime;
		pressA = player.GetButtonDown("A");

		if(inside && pressA && !used)
		{
			used = true;
			power = 100;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("powercharge"))
		{
			inside =true;
		}
	}
	void OnTriggerExit(Collider col)
	{
		if(col.CompareTag("powercharge"))
		{
			inside =false;
		}
	}
}
