using UnityEngine;
using System.Collections;

/// <summary>
/// Move the cursor in any of the 4 cardinal directions based on keyboard input
/// </summary>
public class CursorCardinalMotion : MonoBehaviour {
	// If button is held, cursor should move once every couple milliseconds
	// Cooldown is time until next input accepted
	float cooldown = 0;

	/// <summary>
	/// Time between repeated input is accepted if button is held.
	/// </summary>
	public float waitTime;
	
	void Update () {
		// Do nothing if unit menu is open
		if (UnitMenu.current.isOpen)
			return;

		// Potential change in location based on input
		Loc dLoc = determineOffset ();

		// Can't move if still cooling down from last movement.
		// Reset cooldown if no movement is attempted this tic.
		if (dLoc.Equals (Loc.zero)) {
			cooldown = 0;
			return;
		} else {
			if (cooldown > 0) {
				cooldown -= Time.deltaTime;
				return;
			} else {
				cooldown = waitTime;
			}
		}

		// Check if potential new location is in bounds
		// If it is, move to that location
		Loc potLoc = Cursor.current.loc.plus (dLoc);
		if ( World.current.isInBounds ( potLoc ) ) {
			// Change cursor's stored location
			Cursor.current.loc = potLoc;

			// Change cursor's actual position to match given location and be on ground
			transform.position = World.current.onGround (potLoc);
		}
	}

	/// <summary>
	/// Determine the offset that the cursor will theoretically experience based on the keypresses
	/// observed this tic.
	/// </summary>
	/// <returns>The potential offset of cursor.</returns>
	Loc determineOffset ()
	{
		Loc offset = new Loc ();

		// Add to offset based on keyboard input
		// Must take perspective (rotation) of camera into consideration
		if (Input.GetButton ("Up"))
			offset = offset.plus (new Loc (0, 1, Cam.main.perspective));
		if (Input.GetButton ("Down"))
			offset = offset.plus (new Loc (0, -1, Cam.main.perspective));
		if (Input.GetButton ("Left"))
			offset = offset.plus (new Loc (-1, 0, Cam.main.perspective));
		if (Input.GetButton ("Right"))
			offset = offset.plus (new Loc (1, 0, Cam.main.perspective));

		return offset;
	}
}
