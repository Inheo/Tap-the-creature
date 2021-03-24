using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Helper
{
    public static void Buy(this Button button, int index, int price, Action<int, int> Buy)
    {
        button.onClick.AddListener(delegate () {
            Buy(index, price);
        });
    }
    public static void ChangePlanet(this Button button, int index, Animator animator, Action<int, Animator> ChangePlanet)
    {
        button.onClick.AddListener(delegate () {
            ChangePlanet(index, animator);
        });
    }
    public static void Click(this Button button, Action Click)
    {
        button.onClick.AddListener(delegate () {
            Click();
        });
    }

    public static int IndexOf(this string[] array, string item)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (item.Equals(array[i]))
            {
                return i;
            }
        }
        return 0;
    }
    public static bool Contains(this string[] array, string item)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (item.Equals(array[i]))
            {
                return true;
            }
        }
        return false;
    }

    public static float Width(this Camera camera)
    {
        return camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, 0)).x - camera.transform.position.x;
    }
    public static float Height(this Camera camera)
    {
        return camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, 0)).y - camera.transform.position.y;
    }
}
