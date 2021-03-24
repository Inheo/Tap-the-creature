using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    private AudioSource _click;
    void Start()
    {
        _click = GameObject.FindGameObjectWithTag("ClickEffect").GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        if (PlayerPrefs.GetInt(SoundType.Sound.ToString(), 1) == 1)
        {
            _click.Play();
        }
    }
}
