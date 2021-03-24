using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackground : MonoBehaviour
{
    public GameObject Background;

    public void Spawn(int index, Color colorBackground, Transform transformCreature)
    {
        GameObject bg = Instantiate(Background, transform);
        BackgroundInfo bgIngo = bg.GetComponent<BackgroundInfo>();
        for (int i = 0; i < bgIngo.spriteBackground.Length; i++)
        {
            bgIngo.spriteBackground[i].color = colorBackground;
        }
        bg.transform.position = transformCreature.position;
    }
}