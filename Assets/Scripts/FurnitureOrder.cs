using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FurnitureOrder : MonoBehaviour
{
    public GameObject PlayerBarstoolLayer;
    void Start()
    {
        if (PlayerBarstoolLayer == null)
        {
            PlayerBarstoolLayer = GameObject.FindGameObjectWithTag("PlayerBarstool");
        }
    }
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (PlayerMovement.PlayerY < 0.4529882 && scene.name == "CabinInterior")
            PlayerBarstoolLayer.GetComponent<Renderer>().sortingOrder = 3;
        else
            PlayerBarstoolLayer.GetComponent<Renderer>().sortingOrder = 0;
    }
}
