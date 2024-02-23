using UnityEngine;
using System.Collections;

public class FadeInEffect : MonoBehaviour
{
    public CanvasGroup uiElement; // Assign your Canvas Group here
    public float fadeInTime = 1.0f; // Duration of the fade in

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float startAlpha = uiElement.alpha; // Start from current alpha
        float time = 0;

        while (time < fadeInTime)
        {
            time += Time.deltaTime;
            uiElement.alpha = Mathf.Lerp(startAlpha, 1, time / fadeInTime); // Lerp from current alpha to 1
            yield return null; // Wait for the next frame
        }

        uiElement.alpha = 1; // Ensure the alpha is set to 1
    }
}
