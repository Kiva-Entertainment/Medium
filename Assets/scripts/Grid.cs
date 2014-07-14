using UnityEngine;
using System.Collections;

/// <summary>
/// A single square on the field.
/// Can be a hole.
/// </summary>
public class Grid {

	public bool isHole;
	public float height;
	
	public Grid () {
		isHole = true;
		height = 0;
	}
	
	public Grid (float height) {
		isHole = false;
		this.height = height;
	}
}
