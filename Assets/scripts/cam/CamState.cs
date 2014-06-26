using UnityEngine;

/// <summary>
/// The state of the camera
/// Contains all information that is publically accesible
/// </summary>
public class CamState {

	/// <summary>
	/// The GameObject that camera follows
	/// Camera rotates around and zooms into/away from this object
	/// </summary>
	public static GameObject subject;

	/// <summary>
	/// The perspective of the camera, expressed as degrees of rotation about y axis
	/// Precondition - perspective will always be one of the following - {0, 90, 180, 270}
	/// </summary>
	public static int perspective;

	/// <summary>
	/// How zoomed in the camera is upon subject currently
	/// Expressed as distance between camera and subject
	/// Distance is checked and modified by camZoom script
	/// </summary>
	public static float dist;

}
