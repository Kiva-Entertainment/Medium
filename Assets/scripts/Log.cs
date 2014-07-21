using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Log : MonoBehaviour {
	public static Stack<Move> moves { get; private set; }
	public static Log current;

	void Awake () {
		moves = new Stack<Move> ();
		current = this;
	}

	void Update () {
		// If undo key was pressed, undo last move made
		if ( !Input.GetButtonDown ("Undo") )
			return;

		// Deselect unit
		CursorSelect.current.close ();

		// If log is empty, return
		Move m = pop ();
		if (m == null)
			return;

		// Perform the opposite of the move which was performed
		m.getOpposite ().perform ();
	}
	
	// TODO actions can also be logged
	public void push (Move m) {
		moves.Push (m);
	}

	Move pop () {
		if (moves.Count == 0)
			return null;
		else
			return moves.Pop ();
	}
}
