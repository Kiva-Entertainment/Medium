using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveRange {
	/// <summary>
	/// Determine the move range for a given unit.
	/// Return a list of all moves that unit can do.
	/// </summary>
	/// <param name="unit">The unit being considered.</param>
	public static List<Move> determine(Unit unit) {

		// Size of array should be 1 more than twice the units max movement, in both directions.
		int size = unit.mvCur * 2 + 1;
		int?[,] moveAry = new int?[size, size];

		spread (moveAry,
		        unit.mvCur,
		        unit.mvCur,
		        0,
		        unit.mvCur,
		        World.current.getHeight(unit.loc),
		        unit.jump);

		// Generate list of moves to return
		List<Move> moves = new List<Move> ();
		for ( int x = 0; x < moveAry.GetLength(0); x++ )
			for ( int y = 0; y < moveAry.GetLength(1); y++ )
				if ( moveAry[x,y] != null )
					moves.Add( new Move ( new Loc(x,y).plus(unit.loc), (int) moveAry[x,y]));

		return moves;
	}

	/// <summary>
	/// Updates moveAry as it goes.
	/// </summary>
	/// <param name="moveAry">Move ary.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="dMv">Delta movement caused by this move.</param>
	static void spread (int?[,] moveAry, int x, int y, int dMv, int mvMax, float lastHeight, float jump)
	{
		float height = World.current.getHeight (x, y);

		// RETURN IF SPACE INVALID
		if (!World.current.isInBounds (new Loc (x, y)))
			return;
		if (Mathf.Abs (height - lastHeight) > jump)
			return;
		if (dMv >= mvMax)
			return;
		if (!World.current.isAvailable (new Loc (x, y)))
			return;

		// If potential space to move to is empty or the movement to get to it
		// is more than current dMv, mark space attainable
		if (moveAry[x,y] == null || moveAry[x,y] > dMv) {
			moveAry[x,y] = dMv;

			// Spread in each of 4 cardinal directions
			foreach (int dl in new int[] {-1, 1}) {
				spread(moveAry, x + dl, y, dMv + 1, mvMax, height, jump);
				spread(moveAry, x, y + dl, dMv + 1, mvMax, height, jump);
			}
		}
	}
}

/// <summary>
/// TODO
/// </summary>
public class Move {
	public readonly Loc loc;
	public readonly int mvConsumed;

	public Move (Loc loc, int mvConsumed) {
		this.loc = loc;
		this.mvConsumed = mvConsumed;
	}
}
