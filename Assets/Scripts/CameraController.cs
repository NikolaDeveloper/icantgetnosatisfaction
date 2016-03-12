using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private float stoppedPos = -200f;
	private float maxSpeedPos = 500f;

	private float maxSpeed = 10f;
	private float moveIncrement = 1f;

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


	public void moveCameraBasedOnTrainSpeed (float pos, float speed) {

		float currentCameraPos = Camera.main.transform.position.x;

		float speedPercentage = speed / maxSpeed;
		float cameraPercentage = (stoppedPos - maxSpeedPos) * speedPercentage;

		float newCameraPos = cameraPercentage + (pos - stoppedPos) + currentCameraPos;

		if (newCameraPos > currentCameraPos) {
			setCameraX(currentCameraPos += moveIncrement);
		} else {
			setCameraX(currentCameraPos -= moveIncrement);
		}

		currentCameraPos = Camera.main.transform.position.x;

		/*if (currentCameraPos < maxSpeedPos) {
			setCameraX(maxSpeedPos);
		} else if (currentCameraPos > stoppedPos) {
			setCameraX(stoppedPos);
		}*/

	}


}
