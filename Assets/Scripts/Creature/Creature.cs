using System;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public Color colorForClickParticleSystem;
    public Color ColorBackground;
    public AudioClip audioClipForClick;
    /// <summary>
    /// Расстояние между существами
    /// </summary>
    [HideInInspector]
    public float offsetX;

    [SerializeField]
    private GameObject[] bodyParts;
    private Vector2 normalScale;
    private Vector2 clickedScale;

    public BankaForCreature _BankaForCreature;

    public event Action Click;
    public event Action<AudioClip> StartClickClip;
    

    public int CountBodyParts { get => bodyParts.Length;}

    private void Awake()
    {
        bodyParts = new GameObject[transform.childCount - 1];
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i] = transform.GetChild(i + 1).gameObject;
        }
    }
    private void Start()
    {
        normalScale = transform.localScale;
        clickedScale = new Vector2(normalScale.x - 0.05f, normalScale.y - 0.05f);
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].SetActive(false);
        }
        Click += StartClip;
    }
    private void OnMouseDown()
    {
        if(Click != null)
            transform.localScale = clickedScale; 
    }
    private void OnMouseUp()
    {
        transform.localScale = normalScale;
        Click?.Invoke();
    }

    private void StartClip()
    {
        if (PlayerPrefs.GetInt(SoundType.Sound.ToString(), 1) != 0)
        {
            StartClickClip?.Invoke(audioClipForClick);
        }
    }

    public void ShowBodyPart(int i)
    {
        if(i < bodyParts.Length)
        {
            bodyParts[i].SetActive(true);
        }
    }
    public void BankClose()
    {
        _BankaForCreature.moveBank = true;
    }
}
