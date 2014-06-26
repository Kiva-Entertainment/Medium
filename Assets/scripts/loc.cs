using UnityEngine;
using System;

/// <summary>
/// Position of a thing, 2 dimensional
/// Does not have y value
/// </summary>
public class Loc {
	public int x { get; set; }
	public int z { get; set; }

	public Vector3 asVect () {
		return new Vector3 (x, 0, z);
	}
	
	public Loc () {
		x = z = 0;
	}
	
	public Loc (int x, int z) {
		this.x = x;
		this.z = z;
	}

	// Precondition: theta is multiple of 90 degrees
	public Loc (int x, int z, int theta) {
		Vector3 v = Quaternion.Euler (0, theta, 0) * new Loc (x, z).asVect ();

		// Round and convert
		this.x = (int)Math.Round(v.x, 0);
		this.z = (int)Math.Round(v.z, 0);
	}

	public Loc plus (Loc l) {
		return new Loc (x + l.x, z + l.z);
	}
}
