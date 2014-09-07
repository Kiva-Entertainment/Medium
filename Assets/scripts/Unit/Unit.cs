using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Anything which does or can occupy a space on a map.
/// Such as: A barrel, a wizard, a bear.
/// </summary>
public class Unit {
	/// <summary>
	/// A list of all units currently deployed.
	/// Used internally to find units in given locations.
	/// </summary>
	private static List<Unit> activeUnits = new List<Unit> ();

	public string name { get; private set; }
	public string type { get; private set; }

	public int hpMax;
	public int hpCur;
	public int spMax;
	public int spCur;

	public int actMax;
	public int actCur;
	public int mvMax;
	public int mvCur;
	public float jump;

	public int strength { get; private set; }
	public int toughness { get; private set; }
	public int intelligence { get; private set; }
	public int willpower { get; private set; }
	public float regen { get; private set; }

	public LinkedList<Skill> skills { get; private set; }

	/// <summary>
	/// Whether or not this unit is deployed, or the stats reference a theoretical unit.
	/// </summary>
	public bool deployed { get; private set; }
	/// <summary>
	/// The game object which represents this unit, is none if not deployed.
	/// </summary>
	GameObject self;
	/// <summary>
	/// If deployed, the current location of this unit.
	/// </summary>
	public Loc loc { get; private set; }
	/// <summary>
	/// The id of the team which unit is on currently.
	/// </summary>
	public int team { get; private set; }

	public Unit (string name = "Bob",
				string type = "Golem",
				int hpMax = 100,
				int hpCur = 100,
				int spMax = 100,
				int spCur = 0,
				int actMax = 1,
				int actCur = 1,
				int mvMax = 5,
				int mvCur = 5,
				float jump = 0.5f,
				int strength = 100,
				int toughness = 100,
				int intelligence = 100,
				int willpower = 100,
				float regen = 0.1f,
				int team = 0
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
		this.jump = jump;
		this.strength = strength;
		this.toughness = toughness;
		this.intelligence = intelligence;
		this.willpower = willpower;
		this.regen = regen;

		this.team = team;
		deployed = false;

		skills = new LinkedList<Skill> ();
	}

	/// <summary>
	/// Deploy this unit to a given location on the map.
	/// </summary>
	/// <param name="loc">The location that the unit is being deployed to.</param>
	/// <param name="ready">If the unit is ready to act upon being deployed.</param>
	public void deploy (Loc loc, bool ready = false) {
		if ( deployed )
			throw new System.Exception("Unit you attempted to deploy is already deployed");

		// Make an object of unit's type at origin, then move it to current loc
		self = Object.Instantiate (Resources.Load (type),
									Vector3.zero,
									Quaternion.identity) as GameObject;
		self.transform.Rotate (Vector3.up, 180 * team);
		move (loc);
		deployed = true;

		if (!ready)
			actCur = mvCur = 0;

		activeUnits.Add (this);
	}

	/// <summary>
	/// Move unit to given space.
	/// Precondition: Space must be valid.
	/// Space should be on the ground for current map, so y must be raised from xz plane.
	/// </summary>
	public void move (Loc loc, int mvConsumed = 0) {
		// Change the stored and real location of unit
		this.loc = loc;
		self.transform.position = World.current.onGround (loc);

		// Consume movement
		mvCur -= mvConsumed;
	}

	/// <summary>
	/// Refresh this unit, called when unit's turn is over.
	/// </summary>
	public void refresh () {
		actCur = actMax;
		mvCur = mvMax;

		spCur += Mathf.RoundToInt ( spMax * regen );
		if (spCur > spMax)
			spCur = spMax;
	}

	// Precondition - amount is at least zero
	public void takeDamage (int amount) {
		hpCur -= amount;

		if (hpCur <= 0) {
			die ();
			return;
		}

		// Play effects
		playAnim ("hit");
	}

	/// <summary>
	/// Unit dies.
	/// </summary>
	void die ()
	{
		// If unit has death animation, wait for it to end
		float waitLength = 0.0f;
		if (playAnim ("death"))
			waitLength = 1.400f;
		GameObject.Destroy (self, waitLength);

		deployed = false;
		loc = null;

		activeUnits.Remove (this);
	}

	public void cycleSkillsDown () {
		// Add last at beginning, then remove last
		skills.AddFirst (skills.Last.Value);
		skills.RemoveLast ();
	}

	public void cycleSkillsUp () {
		// Add first to end, then remove first
		skills.AddLast (skills.First.Value);
		skills.RemoveFirst ();
	}

	/// <summary>
	/// Play animations with given name once.
	/// If unit does not have animation, play nothing and return false.
	/// Precondition: Self must have Animation component.
	/// </summary>
	/// <param name="anim">Name of animation to play once.</param>
	public bool playAnim (string animName) {
		Animation anim = self.GetComponent<Animation> ();

		if (anim[animName]) {
			anim[animName].wrapMode = WrapMode.Once;
			anim.Play (animName);

			anim.PlayQueued ("idle");

			return true;
		} else
			return false;
	}

	public void addSkill (Skill skill) {
		skills.AddLast (skill);
	}

	/// <summary>
	/// Get the unit in given location, or return null if none found.
	/// Location can be outside of bounds, but null will be returned.
	/// </summary>
	/// <returns>The unit in given location.</returns>
	/// <param name="l">Location to consider.</param>
	public static Unit get(Loc loc) {
		foreach (Unit unit in activeUnits)
			if (unit.deployed)
				if (unit.loc.Equals (loc))
					return unit;
		
		return null;
	}

	/// <summary>
	/// Refresh all units on given team.
	/// </summary>
	/// <param name="team">ID of team to refresh.</param>
	public static void refreshTeam(int team) {
		foreach (Unit unit in activeUnits) {
			if (unit.team == team) {
				unit.refresh ();
			}
		}
	}
}
