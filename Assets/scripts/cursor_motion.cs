using UnityEngine;
using System.Collections;

public class cursor_motion : MonoBehaviour {
	/// <summary>
	/// The current location of cursor in standardized location format
	/// </summary>
	private loc curLoc;

	/// <summary>
	/// Camera, follows cursor when it moves
	/// </summary>
	public GameObject cam;

	/// <summary>
	/// The world, has information about the state of the world at all times
	/// </summary>
	public GameObject world;
	/// <summary>
	/// All information about the ground, such as the width and length, the height of each space, etc.
	/// </summary>
	private world_ground g;

	void Awake () {
		g = world.GetComponent<world_ground> ();
	}
	
	void Start () {
		// Precondition: Cursor object starts at (0,0)
		curLoc = new loc (0, 0);
	}

	void Update () {
		handleInput ();
	}

	void handleInput () {
		// The potential change in location based on input
		loc dLoc;

		if (Input.GetButtonDown ("Up"))
			dLoc = new loc (0, 1);
		else if (Input.GetButtonDown ("Down"))
			dLoc = new loc (0, -1);
		else if (Input.GetButtonDown ("Left"))
			dLoc = new loc (-1, 0);
		else if (Input.GetButtonDown ("Right"))
			dLoc = new loc (1, 0);
		else
			dLoc = new loc(0, 0);

		// Check if potential new location is in bounds,
		// if it is, move to that location, and move camera too
		loc potLoc = curLoc.plus (dLoc);
		if ( g.inBounds ( potLoc ) ) {
			curLoc = potLoc;
			transform.position = curLoc.asVect();

			// Move camera
			cam.transform.position += dLoc.asVect();
		}

	}
}
