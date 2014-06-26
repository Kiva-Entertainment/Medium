using UnityEngine;
using System.Collections;

/// <summary>
/// Setup the starting state of and position/orientation of camera.
/// </summary>
public class CamSetup : MonoBehaviour {

	/// <summary>
	/// The GameObject that camera follows
	/// </summary>
	public GameObject subject;

	/// <summary>
	/// Angle (up from xz plane) that camera points at subject. In degrees
	/// </summary>
	public float lift;

	/// <summary>
	/// How zoomed in the camera is initially
	/// Represented in distance between camera and subject
	/// </summary>
	public float dist;
	
	void Awake () {
		CamState.subject = subject;
		CamState.dist = dist;
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
