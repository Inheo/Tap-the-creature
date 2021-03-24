using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AllParametrs
{
    public const string KEY = "allParametrs";

    public int level = 1,
        coins = 0;
    public int selectedPlanet = 0;
    public int[] CreatureActive = { 0, 0, 0, 0, 0 };
    public int[] ImageProgress = { 0, 0, 0, 0, 0 };
    public int[] ProgressBars = { 0, 0, 0, 0, 0 };

    public void SaveAllParametrs(AllParametrs allParametrs)
    {
        string json = JsonUtility.ToJson(allParametrs);
        PlayerPrefs.SetString(KEY, json);
        //GPGSManager.WriteSaveData(Encoding.UTF8.GetBytes(json));
    }

    public static AllParametrs LoadAllParametrs()
    {
        return JsonUtility.FromJson<AllParametrs>(PlayerPrefs.GetString(KEY, DefaultParametrValue()));
    }

    private static string DefaultParametrValue()
    {
        string json = JsonUtility.ToJson(new AllParametrs());
        PlayerPrefs.SetString(KEY, json);
        return PlayerPrefs.GetString(KEY);
    }
}
