using UnityEngine;
using System.Collections;

public class CursorToNext : MonoBehaviour {
	
	void Update () {
		if (Input.GetButton ("ToNext"))
			moveToNextUnit ();
	}

	/// <summary>
	/// Move to the next unit who acts this turn.
	/// </summary>
	void moveToNextUnit ()
	{
		// TODO implement at some point in the future

		// TODO it is kinda dumb that I have to get this info from clock instead of world
		// And by dumb I mean counterintuitive
		int actingTeam = Clock.current.activeTeam;
		// World.current.units
	}
}
