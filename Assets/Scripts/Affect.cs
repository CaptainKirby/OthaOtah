using UnityEngine;
using System.Collections;
using Rewired;
public class Affect : MonoBehaviour {
	Player player;
	public bool pressY;
	public bool pressA;
	public Vector3 inputDirLeft;
	public Vector3 inputDirRight;

	bool pushedLeft;
	bool pushedRight;
	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(0);

	}
	
	// Update is called once per frame
	void Update () {
		inputDirLeft.x = player.GetAxis("AxisLeftHorizontal");
		inputDirLeft.z = player.GetAxis("AxisLeftVertical");
		inputDirRight.x = player.GetAxis("AxisRightHorizontal");
		inputDirRight.z = player.GetAxis("AxisRightVertical");
		
		pressA = player.GetButtonDown("Y");
		pressA = player.GetButtonDown("A");

		Debug.DrawLine(this.transform.position, -transform.up *8);

		if(pressA)
		{
//			Debug.Log("GGNEO");
			StartCoroutine(Jump ());
		}
		if(inputDirLeft.magnitude > 0.8f && !pushedLeft)
		{
			pushedLeft = true;
			StartCoroutine(PushTop(inputDirLeft));
		}
		if(inputDirLeft.magnitude <0.8f && pushedLeft)
			pushedLeft = false;
		{

		}

		if(inputDirRight.magnitude > 0.8f && !pushedRight)
		{
			pushedRight = true;
			StartCoroutine(PushBot(inputDirRight));
		}
		if(inputDirRight.magnitude <0.8f && pushedRight)
			pushedRight = false;
		{
			
		}
	}

	IEnumerator PushTop(Vector3 dir)
	{
		bool onOff = true;
		float mTime = 0;
		while(onOff)
		{
			if(mTime < 0.1f)
			{
				mTime += Time.deltaTime;
				rigidbody.AddForceAtPosition(dir * 3, transform.up * 8, ForceMode.Impulse);
			}
			else
			{
				onOff = false;
				yield break;
			}
			yield return null;
		}
	}
	IEnumerator PushBot(Vector3 dir)
	{
		bool onOff = true;
		float mTime = 0;
		while(onOff)
		{
			if(mTime < 0.1f)
			{
				mTime += Time.deltaTime;
				rigidbody.AddForceAtPosition(dir * 6, -transform.up * 3, ForceMode.Impulse);
			}
			else
			{
				onOff = false;
				yield break;
			}
			yield return null;
		}
	}
	IEnumerator Jump()
	{
		bool onOff = true;
		float mTime = 0;
		while(onOff)
		{
			if(mTime < 0.1f)
			{
				mTime += Time.deltaTime;
				rigidbody.AddForceAtPosition(transform.up * 10, transform.up *3, ForceMode.Impulse);

			}
			else
			{
				onOff = false;
				yield break;
			}
			yield return null;
		}
	}


}
