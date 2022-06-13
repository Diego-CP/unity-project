using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    void Awake() {
        // Loop through every sound, adding a gameobject for each one
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name) {
        // Find the element in sounds that has a matching name and set it to s 
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // Verify that the sound exists
        if (s == null) {
            Debug.Log("No such sound with name " + name + " exists");
            return;
        }
        s.source.Play();
    }
}
