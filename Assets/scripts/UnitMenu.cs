using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

		displaySkills ();
	}

	void displaySkills () {
		string result = "";

		foreach (Skill s in actor.skills) {
			result += s.getName () + "\n";
		}

		gameObject.guiText.text = result;
	}

	void Update () {
		if (Input.GetButtonDown ("Up")) {
			actor.cycleSkillsUp ();
			displaySkills ();

		} else if (Input.GetButtonDown ("Down")) {
			actor.cycleSkillsDown ();
			displaySkills ();

		} else if (Input.GetButtonDown ("Select")) {

		} else if (Input.GetButtonDown ("Deselect")) {
			exit ();
		}
	}

	void exit ()
	{
		gameObject.SetActive (false);
		// Reactivate cursor
	}
}
