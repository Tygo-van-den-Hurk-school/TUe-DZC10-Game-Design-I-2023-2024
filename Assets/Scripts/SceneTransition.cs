using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public GameObject fadeInCanvas;
    public GameObject fader;              //duplicate which for some reason sort of causes things to work??

    public Button startButton;

    public FadeOut fadeout;

    void Start()
    {
        fader.SetActive(false);
        startButton.onClick.AddListener(LoadGameScene);
    }

    public void LoadGameScene()
    {
        // Load async to allow for fading in and out
        CanvasGroup canvasGroup = fader.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;
        fader.SetActive(true); 
        StartCoroutine(LoadSceneAsync(canvasGroup));
    }

    private IEnumerator LoadSceneAsync(CanvasGroup canvasGroup)
    {
        yield return new WaitForSeconds(1.0f);
        fader.SetActive(true); // failsafe

        float duration = 2.0f;
        float startAlpha = 0.0f;
        float endAlpha = 1.0f;
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / duration);
            canvasGroup.alpha = alpha;

            yield return null;
        }
        canvasGroup.alpha = endAlpha;
        Time.timeScale = 1;

        //fadeout.work = true;
        SceneManager.LoadScene("DevelopmentScene");
    }
}
