using UnityEngine;
using System.Collections;

public class PlayerStats {

	public static PlayerStats instance;

	public string trainLineName = "";
	public int playerMoney = 500;
	public int satisfaction = 50;
	public Color mainColor = Color.white;
	public Color secondaryColor = Color.white;
	public Color tertiaryColor = Color.white;

	public static PlayerStats GetInstance () {
		
		if (instance == null) {
			instance = new PlayerStats();
		}

		return instance;

	}

}
