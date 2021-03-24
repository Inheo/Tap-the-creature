using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddingInformationToSalePanels : MonoBehaviour
{
    [System.Serializable]
    public struct PanelForSale
    {
        public string namePlanet;
        public bool isSale;
        public Sprite spriteHero;
    }

    public int CountSalePanel { get => panelForSales.Length; }

    [SerializeField] PanelForSale[] panelForSales;
    public void AddInformationInSalePanel(int i, GameObject panels, MenuController menuController, AllParametrs allParametrs)
    {
        PlanetInformation planetInformation = panels.GetComponent<PlanetInformation>();

        planetInformation.TextName.text = panelForSales[i].namePlanet;
        int indexCompletedCreature = allParametrs.ImageProgress[i] == 5 ? allParametrs.CreatureActive[i] + 1 : allParametrs.CreatureActive[i];
        planetInformation.TextProgress.text = indexCompletedCreature + "/5";
        planetInformation.ImageHero.sprite = panelForSales[i].spriteHero;

        Animator animatorPlanet = planetInformation.GetComponent<Animator>();

        planetInformation.ButtonSelected.ChangePlanet(i, animatorPlanet, menuController.ChangePlanet);
        planetInformation.ButtonSelected.Click(menuController.click.OnClick);
    }
}
