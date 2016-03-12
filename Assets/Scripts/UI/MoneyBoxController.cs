using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyBoxController : MonoBehaviour {

	public Text moneyText;
	public GameObject parent;
	public GameObject earningText;

	// Update is called once per frame
	void Update () {
		moneyText.text = PlayerStats.GetInstance ().playerMoney.ToString ("C0");
	}

	public void GainFare(){
		StartCoroutine (addFare());
	}

	IEnumerator addFare(){
		GameObject newFare = GameObject.Instantiate(earningText);
		newFare.transform.SetParent (parent.transform, false);
		newFare.gameObject.SetActive (true);
		newFare.GetComponent<Animator> ().Play ("FareGain");

		Debug.Log ("ADD FARE ICON");

		yield return new WaitForSeconds (1);

		GameObject.Destroy (newFare);


	}
}
