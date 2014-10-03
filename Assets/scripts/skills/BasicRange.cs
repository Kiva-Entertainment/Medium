// Methods for computing basic types of range
using System.Collections.Generic;

static class BasicRange {
	public static List<Loc> self (Unit unit) {
		List<Loc> result = new List<Loc> ();
		result.Add (unit.loc);
		return result;
	}

	/// <summary>
	/// All spaces up to dist from unit, cardinally
	/// </summary>
	public static List<Loc> sightline (Unit unit, int dist) {
		List<Loc> result = new List<Loc> ();

		foreach (Loc offset in Loc.cardinals)
			for (int i = 1; i <= dist; i++)
				result.Add (unit.loc.plus (offset.scale (i)));
		
		return result;
	}
}