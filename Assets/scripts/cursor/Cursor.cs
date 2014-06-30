using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	/// <summary>
	/// The only cursor in the scene
	/// </summary>
	public static Cursor current;

	/// <summary>
	/// Current location of cursor.
	/// </summary>
	public Loc loc;

	void Awake () {
		// Perform actual transformation of cursor to origin
		transform.position = Vector3.zero;

		// Set fields with starting values
		loc = new Loc (0, 0);

		// Make this cursor publically accessible
		current = this;
	}
}
