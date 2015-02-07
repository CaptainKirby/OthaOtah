using UnityEngine;
using System.Collections;
using Rewired;

public class MoveParts : MonoBehaviour {
	public GameObject topPart;
	public GameObject tray;
	private bool pressY;

	private bool crouch;
	bool crouching;
	Player player;
	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(0);

	}
	
	// Update is called once per frame
	void Update () {
		pressY = player.GetButtonDown("Y");

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

	IEnumerator Crouch(bool goDown)
	{
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
