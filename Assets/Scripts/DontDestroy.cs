using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public enum SoundType
    {
        Sound,
        ClickEffect
    }

    public SoundType Sound = SoundType.Sound;
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag(Sound.ToString()).Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
}
