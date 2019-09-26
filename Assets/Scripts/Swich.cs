using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Swich: MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Teleporting;
    public GameObject TeleportingArea;
    private SteamVR_TrackedObject controller;
    public SteamVR_Action_Boolean BuildAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Menu");
    private Hand pointerHand = null;
    private Player player = null;
    public GameObject BuildUI;
    public static bool bulldmode = false;
    public GameObject PauseMenu;
    public GameObject Weapon1;
    public GameObject Weapon2;
    public GameObject Weapon3;
    public GameObject Weapon4;
    public GameObject Weapon5;
    //public GameObject Weapon6;
    public GameObject Weapon1CrossHiar;
    public GameObject Weapon2CrossHiar;
    public GameObject Weapon3CrossHiar;
    public GameObject Weapon4CrossHiar;
    public GameObject Weapon5CrossHiar;
    //public GameObject Weapon1CrossHiar;

    public static bool time = false;
    public GameObject gameoverUI;
    public SceneFader sceneFader;
    public Text uitext;

    public static bool GameStart = false;
    public static bool GameIsOver = false;

    // Update is called once per frame
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
            GameManager.GameIsOver = false;
            GameManager.IsDead = false;

    }

    void Update()
    {
        foreach (Hand hand in player.hands)
        {
            if (WasMenuButtonPressed(hand))
            {                
                 //BuildModeEnd();                                
            }

            if (WasTeleportButtonReleased(hand))
            {
                //bulldmode = false;
            }
        }
        if(GameIsOver == false)
        {
            if (GameManager.GameIsOver == true)
            {
                EndGame();
            }
        }        
    }

    public static void Toggle()
    {        
        if (!time)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private bool WasMenuButtonPressed(Hand hand)
    {

        if (IsEligibleForGrabPinch(hand))
        {
            if (hand.noSteamVRFallbackCamera != null)
            {
                return Input.GetKeyDown(KeyCode.T);
            }
            else
            {
                return BuildAction.GetStateDown(hand.handType);

                //return hand.controller.GetPressDown( SteamVR_Controller.ButtonMask.Touchpad );
            }
        }

        return false;
    }

    private bool WasTeleportButtonReleased(Hand hand)
    {
        if (IsEligibleForGrabPinch(hand))
        {
            if (hand.noSteamVRFallbackCamera != null)
            {
                return Input.GetKeyUp(KeyCode.T);
            }
            else
            {
                return BuildAction.GetStateUp(hand.handType);

                //return hand.controller.GetPressUp( SteamVR_Controller.ButtonMask.Touchpad );
            }
        }

        return false;
    }

    public bool IsEligibleForGrabPinch(Hand hand)
    {
        if (hand == null)
        {
            return false;
        }

        if (!hand.gameObject.activeInHierarchy)
        {
            return false;
        }

        if (hand.hoveringInteractable != null)
        {
            return false;
        }

        if (hand.noSteamVRFallbackCamera == null)
        {
            if (hand.isActive == false)
            {
                return false;
            }

            //Something is attached to the hand
            if (hand.currentAttachedObject != null)
            {
                AllowTeleportWhileAttachedToHand allowTeleportWhileAttachedToHand = hand.currentAttachedObject.GetComponent<AllowTeleportWhileAttachedToHand>();

                if (allowTeleportWhileAttachedToHand != null && allowTeleportWhileAttachedToHand.teleportAllowed == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void BuildModeStart()
    {
        //sceneFader.FadeTo("1");
        Player1.SetActive(true);
        Player2.SetActive(false);
        Teleporting.SetActive(true);
        TeleportingArea.SetActive(true);
        BuildUI.SetActive(true);
        bulldmode = true;

        //ui.SetActive(false);

        GameIsOver = false;
        GameStart = true;
        time = true;
        uitext.text = "CONTINUE";

        Toggle();

        //PauseMenu.SetActive(false);
    }

    public void BuildModeEnd()
    {
        //sceneFader.FadeTo("2");
        Player1.SetActive(false);
        Player2.SetActive(true);
        Teleporting.SetActive(false);
        TeleportingArea.SetActive(false);
        BuildUI.SetActive(false);
        bulldmode = false;


        //ui.SetActive(true);
        time = false;

        Toggle();

        //PauseMenu.SetActive(true);
    }

    public void EndGame()
    {
        //sceneFader.FadeTo("2");
        Player1.SetActive(false);
        Player2.SetActive(true);
        Teleporting.SetActive(false);
        TeleportingArea.SetActive(false);
        BuildUI.SetActive(false);
        gameoverUI.SetActive(true);
        Weapon1.SetActive(false);
        Weapon2.SetActive(false);
        Weapon3.SetActive(false);
        Weapon4.SetActive(false);
        Weapon5.SetActive(false);
        //Weapon6.SetActive(false);
        Weapon1CrossHiar.SetActive(false);
        Weapon2CrossHiar.SetActive(false);
        Weapon3CrossHiar.SetActive(false);
        Weapon4CrossHiar.SetActive(false);
        Weapon1CrossHiar.SetActive(false);
        //Weapon1CrossHiar.SetActive(false);

        //Shop.GetGun = false;
        //Shop.GetBow = false;

        //ui.SetActive(true);

        Toggle();        
    }

    public void Retry()
    {
        sceneFader.FadeTo("2");
        PlayerStats.Money = 500;
        PlayerStats.Lives = 5;
        PlayerStats.Rounds = 0;
        WaveSpawner.waveIndex = 0;
        WaveSpawner.EnemiesAlive = 0;
        Shop.GetGun = false;
        Shop.GetBow = false;
        Shop.GetRevolver = false;
        Shop.GetCure = false;
        WeaponCtrl.useBow = false;
        WeaponCtrl.useGun = false;
        WeaponCtrl.useRevolver = false;
        WeaponCtrl.useCure = false;
        WeaponCtrl.useWeapon = false;
        Weapon1.SetActive(false);
        Weapon2.SetActive(false);
        Weapon3.SetActive(false);
        Weapon4.SetActive(false);
        Weapon5.SetActive(false);
        //Weapon6.SetActive(false);
        Weapon1CrossHiar.SetActive(false);
        Weapon2CrossHiar.SetActive(false);
        Weapon3CrossHiar.SetActive(false);
        Weapon4CrossHiar.SetActive(false);
        Weapon1CrossHiar.SetActive(false);
        //Weapon1CrossHiar.SetActive(false);

        GameStart = false;

        var EnemyClones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var clone in EnemyClones)
        {
            Destroy(clone);
        }

        var TowreClones = GameObject.FindGameObjectsWithTag("Tower");
        foreach (var clone in TowreClones)
        {
            Destroy(clone);
        }
    }
}