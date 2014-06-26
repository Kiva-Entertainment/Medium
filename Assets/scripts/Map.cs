using UnityEngine;
using System.Collections;

/// <summary>
/// All of the information about a map including:
/// Grid array, dimensions, source, game model, etc.
/// </summary>
public class Map {

	/// <summary>
	/// Array of grids which make up the map.
	/// Each has a height and is or isn't a hole
	/// </summary>
	private Grid[,] gridArray;
	private string source;
	private int x;
	private int z;

	public Map () {
		// TODO(kgeffen) Actually load this info from somewhere.

		gridArray = new Grid[7, 5];
		for (int i = 0; i < gridArray.GetLength(0); i++) {
			for (int j = 0;  j < gridArray.GetLength(1); j++) {
				gridArray[i, j] = new Grid(i * j);
			}
		}

		x = gridArray.GetLength (0);
		z = gridArray.GetLength (1);
	}

	public bool isInBounds (Loc l) {
		// Out of bounds if x/z is less than 0 or more than highest respective x/z
		return !(l.x < 0 ||
		         l.x > x - 1 ||
		         l.z < 0 ||
		         l.z > z - 1 );
	}
}
