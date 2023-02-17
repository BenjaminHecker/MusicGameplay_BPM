using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [System.Serializable]
    struct Sound
    {
        public Note.Keys key;
        public AudioClip clip;
    }

    [SerializeField] private List<Sound> sounds = new List<Sound>();

    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        source = GetComponent<AudioSource>();
    }

    public static void Play(Note.Keys key)
    {
        foreach (Sound s in instance.sounds)
        {
            if (s.key == key)
            {
                instance.source.clip = s.clip;
                instance.source.Play();
            }
        }
    }
}
