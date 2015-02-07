using UnityEngine;
using System.Collections;

public class Bottle : MonoBehaviour {

	private BottleManager bottleMgr;
	private Objectives objectives;
	void Start () {
		objectives = GameObject.FindObjectOfType<Objectives>();
		bottleMgr = GameObject.FindObjectOfType<BottleManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log (objectives.clients[objectives.curClient].name);
		if(col.CompareTag("deposit")&& col.transform.parent.gameObject == objectives.curClientObj)
		{
			Debug.Log (col.transform.parent.name + col.transform.parent.gameObject);
			bottleMgr.deliveredBottles ++;
			bottleMgr.curBottles.Remove(this.gameObject);
			this.gameObject.SetActive(false);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("pickup") && !col.gameObject.CompareTag("glass")  )
		{
			bottleMgr.destroyedBottles++;
			bottleMgr.curBottles.Remove(this.gameObject);

			this.gameObject.SetActive(false);
		}

	}
}
