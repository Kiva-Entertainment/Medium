using UnityEngine;
using System.Collections;

public class cursorMotion : MonoBehaviour {
	private loc curLoc;
	public GameObject ground;
	private ground g;

	void Awake () {
		g = ground.GetComponent<ground> ();
	}

	// Use this for initialization
	void Start () {
		curLoc = new loc ();
	}
	
	// Update is called once per frame
	void Update () {
		handleInput ();
	}

	void handleInput () {
		loc dLoc;

		if (Input.GetButtonDown ("Up"))
			dLoc = new loc (0, 1);
		else if (Input.GetButtonDown ("Down"))
			dLoc = new loc (0, -1);
		else if (Input.GetButtonDown ("Left"))
			dLoc = new loc (-1, 0);
		else if (Input.GetButtonDown ("Right"))
			dLoc = new loc (1, 0);
		else
			dLoc = new loc();

		loc potLoc = curLoc.plus (dLoc);
		if ( g.inBounds ( potLoc ) ) {
			curLoc = potLoc;
			transform.position = new Vector3(curLoc.x, 0, curLoc.z);
		}

	}
}
