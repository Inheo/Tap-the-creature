using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAnimationController : MonoBehaviour
{
    [SerializeField]
    private SnapScrolling snapScrolling;

    public void StartAnimation(int i)
    {
        snapScrolling.panels[i].GetComponent<Animator>().SetBool("select", true);
    }
    public void StopAnimation(int i)
    {
        snapScrolling.panels[i].GetComponent<Animator>().SetBool("select", false);
    }
}
