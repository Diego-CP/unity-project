using UnityEngine.Audio;
using UnityEngine;

// make the class serializable so it can be edited in inspector
[System.Serializable]
public class Sound {
    public AudioClip clip;
    public string name;

    // Set a range for the allowed volume
    [Range(0.0f, 1.0f)]
    public float volume;

    [Range(0.1f, 3.0f)]
    // Set a range for the allowed pitch
    public float pitch;

    // Even if variable is public, it cannot be edited in inspector
    [HideInInspector]
    public AudioSource source;
}
