using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void setCameraX (float pos) {
		this.transform.position = new Vector3(pos, this.transform.position.y, this.transform.position.z);
	}


	public void moveCameraBasedOnTrainPos (float pos, float timeToStop) {
		
		float cameraNeutral = pos + 500f;
		float targetCameraPos = transform.position.x - (cameraNeutral - transform.position.x);

		this.setCameraX(transform.position.x - ((targetCameraPos - transform.position.x) / timeToStop));

	}


}
