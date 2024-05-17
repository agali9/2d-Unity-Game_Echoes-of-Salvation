using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MakeDecision : MonoBehaviour
{

    private bool isPlayerNearAngel = false;
    private bool isPlayerNearDevil = false;
    public bool AngelActive = false;
    public bool DevilActive = false;
    public GameObject DisablePlayerMovement;
    GameObject TalkAngel;
    GameObject TalkDevil;
    GameObject AngelInteract;
    GameObject DevilInteract;

    void Start()
    {
        TalkAngel = GameManager.Instance.TalkAngel;
        TalkDevil = GameManager.Instance.TalkDevil;
        AngelInteract = GameManager.Instance.AngelInteract;
        DevilInteract = GameManager.Instance.DevilInteract;

        TalkAngel.SetActive(false);
        TalkDevil.SetActive(false);
        AngelInteract.SetActive(false);
        DevilInteract.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNearAngel && Input.GetKeyDown(KeyCode.Space))
        {
            AngelInteract.SetActive(false);
            isPlayerNearAngel = false;
            TalkAngel.SetActive(true);
            AngelActive = true;
            TalkDevil.SetActive(false);
            DisablePlayerMovement.GetComponent<PlayerMovement>().enabled = false;
        }
        if (isPlayerNearDevil && Input.GetKeyDown(KeyCode.Space))
        {
            DevilInteract.SetActive(false);
            TalkDevil.SetActive(true);
            DevilActive = true;
            isPlayerNearDevil = false;
            TalkAngel.SetActive(false);
            DisablePlayerMovement.GetComponent<PlayerMovement>().enabled = false;
        }
        if (DevilActive)
        {
            ChooseDevil();
        }
        if (AngelActive)
        {
            ChooseAngel();
        }
    }
    public void ChooseAngel()
    {
        if (Input.GetKeyDown(KeyCode.N).Equals(true))
        {
            TalkAngel.SetActive(false);
            AngelActive = false;
            DisablePlayerMovement.GetComponent<PlayerMovement>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene(8);
            AngelActive = false;
            TalkAngel.SetActive(false);
        }
    }

    public void ChooseDevil()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            TalkDevil.SetActive(false);
            DevilActive = false;
            DisablePlayerMovement.GetComponent<PlayerMovement>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene(9);
            DevilActive = false;
            TalkDevil.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "MakeDecisionHeaven")
        {
            AngelInteract.SetActive(true);
            isPlayerNearAngel = true;
        }
        if (collision.gameObject.name == "MakeDecisionHell")
        {
            DevilInteract.SetActive(true);
            isPlayerNearDevil = true;
        }
    }
}
