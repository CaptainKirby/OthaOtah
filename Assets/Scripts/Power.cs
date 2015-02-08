using UnityEngine;
using System.Collections;
using Rewired;

public class Power : MonoBehaviour {
	public float power = 100;
	bool inside;
	Player player;
	private bool pressA;
	private bool used;
	public GameObject powerGauge;
	public bool dead;
	public GameObject chargeParticle;
	public GameObject deathParticle;
	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(0);
		powerGauge = GameObject.Find("powerGauge");

	}
	
	// Update is called once per frame
	void Update () {
	
		power -= Time.deltaTime;
		pressA = player.GetButtonDown("A");
		powerGauge.renderer.material.SetFloat("_Cutoff", 1.0f - power/100);
		if(inside && pressA && !used)
		{
			Instantiate(chargeParticle,this.transform.position,chargeParticle.transform.rotation);

			used = true;
			power = 100;
		}
		if(power <= 11 && !dead && !GetComponent<Movement>().starting)
		{
			Instantiate(deathParticle,this.transform.position,deathParticle.transform.rotation);
			dead = true;
			GetComponent<Objectives>().end = true;
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
