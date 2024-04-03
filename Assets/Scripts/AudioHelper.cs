using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume = 1.0f){
        GameObject audioParent = new GameObject("2DAudio");
        AudioSource audioSource = audioParent.AddComponent<AudioSource>();
        //AudioSource audioSource = Instantiate(audioSource);

        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();
        Object.Destroy(audioParent, clip.length);
        Debug.Log("Destroying Audio Source");
        return audioSource;
    }
}
