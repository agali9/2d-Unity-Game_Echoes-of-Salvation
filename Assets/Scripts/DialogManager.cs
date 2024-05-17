using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class DialogManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogText;
    public Image DialogBox;
    public GameObject DialogBox1;
    public GameObject DisableDialogBox;
    public static bool triggerSat;
    public static bool triggerFloat;
    public Animator HellDooranimator;
    public Animator Particleanimator;
    public static SpriteRenderer _spriteRenderer;
    public static SpriteRenderer LeftHeavenDoor_spriteRenderer;
    public static SpriteRenderer RightHeavenDoor_spriteRenderer;
    public static SpriteRenderer angel_renderer;
    public static SpriteRenderer demon_renderer;

    Color WholeHellPortalAlpha;
    Color LeftHeavenDoorAlpha;
    Color RightHeavenDoorAlpha;
    Color DemonAlpha;
    Color AngelAlpha;

    public GameObject LeftHeavenDoor;
    public GameObject RightHeavenDoor;
    public GameObject Fallen_Angel;
    public GameObject Angel;

    public GameObject AngelParticles1;
    public GameObject AngelParticles2;
    public GameObject AngelParticles3;
    public GameObject AngelParticles4;

    private bool canMoveToNextSentence = true;
    private Queue<string> sentences;
    private Queue<string> characters;
    void Start()
    {
        sentences = new Queue<string>();
        characters = new Queue<string>();

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "OpenScene")
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            WholeHellPortalAlpha = _spriteRenderer.GetComponent<SpriteRenderer>().color;

            LeftHeavenDoor_spriteRenderer = LeftHeavenDoor.GetComponent<SpriteRenderer>();
            RightHeavenDoor_spriteRenderer = RightHeavenDoor.GetComponent<SpriteRenderer>();

            LeftHeavenDoorAlpha = LeftHeavenDoor_spriteRenderer.GetComponent<SpriteRenderer>().color;
            RightHeavenDoorAlpha = RightHeavenDoor_spriteRenderer.GetComponent<SpriteRenderer>().color;

            demon_renderer = Fallen_Angel.GetComponent<SpriteRenderer>();
            angel_renderer = Angel.GetComponent<SpriteRenderer>();

            DemonAlpha = Fallen_Angel.GetComponent<SpriteRenderer>().color;
            AngelAlpha = Angel.GetComponent<SpriteRenderer>().color;

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && DialogTrigger.DisableB && canMoveToNextSentence)
        {
            DisplayNextSentence();
            StartCoroutine(AngelFlash());
        }
    }

    public void StartDialog(Dialog dialog)
    {
        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string character in dialog.character)
        {
            characters.Enqueue(character);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        string character = characters.Dequeue();
        nameText.text = character;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        DialogBox.gameObject.SetActive(false);
        if (sceneName == "Cutscene")
            triggerSat = true;
        if (sceneName == "Kidnapping")
            triggerFloat = true;
        if (sceneName == "OpenScene")
        {
            StartCoroutine(EnableDialog());
        }
        DialogTrigger.DisableB = false;
        Debug.Log("End of Conversation");
    }

    IEnumerator AngelFlash()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if ((sceneName == "OpenScene") && (sentences.Count == 4 || sentences.Count == 8))
        {
            DisableDialogBox.SetActive(false);
            canMoveToNextSentence = false;
            DialogTrigger.angelFlash = true;
            if (sentences.Count == 4) 
            {
                DemonAlpha.a = 255f;
                demon_renderer.GetComponent<SpriteRenderer>().color = DemonAlpha;
            }
            if (sentences.Count == 8)
            {
                AngelAlpha.a = 255f;
                angel_renderer.GetComponent<SpriteRenderer>().color = AngelAlpha;
            }
            yield return new WaitForSeconds(5f);
            DisableDialogBox.SetActive(true);
            canMoveToNextSentence = true;
        }
        if (sceneName == "OpenScene" && sentences.Count == 2)
        {
            DisableDialogBox.SetActive(false);
            canMoveToNextSentence = false;
            Shake.start = true;
            HellDooranimator.SetBool("PortalEmerge", true);
            yield return new WaitForSeconds(7f);
            WholeHellPortalAlpha.a = 255f;
            _spriteRenderer.GetComponent<SpriteRenderer>().color = WholeHellPortalAlpha;
            HellDooranimator.SetBool("PortalEmerge", false);
            DisableDialogBox.SetActive(true);
            canMoveToNextSentence = true;
        }
        if (sceneName == "OpenScene" && sentences.Count == 11)
        {
            DisableDialogBox.SetActive(false);
            canMoveToNextSentence = false;
            AngelParticles1.GetComponent<PathFollower>().enabled = true;
            AngelParticles2.GetComponent<PathFollower>().enabled = true;
            AngelParticles3.GetComponent<PathFollower>().enabled = true;
            AngelParticles4.GetComponent<PathFollower>().enabled = true;
            yield return new WaitForSeconds(13f);
            Particleanimator.SetBool("ParticlePart", true);
            yield return new WaitForSeconds(1f);
            LeftHeavenDoorAlpha.a = 255f;
            LeftHeavenDoor_spriteRenderer.GetComponent<SpriteRenderer>().color = LeftHeavenDoorAlpha;
            RightHeavenDoorAlpha.a = 255f;
            RightHeavenDoor_spriteRenderer.GetComponent<SpriteRenderer>().color = RightHeavenDoorAlpha;
            yield return new WaitForSeconds(7.5f);
            Particleanimator.SetBool("ParticlePart", false);
            DisableDialogBox.SetActive(true);
            canMoveToNextSentence = true;
        }
    }

    IEnumerator EnableDialog()
    {
        DisableDialogBox.SetActive(false);
        yield return new WaitForSeconds(2f);
        DialogBox1.SetActive(true);
        yield return new WaitForSeconds(4f);
        DialogBox1.SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(7);
    }

}
