using UnityEngine;
using System.Collections;

/// <summary>
/// The world in which the current battle is taking place.
/// Static world "current" has lots of information and utitlity necessary for scripts throughout project.
/// </summary>
public class World : MonoBehaviour {
	/// <summary>
	/// Singleton world which describes the state of things at the moment.
	/// </summary>
	public static World current;
	private Map map;
	public Unit[] units { get; private set; }

	void Awake () {
		map = new Map ();
		current = this;

		// TODO(kgeffen) Unit data should be imported from json or something
		units = new Unit[4];
		for (int i = 0; i < units.Length; i++)
		{
			units[i] = Generator.getGolem(i%2);

			// Deploy unit
			units[i].deploy(new Loc(i, i));
		}
	}

	/// <summary>
	/// Check if given location is within the bounds of the current map.
	/// </summary>
	/// <returns><c>true</c>, if location is in bounds, <c>false</c> otherwise.</returns>
	/// <param name="l">Location to check.</param>
	public bool isInBounds (Loc l) {
		return map.isInBounds (l);
	}

	/// <summary>
	/// Get the height of a given space on the current map.
	/// </summary>
	/// <returns>The height of given location.</returns>
	public float getHeight (Loc loc) { return map.getHeight (loc); }
	/// <summary>
	/// Get the height of a given space on the current map.
	/// </summary>
	/// <returns>The height of given location.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public float getHeight (int x, int z) { return getHeight (new Loc (x, z)); }

	/// <summary>
	/// Get the unit in given location, or return null if no unit found/location is not in bounds.
	/// </summary>
	/// <returns>The unit in given location.</returns>
	/// <param name="l">Location to consider.</param>
	public Unit getUnit (Loc l) {

		foreach (Unit unit in units)
			if (unit.deployed)
				if (unit.loc.Equals (l))
					return unit;

		return null;
	}

	/// <summary>
	/// For given location, return vector which is that locations position atop ground.
	/// Effectively taking location, which is in xz plane, and projecting it such that it is atop ground.
	/// </summary>
	public Vector3 onGround (Loc l) {
		return l.asVect () + new Vector3(0, map.getHeight(l), 0);
	}

	/// <summary>
	/// Check if given location on current map is a hole.
	/// </summary>
	/// <returns><c>true</c>, if location is a hole, <c>false</c> otherwise.</returns>
	/// <param name="l">Location to check for hole status.</param>
	public bool isHole (Loc l) { return map.isHole (l); }
}
