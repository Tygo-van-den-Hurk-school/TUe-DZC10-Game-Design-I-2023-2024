using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public GameObject fadeOutCanvas;
    public bool work = false;
    public int sceneReloads = 0;

    // Set the fade-out duration
    public float fadeOutDuration = 2.0f;

    void Awake()
    {
        if (sceneReloads == 0)
        {
            work = true;
            Debug.Log("work set to true successfully");
            StartCoroutine(FadeOutSceneAsync()); //failsafe?
        }
        sceneReloads += 1;     //ensure the fadeout does not happen again on reloading the scene hopefully maybe
    }

    void Update()
    {
        if (work == true)
        {
        fadeOutCanvas.SetActive(true);
        Debug.Log("FadeOut started");
        // Trigger the fade-out effect
        StartCoroutine(FadeOutSceneAsync());
        }
    }

    public IEnumerator FadeOutSceneAsync()
    {
        fadeOutCanvas.SetActive(true);
        CanvasGroup canvasGroup = fadeOutCanvas.GetComponent<CanvasGroup>();
        float startAlpha = 1.0f; // Start with a fully black canvas
        float endAlpha = 0.0f;
        float timeElapsed = 0.0f;

        yield return new WaitForSeconds(1.5f);

        while (timeElapsed < fadeOutDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeOutDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
        work = false;
        fadeOutCanvas.SetActive(false);
    }
}
