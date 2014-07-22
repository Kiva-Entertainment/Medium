using UnityEngine;
using System;

/// <summary>
/// Position of a thing, 2 dimensional.
/// Does not have y value.
/// </summary>
public class Loc : IEquatable<Loc> {
	public int x;
	public int z;
	
	public Loc () {
		x = z = 0;
	}
	
	public Loc (int x, int z) {
		this.x = x;
		this.z = z;
	}

	// Precondition: theta is multiple of 90 degrees
	// Make a location with given x and z, rotated by theta degrees
	public Loc (int x, int z, int theta) {
		Vector3 v = Quaternion.Euler (0, theta, 0) * new Loc (x, z).asVect ();

		// Round and convert
		this.x = (int) Math.Round(v.x, 0);
		this.z = (int) Math.Round(v.z, 0);
	}

	/// <summary>
	/// Return this location as a vector, with 0 as its y value.
	/// </summary>
	/// <returns>The resulting vector.</returns>
	public Vector3 asVect () {
		return new Vector3 (x, 0, z);
	}

	/// <summary>
	/// Sum 2 locations and return result.
	/// </summary>
	/// <param name="l">Location to add to given location.</param>
	public Loc plus (Loc l) {
		return new Loc (x + l.x, z + l.z);
	}

	public bool Equals (Loc l2) {
		return x == l2.x && z == l2.z;
	}

	/// <summary>
	/// The "origin" location that has x and z equal to 0.
	/// </summary>
	/// <value>The zero location.</value>
	public static Loc zero
	{
		get { return new Loc (0, 0); }
	}

	public static Loc[] cardinals
	{
		get { return new Loc[4] {new Loc(1,0), new Loc(0,1), new Loc(-1,0), new Loc(0,-1)}; }
	}
}
