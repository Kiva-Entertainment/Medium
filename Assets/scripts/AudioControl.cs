using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Sound {positive,
	negative}

/// <summary>
/// Controls all sourceless audio played in game.
/// </summary>
public class AudioControl : MonoBehaviour {
	public static AudioControl current;

	public AudioClip positive;
	public AudioClip negative;

	/// <summary>
	/// A dictionary mapping each Sound enum value to a sound set above.
	/// </summary>
	private Dictionary<Sound, AudioClip> dict;
	
	void Start () {
		current = this;
		// NOTE dictionary cannot be set up above because clips aren't static
		dict = new Dictionary<Sound, AudioClip>()
		{
			{Sound.positive, positive},
			{Sound.negative, negative}
		};
	}

	public void play (Sound sound) {
		AudioSource.PlayClipAtPoint( dict[sound], Cam.main.transform.position);
	}
}
