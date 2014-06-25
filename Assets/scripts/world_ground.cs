using UnityEngine;
using System.Collections;

public class world_ground : MonoBehaviour {

	private Grid[,] grids;

	void Start () {
		// Data for spaces will be read from json here at some point
		// TODO(kgeffen) do that thing with the jsons
		grids = new Grid[10, 10];
		for (int i = 0; i < 10; i++) {
			for (int j = 0;  j < 10; j++) {
				grids[i, j] = new Grid(i * j);
			}
		}
	}

	/// <summary>
	/// Get the width of the map
	/// </summary>
	/// <returns> Width of map </returns>
	private int getX () { return grids.GetLength (0); }
	private int getZ () { return grids.GetLength (1); }

	public bool inBounds (loc l) {
		// Out of bounds if x/z is less than 0 or more than highest respective x/z
		return !(l.x < 0 ||
		        l.x > getX () - 1 ||
		        l.z < 0 ||
		        l.z > getZ () - 1 );
	}
}

// TODO(kgeffen) Make this it's own class if other scripts need to access it
/// <summary>
/// A single square on the field
/// Unless a hole, has height
/// </summary>
public class Grid {
	public bool isHole { get; set; }
	public int height { get; set; }
	
	public Grid () {
		isHole = true;
		height = 0;
	}
	
	public Grid (int height) {
		isHole = false;
		this.height = height;
	}
}
