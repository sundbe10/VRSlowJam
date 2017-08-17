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
		public AudioClip[] audioClips;
		public float volume = 1;
	}

	public Sound[] sounds;

	List<Sound> soundsList;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource>();
		soundsList = sounds.ToList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySound(string identifier){
		Debug.Log(soundsList[6]);
		Sound soundClip = soundsList.Where(s => s.identifier == identifier).FirstOrDefault();
		if(soundClip == null || soundClip.audioClips.Length == 0){
			Debug.LogError("Could not play sound "+ identifier);
			return;
		}
		audioSource.PlayOneShot(soundClip.audioClips[Random.Range(0,soundClip.audioClips.Length)], soundClip.volume);
	}
}
