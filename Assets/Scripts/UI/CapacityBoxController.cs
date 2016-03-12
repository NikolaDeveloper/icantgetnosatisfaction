using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CapacityBoxController : MonoBehaviour {

	public Text capacityText;
	public int maxCapacity;
	private int currentCapacity;

	// Use this for initialization
	void Start () {
	
	}

	public void SetMaximumCapacity(int max){
		maxCapacity = max;
	}

	public void SetMaximumCapacity(int max, int value){
		maxCapacity = max;
		currentCapacity = value;
	}

	public void addToCurrentCapacity(int amount){
		
		if (currentCapacity >= maxCapacity) {
			currentCapacity = maxCapacity;
			return;
		}

		currentCapacity += amount;
	}

	public void addToCurrentCapacity(){
		
		if (currentCapacity >= maxCapacity) {
			currentCapacity = maxCapacity;
			return;
		}

		currentCapacity++;
	}

	public void removeFromCurrentCapacity(int amount){
		if (currentCapacity <= 0) {
			currentCapacity = 0;
			return;
		}

		currentCapacity -= amount;
	}

	public void removeFromCurrentCapacity(){

		if (currentCapacity <= 0) {
			currentCapacity = 0;
			return;
		}

		currentCapacity--;
	}
	
	// Update is called once per frame
	void Update () {
		capacityText.text = currentCapacity + "/" + maxCapacity;
	}
}
