using UnityEngine;
using System.Collections;

public class ground : MonoBehaviour {

	private Grid[,] grids;

	void Start () {
		// Data for spaces will be read from json here at some point
		// TODO(kgeffen)
		grids = new Grid[10, 10];
		for (int i = 0; i < 10; i++) {
			for (int j = 0;  j < 10; j++) {
				grids[i, j] = new Grid(i * j);
			}
		}
	}

	void Update () {
		
	}

	/// <summary>
	/// Get the width of the map
	/// </summary>
	/// <returns> Width of map </returns>
	private int getX () { return grids.GetLength (0); }
	private int getZ () { return grids.GetLength (1); }

	public bool inBounds (loc l) {
		// TODO(kgeffen) explain adding 1
		return !(l.x < 0 ||
		        l.x + 1 > getX () ||
		        l.z < 0 ||
		        l.z + 1 > getZ ());
	}
}

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
