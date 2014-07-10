using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveRange {
	/// <summary>
	/// Determine the move range for a given unit.
	/// Return a list of all moves that unit can do.
	/// </summary>
	/// <param name="unit">The unit being considered.</param>
	public static Move[] determine(Unit unit) {

		// Size of array should be 1 more than twice the units max movement, in both directions.
		int size = unit.mvCur * 2 + 1;
		int?[,] moveAry = new int?[size, size];

		spread (moveAry,
				unit.mvCur,
				unit.mvCur,
				0,
				World.current.getHeight(unit.loc),
				unit);

		// Generate list of moves to return
		List<Move> moves = new List<Move> ();
		for ( int x = 0; x < size; x++ )
			for ( int y = 0; y < size; y++ )
				if ( moveAry[x,y] != null )
					moves.Add( new Move ( new Loc(x,y).plus(unit.loc), (int) moveAry[x,y]));

		return moves.ToArray();
	}

	/// <summary>
	/// Updates moveAry as it goes.
	/// </summary>
	/// <param name="moveAry">Move ary.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="dMv">Delta movement caused by this move.</param>
	static void spread (int?[,] moveAry, int x, int y, int dMv, float lastHeight, Unit unit)
	{
		Loc realLoc = unit.loc.plus (new Loc (x, y));

		// RETURN IF SPACE INVALID
		if (!World.current.isInBounds (realLoc))
			return;
		if (!World.current.isAvailable (new Loc (x, y)))
			return;
		if (dMv >= unit.mvMax)
			return;

		float height = World.current.getHeight (x, y);

		if (Mathf.Abs (height - lastHeight) > unit.jump)
			return;

		//// NOTE for next time
		// things aren't working atm because the x,y are not offset from unit,
		// unit is at center of array, not at 0,0




		// If space hasn't been considered or currently considered path is
		// shorter than any considered before, mark space with dMv
		if (moveAry[x,y] == null || moveAry[x,y] > dMv) {
			moveAry[x,y] = dMv;

			// Spread in each of 4 cardinal directions
			foreach (int dl in new int[] {-1, 1}) {
				spread(moveAry, x + dl, y, dMv + 1, height, unit);
				spread(moveAry, x, y + dl, dMv + 1, height, unit);
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
