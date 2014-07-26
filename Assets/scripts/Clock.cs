using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {
	public static Clock current;

	/// <summary>
	/// The id of whichever team is active currently.
	/// </summary>
	/// <value>The id of the acyive team.</value>
	public int activeTeam { get; private set; }

	// Use this for initialization
	void Start () {
		activeTeam = 0;
		current = this;
	}

	void Update () {
		if ( Input.GetButtonDown ("Pass") )
			endTurn ();
	}

	/// <summary>
	/// End the current turn and allow next team to take a turn.
	/// </summary>
	void endTurn ()
	{
		// Stop selecting unit
		CursorSelect.current.close ();
		// DO not allow undos of previous turn
		Log.current.clear ();

		foreach (Unit unit in World.current.units) {
			if (unit.team == activeTeam) {
				unit.refresh ();
			}
		}

		// TODO Add support for more than 2 teams
		if (activeTeam == 0)
			activeTeam = 1;
		else
			activeTeam = 0;
	}
}
