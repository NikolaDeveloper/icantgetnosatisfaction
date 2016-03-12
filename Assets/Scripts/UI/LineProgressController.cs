using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LineProgressController : MonoBehaviour {

	public GameObject barFill;
	public Text lineName;
	public int progress;

	// Update is called once per frame
	void Update () {

		float current = TrainController.Instance.transform.position.x;
		float end = ProcGen.Instance.AllStations [ProcGen.Instance.AllStations.Count - 1].distanceFromOrigin;

		progress = Mathf.FloorToInt((current / end) * 100);
	
		lineName.text = PlayerStats.GetInstance ().trainLineName;
		float prog = (float)progress;
		barFill.transform.localScale = new Vector3 (prog / 100, 1);

	}
}
