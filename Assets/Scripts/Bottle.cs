using UnityEngine;
using System.Collections;

public class Bottle : MonoBehaviour {

	private BottleManager bottleMgr;
	private Objectives objectives;
	private AudioClip[] glassCling;
	private AudioClip[] glassBreak;

	private bool playable = true;
	public GameObject wineSpill;
//	private SoundManager sM;
	void Start () {
//		sM = GameObject.FindObjectOfType<SoundManager>();
		objectives = GameObject.FindObjectOfType<Objectives>();
		bottleMgr = GameObject.FindObjectOfType<BottleManager>();
		glassCling = SoundManager.LoadAllFromGroup("glass");
		glassBreak = SoundManager.LoadAllFromGroup("glassBreak");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
//		Debug.Log (objectives.clients[objectives.curClient].name);
		if(col.CompareTag("deposit")&& col.transform.parent.gameObject == objectives.curClientObj)
		{
			Instantiate(wineSpill,this.transform.position, Quaternion.identity);

//			Debug.Log (col.transform.parent.name + col.transform.parent.gameObject);
			bottleMgr.deliveredBottles ++;
			bottleMgr.curBottles.Remove(this.gameObject);
			this.gameObject.SetActive(false);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("glass") && playable)
		{
			StartCoroutine(PlayBing());
		}
		if(!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("pickup") && !col.gameObject.CompareTag("glass")  )
		{
			Instantiate(wineSpill,this.transform.position, Quaternion.identity);
			SoundManager.PlaySFX(glassBreak[Random.Range(0, glassBreak.Length)], false, 0, 1.5f, 1, transform.position); 

			bottleMgr.destroyedBottles++;
			bottleMgr.curBottles.Remove(this.gameObject);

			this.gameObject.SetActive(false);
		}

	}

	IEnumerator PlayBing()
	{
		playable = false;

		SoundManager.PlaySFX(glassCling[Random.Range(0, glassCling.Length)], false, 0, 1, 1, transform.position); 
		yield return new WaitForSeconds(0.2f);
		playable = true;
	}


}
