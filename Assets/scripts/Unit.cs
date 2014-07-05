using UnityEngine;
using System.Collections;

/// <summary>
/// A unit is anything which occupies a space on the map.
/// Soldiers, barrels, etc. are all units.
/// </summary>
public class Unit : MonoBehaviour {
	public UnitStats stats;
}

/// <summary>
/// The stats for a unit.
/// All information about the unit besides physical presence.
/// In other words, everything that can be known even if unit is not deployed.
/// </summary>
public class UnitStats {
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

	public UnitStats (string name = "Bob",
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

	public void deploy (Loc loc) {

		GameObject ob = Object.Instantiate (Resources.Load ("Soldier"),
											loc.asVect(),
											Quaternion.identity) as GameObject;
		return;
//		//spawn object
//		GameObject ob = new GameObject("Cool GameObject made from Code");
//		//Add Components
//		// Add a mesh filter to ob
//		MeshFilter m = ob.AddComponent<MeshFilter> ();
//		// Get the mesh that we want, and switch ob's mesh to desired mesh
//		GameObject tempOb = Resources.Load ("Soldier") as GameObject;
//		m.mesh = tempOb.GetComponent<MeshFilter> ().mesh;
//		// Add a renderer to ob
//		ob.AddComponent<MeshRenderer> ();
	}
}
