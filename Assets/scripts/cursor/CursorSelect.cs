using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Move the cursor in any of the 4 cardinal directions based on keyboard input
/// </summary>
public class CursorSelect : MonoBehaviour {
	public static CursorSelect current { get; private set; }

	List<GameObject> markers = new List<GameObject> ();

	// What the cursor is about to do.
	enum Job {SelectingActor,
			SelectingSpace,
			SelectingSkill,
			SelectingTarget};

	Job job = Job.SelectingActor;
	Unit actor;
	Move[] validMoves;
	Loc[] skillRange;
	/// <summary>
	/// After unit menu is exited, should wait for a tic so that key presses while menu is open
	/// are not observed and recponded to immediately.
	/// </summary>
	bool wait = false;

	/// <summary>
	/// The skill for which actor is performing and targets are being selected for.
	/// </summary>
	Skill currentSkill = null;

	void Awake () {
		current = this;
	}

	/// <summary>
	/// Each update, if select button is pressed, do various form of selection based on current context.
	/// </summary>
	void Update () {
		if (wait) {
			wait = false;
			return;
		}

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
			case Job.SelectingSpace:
				selectSpace();
				return;
			case Job.SelectingTarget:
				selectTarget ();
				return;
			case Job.SelectingSkill:
				return;
		}
	}

	public void unitMenuClosing (Skill selectedSkill) {
		// Small wait should occur so that key input not observed immediately.
		wait = true;

		if (selectedSkill == null)
			job = Job.SelectingSpace;
		else {
			job = Job.SelectingTarget;
			currentSkill = selectedSkill;

			// Remove old markers, which indicated valid spaces to move to
			foreach (GameObject o in markers)
				Destroy(o);

			// Add new markers
			skillRange = selectedSkill.getRange (onlyValid: false);
			foreach ( Loc target in skillRange ) {
				
				// Add all appropriate markers and store them in a list so they can be removed later
				markers.Add(
					Object.Instantiate (Resources.Load ("marker"),
				                    World.current.onGround(target),
				                    Quaternion.identity) as GameObject);
			}
		}

		// Either way, enable movement
		GetComponent<CursorCardinalMotion> ().enabled = true;
	}

	/// <summary>
	/// Select an actor to do things such as move, attack, inspect, etc.
	/// </summary>
	void selectActor ()
	{
		Unit u = World.current.getUnit (Cursor.current.loc);
		// Only select if there is a unit in space and that unit acts this turn
		if (u  != null && u.team == Clock.current.activeTeam) {

			gameObject.renderer.material.color = Color.green;
			actor = u;
			job = Job.SelectingSpace;

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
	/// If actor space is selected, open unit menu so actor can perform skill.
	/// </summary>
	void selectSpace ()
	{
		Loc cursorLoc = Cursor.current.loc;

		// Open menu if actor was selected
		if (cursorLoc.Equals (actor.loc))
			openUnitMenu ();
		else
			potMoveActor (cursorLoc);
	}

	/// <summary>
	/// Move actor to given location if move to location is valid.
	/// </summary>
	void potMoveActor (Loc targetLoc)
	{
		// Determine which move gets actor to given position, if any
		Move move = null;
		foreach (Move m in validMoves)
			if (m.end.Equals (targetLoc))
				move = m;
		
		// Move actor to given loc if a valid exists
		if ( move != null ) {
			
			move.perform ();
			
			// Record move in log
			Log.current.push(move);

			// Remove all markers
			foreach (GameObject o in markers)
				Destroy(o);

			// Reselect actor
			job = Job.SelectingActor;
			selectActor ();
		}
	}

	/// <summary>
	/// Selects the target.
	/// </summary>
	void selectTarget ()
	{
		Loc[] validTargets = actor.skills.First.Value.getRange (onlyValid: true);

		bool canPerform = false;
		foreach (Loc target in validTargets)
			if (target.Equals(Cursor.current.loc))
				canPerform = true;

		if (canPerform) {
			currentSkill.perform (Cursor.current.loc);

			actor.actCur--;

			// TODO log should only clear for some skills
			Log.current.clear ();

			// Skill has been performed, deselect actor
			deselect ();
		}
	}

	/// <summary>
	/// Open the unit menu for selected unit.
	/// </summary>
	void openUnitMenu ()
	{
		UnitMenu.current.open (actor);

		// disable movement while menu is open
		GetComponent<CursorCardinalMotion> ().enabled = false;

		// TODO remove scaffolding
		gameObject.renderer.material.color = Color.blue;
	}

	void deselect ()
	{
		// TODO depend on context
		if (job == Job.SelectingSkill)
			return;

		gameObject.renderer.material.color = Color.red;

		job = Job.SelectingActor;

		// Remove all markers
		foreach (GameObject o in markers)
			Destroy(o);
	}

	public void close () {
		deselect ();
	}
}
