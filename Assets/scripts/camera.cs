using UnityEngine;
using System.Collections;

using System;

public class camera : MonoBehaviour {

	public GameObject focus;
	/// <summary>
	/// Distance from camera to focus
	/// </summary>
	public float distance;
	/// <summary>
	/// Angle up from ground that camera points at focus. In degrees
	/// </summary>
	public float angle;

	// Use this for initialization
	void Start () {
	
	}
	
	/// <summary>
	/// Position camera in correct location and orientation pointed at focus
	/// </summary>
	void Update () {
		// set camera position to *distance* back from *focus*
		gameObject.transform.position = focus.transform.position + new Vector3 (0, 0, -distance);

		// reset rotation, must be done before rotation adjustments get made
		gameObject.transform.rotation = new Quaternion ();

		// rotate up from ground by *angle* degrees
		gameObject.transform.RotateAround (focus.transform.position,
		                                  new Vector3 (1, 0, 0),
		                                  angle);
		
		// rotate around y axis 45 degrees to get angled view
		gameObject.transform.RotateAround (focus.transform.position,
		                                   new Vector3 (0, 1, 0),
		                                   45);
	}
}
