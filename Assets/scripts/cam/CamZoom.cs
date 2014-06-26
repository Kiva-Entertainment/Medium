using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Move towards/away from subject based on user input.
/// Effectively zoom in/out from cursor.
/// </summary>
public class CamZoom : MonoBehaviour {

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

	// TODO(kgeffen) Add a check that distance is within bounds of acceptable distance

	/// <summary>
	/// Zoom in/out based on zoom axis input
	/// </summary>
	void Update ()
	{
		// Change in dist caused by current keypress
		float dDist = Input.GetAxis ("Zoom") * distIncrement * Time.deltaTime;
		
		// Determine if can zoom
		float potentialDistance = Cam.main.dist - dDist;
		bool canZoom = potentialDistance < maxDist &&
			potentialDistance > minDist;
		
		// If can, do and set dist to new dist
		if (canZoom) {
			Cam.main.dist = potentialDistance;
			transform.position = Vector3.MoveTowards(transform.position,
			                                         Cam.main.subject.transform.position,
			                                         dDist);
		}
	}

}
