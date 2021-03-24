using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInformation : MonoBehaviour
{
    public bool isSale = true;
    [SerializeField]
    private Text _textName;
    [SerializeField]
    private Text _textProgress;
    [SerializeField]
    private Image _imageHero;
    //[SerializeField]
    //private Button _buttonBuy;
    [SerializeField]
    private Button _buttonSelected;

    public Text TextName { get => _textName; }
    public Text TextProgress { get => _textProgress; }
    public Image ImageHero { get => _imageHero; }
    //public Button ButtonBuy { get => _buttonBuy; }
    public Button ButtonSelected { get => _buttonSelected; }
}
