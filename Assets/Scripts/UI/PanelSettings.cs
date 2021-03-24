using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelSettings : MonoBehaviour
{
    private Animation animation;
    public Image imageSoundButton;
    public Clicker Clicker;

    private bool enabledActiveCreatureCollider = true;
    private void Awake()
    {
        animation = GetComponent<Animation>();
        transform.localScale = Vector3.zero;
        if (PlayerPrefs.GetInt(SoundType.Sound.ToString(), 1) == 1)
            ChangeColor(imageSoundButton, new Color(1, 1, 1, 1));
        else
            ChangeColor(imageSoundButton, new Color(1, 1, 1, 0.75f));
    }
    public void ShowOrHidePanelSettings()
    {
        if(transform.localScale.x == 1)
        {
            animation.Play("Hide");
            enabledActiveCreatureCollider = true;
        }
        else
        {
            animation.Play("Show");
            enabledActiveCreatureCollider = false;
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            OnOrOffActiveCreature();
        }
    }
    public void ToMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnOrOffSound()
    {
        if(PlayerPrefs.GetInt(SoundType.Sound.ToString(), 1) == 1)
        {
            PlayerPrefs.SetInt(SoundType.Sound.ToString(), 0);
            ChangeColor(imageSoundButton, new Color(1, 1, 1, 0.75f));
        }
        else
        {
            PlayerPrefs.SetInt(SoundType.Sound.ToString(), 1);
            ChangeColor(imageSoundButton, new Color(1, 1, 1, 1));
        }
    }

    private void ChangeColor(Image image, Color color)
    {
        image.color = color;
    }

    private void OnOrOffActiveCreature()
    {
        BoxCollider2D boxCollider2D = Clicker.Creature.GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = enabledActiveCreatureCollider;
    }
}
