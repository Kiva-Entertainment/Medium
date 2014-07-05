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
		UnitStats[] statsAry = new UnitStats[2];
		for (int i = 0; i < statsAry.Length; i++)
		{
			statsAry[i] = new UnitStats("Bob " + i);

			// Deploy unit
			statsAry[i].deploy(new Loc(i, i));
		}
	}
	
	public World () {
		map = new Map ();
	}
	
	public bool isInBounds (Loc l) {
		return map.isInBounds (l);
	}
}
