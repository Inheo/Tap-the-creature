using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    //private AllParametrs allParametrs = new AllParametrs();
    //public Text coins;
    //public Button[] selectedHero, buyHero;

    //private void Awake()
    //{
    //    coins.text = allParametrs.coins.ToString();
    //}

    //public void InteractableButtons()
    //{
    //    allParametrs = JsonUtility.FromJson<AllParametrs>(PlayerPrefs.GetString("allParametrs"));
    //    for (int i = 0; i < MenuController.NamesOfAvailableHeroes.Length; i++)
    //    {
    //        selectedHero[i].interactable = allParametrs.openHero[i];
    //        buyHero[i].interactable = !allParametrs.openHero[i];
    //    }
    //}
    //public void ChangeHero(int i)
    //{
    //    AllParametrs all = new AllParametrs();
    //    allParametrs.selectedHero = MenuController.NamesOfAvailableHeroes[i];
    //    allParametrs.SaveAllParametrs(allParametrs);
    //}

    //public void Buy(int indexHeroForBuy, int price)
    //{
    //    if (allParametrs.coins >= price)
    //    {
    //        allParametrs.coins -= price;
    //        allParametrs.openHero[indexHeroForBuy] = true;
    //        allParametrs.SaveAllParametrs(allParametrs);
    //        selectedHero[indexHeroForBuy].interactable = true;
    //        buyHero[indexHeroForBuy].interactable = false;
    //        coins.text = allParametrs.coins.ToString();
    //    }
    //}

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}