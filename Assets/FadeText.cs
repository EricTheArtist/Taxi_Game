using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    public float duration = 0.5f; // Animation duration in seconds
    private TextMeshProUGUI textMeshProText;
    private Color originalColor;
    private float elapsedTime;

    private void Awake()
    {
        textMeshProText = GetComponent<TextMeshProUGUI>();
        originalColor = textMeshProText.color;
    }

    private void OnEnable()
    {
        StartFade();
    }

    private void StartFade()
    {
        elapsedTime = 0f;
        InvokeRepeating(nameof(UpdateFade), 0f, Time.deltaTime);
    }

    private void UpdateFade()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= duration)
        {
            // Animation completed, set the final alpha value
            SetAlpha(0f);
            CancelInvoke(nameof(UpdateFade));
        }
        else
        {
            // Calculate the alpha value using linear interpolation (Lerp)
            float normalizedTime = elapsedTime / duration;
            float alpha = Mathf.Lerp(1f, 0f, normalizedTime);
            SetAlpha(alpha);
        }
    }

    private void SetAlpha(float alpha)
    {
        Color newColor = originalColor;
        newColor.a = alpha;
        textMeshProText.color = newColor;
    }
}
