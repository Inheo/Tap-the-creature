using System;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    public PlanetAnimationController planetAnimationController;
    public GameObject panelForSalePrefab;


    [Range(0, 500)]
    [Header("Расстояние между панельками")]
    public float panelOffset;
    [Range(0, 20f)]
    public float snapSpeed;
    [Range(0, 20f)]
    public float scaleSpeed;
    [Range(0, 50)]
    public float scaleOffset;
    [Range(0, 1f)]
    public float addScale;
    //[Header("Все герои для покупки")]
    public GameObject[] panels;

    public ScrollRect scrollRectScrollView;

    public MenuController menuController;

    private int countSalePanel;
    private bool isScrolling = false;
    private int panCount,
                selectedPanelsID = 0;

    private Vector2 vectorContent;
    private Vector2[] panelsPosition,
                        panelsScale;

    private RectTransform rectTransformContent;

    private bool startScroll = false;

    private AddingInformationToSalePanels addingInformationToSalePanels;

    void Awake()
    {
        addingInformationToSalePanels = GetComponent<AddingInformationToSalePanels>();
        countSalePanel = addingInformationToSalePanels.CountSalePanel;
        rectTransformContent = GetComponent<RectTransform>();
        panels = new GameObject[countSalePanel];
        panelsPosition = new Vector2[countSalePanel];
        panelsScale = new Vector2[countSalePanel];

    }

    private void Start()
    {
        //Создание панелек и просвоение цены, спрайта героя и спрятать кнопку купить тех кого нельзя купить
        for (int i = 0; i < countSalePanel; i++)
        {
            panels[i] = Instantiate(panelForSalePrefab, transform);

            addingInformationToSalePanels.AddInformationInSalePanel(i, panels[i], menuController, menuController.allParametrs);
        }

        for (int i = 0; i < countSalePanel; i++)
        {
            if (i == 0) continue;
            panels[i].transform.localPosition = new Vector2(panels[i - 1].transform.localPosition.x + panels[i].GetComponent<RectTransform>().sizeDelta.x - panelOffset,
                                                            panels[i].transform.localPosition.y);
            panelsPosition[i] = -panels[i].transform.localPosition;
        }
    }

    void FixedUpdate()
    {
        if (isScrolling && !startScroll) startScroll = true;
        if (rectTransformContent.anchoredPosition.x <= panelsPosition[0].x && !isScrolling ||
            rectTransformContent.anchoredPosition.x >= panelsPosition[panelsPosition.Length - 1].x && !isScrolling)
        {
            scrollRectScrollView.inertia = false;
        }
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panels.Length; i++)
        {
            float distance = Mathf.Abs(rectTransformContent.anchoredPosition.x - panelsPosition[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                planetAnimationController.StopAnimation(selectedPanelsID);
                if (!startScroll) selectedPanelsID = menuController.allParametrs.selectedPlanet;
                else
                {
                    selectedPanelsID = i;
                }
                planetAnimationController.StartAnimation(selectedPanelsID);
            }
            float scale = Mathf.Clamp(1 / (distance / panelOffset) * scaleOffset, 0.5f, 1f);
            panelsScale[i].x = Mathf.SmoothStep(scale + addScale, panels[i].transform.localScale.x, scaleSpeed * Time.fixedDeltaTime);
            panelsScale[i].y = Mathf.SmoothStep(scale + addScale, panels[i].transform.localScale.y, scaleSpeed * Time.fixedDeltaTime);
            panels[i].transform.localScale = panelsScale[i];
        }
        float scrollVeloicity = Mathf.Abs(scrollRectScrollView.velocity.y);
        if (scrollVeloicity < 400 && !isScrolling) scrollRectScrollView.inertia = false;
        if (isScrolling || scrollVeloicity > 400) return;
        vectorContent.x = Mathf.SmoothStep(rectTransformContent.anchoredPosition.x, panelsPosition[selectedPanelsID].x, snapSpeed * Time.fixedDeltaTime);
        rectTransformContent.anchoredPosition = vectorContent;
    }


    public void Scrolling(bool isScrolling)
    {
        this.isScrolling = isScrolling;
        if (isScrolling) scrollRectScrollView.inertia = true;
        else menuController.allParametrs.selectedPlanet = selectedPanelsID;
    }

}
