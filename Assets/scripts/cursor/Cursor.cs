using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	/// <summary>
	/// The only cursor.
	/// May change when 2-p exists
	/// </summary>
	public static Cursor single;

	/// <summary>
	/// Current location of cursor.
	/// </summary>
	public Loc loc;

	void Awake () {
		// Perform actual transformation
		transform.position = Vector3.zero;

		// Set fields
		loc = new Loc (0, 0);
		single = this;
	}
}
