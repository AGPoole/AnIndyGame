using UnityEngine;
using System.Collections;

public class SoundEffects : MonoBehaviour {
    AudioSource s;

    static SoundEffects soundEffects;
    public AudioSource death;
    public AudioSource collect;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (soundEffects == null)
            soundEffects = this;
        else
            Destroy(this.gameObject);
    }

    public static void playDeath()
    {
        if (soundEffects != null)
            soundEffects.death.Play();
    }

    public static void playCollect()
    {
        if(soundEffects != null)
            soundEffects.collect.Play();
    }
}
