using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMenu : MonoBehaviour {
	public static UnitMenu current { get; private set; }
	private Unit actor;
	
	// Menu not active at game start
	void Start () {
		gameObject.SetActive (false);
		current = this;
	}

	/// <summary>
	/// Open the unit menu for given unit.
	/// </summary>
	/// <param name="unit">The unit menu is being opened for.</param>
	public void open (Unit unit) {
		gameObject.SetActive (true);

		actor = unit;

		displaySkills ();
	}

	void displaySkills () {
		string result = "";

		// TODO change this to be prettier
		result += actor.skills.First.Value.getExtent () + " | ";
		result += actor.skills.First.Value.getCost () + " ";

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

		} else if (Input.GetButtonDown ("Select") && actor.actCur > 0) {
			select ();

		} else if (Input.GetButtonDown ("Deselect")) {
			exit ();

		} else if (Input.GetButtonDown ("RaiseExtent")) {
			actor.skills.First.Value.raiseExtent ();
			displaySkills ();

		} else if (Input.GetButtonDown ("LowerExtent")) {
			actor.skills.First.Value.lowerExtent ();
			displaySkills ();
		}
	}

	/// <summary>
	/// Select the skill viewed currently.
	/// </summary>
	void select ()
	{
		CursorSelect.current.unitMenuClosing (actor.skills.First.Value);
		gameObject.SetActive (false);
	}

	void exit ()
	{
		CursorSelect.current.unitMenuClosing (null);
		gameObject.SetActive (false);
	}
}
