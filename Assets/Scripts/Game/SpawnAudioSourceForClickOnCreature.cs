using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAudioSourceForClickOnCreature : MonoBehaviour
{
    public AudioSource[] AudioSources = new AudioSource[10];
    int indexClickClip = 0;
    public void SpawnAudioSource()
    {
        GameObject audioSource = new GameObject();
        audioSource.AddComponent<AudioSource>();
        for (int i = 0; i < AudioSources.Length; i++)
        {
            AudioSources[i] = Instantiate(audioSource, transform).GetComponent<AudioSource>();
            AudioSources[i].volume = 0.5f;
        }
    }

    public void StartClickClip(AudioClip audioClip)
    {
        AudioSources[indexClickClip].clip = audioClip;
        AudioSources[indexClickClip].Play();
        indexClickClip++;
        if(indexClickClip >= AudioSources.Length)
        {
            indexClickClip = 0;
        }
    }
}
