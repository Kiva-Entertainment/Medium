using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMenu : MonoBehaviour {
	public static UnitMenu current;
	/// <summary>
	/// If this unit menu is open currently.
	/// Cursor movement/selection doesn't occur when menu is open.
	/// </summary>
	public bool isOpen { get; private set; }

	private Unit actor;
	
	// Menu not active at game start
	void Start () {
		gameObject.SetActive (false);
		isOpen = false;

		current = this;
	}

	public void open (Unit unit) {
		gameObject.SetActive (true);
		isOpen = true;

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
		isOpen = false;
		gameObject.SetActive (false);
		// Reactivate cursor
	}
}
