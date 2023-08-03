using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeEffects : MonoBehaviour
{
    [SerializeField] private VolumeProfile _volume;

    [SerializeField] private float targetSaturation;

    private void Awake()
    {
        ResetSaturation();
    }

    public void SetColorAdjustmentsDead()
    {
        StartCoroutine(SmoothColorAdjustment());
    }

    private IEnumerator SmoothColorAdjustment()
    {
        ColorAdjustments cA;
        if (_volume.TryGet<ColorAdjustments>(out cA))
        {
            while (cA.saturation.value >= targetSaturation)
            {
                cA.saturation.value -= 5;
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    public void ResetSaturation()
    {
        // reset saturation from death
        if (_volume.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.saturation.value = 0;
        }
    }
}
