using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionControl : MonoBehaviour
{
    public float speed = 6;
    public GameObject ParentCreature;
    private Vector3 firstCreature,
                    lastCreature;

    public bool ResolutionInMotion = false;

    private bool isMove = false;

    private int indexActiveCreature = 0;

    private Vector3 touch;
    private Vector3 cameraPosition;

    private MoveCameraNextCreature moveCamera;

    private Transform[] _creatures;
    private void Awake()
    {
        ResolutionInMotion = false;
        moveCamera = GetComponent<MoveCameraNextCreature>();
    }
    private void Start()
    {
        firstCreature = ParentCreature.transform.GetChild(0).position;
        lastCreature = ParentCreature.transform.GetChild(ParentCreature.transform.childCount - 1).position;
        _creatures = new Transform[ParentCreature.transform.childCount];
        for (int i = 0; i < _creatures.Length; i++)
        {
            _creatures[i] = ParentCreature.transform.GetChild(i);
        }
    }
    private void Update()
    {
        if (ResolutionInMotion)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                cameraPosition = transform.position;
                isMove = false;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = touch - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = new Vector3(direction.x, 0, 0);
                Vector3 resultPosition = transform.position + direction;
                resultPosition = new Vector3(Mathf.Clamp(resultPosition.x, firstCreature.x, lastCreature.x), resultPosition.y, resultPosition.z);
                transform.position = resultPosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePosition = transform.position - cameraPosition;
                if (mousePosition.x > 0)
                {
                    indexActiveCreature = Mathf.Clamp(++indexActiveCreature, 0, _creatures.Length - 1);
                }
                else if(mousePosition.x < 0)
                {
                    indexActiveCreature = Mathf.Clamp(--indexActiveCreature, 0, _creatures.Length - 1);
                }
                isMove = true;
            }
            if (isMove)
            {
                moveCamera.MoveToCreature(_creatures[indexActiveCreature], speed);
            }
        }
    }
}
