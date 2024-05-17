using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Saturation : MonoBehaviour
{
    public float transitionNum;

    public static bool flash;

    public float changePerSecond;

    public float reversetransition;

    [SerializeField] private Volume postProcessVolume;

    private ColorAdjustments ColorAdjustment;
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        postProcessVolume.profile.TryGet(out ColorAdjustment);
        if (StartGlow.KidnapSat && sceneName == "Decision")
        {
            StartCoroutine(FlashIn());
        }
        else
        {
            ColorAdjustment.saturation.value = 0f;
            ColorAdjustment.contrast.value = 0f;
        }
    }
    void Update()
    {
        if (DialogManager.triggerSat)
        {
            StartCoroutine(FadeOut());
            DialogManager.triggerSat = false;
        }
    }

    IEnumerator FadeOut()
    {
        while (transitionNum < 100)
        {
            transitionNum += changePerSecond * Time.deltaTime;
            ColorAdjustment.saturation.value = -transitionNum;
            ColorAdjustment.contrast.value = transitionNum;
            Debug.Log(transitionNum);
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(2f);
        flash = true;
    }

    IEnumerator FadeIn()
    {
        Debug.Log("It works");
        while (reversetransition < 100)
        {
            reversetransition += changePerSecond * Time.deltaTime;
            ColorAdjustment.saturation.value = -100 + reversetransition;
            ColorAdjustment.contrast.value = 100 - reversetransition;
            Debug.Log(reversetransition);
            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator FlashIn()
    {
        ColorAdjustment.saturation.value = -100f;
        ColorAdjustment.contrast.value = 100f;
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeIn());
        StartGlow.KidnapSat = false;
    }
}