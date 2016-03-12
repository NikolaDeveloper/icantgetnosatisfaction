using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LineProgressController : MonoBehaviour {

	public GameObject barFill;
	public Text lineName;
	public int progress;

	// Update is called once per frame
	void Update () {
	
		lineName.text = PlayerStats.GetInstance ().trainLineName;
		float prog = (float)progress;
		barFill.transform.localScale = new Vector3 (prog / 100, 1);

	}
}
