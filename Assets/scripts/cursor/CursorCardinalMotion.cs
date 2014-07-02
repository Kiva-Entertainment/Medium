using UnityEngine;
using System.Collections;

/// <summary>
/// Move the cursor in any of the 4 cardinal directions based on keyboard input
/// </summary>
public class CursorCardinalMotion : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		// Potential change in location based on input
		Loc dLoc = new Loc(0, 0);

		// Add to offset based on keyboard input
		// Must take perspective (rotation) of camera into consideration
		if (Input.GetButtonDown ("Up"))
			dLoc = dLoc.plus (new Loc (0, 1, Cam.main.perspective));
		if (Input.GetButtonDown ("Down"))
			dLoc = dLoc.plus (new Loc (0, -1, Cam.main.perspective));
		if (Input.GetButtonDown ("Left"))
			dLoc = dLoc.plus (new Loc (-1, 0, Cam.main.perspective));
		if (Input.GetButtonDown ("Right"))
			dLoc = dLoc.plus (new Loc (1, 0, Cam.main.perspective));

		// Check if potential new location is in bounds
		// If it is, move to that location
		Loc potLoc = Cursor.current.loc.plus (dLoc);
		if ( World.current.isInBounds ( potLoc ) ) {

			// Change cursor's stored location
			Cursor.current.loc = potLoc;

			// Change cursor's actual position
			float height = World.current.map.getHeight (potLoc);
			transform.position = potLoc.asVect () + new Vector3(0, height, 0);
		}
	}
}
