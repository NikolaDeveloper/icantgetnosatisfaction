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

		bool isAccelerating = false;

		if (Input.GetKey("right")) {
			// Accelerating after emergency stopping cancels the emergency stop
			isEmergencyStopping = false;
			isAccelerating = true;
			throttleSpeed = throttleSpeed + throttleIncrement;
		}

		// Cant decelerate faster than an emergency stop
		if (Input.GetKey("left") && !isEmergencyStopping) {
			throttleSpeed = throttleSpeed - throttleIncrement;
		}

		// Allow user to hit space once instead of requiring them to hold it down
		if (Input.GetKey("space") || isEmergencyStopping) {
			isEmergencyStopping = true;
			throttleSpeed = throttleSpeed - (3 * throttleIncrement);
		}

		if (throttleSpeed < 0f) {
			throttleSpeed = 0f;
		}

		transform.position += new Vector3(throttleSpeed, 0f, 0f);
			
		float currentX = transform.position.x;

		if (currentX > cameraThreshold) {
			mainCamera.transform.position = new Vector3(currentX - cameraThreshold, mainCamera.transform.position.y, mainCamera.transform.position.z);
		}

	}

}
