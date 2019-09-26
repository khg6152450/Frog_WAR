using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelector;
    public GameObject Main;
    public GameObject player1;
    public GameObject player2;
    public GameObject lefthand;
    public GameObject righthand1;
    public GameObject righthand2;

    public GameObject frog;
    public GameObject frog2;
    public GameObject frog3;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject meteor;

    static bool select = false;

    private Player player = null;
    private SteamVR_TrackedObject controller;

    public string levelToLoad = "MainLevel";

    public SceneFader sceneFader;
    public bool isTutorial = false;
    public GameObject tutorial;


    void Start()
    {
        controller = GetComponent<SteamVR_TrackedObject>();

        player = Valve.VR.InteractionSystem.Player.instance;

        if (player == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> Teleport: No Player instance found in map.");
            Destroy(this.gameObject);
            return;
        }
    }

    public void Play()
    {
        //sceneFader.FadeTo(levelToLoad);
        Main.SetActive(false);
        levelSelector.SetActive(true);
        lefthand.SetActive(true);
        righthand1.SetActive(true);
        righthand2.SetActive(false);
        player1.SetActive(false);
        player2.SetActive(true);
        player.leftHand.BuildMode = false;
        player.rightHand.BuildMode = false;

        var FrogClones = GameObject.FindGameObjectsWithTag("Frog");
        foreach (var clone in FrogClones)
        {
            Destroy(clone);
        }
    }

    public void Play2()
    {        
        MainEnemy2.clicked = true;
        select = true;
        button2.SetActive(false);
        button3.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Exciting...");
        Application.Quit();
        //Debug.Log("Restarting...");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Tuto()
    {
        if(isTutorial == false)
        {
            tutorial.SetActive(true);
            isTutorial = true;
        } else if(isTutorial == true)
        {
            tutorial.SetActive(false);
            isTutorial = false;
        }
        
    }

    void Update()
    {
        if(meteor.activeInHierarchy == false)
        {
            StartCoroutine(waitTime());
        }
        if(select == true)
        {
            StartCoroutine(waitTime2());
        }
    }

    IEnumerator waitTime()
    {
        frog.SetActive(true);
        yield return new WaitForSeconds(3f);
        frog2.SetActive(true);
        yield return new WaitForSeconds(2f);
        button1.SetActive(true);
    }

    IEnumerator waitTime2()
    {
        frog3.SetActive(true);
        button2.SetActive(false);
        yield return new WaitForSeconds(2f);
        button3.SetActive(true);
    }
}
