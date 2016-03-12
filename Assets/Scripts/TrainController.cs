using UnityEngine;
using UnityEngine.Sprites;
using System.Collections;

public class TrainController : MonoBehaviour {

	public float throttleSpeed = 0f;
	public float throttleIncrement = 0.05f;
	public float trackMoveIncrement = 0.5f;

	public static int passengerCapacity = 1000;
	public static int passengerFull = 500;

	public static TrainController Instance;

	public int currentTrack = 1;

	private bool isEmergencyStopping = false;
	private bool trackDirectionUp = false;
	private int lastStationId = 0;
	private bool wasStoppedAtStation = false;
	private bool isStoppedAtStation = false;
	private float[] trackPositions;
	private int currentStationId = 0;

	private int passengersToDisembark = 0;
	private float lastDisembarkment = 0;

	private bool finalStation = false;

	public SpriteRenderer mainTrain;
	public SpriteRenderer line;
	public SpriteRenderer windows;

	public bool gameOver = false;


	void Awake () {
		Instance = this;
	}


	// Use this for initialization
	void Start () {
		TrainController.passengerFull = StationsController.Instance.getInitialPassengers(TrainController.passengerCapacity, TrainController.passengerFull);

		trackPositions = new float[3] {75f, 0f, -75f};

		mainTrain.color = PlayerStats.GetInstance ().mainColor;
		line.color =	 PlayerStats.GetInstance ().secondaryColor;
		windows.color = PlayerStats.GetInstance ().tertiaryColor;

		Debug.Log ("COLOR" + PlayerStats.GetInstance ().mainColor);
	}
	
	// Update is called once per frame
	void Update () {

		checkGameOverConditions();
		passengerEmbarkment();
		moveTrain();
	}


	void checkGameOverConditions () {

		if (PlayerStats.GetInstance ().isGameOver()) {
			gameOver = true;
			UIManager.instance.GameOverTime ();
		}

	}


	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "station") {

			int stationId = col.gameObject.GetComponent<CreateMyStations>().id;

			if (stationId == 7) {
				finalStation = true;
				gameOver = true;
				Debug.Log ("Triggering game over");
				UIManager.instance.CompleteGame();
			}

			if (stationId != 1) {
				isStoppedAtStation = true;
				lastDisembarkment = Time.realtimeSinceStartup;;
				passengersToDisembark = Mathf.FloorToInt(passengerFull / 10);
				StationsController.Instance.calculatePassengerNumbers();
			}
			currentStationId = stationId;
		}
	}


	void OnTriggerExit2D(Collider2D col) {
		if (col.tag == "station") {
			isStoppedAtStation = false;

			PlayerStats.GetInstance().satisfaction -= Mathf.RoundToInt(passengersToDisembark * 0.5f);

			int stationId = col.gameObject.GetComponent<CreateMyStations> ().id;

			if (stationId != lastStationId && stationId !=1) {
				UIManager.instance.MissStation ();
			}
		}
	}


	private void passengerEmbarkment () {

		if (gameOver) {
			return;
		}

		if (isStoppedAtStation && throttleSpeed == 0f) {

			float currentTime = Time.realtimeSinceStartup;

			if (currentStationId > lastStationId) {
				SoundController.Instance.PlayStopAtStationSound();
				lastStationId = currentStationId;
			}

			if (!wasStoppedAtStation) {
				Debug.Log ("ARRIVING AT STATION");
				StationsController.Instance.arriveAtStation(passengersToDisembark);
			}

			wasStoppedAtStation = true;

			// Get new passengers from the station
			int newPassengers = StationsController.Instance.embarkPassenger();
			PlayerStats.GetInstance().playerMoney += (10 * newPassengers);
			if (newPassengers > 0) {
				UIManager.instance.moneyBox.GainFare ();
				SoundController.Instance.PlayMoneyGainSound();
			}
			passengerFull += newPassengers;

			// Two seconds between passengers disembarking
			if ((currentTime - lastDisembarkment > 0.2f) && passengersToDisembark > 0) {
				lastDisembarkment = currentTime;
				passengerFull--;
				passengersToDisembark--;
				//Debug.Log (passengersToDisembark);
				StationsController.Instance.disembarkPassenger();
			}
				
		} else {

			if (wasStoppedAtStation) {
				wasStoppedAtStation = false;
				Debug.Log ("DEPARTING STATION");
				StationsController.Instance.departStation();
			}

		}

	}


	private void moveTrain () {

		if (gameOver) {
			return;
		}

		CameraController cameraController = Camera.main.GetComponent<CameraController>();

		float currentDeceleration = ((Time.deltaTime * 1000) * throttleIncrement);

		if (Input.GetKey("right")) {
			// Accelerating after emergency stopping cancels the emergency stop
			isEmergencyStopping = false;
			throttleSpeed = throttleSpeed + throttleIncrement;
		}

		// Cant decelerate faster than an emergency stop
		if (Input.GetKey("left") && !isEmergencyStopping) {
			currentDeceleration = -(2.5f * throttleIncrement);
			throttleSpeed = throttleSpeed + currentDeceleration;
		}

		// Allow user to hit space once instead of requiring them to hold it down
		if (Input.GetKey("space") || isEmergencyStopping) {

			if (!isEmergencyStopping) {
				SoundController.Instance.PlayScreechBrakeSound();
			}

			isEmergencyStopping = true;

			currentDeceleration = -(5 * throttleIncrement);
			throttleSpeed = throttleSpeed + currentDeceleration;
			UIManager.instance.EmergencyStop ();
		}

		if (Input.GetKeyUp("up")) {
			if (currentTrack > 0) {
				trackDirectionUp = true;
				Debug.Log ("Moving Up");
				currentTrack--;
			}
		}

		if (Input.GetKeyUp("down")) {
			if (currentTrack < trackPositions.Length - 1) {
				trackDirectionUp = false;
				Debug.Log ("Moving Down");
				currentTrack++;
			}
		}

		if (this.transform.position.y < trackPositions[currentTrack]) {
			this.setTrainY(this.transform.position.y + ((Time.deltaTime * 1000) * trackMoveIncrement));
		} else if (this.transform.position.y > trackPositions[currentTrack]) {
			this.setTrainY(this.transform.position.y - ((Time.deltaTime * 1000) * trackMoveIncrement));
		}

		if (trackDirectionUp && this.transform.position.y > trackPositions[currentTrack]) {
			this.setTrainY(trackPositions[currentTrack]);
		} else if (!trackDirectionUp && trackPositions[currentTrack] > this.transform.position.y) {
			this.setTrainY(trackPositions[currentTrack]);
		}

		if (throttleSpeed <= 0f) {
			SoundController.Instance.TrainMovingSoundOFF ();
			isEmergencyStopping = false;
			throttleSpeed = 0f;
		} else if (throttleSpeed > 10f) {
			throttleSpeed = 10f;
		} else {
			SoundController.Instance.TrainMovingSoundON();
		}

		if (isEmergencyStopping && (Time.frameCount % 10) == 0) {
			PlayerStats.GetInstance().satisfaction -= Mathf.CeilToInt(20f * (Time.deltaTime));
		}

		transform.position += new Vector3(throttleSpeed, 0f, 0f);

		float currentX = transform.position.x;

		if (throttleSpeed > 0f) {
			cameraController.moveCameraBasedOnTrainPos(currentX, throttleSpeed / throttleIncrement);
			//cameraController.moveCameraBasedOnTrainSpeed(currentX, throttleSpeed);
		}

		if (Camera.main.transform.position.x > currentX + 500f) {
			cameraController.setCameraX(currentX + 500f);
		} else if (Camera.main.transform.position.x < currentX - 200f) {
			cameraController.setCameraX(currentX - 200f);
		}

	}


	private void setTrainY (float pos) {
		this.transform.position = new Vector3(
			this.transform.position.x,
			pos,
			this.transform.position.z
		);
	}


}
