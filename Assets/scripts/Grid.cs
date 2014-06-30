using UnityEngine;
using System.Collections;

/// <summary>
/// A single square on the field.
/// Unless a hole, has height.
/// </summary>
public class Grid {

	public bool isHole;
	public int height;
	
	public Grid () {
		isHole = true;
		height = 0;
	}
	
	public Grid (int height) {
		isHole = false;
		this.height = height;
	}
}
