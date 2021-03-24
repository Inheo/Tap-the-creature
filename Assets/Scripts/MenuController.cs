using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private static string[] namesOfAvailablePlanet = { "Fantus", "Tarki", "Termen", "Sarikum" };
    public static string[] NamesOfAvailablePlanet { get { return namesOfAvailablePlanet; } }

    public Button[] allButtons;

    public Click click;

    public BlackScreen blackScreen;

    public AllParametrs allParametrs = new AllParametrs();


    private void Awake()
    {
        click = GameObject.FindGameObjectWithTag("ClickEffect").GetComponent<Click>();
        LoadJson();
        allParametrs.SaveAllParametrs(allParametrs);
    }

    private void Start()
    {
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].Click(click.OnClick);
        }
        if (TransferringDataBetweenScenes.startOpenedBlackScreenInGameScene)
        {
            blackScreen.StartManifestation();
        }
    }

    private void Update()
    {
        //if(blackScreen.transform.localScale == Vector3.zero)
        //{
        //    OpenScene("Game");
        //}
    }
    private void LoadData(byte[] data)
    {
        string saveData = Encoding.UTF8.GetString(data, 0, data.Length);
        PlayerPrefs.SetString(AllParametrs.KEY, saveData);
    }

    private void LoadJson()
    {

        AllParametrs paramOpenHero = new AllParametrs();

        if (!PlayerPrefs.HasKey(AllParametrs.KEY))
        {
            string json = JsonUtility.ToJson(allParametrs);
            PlayerPrefs.SetString(AllParametrs.KEY, json);
        }
        else
        {
            allParametrs = JsonUtility.FromJson<AllParametrs>(PlayerPrefs.GetString(AllParametrs.KEY));
        }
    }


    public void OpenScene(string name)
    {
        TransferringDataBetweenScenes.startOpenedBlackScreenInGameScene = false;
        SceneManager.LoadScene(name);
        allParametrs.SaveAllParametrs(allParametrs);
    }

    public void ChangePlanet(int i, Animator animatorPlanet)
    {
        allParametrs.selectedPlanet = i;
        animatorPlanet.SetBool("selected", true);
        blackScreen.startBlackout = true;
        Invoke(nameof(OpenSceneGame), blackScreen.startScale.x * 2 / blackScreen.speedBlackout);
    }
    public void OpenSceneGame()
    {
        OpenScene("Game");
    }
    private void OnApplicationQuit()
    {
        allParametrs.SaveAllParametrs(allParametrs);
    }
}
