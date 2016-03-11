using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class LineNameGenerator : MonoBehaviour {

	public InputField inputText;

	JSONObject lineNames;
	List<string> suffixes;
	List<string> prefixes;

	// Use this for initialization
	void Start () {
	
		lineNames = DataLoader.read ("LineNames");

		suffixes = new List<string> ();
		prefixes = new List<string> ();

		for (int i = 0; i < lineNames.Count; i++) {
			JSONObject obj = lineNames [i];

			if (obj.GetField ("type").n == 0) {
				prefixes.Add (obj.GetField ("name").PrintFormattedString());
			} else {
				suffixes.Add (obj.GetField ("name").PrintFormattedString());
			}
		
		}
	}

	public void generateName(){
		string lineString;

		//Get a prefix first
		lineString = prefixes[Random.Range(0, prefixes.Count-1)];
		lineString = (lineString + suffixes [Random.Range (0, suffixes.Count - 1)]).ToString ();

		inputText.text = lineString;

		Debug.Log (lineString);
	
	}

}
