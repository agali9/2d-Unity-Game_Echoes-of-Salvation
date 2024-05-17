using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public Image DialogBox;
    public bool enable;
    public static bool angelFlash;
    public static bool DisableB;
    private void Start()
    {
        enable = true;
        StartCoroutine(DialogStart());
    }
    private void Update()
    {
        if (DialogBox.enabled && enable)
        {
            TriggerDialog();
            enable = false;
        }
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    IEnumerator DialogStart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Kidnapping")
            yield return new WaitForSeconds(30f);
        else if (sceneName == "Cutscene")
            yield return new WaitForSeconds(15f);
        else if (sceneName == "OpenScene")
        {
            yield return new WaitForSeconds(20f);
            //angelFlash = true;
            //yield return new WaitForSeconds(3f);
        }
        DialogBox.gameObject.SetActive(true);
        DisableB = true;
    }
}
