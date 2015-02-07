using UnityEngine;
using System.Collections;
using Rewired;

public class Movement : MonoBehaviour {

	Player player;
	public float speed = 0;
	public float dashSpeed;
	public float dashDur = 0.1f;
	public float accel = 3f;
	public float drag = 5f;
	public float movementMax = 5f;
	private bool pressY;
	private bool pressA;
	private Vector3 inputDirLeft;
	private Vector3 inputDirRight;

	public float targetAngle = 0;
	public float angle = 0f;
	private float angularVelocity = 0f;
	
	public float angleSmoothDuration = 0.05f;
	public float angleMaxSpeed = 5800f;

	public GameObject gfxObj;
	void Start () 
	{
		player = ReInput.players.GetPlayer(0);
	}
	

	void FixedUpdate () 
	{
		inputDirLeft.x = player.GetAxis("AxisLeftHorizontal");
		inputDirLeft.z = player.GetAxis("AxisLeftVertical");

		inputDirRight.x = player.GetAxis("AxisRightHorizontal");
		inputDirRight.z = player.GetAxis("AxisRightVertical");

		pressY = player.GetButtonDown("Y");
		pressA = player.GetButtonDown("A");
		inputDirRight = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDirRight;

		//		speed = speed + accel * inputDirLeft.magnitude * Time.deltaTime;
//		speed = Mathf.Clamp(speed, 0f, movementMax);
//		speed = speed - speed * Mathf.Clamp01(drag * Time.deltaTime);
//		rigidbody.AddForce(inputDirLeft * speed, ForceMode.Acceleration);
//		rigidbody.velocity = inputDirLeft * speed * 0.985f; 
		rigidbody.MovePosition(rigidbody.position + inputDirLeft * speed * Time.deltaTime);

		if(inputDirRight.magnitude > 0.3f)
		{
			float newTargetAngle = Mathf.Atan2(inputDirRight.x, inputDirRight.z) * Mathf.Rad2Deg;
			targetAngle = newTargetAngle;
		}
		angle = Mathf.SmoothDampAngle(angle, targetAngle, ref angularVelocity, angleSmoothDuration, angleMaxSpeed);

		rigidbody.MoveRotation(Quaternion.Euler(0,angle,0));

//		gfxObj.transform.localRotation = Quaternion.Euler(0, angle, 0); 



	}
}
