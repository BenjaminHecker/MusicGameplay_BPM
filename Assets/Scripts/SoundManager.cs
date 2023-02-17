using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    struct Sound
    {
        public string name;
        public AudioClip clip;
    }

    [SerializeField] private List<Sound> sounds = new List<Sound>();


}
