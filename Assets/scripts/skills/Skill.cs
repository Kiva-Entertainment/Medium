using System.Collections.Generic;

/// <summary>
/// A skill is any skill performed by a unit.
/// It contains data such as - Who performed it, Where they targetted, What choices they made
/// </summary>
public interface Skill {
	/// <summary>
	/// The cost, in sp, of performing this skill.
	/// </summary>
	int getCost ();
	/// <summary>
	/// The name of this skill.
	/// </summary>
	string getName ();
	/// <summary>
	/// How many targets this skill demands.
	/// A target is a location of the current map.
	/// </summary>
	//int getNumTargets ();
	/// <summary>
	/// How far from actor this skill can target.
	/// </summary>
	//int getRange ();

	/// <summary>
	/// Get a list of all locations that skill could be performed on.
	/// </summary>
	/// <returns>List of locations skill can target.</returns>
	/// <param name="onlyValid">
	/// If set to <c>true</c> only valid spaces are returned.
	/// For example, punch can hit adjacent space, but space is not valid if it is empty.
	/// </param>
	Loc[] getRange (bool onlyValid = false);

	// TODO add extent

	Skill setActor (Unit u);

	/// <summary>
	/// Perform the given skill with targets at given locations.
	/// Precondition: locs has correct length (Equal to numTargets).
	/// </summary>
	/// <param name="locs">A list of the targets, ordered.</param>
	void perform (params Loc[] locs);
}
