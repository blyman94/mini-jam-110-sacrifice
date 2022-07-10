using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private CanvasGroupRevealer revealer;

    private void Start()
    {
        revealer.FadeOut();
    }
}
