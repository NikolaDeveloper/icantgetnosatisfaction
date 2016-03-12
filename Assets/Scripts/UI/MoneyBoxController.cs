using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyBoxController : MonoBehaviour {

	public Text moneyText;
	
	// Update is called once per frame
	void Update () {
		moneyText.text = PlayerStats.GetInstance ().playerMoney.ToString ("C0");
	}
}
