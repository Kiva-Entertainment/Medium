using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Move the cursor in any of the 4 cardinal directions based on keyboard input
/// </summary>
public class CursorSelect : MonoBehaviour {

	List<GameObject> markers = new List<GameObject>();

	// WHat the cursor is about to do.
	enum Job {SelectingActor,
			selectingSpace};

	Job job = Job.SelectingActor;
	Unit actor;

	/// <summary>
	/// Each update, if select button is pressed, do various form of selection based on current context.
	/// </summary>
	void Update () {
		if (!Input.GetButtonDown ("Select"))
			return;

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
			Move[] validMoves = MoveRange.determine(u);

			foreach ( Move m in validMoves ) {

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
		if (World.current.isAvailable (cursorLoc)) {
			gameObject.renderer.material.color = Color.red;
			actor.move(cursorLoc);
			job = Job.SelectingActor;

			foreach(GameObject o in markers)
				Destroy(o);
		}
	}
}
