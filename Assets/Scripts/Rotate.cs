using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public bool right;
	public float speed = 5;
	public bool up = true;
	public bool forward = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(up)
		{
		if(!right)
		{
			transform.Rotate(Vector3.up * Time.deltaTime * speed);
		}
		else transform.Rotate(-Vector3.up* Time.deltaTime * speed);
		}
		if(forward)
		{
			if(!right)
			{
				transform.Rotate(Vector3.forward * Time.deltaTime * speed);
			}
			else transform.Rotate(-Vector3.forward* Time.deltaTime * speed);
		}

	}
}
