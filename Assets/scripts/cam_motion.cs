using UnityEngine;
using System.Collections;

using System;

public class cam_motion : MonoBehaviour
{
	/// <summary>
	/// Get the perspective of the camera, in degrees rotated around cursor
	/// </summary>
	/// <returns> Angle in degrees that camera is rotated around cursor </returns>
	public int getPerspective () {
		return realRot;
	}

	/// <summary>
	/// Object that camera follows and focuses on
	/// </summary>
	public GameObject subject;
	/// <summary>
	/// Current distance from camera to subject
	/// </summary>
	public float distance;
	/// <summary>
	/// Maximum distance between camera and subject 
	/// </summary>
	public float maxDist;
	/// <summary>
	/// Minimum distance between camera and subject
	/// </summary>
	public float minDist;
	/// <summary>
	/// How far towards/away from subject camera moves each second when zooming in/out
	/// </summary>
	public float distIncrement;

	/// <summary>
	/// Current angle (up from xz plane) that camera points at subject. In degrees
	/// </summary>
	public float lift;
	// TODO(kgeffen) Let camera rotate up/down in xz plane
	
	void Start () {
		// Camera rotation and translation corrections all happen here
		// NOTE(kgeffen) They happen here instead of in combination with unity because
		// 1) Unity has bad ui 2) This makes the vector math clear 3) This is easier to version track
		// In order to get slanted diagonally down look for view, camera must be moved/rotated

		// Move camera to dist back from subject
		transform.position = subject.transform.position + new Vector3 (0, 0, -distance);

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

	void Update ()
	{
		handleZoom ();
		handleRotation ();
	}

	/// <summary>
	/// Remaining angle of rotation around subject that camera aims to achieve
	/// Over several frames, rotates this many degrees
	/// </summary>
	public double remRot = 0;
	/// <summary>
	/// The angle of rotation camera will posses once it has finished rotating
	/// Determines current perspective of camera, which affects cursor motion
	/// </summary>
	private int realRot = 0;
	/// <summary>
	/// Number of seconds until slow rotation finishes
	/// Lower number means jerkier camera movement, higher means 'lazier' camera rotation
	/// </summary>
	public double timeToRot;
	/// <summary>
	/// Gradually rotates based on keypress
	/// </summary>
	void handleRotation ()
	{
		// Determine how much camera wants to rotate
		// And the eventual rotation of camera
		if (Input.GetButtonDown ("RotRight")) {
			remRot += 90;
			realRot += 90;
		}
		else if (Input.GetButtonDown ("RotLeft")) {
			remRot -= 90;
			realRot -= 90;
		}

		// Ensure that realRot is within bounds 0-360
		// Otherwise it could grow beyond bounds of acceptable int values
		realRot = realRot % 360;

		// Ensure within bounds -180,180
		// If trying to rotate more than 180 degrees, instead rotate by negative complement
		if (remRot > 180)
			remRot -= 360;
		else if (remRot < -180)
			remRot += 360;

		// Rotate towards targetRot
		// Think in units deg * sec / sec = deg
		// dRot is Amount to rotate this tic
		double dRot = (remRot / 2) * Time.deltaTime / timeToRot;

		// Apply that rotation
		gameObject.transform.RotateAround (subject.transform.position,
											new Vector3 (0, 1, 0),
											(float)dRot);
		// Track that rotation by dRot has happened
		remRot -= dRot;
	}

	/// <summary>
	/// Zoom in/out based on zoom axis input
	/// </summary>
	void handleZoom ()
	{
		// Change in dist caused by current keypress
		float dDist = Input.GetAxis ("Zoom") * distIncrement * Time.deltaTime;

		// Determine if can zoom
		float potentialDistance = distance - dDist;
		bool canZoom = potentialDistance < maxDist &&
			potentialDistance > minDist;

		// If can, do and set dist to new dist
		if (canZoom) {
			distance = potentialDistance;
			transform.position = Vector3.MoveTowards(transform.position,
													subject.transform.position,
													dDist);
		}
	}
}
