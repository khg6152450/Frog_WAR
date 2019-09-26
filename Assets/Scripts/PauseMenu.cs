using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    private SteamVR_TrackedObject controller;
    public SteamVR_Action_Boolean BuildAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Menu");
    private Hand pointerHand = null;
    private Player player = null;

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

    void Update()
    {
        foreach (Hand hand in player.hands)
        {
            if (WasMenuButtonPressed(hand))
            {
                Toggle();                
            }
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


    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    // Retry는 빌드 인덱스를 받아서 적용
    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name); //  아무의미없음 ㅋ;

        //StartCoroutine(wait2(levelName));
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName); //  아무의미없음 ㅋ;

        //StartCoroutine(wait2(levelName));
    }

    public void GoMainMenu()
    {
        Debug.Log("mainmenu");
        

    }

    public void GoRetry()
    {
        Debug.Log("retry");


    }

    IEnumerator wait2(string levelName)
    {
        yield return new
        WaitForSeconds(1f);

        Debug.Log("OK1");
        yield return 0;

        //if (levelName == level01)
        {
            Debug.Log("OK2");
            //Level1.SetActive(true);
            //Selector.SetActive(false);
        }
    }
}
