using UnityEngine;
using System.Collections;

public class UnitMenu : MonoBehaviour {
	public static UnitMenu current;
	
	// Menu not active at game start
	void Start () {
		gameObject.SetActive (false);
		current = this;
	}

	public void open (Unit unit) {
		gameObject.SetActive (true);

		// Determine list of valid actions

	}
}
