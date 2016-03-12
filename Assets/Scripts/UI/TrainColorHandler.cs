using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrainColorHandler : MonoBehaviour {

	public Image mainTrain;
	public Image line;
	public Image windows;

	public InputField lineName;

	public void SetColors(){
		PlayerStats.GetInstance ().mainColor = mainTrain.color;
		PlayerStats.GetInstance ().secondaryColor = line.color;
		PlayerStats.GetInstance ().tertiaryColor = windows.color;

		Debug.Log (mainTrain.color);
	}

	public void setLineName(){
		PlayerStats.GetInstance ().trainLineName = lineName.text;
	}

}
