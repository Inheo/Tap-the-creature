using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BlackScreen blackScreen;
    [SerializeField] Clicker clicker;

    public Click click;
    private void Start()
    {
        blackScreen.StartManifestation();
        click = GameObject.FindGameObjectWithTag("ClickEffect").GetComponent<Click>();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        clicker.allParametrs.SaveAllParametrs(clicker.allParametrs);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        clicker.allParametrs.SaveAllParametrs(clicker.allParametrs);
    }
    public void ClickSoundStart()
    {
        click.OnClick();
    }

    public void GameEnd()
    {
        blackScreen.startBlackout = true;
        Invoke(nameof(Menu), blackScreen.startScale.x * 2 / blackScreen.speedBlackout);
    }

    void OnApplicationQuit()
    {
        clicker.allParametrs.SaveAllParametrs(clicker.allParametrs);
    }

}
