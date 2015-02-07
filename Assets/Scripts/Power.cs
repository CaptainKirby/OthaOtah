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
	public GameObject particle;
	public GameObject shutDownParticle;
	public bool dead;
	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(0);

	}
	
	// Update is called once per frame
	void Update () {
	
		power -= Time.deltaTime;
		pressA = player.GetButtonDown("A");
		powerGauge.renderer.material.SetFloat("_Cutoff", 1.0f - power/100);
		if(inside && pressA && !used)
		{
			used = true;
			power = 100;
			Instantiate(particle, this.transform.position, Quaternion.identity);
		}

		if(power <= 11 && !dead)
		{
			dead = true;
			Instantiate(shutDownParticle, this.transform.position, Quaternion.identity);
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
