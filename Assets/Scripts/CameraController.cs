using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void moveCameraX (float pos) {
		this.transform.position = new Vector3(pos, this.transform.position.y, this.transform.position.z);
	}


}
