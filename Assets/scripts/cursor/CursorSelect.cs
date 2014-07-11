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

			foreach ( Move m in validMoves ) {

				// Add all appropriate markers and store them in a list so they can be removed later
				markers.Add(
					Object.Instantiate (Resources.Load ("marker"),
										World.current.onGround(m.loc),
										Quaternion.identity) as GameObject);
			}
		}
	}

	/// <summary>
	/// Select a space for the currently selected actor to move to.
	/// </summary>
	void selectSpace ()
	{
		Loc cursorLoc = Cursor.current.loc;

		bool spaceIsMarked = false;
		foreach (Move m in validMoves)
			if (m.loc.Equals (cursorLoc))
				spaceIsMarked = true;

		if (spaceIsMarked && World.current.isAvailable (cursorLoc)) {
			gameObject.renderer.material.color = Color.red;
			actor.move(cursorLoc);
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
