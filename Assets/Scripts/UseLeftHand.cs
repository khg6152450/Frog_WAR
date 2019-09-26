using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class UseLeftHand : MonoBehaviour
{
    private SteamVR_TrackedObject controller;
    public SteamVR_Action_Boolean BuildAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Menu");
    private Hand pointerHand = null;
    private Player player = null;

    public static bool CheckLeftHand = false;

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
        foreach (Hand hand in player.hands)
        {
            if (WasMenuButtonPressed(hand))
            {
                CheckLeftHand = true;
                UseRightHand.CheckRightHand = false;
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
}