using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndKidnap : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Saturation.flash = true;
        }        
    }
}
