using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMenuAfterTime : MonoBehaviour
{
    [SerializeField] private CanvasGroupRevealer revealer;

    public float HideTimer { get; set; }

    private void Update()
    {
        if (HideTimer > 0.0f)
        {
            HideTimer -= Time.deltaTime;
            if (HideTimer <= 0.0f)
            {
                HideGroup();
            }
        }
    }

    private void HideGroup()
    {
        revealer.HideGroup();
    }
}
