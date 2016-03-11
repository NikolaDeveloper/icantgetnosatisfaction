using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColourControlmage : MonoBehaviour {

	public Image objectImage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		gameObject.GetComponent<Image> ().color = objectImage.color;
	}
}
