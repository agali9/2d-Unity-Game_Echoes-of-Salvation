using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNext : MonoBehaviour
{
    public static float TrackYPos;
    public Vector3 YPos;
    public static bool trackPos;
    private Animator animator;
    public static bool CabinDoor;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (FlashImage.StartPos)
        {
            transform.position = new Vector2(-4, 0.11f);
        }
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (trackPos)
        {
            if (sceneName == "Cabin") 
            { 
                transform.position = new Vector2(11, TrackYPos);
                animator.SetFloat("moveX", -0.1f);
            }
            if (sceneName == "Decison")
            {
                transform.position = new Vector2(-11, TrackYPos);
                animator.SetFloat("moveX", 0.1f);
            }
            trackPos = false;
        }
        if (CabinDoor)
        {
            if (sceneName == "Cabin")
            {
                transform.position = new Vector2(1.58f, 0.37f);
                animator.SetFloat("moveY", -0.1f);
            }
            if (sceneName == "CabinInterior")
            {
                transform.position = new Vector2(7.49f, -4.99f);
                animator.SetFloat("moveY", 0.1f);
            }
            CabinDoor = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "LoadNext")
        {
            if (collision.gameObject.tag == "LoadCabin")
            {
                TrackYPos = transform.position.y;
                SceneManager.LoadScene(4);
                trackPos = true;
            }
            else if (collision.gameObject.tag == "LoadDecision")
            {
                TrackYPos = transform.position.y;
                SceneManager.LoadScene(3);
                trackPos = true;
            }
        }
        if (collision.gameObject.name == "CabinDoor")
        {
            if(collision.gameObject.tag == "LoadExterior")
            {
                SceneManager.LoadScene(4);
                CabinDoor = true;
            }
            if (collision.gameObject.tag == "LoadInterior")
            {
                SceneManager.LoadScene(5);
                CabinDoor = true;
            }
        }
    }
}
