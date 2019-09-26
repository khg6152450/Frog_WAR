using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FirstSwich: MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    
    void Start()
    {
        Player1.SetActive(true);
        Player2.SetActive(false);
    }

    
}