using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashController : MonoBehaviour
{
    [SerializeField] FlashImage _flashImage = null;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Kidnapping")
        {
            _flashImage.StartFlash(4f, 3f);
        }
    }
    void Update()
    {
        if (Saturation.flash)
        {
            _flashImage.StartFlash(4f, 3f);
            Saturation.flash = false;
        }
        if (DialogTrigger.angelFlash)
        {
            _flashImage.StartFlash(7f, 3f);
            DialogTrigger.angelFlash = false;
        }
    }
}
