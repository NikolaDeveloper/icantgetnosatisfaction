using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrainColorHandler : MonoBehaviour {

	public Image mainTrain;
	public Image line;
	public Image windows;

	public void SetColors(){
		PlayerStats.GetInstance ().mainColor = mainTrain.color;
		PlayerStats.GetInstance ().secondaryColor = line.color;
		PlayerStats.GetInstance ().tertiaryColor = windows.color;

		Debug.Log (mainTrain.color);
	}

}
