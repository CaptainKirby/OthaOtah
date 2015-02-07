using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetDir = this.transform.position - Camera.main.transform.position;
		this.transform.LookAt(targetDir);
	}
}
