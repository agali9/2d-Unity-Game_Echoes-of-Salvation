using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGlow : MonoBehaviour
{
    public Animator animator;
    bool DemonLastWords = true;
    public GameObject LastDialogBox;
    public GameObject LastDialog;
    public GameObject LastDialogName;
    public static bool KidnapSat;
    public SpriteRenderer alpha;
    private void Start()
    {
        LastDialogBox.SetActive(false);
    }
    private void Update()
    {
        if (DialogManager.triggerFloat)
        {
            Debug.Log("Animate");
            animator.SetBool("triggerFloat", true);
            DialogManager.triggerFloat = false;
            animator.SetBool("Vanish", true);
        }
        if(alpha.color.a == 0 && DemonLastWords)
        {
            StartCoroutine(EndKidnapping());
            DemonLastWords = false;
        }
    }

IEnumerator EndKidnapping()
    {
        yield return new WaitForSeconds(2f);
        LastDialogBox.SetActive(true);
        LastDialogName.SetActive(true);
        LastDialog.SetActive(true);
        yield return new WaitForSeconds(10f);
        LastDialogBox.SetActive(false);
        Debug.Log("Dialog");
        LastDialogName.SetActive(false);
        LastDialog.SetActive(false);
        yield return new WaitForSeconds(3f);
        Saturation.flash = true;
        KidnapSat = true;
    }

}
