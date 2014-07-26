using UnityEngine;
using System.Collections;

public class Inspector : MonoBehaviour {

	/// <summary>
	/// Whether or not the inspector is open.
	/// </summary>
	bool open = false;

	// Update is called once per frame
	void Update () {
		if ( Input.GetButtonDown ("Inspect") ) {
			if (open)
				gameObject.guiText.text = "";
			else
				inspect ();

			open = !open;
		}
	}

	/// <summary>
	/// Bring up information on the unit cursor is over, if there is one.
	/// </summary>
	void inspect ()
	{
		Unit u = World.current.getUnit (Cursor.current.loc);
		if (u != null) {
			string result = 
					u.name +
					"\nTEAM : " + u.team +
					"\nHP : " + u.hpCur + "/" + u.hpMax +
					"\nSP : " + u.spCur + "/" + u.spMax +
					"\nACT : " + u.actCur + "/" + u.actMax +
					"\nMV : " + u.mvCur + "/" + u.mvMax;

			gameObject.guiText.text = result;
		}
	}
}
