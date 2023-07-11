using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private float scalingAnimationTime = 0;
    [SerializeField] private float scalingAnimationSpeed = 0;
    [SerializeField] private float scaleSizeMultiplier = 0;

    [SerializeField] private bool scalingRepeatable;

    private Vector3 originalScale;
    private Vector3 MaxScale;
    private IEnumerator Start()
    {
        originalScale = transform.localScale;
        MaxScale = originalScale * scaleSizeMultiplier;
        while (scalingRepeatable)
        {
            //scale up
            yield return RepeatScale(originalScale, MaxScale, scalingAnimationTime);
            //scale down
            yield return RepeatScale(MaxScale, originalScale , scalingAnimationTime);
        }   
    }

    public IEnumerator RepeatScale(Vector3 a, Vector3 b, float time)
    {
        float i = 0f;
        float rate = (1f / time) * scalingAnimationSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
    
}
