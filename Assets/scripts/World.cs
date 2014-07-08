using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	public static World current;
	public Map map;
	public Unit[] units;

	void Awake () {
		map = new Map ();
		current = this;

		// TODO(kgeffen) Unit data should be imported from json or something
		units = new Unit[2];
		for (int i = 0; i < units.Length; i++)
		{
			units[i] = new Unit("Bob " + i);

			// Deploy unit
			units[i].deploy(new Loc(i, i));
		}
	}
	
	public World () {
		map = new Map ();
	}
	
	public bool isInBounds (Loc l) {
		return map.isInBounds (l);
	}

	/// <summary>
	/// Get unit in given location, or return null if no unit found/location is not in bounds.
	/// </summary>
	/// <returns>The unit in given location.</returns>
	/// <param name="l">Location to consider.</param>
	public Unit getUnit (Loc l) {
		foreach (Unit unit in units) {
			if(unit.getLoc().Equals (l))
				return unit;
		}

		return null;
	}

	/// <summary>
	/// Check if given location is available for unit to move to.
	/// Must both be empty and on map.
	/// </summary>
	public bool isAvailable (Loc l) {
		return isInBounds (l) && getUnit (l) == null;
	}

	/// <summary>
	/// For given location, return vector which is that locations position atop ground.
	/// Effectively taking location, which is in xz plane, and projecting it such that it is atop ground.
	/// </summary>
	public Vector3 onGround (Loc l) {
		return l.asVect () + new Vector3(0, map.getHeight(l), 0);
	}
}
