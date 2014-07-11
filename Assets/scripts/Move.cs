using UnityEngine;
using System.Collections;

/// <summary>
/// A movement of a unit to a given location which consumes some amount of mv based on the length
/// of the path the unit must follow to get there.
/// </summary>
public class Move {
	public readonly Unit actor;
	public readonly Loc start;
	public readonly Loc end;
	public readonly int mvConsumed;
	
	public Move (Unit actor, Loc start, Loc end, int mvConsumed) {
		this.actor = actor;
		this.start = start;
		this.end = end;
		this.mvConsumed = mvConsumed;
	}

	/// <summary>
	/// Perform this move.
	/// </summary>
	public void perform () {
		actor.move (end, mvConsumed);
	}

	/// <summary>
	/// Return the opposite of this move.
	/// </summary>
	/// <returns>The move which undos this move.</returns>
	public Move getOpposite () {
		return new Move (actor, end, start, -mvConsumed);
	}
}
