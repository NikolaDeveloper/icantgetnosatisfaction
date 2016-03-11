using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public SatisfactionBarController satBar;
	public CapacityBoxController capacityBox;
	public OffloadBoxController offloadBox;


	// Use this for initialization
	void Start () {
		instance = this;
		offloadBox.ArrivedAtStation (100);
	}
	
	// Update is called once per frame
	void Update () {
		offloadBox.PassengerDisembark ();
	}
}
