using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlashImage : MonoBehaviour
{
    public static bool fadeout;

    public static bool StartPos;

    public Image LastSentence;

    Image _image = null;

    Coroutine currentFlashRoutine = null;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Kidnapping")
        {
            _image.color = new Color32(255, 255, 255, 255);
        }
        if (sceneName == "Decision" && StartGlow.KidnapSat)
        {
            StartFlash(4f, 3f);
        }
        if (sceneName == "OpenScene")
        {
            _image.color = new Color32(255, 255, 255, 255);
            StartFlash(4f, 3f);
        }
        if (sceneName == "Cutscene" && StartGlow.KidnapSat)
        {
            StartFlash(4f, 3f);
        }
    }
    public void StartFlash(float secondsForOneFlash, float MaxAlpha)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        MaxAlpha = Mathf.Clamp(MaxAlpha, 0, 1);
        if (currentFlashRoutine != null)
            StopCoroutine(currentFlashRoutine);
        if(Saturation.flash)
        {
            currentFlashRoutine = StartCoroutine(FlashIn(secondsForOneFlash, MaxAlpha));
        }
        else if (sceneName == "Kidnapping" || StartGlow.KidnapSat)
        {
            currentFlashRoutine = StartCoroutine(FlashOut(secondsForOneFlash, MaxAlpha));
        }
        else if (sceneName == "OpenScene" && DialogTrigger.angelFlash)
        {
            currentFlashRoutine = StartCoroutine(FlashIn(secondsForOneFlash, MaxAlpha));
            currentFlashRoutine = StartCoroutine(FlashOut(secondsForOneFlash, MaxAlpha));
        }
        else if (sceneName == "OpenScene")
        {
            currentFlashRoutine = StartCoroutine(FlashOut(secondsForOneFlash, MaxAlpha));
        }
    }

    IEnumerator FlashIn(float secondsForOneFlash, float MaxAlpha)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Cutscene")
        {
            LastSentence.gameObject.SetActive(true);
            yield return new WaitForSeconds(7f);
            LastSentence.gameObject.SetActive(false);
        }

        float flashInDuration = secondsForOneFlash / 2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0, MaxAlpha, t / flashInDuration);
            _image.color = colorThisFrame;
            yield return null;
        }

        if (sceneName == "Cutscene")
            SceneManager.LoadScene("Kidnapping");
        else if (sceneName == "Kidnapping")
        {
            SceneManager.LoadScene(6);
            StartPos = true;
        }
        else if (sceneName == "OpenScene")
            yield return new WaitForSeconds(1f);
    }

    IEnumerator FlashOut(float secondsForOneFlash, float MaxAlpha)
    {
        float flashOutDuration = secondsForOneFlash / 2;
        _image.color = new Color32(255, 255, 255, 255);
        for (float t = 0; t <= flashOutDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(MaxAlpha, 0, t / flashOutDuration);
            _image.color = colorThisFrame;
            yield return null;
        }

        _image.color = new Color32(255, 255, 255, 0);
    }
}
