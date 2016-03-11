using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SatisfactionBarController : MonoBehaviour {

	[Range(0,100)]
	public int satisfaction;

	public Image barFill;
	public Text progressText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float satFloat = (float)satisfaction;
		barFill.transform.localScale = new Vector3 (1, satFloat / 100f);

		progressText.text = satFloat + "%";
	}
}
