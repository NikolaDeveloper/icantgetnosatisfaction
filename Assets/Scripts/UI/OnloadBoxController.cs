using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnloadBoxController : MonoBehaviour {

	public Text offloadText;
	public Animator animator;

	private bool left = false;
	private bool arrivedEmpty = false;

	public int onloadCount;

	void Awake(){
		gameObject.SetActive (false);
		
	}

	public void ArrivedAtStation(int amountToOnload){
		if (amountToOnload > 0) {
			onloadCount = amountToOnload;
			UpdateText ();
			gameObject.SetActive (true);
			animator.Play ("Screen Drop Top");
			left = false;
		} else {
			arrivedEmpty = true;
		}
	}

	public void PassengerEmbark(){
		onloadCount -= 1;
		UIManager.instance.capacityBox.addToCurrentCapacity ();

		if (onloadCount <= 0) {
			onloadCount = 0;
			if (left == false) {
				LeftStation ();
			}
		}

		UpdateText ();


	}

	public void LeftStation(){
		if (!arrivedEmpty) {
			animator.Play ("Screen Up Top");
		}
		left = true;
	}

	void UpdateText(){
		offloadText.text = onloadCount.ToString ();
	}

}
