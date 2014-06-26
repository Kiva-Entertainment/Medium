using UnityEngine;
using System.Collections;

public class CursorSetup : MonoBehaviour {

	void Awake () {
		transform.position = Vector3.zero;
		CursorState.loc = new Loc (0, 0);
	}
}
