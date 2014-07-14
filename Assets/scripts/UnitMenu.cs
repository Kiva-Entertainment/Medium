using UnityEngine;
using System.Collections;

public class UnitMenu : MonoBehaviour {
	public static UnitMenu current;
	private Unit actor;
	
	// Menu not active at game start
	void Start () {
		gameObject.SetActive (false);
		current = this;
	}

	public void open (Unit unit) {
		gameObject.SetActive (true);

		actor = unit;

		gameObject.guiText.text = actor.skills[0].getName ();
		// Determine list of valid actions

	}
}
