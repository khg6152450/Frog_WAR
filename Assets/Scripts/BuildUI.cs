using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BuildUI : MonoBehaviour
{
    public GameObject Object1;
    public GameObject Object2;
    public GameObject Object3;
    public GameObject Object4;
    public GameObject Object5;
    public GameObject BuildArea;
    public GameObject BuildArea1;
    public GameObject BuildArea2;
    public GameObject BuildArea3;
    public static int stageLevel = 0;

    private SteamVR_TrackedObject controller;
    public SteamVR_Action_Boolean BuildAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Menu");
    private Hand pointerHand = null;
    private Player player = null;

    public static bool WeaponMode = false;
    public static bool BuildMode = false;

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
    }

    void Update()
    {
        if (stageLevel == 1)
        {
            BuildArea = BuildArea1;
        } else if(stageLevel == 2)
        {
            BuildArea = BuildArea2;
        }
        else if (stageLevel == 3)
        {
            BuildArea = BuildArea3;
        }

        foreach (Hand hand in player.hands)
        {
            if (WasMenuButtonReleased(hand))
            {
                
                if(UseRightHand.CheckRightHand == true && UseLeftHand.CheckLeftHand == false)
                {
                    if(WeaponMode == false)
                    {
                        WeaponModeStart();
                    }
                    else
                    {
                        WeaponModeEnd();
                    }
                }
                else if(UseRightHand.CheckRightHand == false && UseLeftHand.CheckLeftHand == true)
                {
                    if(BuildMode == false)
                    {
                        BuildModeStart();
                    }
                    else
                    {
                        BuildModeEnd();
                    }
                }
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

                //return hand.controller.GetPressDown( SteamVR_Controller.ButtonMask.Touchpad );
            }
        }

        return false;
    }

    private bool WasMenuButtonReleased(Hand hand)
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
        Object1.SetActive(true);
        Object2.SetActive(false);
        Object3.SetActive(true);
        Object4.SetActive(false);
        Object5.SetActive(false);
        BuildArea.SetActive(true);
        player.leftHand.BuildMode = true;
        player.rightHand.BuildMode = true;
        player.leftHand.ShopMode = false;
        player.rightHand.ShopMode = false;
        BuildMode = true;
    }

    public void BuildModeEnd()
    {
        Object1.SetActive(false);
        Object2.SetActive(true);
        Object3.SetActive(false);
        Object4.SetActive(true);
        Object5.SetActive(false);
        BuildArea.SetActive(false);
        player.leftHand.BuildMode = false;
        player.rightHand.BuildMode = false;
        player.leftHand.ShopMode = false;
        player.rightHand.ShopMode = false;
        BuildMode = false;
    }

    public void WeaponModeStart()
    {
        Object1.SetActive(false);
        Object2.SetActive(false);
        Object3.SetActive(true);
        Object4.SetActive(false);
        Object5.SetActive(true);
        BuildArea.SetActive(true);
        player.leftHand.BuildMode = false;
        player.rightHand.BuildMode = false;
        player.leftHand.ShopMode = true;
        player.rightHand.ShopMode = true;
        WeaponMode = true;
    }

    public void WeaponModeEnd()
    {
        Object1.SetActive(false);
        Object2.SetActive(true);
        Object3.SetActive(false);
        Object4.SetActive(true);
        Object5.SetActive(false);
        BuildArea.SetActive(false);
        player.leftHand.BuildMode = false;
        player.rightHand.BuildMode = false;
        player.leftHand.ShopMode = false;
        player.rightHand.ShopMode = false;
        WeaponMode = false;
    }
}