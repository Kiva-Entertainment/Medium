using UnityEngine;
using System.Collections;

/// <summary>
/// Move the cursor in any of the 4 cardinal directions based on keyboard input
/// </summary>
public class CursorSelect : MonoBehaviour {

	// WHat the cursor is about to do.
	enum Job {SelectingActor,
			selectingSpace};

	Job job = Job.SelectingActor;
	Unit actor;

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

	void selectActor ()
	{
		Unit u = World.current.getUnit (Cursor.current.loc);
		if (u  != null) {
			actor = u;
			job = Job.selectingSpace;
		}
	}

	void selectSpace ()
	{
		Loc cursorLoc = Cursor.current.loc;
		if (World.current.isAvailable (cursorLoc)) {
			actor.move(cursorLoc);
			job = Job.SelectingActor;
		}
	}
}
