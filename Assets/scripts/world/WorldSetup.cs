using UnityEngine;
using System.Collections;

public class World {
	public static World current;
	public Map map;
	
	public World () {
		map = new Map ();
	}

	public bool isInBounds (Loc l) {
		return map.isInBounds (l);
	}
}

public class WorldSetup : MonoBehaviour {
	void Awake () {
		World.current = new World ();
	}
}