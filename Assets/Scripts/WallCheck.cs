using UnityEngine;
using System.Collections;

public class WallCheck : MonoBehaviour 
{
	public GameObject tempDisable = null;
	public Transform target;
	public bool faded;
	private bool oneWay;
	public GameObject lastDisabled = null;

	public void Start()
	{
		StartCoroutine("CastRay");
	}

	IEnumerator CastRay()
	{

		RaycastHit hit;
		while(true)
		{

			if(target != null)
			{
//			Debug.DrawRay(transform.position, target.position - transform.position, Color.red,100);
			if (Physics.Raycast(transform.position, target.position - transform.position, out hit,100 ))
			{
//				Debug.Log(hit.collider.gameObject);
//				tempDisable = hit.collider.gameObject;

				if(hit.collider.CompareTag("RemoveWhenBehind") && !faded)
				{
					faded = true;
					StartCoroutine(TweenAlpha(hit.collider.gameObject, false));
					lastDisabled = hit.collider.gameObject;
				}
				if(faded)
				{
					if(hit.collider.gameObject != lastDisabled)
					{
						faded = false;
						StartCoroutine(TweenAlpha(lastDisabled, true));

						Debug.Log ("huesgnuegensu");
					}
				}
//				else
//				{
//					if(hit.collider.gameObject != lastDisabled)
//					{
//						StartCoroutine(TweenAlpha(hit.collider.gameObject, true));
//						faded = false;
//					}
//				}
//				if(hit.collider.CompareTag("RemoveWhenBehind"))
//				{
////					hit.collider.renderer.enabled = false;
//					if(lastDisabled != hit.collider.gameObject)
//					{
//						StartCoroutine(TweenAlpha(hit.collider.gameObject, false));
//					}
//					lastDisabled = hit.collider.gameObject;
//				}


//				if(lastDisabled != null)
//				{
//					if(lastDisabled != hit.collider.gameObject)
//					{
//						Debug.Log(lastDisabled);
//						StartCoroutine(TweenAlpha(lastDisabled, true));
////						lastDisabled.renderer.enabled = true;
//					}
//				}
			}
		}
			yield return new WaitForSeconds(0.2f);
		}
	}

	void DebugStuff()
	{

	}

	IEnumerator TweenAlpha(GameObject hit, bool toFrom)
	{
		bool onOff = true;
		float mTime = 0;
		if(!toFrom)
		{
			mTime = 1;
		}
		else
		{
			mTime = 0;
		}
		//false = 100 to 0
		while(onOff)
		{
//			Debug.Log (mTime);
			if(!toFrom)
			{
				if(mTime > 0)
				{
//					Debug.Log (mTime);
					mTime -= Time.deltaTime*4;

				}
				else
				{
					onOff = false;
				}

				hit.renderer.material.color = new Color(hit.renderer.material.color.r, hit.renderer.material.color.g, hit.renderer.material.color.b, mTime); 
			}
			if(toFrom)
			{
				if(mTime < 1)
				{
					mTime += Time.deltaTime;
				}
				else
				{
					onOff = false;
				}
				hit.renderer.material.color = new Color(hit.renderer.material.color.r, hit.renderer.material.color.g, hit.renderer.material.color.b, mTime); 
			}
			yield return null;

		}

	}

}
