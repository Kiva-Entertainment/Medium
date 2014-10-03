using System.Collections.Generic;

public static class SkillValidCheck {
	public static Loc[] check(List<Loc> spaces,
	                          bool inBounds = true,
	                          bool unoccupied = false,
	                          bool occupied = false)
	{
		List<Loc> result = new List<Loc> ();
		foreach (Loc space in spaces) {
			if (inBounds)
				if (!World.current.isInBounds(space))
					continue;
			
			if (unoccupied)
				if (Unit.get (space) != null)
					continue;

			if (occupied)
				if (Unit.get (space) == null)
					continue;

			result.Add (space);
		}

		return result.ToArray();
	}
}
