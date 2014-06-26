using UnityEngine;
using System.Collections;

public class CursorCardinalMotion : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		// The potential change in location based on input
		Loc dLoc;
		
		// Must take perspective (rotation) of camera into consideration
		if (Input.GetButtonDown ("Up"))
			dLoc = new Loc (0, 1, CamState.perspective);
		else if (Input.GetButtonDown ("Down"))
			dLoc = new Loc (0, -1, CamState.perspective);
		else if (Input.GetButtonDown ("Left"))
			dLoc = new Loc (-1, 0, CamState.perspective);
		else if (Input.GetButtonDown ("Right"))
			dLoc = new Loc (1, 0, CamState.perspective);
		else
			dLoc = new Loc();
		
		// Check if potential new location is in bounds,
		// if it is, move to that location
		Loc potLoc = CursorState.loc.plus (dLoc);
		if ( World.current.isInBounds ( potLoc ) ) {

			// Change location of cursor to new location
			CursorState.loc = potLoc;
			transform.position = CursorState.loc.asVect();
		}
	}
}
