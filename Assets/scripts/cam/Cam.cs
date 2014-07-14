using UnityEngine;
using System.Collections;

/// <summary>
/// Setup the starting state of and position/orientation of camera.
/// </summary>
public class Cam : MonoBehaviour {

	/// <summary>
	/// The main camera, which focuses on subject.
	/// </summary>
	public static Cam main;

	/// <summary>
	/// The GameObject that camera follows.
	/// Camera rotates around and zooms into/away from this object.
	/// </summary>
	public GameObject subject;
	
	/// <summary>
	/// The perspective of the camera, expressed as degrees of rotation about y axis
	/// Precondition - perspective will always be one of the following - {0, 90, 180, 270}
	/// </summary>
	public int perspective;
	
	/// <summary>
	/// How zoomed in the camera is upon subject currently.
	/// Expressed as distance between camera and subject.
	/// Distance is checked and modified by camZoom script.
	/// </summary>
	public float dist;

	/// <summary>
	/// Angle (up from xz plane) that camera points at subject. In degrees.
	/// </summary>
	public float lift;

	void Awake () {
		main = this;
	}

	void Start () {
		// Camera is placed in correct position/orientation here
		// In order to get slanted diagonally down look for view, camera must be moved/rotated
		
		// Move camera to correct distance back from subject
		transform.position = subject.transform.position + new Vector3 (0, 0, -dist);
		
		// ROTATION SETUP
		// Reset rotation, must be done before rotation adjustments get made
		transform.rotation = new Quaternion ();
		
		// Rotate up from ground by given number of degrees
		transform.RotateAround (subject.transform.position,
		                        new Vector3 (1, 0, 0),
		                        lift);
		
		// Rotate around y axis 45 degrees to get diagonal angled view
		transform.RotateAround (subject.transform.position,
		                        new Vector3 (0, 1, 0),
		                        45);
	}
}
