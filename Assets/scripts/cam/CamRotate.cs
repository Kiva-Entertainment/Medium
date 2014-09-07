using UnityEngine;
using System.Collections;

/// <summary>
/// Rotate the camera around subject based on user input.
/// </summary>
public class CamRotate : MonoBehaviour {
	/// <summary>
	/// Whether the camera can rotate currently.
	/// When unit menu is open, camera cannot rotate, for example
	/// </summary>
	public static bool able = true;

	/// <summary>
	/// Number of seconds for slow rotation to finish half of desired rotation.
	/// In other words, half life of (rotation left to occur).
	/// Lower number means jerkier camera movement, higher means 'lazier' camera rotation.
	/// Precondition - Greater than zero.
	/// </summary>
	public double rotSpeed;

	/// <summary>
	/// Remaining angle of rotation around subject that camera aims to achieve.
	/// Over several frames, rotates this many degrees.
	/// </summary>
	private double rotLeft = 0;

	/// <summary>
	/// Rotate around subject by increment of 90 degrees based on keypress.
	/// Rotation happens slowly based on <see cref="timeToRot"/> .
	/// </summary>
	public void Update ()
	{
		if (!able) return;
		// Update the perspective and the remaining rotation based on keypresses
		updateState ();
		performRotation ();
	}

	/// <summary>
	/// Rotate a fraction of remaining desired rotation.
	/// </summary>
	private void performRotation () {
		// Determine how much rotation should happen this tic
		double dRot = (rotLeft / 2) * Time.deltaTime / rotSpeed;
		
		// Apply that rotation
		gameObject.transform.RotateAround (Cam.main.subject.transform.position,
		                                   new Vector3 (0, 1, 0),
		                                   (float) dRot);

		// Decrement the rotation that still has to happen by amount that just happened
		rotLeft -= dRot;
	}

	private void updateState ()
	{
		// Add/subtract from rotation
		// NOTE(kgeffen) Both button checks happen because if both are pressed the rotation should cancel
		if (Input.GetButtonDown ("RotRight")) {
			rotLeft += 90;
			Cam.main.perspective += 90;
		}
		if (Input.GetButtonDown ("RotLeft")) {
			rotLeft -= 90;
			Cam.main.perspective -= 90;
		}

		// Ensure that perspective is one of - {0, 90, 180, 270}
		Cam.main.perspective %= 360;
	}
}
