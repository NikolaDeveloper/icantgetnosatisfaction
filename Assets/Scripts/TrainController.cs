using UnityEngine;
using System.Collections;

public class TrainController : MonoBehaviour {

	public float throttleSpeed = 0f;
	public float throttleIncrement = 0.05f;

	public float cameraThreshold = 200f;

	public Camera mainCamera;

	private bool isEmergencyStopping = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate () {

		CameraController cameraController = mainCamera.GetComponent<CameraController>();

		bool isDecelerating = false;
		float currentDeceleration = throttleIncrement;

		if (Input.GetKey("right")) {
			// Accelerating after emergency stopping cancels the emergency stop
			isEmergencyStopping = false;
			isDecelerating = false;
			throttleSpeed = throttleSpeed + throttleIncrement;
		}

		// Cant decelerate faster than an emergency stop
		if (Input.GetKey("left") && !isEmergencyStopping) {
			isDecelerating = true;
			currentDeceleration = -throttleIncrement;
			throttleSpeed = throttleSpeed + currentDeceleration;
		}

		// Allow user to hit space once instead of requiring them to hold it down
		if (Input.GetKey("space") || isEmergencyStopping) {
			isDecelerating = true;
			isEmergencyStopping = true;
			currentDeceleration = -(3 * throttleIncrement);
			throttleSpeed = throttleSpeed + currentDeceleration;
		}

		if (throttleSpeed < 0f) {
			isDecelerating = false;
			throttleSpeed = 0f;
		}

		transform.position += new Vector3(throttleSpeed, 0f, 0f);
			
		float currentX = transform.position.x;

		if (throttleSpeed > 0f) {

			float timeToStop = throttleSpeed / throttleIncrement;
			float cameraNeutral = currentX + 500f;

			float targetCameraPos = mainCamera.transform.position.x - (cameraNeutral - mainCamera.transform.position.x);

			cameraController.moveCameraX(mainCamera.transform.position.x - ((targetCameraPos - mainCamera.transform.position.x) / timeToStop));

		}

	}

}
