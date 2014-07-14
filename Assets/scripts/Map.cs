using UnityEngine;
using System.Collections;

/// <summary>
/// All of the information about a map including:
/// Grid array, dimensions, source, game model, etc.
/// </summary>
public class Map {

	/// <summary>
	/// Array of grids which make up the map.
	/// Each has a height and is or isn't a hole.
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
				gridArray[i, j] = new Grid( (i + j) / 10.0f );
			}
		}

		x = gridArray.GetLength (0);
		z = gridArray.GetLength (1);
	}

	/// <summary>
	/// Get the height of given location.
	/// </summary>
	/// <returns>Height of given location.</returns>
	/// <param name="l">Location to check.</param>
	public float getHeight (Loc l) {
		return gridArray [l.x, l.z].height;
	}

	/// <summary>
	/// Check if given location is within this maps bounds.
	/// </summary>
	/// <returns><c>true</c>, if location is in bounds, <c>false</c> otherwise.</returns>
	/// <param name="l">Location to check.</param>
	public bool isInBounds (Loc l) {
		// Out of bounds if x/z is less than 0 or more than highest respective x/z
		return !(l.x < 0 ||
		         l.x > x - 1 ||
		         l.z < 0 ||
		         l.z > z - 1 );
	}

	/// <summary>
	/// Check if this map has a hole at given location.
	/// </summary>
	/// <returns><c>true</c>, if there is a hole at given location, <c>false</c> otherwise.</returns>
	/// <param name="l">Location to check.</param>
	public bool isHole(Loc l) { return gridArray[l.x, l.z].isHole; }
}
