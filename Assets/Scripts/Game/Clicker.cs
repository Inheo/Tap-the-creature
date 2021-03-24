using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    [SerializeField] Text textCountCreature;
    [SerializeField] GameManager gameManager;
    /// <summary>
    /// Скорость перехода к другому существу
    /// </summary>
    public int speedMoveNextCreature = 2;
    /// <summary>
    /// цвет который красит куржки прогресса после развития существа
    /// </summary>
    public Color ColorProgressUpdate;
    /// <summary>
    /// количество которое нужно накликать
    /// </summary>
    public int CountClickForDevelopment;
    /// <summary>
    /// Число на которое умножается количество кликов при развитии существа
    /// </summary>
    public float CoefficientCountClickForDevelopment = 1;
    public GameObject parentCreature;

    /// <summary>
    /// количество кликов
    /// </summary>
    private int _countClick;
    /// <summary>
    /// стадия развития существа
    /// </summary>
    private int stageOfDevelopment = 0;
    /// <summary>
    /// Номер кликаемого существа
    /// </summary>
    private int creatureCount = 0;

    /// <summary>
    /// в начале блокирует анимацию
    /// </summary>
    private bool blockStartAnimation = true,
                    endGame = false;

    private Creature creature;

    private Camera cameraMain;
    private MoveCameraNextCreature moveCamera;
    private CameraMotionControl cameraMotionControl;

    public Image[] ImageProgress;
    public Image imageProgressBar;

    public ParticleSystem particleSystemClick;
    public ParticleSystem particleSystemDevelopment;
    public AudioSource audioSourceDevelopment;
    public AllParametrs allParametrs;

    public Creature Creature { get => creature;}

    void Awake()
    {
        particleSystemDevelopment.gameObject.SetActive(false);
        for (int i = 0; i < ImageProgress.Length; i++)
        {
            ImageProgress[i].color = Color.white;
        }
    }

    private void Start()
    {
        allParametrs = JsonUtility.FromJson<AllParametrs>(PlayerPrefs.GetString(AllParametrs.KEY));

        endGame = false;

        blockStartAnimation = true;
        creatureCount = allParametrs.CreatureActive[allParametrs.selectedPlanet];
        UpdateUI();
        _countClick = allParametrs.ProgressBars[allParametrs.selectedPlanet];

        creature = parentCreature.transform.GetChild(creatureCount).GetComponentInChildren<Creature>();

        SelectNewCreature();

        stageOfDevelopment = allParametrs.ImageProgress[allParametrs.selectedPlanet];
        int length = (stageOfDevelopment) + creatureCount * 5;
        //for (int i = 0; i < length; i++)
        //{
        //    CountClickForDevelopment = (int)(CountClickForDevelopment * CoefficientCountClickForDevelopment);
        //}

        cameraMain = Camera.main;
        moveCamera = cameraMain.GetComponent<MoveCameraNextCreature>();
        cameraMotionControl = cameraMain.GetComponent<CameraMotionControl>();
        FillingProgressBar();
        FillingImageProgrees(stageOfDevelopment);
        ShowBodyPartsCreature(creatureCount);
    }

    private void Update()
    {
        if (cameraMain.transform.position.x != creature.transform.position.x && !endGame)
        {
            MoveToNextCreature();
        }
    }
    public void Click()
    {
        _countClick++;
        FillingProgressBar();
        CheckDevelopment();
        UpdateUI();
        SpawnParicleSystemClick();
        allParametrs.SaveAllParametrs(allParametrs);
    }

    public void UpdateUI()
    {
        textCountCreature.text = $"{(creatureCount + 1)}/{parentCreature.transform.childCount}";
    }
    private void CheckDevelopment()
    {
        if (_countClick >= CountClickForDevelopment)
        {
            DevelopmentBody();
        }
    }

    private void DevelopmentBody()
    {
        blockStartAnimation = false;
        ShowParticleSystemDevelopment();
        FillingImageProgrees();
        creature.ShowBodyPart(stageOfDevelopment);
        stageOfDevelopment++;
        allParametrs.ImageProgress[allParametrs.selectedPlanet] = stageOfDevelopment;
        _countClick = 0;
        allParametrs.ProgressBars[allParametrs.selectedPlanet] = _countClick;
        if (stageOfDevelopment >= creature.CountBodyParts)
        {
            creature.Click -= Click;
            creature.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            // старт анимации закрытия банки
            creature.BankClose();
            if (creatureCount < parentCreature.transform.childCount - 1)
            {
                creatureCount++;
                allParametrs.CreatureActive[allParametrs.selectedPlanet] = creatureCount;
                stageOfDevelopment = 0;
                allParametrs.ImageProgress[allParametrs.selectedPlanet] = stageOfDevelopment;
                //SelectNewCreature(creatureCount);
            }
            else if(creatureCount == parentCreature.transform.childCount - 1 && stageOfDevelopment == 5)
            {
                Invoke(nameof(PassedLevel), 3);
            }
            return;
        }
        CountClickForDevelopment = (int)(CountClickForDevelopment * CoefficientCountClickForDevelopment);
    }

    private void SelectNewCreature()
    {
        if (creatureCount < parentCreature.transform.childCount && !endGame && stageOfDevelopment < ImageProgress.Length)
        {
            if (creatureCount > 0 && !blockStartAnimation)
                StartCoroutine(HideImageProgress(((creature.offsetX / speedMoveNextCreature) / ImageProgress.Length)));
            allParametrs.CreatureActive[allParametrs.selectedPlanet] = creatureCount;
            creature = parentCreature.transform.GetChild(creatureCount).GetComponentInChildren<Creature>();
            creature._BankaForCreature.eventSelectNextCreature += SelectNewCreature;
            creature.Click += Click;
        }
    }

    private void FillingProgressBar()
    {
        allParametrs.ProgressBars[allParametrs.selectedPlanet] = _countClick;
        imageProgressBar.fillAmount = (float)_countClick / (float)CountClickForDevelopment;
    }
    private void FillingImageProgrees()
    {
        allParametrs.ImageProgress[allParametrs.selectedPlanet] = stageOfDevelopment + 1;
        ImageProgress[stageOfDevelopment].color = ColorProgressUpdate;
    }
    private void SpawnParicleSystemClick()
    {
        Vector3 pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        particleSystemClick.startColor = creature.colorForClickParticleSystem;
        Instantiate(particleSystemClick, pos, Quaternion.identity);
    }

    private void ShowParticleSystemDevelopment()
    {
        particleSystemDevelopment.transform.position = creature.transform.position;
        particleSystemDevelopment.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt(SoundType.Sound.ToString(), 1) != 0)
        {
            audioSourceDevelopment.Play();
        }
    }

    private void MoveToNextCreature()
    {
        moveCamera.MoveToCreature(creature.transform, speedMoveNextCreature);
        if (!blockStartAnimation)
        {
            float coeffcient = (creatureCount - 1) * 2;
            imageProgressBar.fillAmount = 1 - (cameraMain.transform.position.x - cameraMain.Width() * coeffcient) / (creature.transform.position.x - cameraMain.Width() * coeffcient);
        }
    }

    private IEnumerator HideImageProgress(float time)
    {
        for (int i = ImageProgress.Length - 1; i > -1; i--)
        {
            yield return new WaitForSeconds(time);
            ImageProgress[i].color = Color.white;
        }
    }

    private void FillingImageProgrees(int length)
    {
        for (int i = 0; i < length; i++)
        {
            ImageProgress[i].color = ColorProgressUpdate;
        }
    }

    private void ShowBodyPartsCreature(int length)
    {
        for (int i = 0; i < length; i++)
        {
            Creature creatureStart = parentCreature.transform.GetChild(i).GetComponent<Creature>();
            for (int j = 0; j < 5; j++)
            {
                creatureStart.ShowBodyPart(j);
                creatureStart._BankaForCreature.InstantStartPositioning();
                creatureStart.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        for (int i = 0; i < stageOfDevelopment; i++)
        {
            parentCreature.transform.GetChild(length).GetComponent<Creature>().ShowBodyPart(i);
        }
        if(creatureCount >= 4 && stageOfDevelopment >= 5)
        {
            endGame = true;
            creature._BankaForCreature.InstantStartPositioning();
            imageProgressBar.fillAmount = 1;
            creature.Click -= Click;
            creature.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            cameraMotionControl.ResolutionInMotion = true;
        }
    }
    private void PassedLevel()
    {
        TransferringDataBetweenScenes.startOpenedBlackScreenInGameScene = true;
        endGame = true;
        gameManager.GameEnd();
    }
}