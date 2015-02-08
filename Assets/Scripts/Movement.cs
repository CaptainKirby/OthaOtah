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
	private bool rotating;
	public bool dead;
	public ParticleSystem footsteps;
	public bool starting;
	private AudioClip[] wallBumps;
	private bool playable;
	void Start () 
	{
		playable = true;
		wallBumps = SoundManager.LoadAllFromGroup("robot");
		player = ReInput.players.GetPlayer(0);
	}
	

	void FixedUpdate () 
	{

		dead = GetComponent<Power>().dead;
		inputDirLeft.x = player.GetAxis("AxisLeftHorizontal");
		inputDirLeft.z = player.GetAxis("AxisLeftVertical");

		inputDirRight.x = player.GetAxis("AxisRightHorizontal");
		inputDirRight.z = player.GetAxis("AxisRightVertical");

		pressY = player.GetButtonDown("Y");
		pressA = player.GetButtonDown("A");
		inputDirLeft = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDirLeft;

		inputDirRight = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDirRight;
//		
		if(!dead && !starting)
		{
			//		speed = speed + accel * inputDirLeft.magnitude * Time.deltaTime;
	//		speed = Mathf.Clamp(speed, 0f, movementMax);
	//		speed = speed - speed * Mathf.Clamp01(drag * Time.deltaTime);
	//		rigidbody.AddForce(inputDirLeft * speed, ForceMode.Acceleration);
	//		rigidbody.velocity = inputDirLeft * speed * 0.985f; 
			RaycastHit hitDown;
			RaycastHit hitFront;
			RaycastHit hitBack;
			RaycastHit hitRight;
			RaycastHit hitLeft;
			if(Physics.Raycast(transform.position, -Vector3.up, out hitDown))
			{
				transform.position = new Vector3 (transform.position.x,hitDown.point.y+ 2, transform.position.z);
			}
			if(Physics.Raycast(transform.position, Vector3.forward, out hitFront, 1f))
			{
//				Debug.Log ("ngues");
				if(playable)
				{
					StartCoroutine(PlayBump());
				}
				transform.position = this.transform.position - Vector3.forward*0.08f;
			}
			if(Physics.Raycast(transform.position, -Vector3.forward, out hitBack, 1f))
			{
				if(playable)
				{
					StartCoroutine(PlayBump());
				}
//				Debug.Log ("ngues");
				transform.position = this.transform.position + Vector3.forward*0.08f;
			}
			if(Physics.Raycast(transform.position, Vector3.right, out hitRight, 1f))
			{
				if(playable)
				{
					StartCoroutine(PlayBump());
				}
//				Debug.Log ("ngues");
				transform.position = this.transform.position - Vector3.right*0.08f;
			}

			if(Physics.Raycast(transform.position, -Vector3.right, out hitLeft, 1f))
			{
				if(playable)
				{
					StartCoroutine(PlayBump());
				}
				//				Debug.Log ("ngues");
				transform.position = this.transform.position + Vector3.right*0.08f;
			}
//			if(!rotating)
//			{
			if(inputDirLeft.magnitude > 0.3f)
								{ 
									footsteps.enableEmission = true;
								}
								else
								{
									footsteps.enableEmission = false;
								}
				rigidbody.MovePosition(rigidbody.position + inputDirLeft * speed * Time.deltaTime);
//			}

			if(inputDirLeft.magnitude > 0.3f)
			{
//				footsteps.enableEmission = false;
				rotating = true;
				float newTargetAngle = Mathf.Atan2(inputDirLeft.x, inputDirLeft.z) * Mathf.Rad2Deg;
				targetAngle = newTargetAngle;
			}
//			else
//			{
//				rotating = false;
//			}
			angle = Mathf.SmoothDampAngle(angle, targetAngle, ref angularVelocity, angleSmoothDuration, angleMaxSpeed);

			rigidbody.MoveRotation(Quaternion.Euler(0,angle,0));
		}
//		gfxObj.transform.localRotation = Quaternion.Euler(0, angle, 0); 



	}

	IEnumerator PlayBump()
	{
		SoundManager.PlaySFX(wallBumps[Random.Range(0, wallBumps.Length)], false, 0, 2, 1, this.transform.position); 
		playable = false;
		yield return new WaitForSeconds(0.5f);
		playable = true;

	}

}
