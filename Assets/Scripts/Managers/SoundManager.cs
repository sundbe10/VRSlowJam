using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

	AudioSource audioSource;

	[System.Serializable]
	public class Sound{
		public string identifier;
		public AudioClip audioClip;
		public float volume = 1;
	}

	public Sound[] sounds;

	List<Sound> soundsList;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		soundsList = sounds.ToList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySound(string identifier){
		Sound soundClip = soundsList.Where(s => s.identifier == identifier).FirstOrDefault();
		if(soundClip == null){
			Debug.LogError("Could not play sound "+ identifier);
			return;
		}
		audioSource.PlayOneShot(soundClip.audioClip, soundClip.volume);
	}
}
