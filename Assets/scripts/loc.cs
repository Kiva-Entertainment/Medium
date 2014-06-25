using UnityEngine;
using System;

/// <summary>
/// Position of a thing, 2 dimensional
/// Does not have y value
/// </summary>
public class loc {
	public int x { get; set; }
	public int z { get; set; }

	public Vector3 asVect () {
		return new Vector3 (x, 0, z);
	}
	
	public loc () {
		x = z = 0;
	}
	
	public loc (int x, int z) {
		this.x = x;
		this.z = z;
	}

	// Precondition: theta is multiple of 90 degrees
	public loc (int x, int z, int theta) {
		Vector3 v = Quaternion.Euler (0, theta, 0) * new loc (x, z).asVect ();

		// Round and convert
		this.x = (int)Math.Round(v.x, 0);
		this.z = (int)Math.Round(v.z, 0);
	}

	public loc plus (loc l) {
		return new loc (x + l.x, z + l.z);
	}
}
