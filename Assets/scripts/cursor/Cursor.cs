using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public static Loc loc { get; private set; }
	public static bool canMove = true;

	// Move cursor to origin and set starting location
	void Awake () {
		transform.position = Vector3.zero;
		loc = new Loc (0, 0);
	}

	void Update () {
		updatePosition ();
	}

	float cooldown = 0;
	public float waitTime;
	private void updatePosition ()
	{
		if (!canMove) { return; }

		Loc dLoc = determineOffset ();
		// Update wait time and see if cursor can move this tic
		if (!updateWaitTime (dLoc)) { return; }
		
		Loc potLoc = loc.plus (dLoc);
		if (World.current.isInBounds (potLoc)) {
			loc = potLoc;
			transform.position = World.current.onGround (potLoc);
		}
	}

	/// <summary>
	/// Determine change in loc caused by keypresses this tic.
	/// </summary>
	/// <returns>Potential offset of cursor.</returns>
	private Loc determineOffset ()
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

	/// <summary>
	/// Adjust wait time until next movement allowed and check if can move currently.
	/// </summary>
	/// <returns><c>true</c>, if can move, <c>false</c> otherwise.</returns>
	/// <param name="dLoc">Attempted change in location.</param>
	private bool updateWaitTime (Loc dLoc)
	{
		if (dLoc.Equals (Loc.zero)) {
			cooldown = 0;
			return false;
		}
		if (cooldown > 0) {
			cooldown -= Time.deltaTime;
			return false;
		}
		cooldown = waitTime;
		return true;
	}
}
