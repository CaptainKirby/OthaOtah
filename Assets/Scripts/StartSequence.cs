using UnityEngine;
using System.Collections;

public class StartSequence : MonoBehaviour {

	public GameObject powerStation;
	public Transform player;
	public GameObject beamParticle;
	bool startChargeUp;
	float power;
	GameObject beam;
	void Awake()
	{
		GetComponent<Movement>().starting = true;
	}
	void Start () {
		GetComponent<Power>().power = 0;

		player = this.gameObject.transform;
		Instantiate(powerStation, new Vector3(player.position.x, player.position.y - 2, player.position.z), Quaternion.identity);
		beam = Instantiate(beamParticle, new Vector3(player.position.x, player.position.y - 2, player.position.z), beamParticle.transform.rotation) as GameObject;
		StartCoroutine(FadeOut(beam));

	}
	

	void Update () {
		if(startChargeUp)
		{
			startChargeUp=false;
			
			StartCoroutine(ChargeUp());
			
		}
	}

	IEnumerator ChargeUp()
	{
		bool onOff = true;
		float mTime = 0;
		Debug.Log("GENGUG");
		while(onOff)
		{
			if(mTime <1)
			{
				mTime += Time.deltaTime/2;
				GetComponent<Power>().power = Mathf.Lerp(0,100,mTime);
			}
			else
			{
				beam.SetActive(false);
				GetComponent<Movement>().starting = false;

				onOff= false;
			}
			yield return null;
		}
	}
	IEnumerator FadeOut(GameObject beam2)
	{
		bool onOff = true;
		float mTime = 0;
		ParticleSystem pS = beam2.GetComponent<ParticleSystem>();
		while(onOff)
		{
			if(mTime <1)
			{
				mTime += Time.deltaTime/3;
				pS.emissionRate = Mathf.Lerp(100, 0, mTime);
			}
			else
			{
				startChargeUp = true;
				onOff= false;

			}
			yield return null;
		}
	}
}
