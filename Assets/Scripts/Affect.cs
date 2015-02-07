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
	[HideInInspector]
	public Vector3 curVel;
	public float pushForce = 3;
	public float jumpForce = 10;
	private Vector3 startPosition;
	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(0);
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		inputDirLeft.x = player.GetAxis("AxisLeftHorizontal");
		inputDirLeft.z = player.GetAxis("AxisLeftVertical");
		inputDirRight.x = player.GetAxis("AxisRightHorizontal");
		inputDirRight.z = player.GetAxis("AxisRightVertical");

//		inputDirLeft = new Vector3(inputDirLeft.x, transform.up, inputDirLeft.z);
//		inputDirLeft= Vector3.Cross(inputDirLeft, transform.up*8);
		pressY = player.GetButtonDown("Y");
		pressA = player.GetButtonDown("A");

		Debug.DrawRay(transform.up * 8, inputDirLeft - transform.up, Color.red);
//		Debug.DrawLine(transform.up * 8, transform.TransformDirection(inputDirLeft)*8);
		Debug.Log (transform.localRotation.eulerAngles.normalized);
//		transform.localRotation = Quaternion.Euler(Vector3.SmoothDamp(this.transform.localRotation.eulerAngles, Vector3.zero, ref curVel,1));

		if(pressY)
		{
//			print ("gensgue");
			transform.localRotation = Quaternion.Euler(0,0,0);
			rigidbody.velocity = new Vector3(0,0,0);
			transform.position =startPosition;
		}
		if(pressA)
		{
//			Debug.Log("GGNEO");
			StartCoroutine(Jump ());
		}
		if(inputDirLeft.magnitude > 0.8f && !pushedLeft)
		{
			pushedLeft = true;
			StartCoroutine(PushTop(inputDirLeft.normalized));
		}
		if(inputDirLeft.magnitude <0.8f && pushedLeft)
			pushedLeft = false;
		{

		}

		if(inputDirRight.magnitude > 0.8f && !pushedRight)
		{
			pushedRight = true;
			StartCoroutine(PushBot(inputDirRight.normalized));
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
				rigidbody.AddForceAtPosition(transform.TransformDirection(dir) * pushForce, transform.up * 8, ForceMode.Impulse);
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
				rigidbody.AddForceAtPosition(transform.TransformDirection(dir) * pushForce, -transform.up * 3, ForceMode.Impulse);
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
				rigidbody.AddForceAtPosition(transform.up * jumpForce, transform.up *3, ForceMode.Impulse);

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
