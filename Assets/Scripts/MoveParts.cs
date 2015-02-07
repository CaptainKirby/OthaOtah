using UnityEngine;
using System.Collections;
using Rewired;

public class MoveParts : MonoBehaviour {
	public GameObject topPart;
	public GameObject tray;
	private bool pressY;
	private bool pressB;

	
	private bool crouch;
	bool crouching;

	private bool flipping;
	private bool flip;
	Player player;
	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(0);

	}
	
	// Update is called once per frame
	void Update () {
		pressY = player.GetButtonDown("Y");
		pressB = player.GetButtonDown("B");

		if(pressB && !flip && !flipping)
		{
			flip = true;
			flipping =true;
			StartCoroutine(Flip());
		}
		
		if(pressY && !crouch && !crouching)
		{
			crouching = true;

			StartCoroutine(Crouch (true));
		}

		if(pressY && crouch && !crouching)
		{
			crouching = true;
			StartCoroutine(Crouch (false));

		}
	}

	IEnumerator Flip()
	{
		flip = false;
		bool onOff = true;
		float mTime = 0;
		bool oneWay = false;
		while(onOff)
		{
			if(!oneWay)
			{
				if(mTime <1)
				{
					mTime += Time.deltaTime * 3;
					tray.transform.localRotation = Quaternion.Euler(Mathf.LerpAngle(0,80,mTime), tray.transform.localRotation.y, tray.transform.localRotation.z);
				}
				else
				{
					mTime = 0;
					oneWay = true;
				}
			}
			else
			{
				if(mTime <1)
				{
					mTime += Time.deltaTime * 2;
					tray.transform.localRotation = Quaternion.Euler(Mathf.LerpAngle(50,0,mTime), tray.transform.localRotation.y, tray.transform.localRotation.z);
				}
				else
				{
					flipping = false;
					mTime = 0;

					onOff = false;

					yield break;
				}
			}

			yield return null;
		}
	}
	IEnumerator Crouch(bool goDown)
	{
//		flip = false;
		bool onOff = true;
		float mTime = 0;
		while(onOff)
		{
			if(goDown)
			{
				if(mTime < 1)
				{
					mTime += Time.deltaTime;
					topPart.transform.localPosition = new Vector3(topPart.transform.localPosition.x, Mathf.Lerp(0,-0.6f, mTime), topPart.transform.localPosition.z);
				}
				else
				{
					crouch= true;
					crouching = false;

					onOff = false;
				}
			}

			else if(!goDown)
			{
				if(mTime < 1)
				{
					mTime += Time.deltaTime;
					topPart.transform.localPosition = new Vector3(topPart.transform.localPosition.x, Mathf.Lerp(-0.6f,0f, mTime), topPart.transform.localPosition.z);
				}
				else
				{
					crouching = false;
					crouch= false;
					onOff = false;
				}
			}
			yield return null;
		}
	}
}
