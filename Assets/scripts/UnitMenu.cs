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
		CamRotate.able = false;

		gameObject.SetActive (true);

		actor = unit;

		displaySkills ();
	}

	void displaySkills () {
		string result = "";

		// TODO change this to be prettier
		result += getCurSkill ().getExtent () + " | ";
		result += getCurSkill ().getCost () + " ";

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
			select ();

		} else if (Input.GetButtonDown ("Deselect")) {
			exit ();

		} else if (Input.GetButtonDown ("RaiseExtent")) {
			getCurSkill ().raiseExtent ();
			displaySkills ();

		} else if (Input.GetButtonDown ("LowerExtent")) {
			getCurSkill ().lowerExtent ();
			displaySkills ();
		}
	}

	/// <summary>
	/// Select the skill viewed currently and exit the unit menu.
	/// </summary>
	void select ()
	{
		exit (skill: getCurSkill() );
	}

	/// <summary>
	/// Close the unit menu and signal that given skill was selected.
	/// </summary>
	/// <param name="skill">The skill selected.</param>
	void exit (Skill skill = null)
	{
		// Enable camera rotation
		CamRotate.able = true;

		CursorSelect.current.unitMenuClosing (skill);
		gameObject.SetActive (false);
	}

	// Utility method for this script, gets the skill currently selected
	Skill getCurSkill () { return actor.skills.First.Value; }
}
