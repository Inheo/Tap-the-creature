using UnityEngine;

public class VolumeState : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(SoundType.Sound.ToString(), 1);
    }
}
public enum SoundType
{
    Sound,
    Effect
}