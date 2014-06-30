using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	public static World current;
	public Map map;

	void Awake () {
		map = new Map ();
		current = this;
	}
	
	public World () {
		map = new Map ();
	}
	
	public bool isInBounds (Loc l) {
		return map.isInBounds (l);
	}
}
