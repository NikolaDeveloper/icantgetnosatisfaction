using UnityEngine;
using System.Collections;

public class OverlayController : MonoBehaviour {

	public int value = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (value >= 300) {
			GameObject.Destroy (gameObject);
		} else {
			value++;
		}
	
	}
}
