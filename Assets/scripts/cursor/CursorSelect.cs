using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Move the cursor in any of the 4 cardinal directions based on keyboard input
/// </summary>
public class CursorSelect : MonoBehaviour {

	List<GameObject> markers = new List<GameObject>();

	// What the cursor is about to do.
	enum Job {SelectingActor,
			selectingSpace};

	Job job = Job.SelectingActor;
	Unit actor;
	Move[] validMoves;

	/// <summary>
	/// Each update, if select button is pressed, do various form of selection based on current context.
	/// </summary>
	void Update () {
		if (Input.GetButtonDown ("Select"))
			select ();
		else if (Input.GetButtonDown ("Deselect"))
			deselect ();
	}

	void select () {
		switch (job)
		{
			case Job.SelectingActor:
				selectActor();
				return;
			case Job.selectingSpace:
				selectSpace();
				return;
		}
	}
	
	/// <summary>
	/// Select an actor to do things such as move, attack, inspect, etc.
	/// </summary>
	void selectActor ()
	{
		Unit u = World.current.getUnit (Cursor.current.loc);
		if (u  != null) {
			gameObject.renderer.material.color = Color.green;
			actor = u;
			job = Job.selectingSpace;

			// Display spaces actor can move to
			validMoves = MoveRange.determine(u);

			foreach ( Move move in validMoves ) {

				// Add all appropriate markers and store them in a list so they can be removed later
				markers.Add(
					Object.Instantiate (Resources.Load ("marker"),
										World.current.onGround(move.end),
										Quaternion.identity) as GameObject);
			}
		}
	}

	/// <summary>
	/// Move the actor to the space selected by cursor if space is valid.
	/// </summary>
	void selectSpace ()
	{
		Loc cursorLoc = Cursor.current.loc;

		// Determine which move gets actor to given position, if any
		Move move = null;
		foreach (Move m in validMoves)
			if (m.end.Equals (cursorLoc))
				move = m;

		// Move actor to given space if possible
		if ( move != null ) {

			move.perform ();

			// Record move in log
			Log.current.push(move);

			// Cleanup
			gameObject.renderer.material.color = Color.red;
			job = Job.SelectingActor;
			// Remove all markers
			foreach (GameObject o in markers)
				Destroy(o);
		}
	}

	void deselect ()
	{
		// TODO depend on context
		gameObject.renderer.material.color = Color.red;

		job = Job.SelectingActor;

		// Remove all markers
		foreach (GameObject o in markers)
			Destroy(o);
	}
}
