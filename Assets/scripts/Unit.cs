using UnityEngine;
using System.Collections;

/// <summary>
/// Anything which does or can occupy a space on a map.
/// Such as: A barrel, a wizard, a bear.
/// </summary>
public class Unit {
	string name;
	string type;

	int hpMax;
	int hpCur;
	int spMax;
	int spCur;

	int actMax;
	int actCur;
	int mvMax;
	int mvCur;

	int strength;
	int toughness;
	int intelligence;
	int willpower;

	/// <summary>
	/// Whether or not this unit is deployed, or the stats reference a theoretical unit.
	/// </summary>
	bool deployed = false;
	/// <summary>
	/// The game object which represents this unit, is none if not deployed.
	/// </summary>
	GameObject self;
	/// <summary>
	/// If deployed, the current location of this unit.
	/// </summary>
	Loc loc;

	public Unit (string name = "Bob",
				string type = "Soldier",
				int hpMax = 100,
				int hpCur = 100,
				int spMax = 100,
				int spCur = 0,
				int actMax = 1,
				int actCur = 1,
				int mvMax = 5,
				int mvCur = 5,
				int strength = 100,
				int toughness = 100,
				int intelligence = 100,
				int willpower = 100
	           ) {
		this.name = name;
		this.type = type;
		this.hpMax = hpMax;
		this.hpCur = hpCur;
		this.spMax = spMax;
		this.spCur = spCur;
		this.actMax = actMax;
		this.actCur = actCur;
		this.mvMax = mvMax;
		this.mvCur = mvCur;
		this.strength = strength;
		this.toughness = toughness;
		this.intelligence = intelligence;
		this.willpower = willpower;
	}

	/// <summary>
	/// Returns true if unit is currently deployed to the field.
	/// </summary>
	public bool isDeployed () { return deployed; }

	/// <summary>
	/// Deploy this unit to a given location on the map.
	/// </summary>
	/// <param name="loc">The location that the unit is being deployed to.</param>
	public void deploy (Loc loc) {
		if ( isDeployed () )
			throw new System.Exception("Unit you attempted to deploy is already deployed");

		// Make an object of unit's type at origin, then move it to current loc
		self = Object.Instantiate (Resources.Load (type),
									Vector3.zero,
									Quaternion.identity) as GameObject;
		move (loc);
		deployed = true;
	}

	/// <summary>
	/// Move unit to given space.
	/// Precondition: Space must be valid.
	/// Space should be on the ground for current map, so y must be raised from xz plane.
	/// </summary>
	public void move (Loc loc) {
		this.loc = loc;
		self.transform.position = World.current.onGround (loc);
	}

	public Loc getLoc () { return loc; }
}
