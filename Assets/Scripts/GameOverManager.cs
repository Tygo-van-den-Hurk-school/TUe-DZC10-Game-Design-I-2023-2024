using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public MovementController movementcontroller;
    public GameObject gameOverCanvas;
    public Button retryButton;

    private bool zoomAndFadeStarted = false;

    // Start is called before the first frame update
    private void Start()
    {
        gameOverCanvas.SetActive(false);
        retryButton.onClick.AddListener(Retry);
    }

    private void Update()
    {
        if (movementcontroller.gameOver == true)
        // This should probably also have a check for zoomAndFadeStarted=false, but simply that causes it to not work anymore, so I removed it for now.
        {
            zoomAndFadeStarted = true;
            gameOverCanvas.SetActive(true); 
            CanvasGroup canvasGroup = gameOverCanvas.GetComponent<CanvasGroup>();
            Camera mainCamera = Camera.main;
            float targetOrthoSize = CalculateTargetOrthoSize(mainCamera, gameOverCanvas);
            Time.timeScale = 0.2f;
            StartCoroutine(FadeInCanvas(canvasGroup, mainCamera, targetOrthoSize));
        }
    }

    private IEnumerator FadeInCanvas(CanvasGroup canvasGroup, Camera mainCamera, float targetOrthoSize)
    // fades canvas and zooms out camera
    {
        gameOverCanvas.SetActive(true); // failsafe

        float duration = 1.0f;
        float startAlpha = 0.0f;
        float endAlpha = 1.0f;
        float timeElapsed = 0.0f;

        float startOrthoSize = mainCamera.orthographicSize;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / duration);
            canvasGroup.alpha = alpha;

            float orthoSize = Mathf.Lerp(startOrthoSize, targetOrthoSize, timeElapsed / duration);
            mainCamera.orthographicSize = orthoSize;

            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        Time.timeScale = 0;

    }

    private void Retry()
    {
        
        Time.timeScale = 1;

        SceneManager.LoadScene("DevelopmentScene");
    }

    private float CalculateTargetOrthoSize(Camera camera, GameObject target)
    {
        RectTransform targetRect = target.GetComponent<RectTransform>();
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        float targetHeight = targetRect.sizeDelta.y;
        float targetWidth = targetRect.sizeDelta.x;
        float orthoSizeY = targetHeight / 2f;
        float orthoSizeX = targetWidth / (2f * camera.aspect);
        return Mathf.Max(orthoSizeY, orthoSizeX);
    }
}
