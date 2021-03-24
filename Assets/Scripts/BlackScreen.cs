using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    public float speedBlackout;
    public bool startBlackout;
    public bool startManifestation;
    public bool hideOnStart;
    public Vector3 startScale;

    private void Awake()
    {
        startScale = transform.localScale;
    }
    void Start()
    {
        if (hideOnStart)
        {
            transform.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startBlackout)
        {
            Blackout();
        }
        else if (startManifestation)
        {
            Manifestation();
        }
    }

    private void Blackout()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, speedBlackout * Time.deltaTime);
    }

    private void Manifestation()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, startScale, speedBlackout * Time.deltaTime);
    }

    public void StartManifestation()
    {
        startManifestation = true;
        transform.localScale = Vector3.zero;
    }
}
