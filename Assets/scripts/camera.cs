using UnityEngine;
using System.Collections;

using System;

public class camera : MonoBehaviour
{

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
	/// How far towards/away from subject camera moves each second
	/// </summary>
	public float distIncrement;

	/// <summary>
	/// Current angle (up from xz plane) that camera points at subject. In degrees
	/// </summary>
	public float angle;

	/// <summary>
	/// Number of seconds until real rotation matches target rotation
	/// </summary>
	public float rotSlowTime;


	void Start () {
		// Move camera to _distance back from _subject
		transform.position = subject.transform.position + new Vector3 (0, 0, -distance);

		// ROTATION SETUP
		// Reset rotation, must be done before rotation adjustments get made
		transform.rotation = new Quaternion ();

		// rotate up from ground by *angle* degrees
		transform.RotateAround (subject.transform.position,
		                                  new Vector3 (1, 0, 0),
		                                  angle);
		
		// rotate around y axis 45 degrees to get angled view
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
	/// Angle of rotation around subject that camera aims to achieve
	/// Gets to this angle over several frames
	/// </summary>
	public double targetRot = 0;
	/// <summary>
	/// Number of seconds until slow rotation finishes
	/// </summary>
	public double timeToRot;
	/// <summary>
	/// Gradually rotates based on keypress
	/// </summary>
	void handleRotation ()
	{
		// Determine targetRot
		if (Input.GetButtonDown ("RotRight"))
			targetRot += 90;
		else if (Input.GetButtonDown ("RotLeft"))
			targetRot -= 90;

		if (targetRot >= 360)
			targetRot -= 360;
		else if (targetRot <= -360)
			targetRot += 360;

		// Rotate towards targetRot
		// Amount to rotate this tic
		double dRot = (targetRot / 2) * Time.deltaTime / timeToRot;
		targetRot -= dRot;

		gameObject.transform.RotateAround (subject.transform.position,
		                                   new Vector3 (0, 1, 0),
		                                   (float)dRot);
	}

	/// <summary>
	/// Zoom in/out based on zoom axis input
	/// </summary>
	void handleZoom ()
	{
		float z = Input.GetAxis ("Zoom");

		float potentialDistance = distance - z * distIncrement * Time.deltaTime;

		if (potentialDistance >= minDist && potentialDistance <= maxDist)
			distance = potentialDistance;
	}
}
