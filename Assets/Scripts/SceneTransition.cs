using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    Fade fade;

    void Start()
    {
        fade = FindObjectOfType<Fade>();
    }



    public void LoadGameScene()
    {
        // Load async to allow for fading in and out
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(sceneToLoad);
    }
}
