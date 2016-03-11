using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OffloadBoxController : MonoBehaviour {

	public Text offloadText;
	public Animator animator;

	private bool left = false;

	public int offloadCount;

	void Awake(){
		gameObject.SetActive (false);
		
	}

	public void ArrivedAtStation(int amountToOffload){
		offloadCount = amountToOffload;
		UpdateText ();
		gameObject.SetActive (true);
		animator.Play ("Screen Drop Top");
		left = false;
	}

	public void PassengerDisembark(){
		offloadCount -= 1;
		UIManager.instance.capacityBox.removeFromCurrentCapacity ();

		if (offloadCount <= 0) {
			offloadCount = 0;
			if (left == false) {
				LeftStation ();
			}
		}

		UpdateText ();


	}

	public void LeftStation(){
		animator.Play ("Screen Up Top");
		left = true;
	}

	void UpdateText(){
		offloadText.text = offloadCount.ToString ();
	}


}
