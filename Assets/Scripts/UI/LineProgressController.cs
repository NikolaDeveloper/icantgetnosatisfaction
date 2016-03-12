using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LineProgressController : MonoBehaviour {

	public Slider barFill;
	public Text lineName;
	public int progress = 0;

	// Update is called once per frame
	void Update () {
	
		lineName.text = PlayerStats.GetInstance ().trainLineName;
		float prog = (float)progress;
		barFill.value = prog / 100;

	}
}
