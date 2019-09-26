using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System.Collections.ObjectModel;
using System;

public class HandCtrl : MonoBehaviour
{

    public GameObject Bullet;

    public Transform FirePos;

    public AudioClip ShotSound;

    private SteamVR_TrackedObject controller;

    private AudioSource _audio;

    public SteamVR_Action_Boolean ShootAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    private Player player = null;

    private Hand pointerHand = null;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<SteamVR_TrackedObject>();

        _audio = GetComponent<AudioSource>();

        player = Valve.VR.InteractionSystem.Player.instance;

        if (player == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> Teleport: No Player instance found in map.");
            Destroy(this.gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {       

        foreach (Hand hand in player.hands)
        {             
            if (WasShootButtonPressed(hand))
            {
                if(player.leftHand.BuildMode == false && player.rightHand.BuildMode == false)
                    Shoot();                
            }
        }
    }



    private IEnumerator PinchHintCoroutine()
    {
        float prevBreakTime = Time.time;
        float prevHapticPulseTime = Time.time;

        while (true)
        {
            bool pulsed = false;

            //Show the hint on each eligible hand
            foreach (Hand hand in player.hands)
            {
                bool showHint = IsEligibleForGrabPinch(hand);
                bool isShowingHint = !string.IsNullOrEmpty(ControllerButtonHints.GetActiveHintText(hand, ShootAction));
                if (showHint)
                {
                    if (!isShowingHint)
                    {
                        ControllerButtonHints.ShowTextHint(hand, ShootAction, "GrabPinch");
                        prevBreakTime = Time.time;
                        prevHapticPulseTime = Time.time;
                    }

                    if (Time.time > prevHapticPulseTime + 0.05f)
                    {
                        //Haptic pulse for a few seconds
                        pulsed = true;

                        hand.TriggerHapticPulse(500);
                    }
                }
                else if (!showHint && isShowingHint)
                {
                    ControllerButtonHints.HideTextHint(hand, ShootAction);
                }
            }

            if (Time.time > prevBreakTime + 3.0f)
            {
                //Take a break for a few seconds
                yield return new WaitForSeconds(3.0f);

                prevBreakTime = Time.time;
            }

            if (pulsed)
            {
                prevHapticPulseTime = Time.time;
            }

            yield return null;
        }
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

    private bool WasShootButtonPressed(Hand hand)
    {
        
        if (IsEligibleForGrabPinch(hand))
        {
            if (hand.noSteamVRFallbackCamera != null)
            {
                return Input.GetKeyDown(KeyCode.T);
            }
            else
            {
                return ShootAction.GetStateDown(hand.handType);

                //return hand.controller.GetPressDown( SteamVR_Controller.ButtonMask.Touchpad );
            }
        }

        return false;
    }

    public void Shoot()
    {
        _audio.PlayOneShot(ShotSound);

        Instantiate(Bullet, FirePos.position, FirePos.rotation);
    }
}