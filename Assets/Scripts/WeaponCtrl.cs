using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System.Collections.ObjectModel;
using System;

public class WeaponCtrl : MonoBehaviour
{
    [Header("Gun")]

    public GameObject Bullet;

    public Transform FirePos1;

    public AudioClip ShotSound1;

    public static int GunDamage = 30;

    public float GunDelay = 0.15f;

    public static bool useGun = false;

    [Header("Crossbow")]

    public GameObject Arrow;

    public Transform FirePos2;

    public AudioClip ShotSound2;

    public static int BowDamage = 75;

    public static float BowDelay = 1.0f;

    public static bool useBow = false;

    [Header("Revolver")]

    public GameObject Bullet3;

    public Transform FirePos3;

    public AudioClip ShotSound3;

    public static int RevolDamage = 19;

    public static float RevolDelay = 1.5f;

    public int Revolverreloading = 6;

    public static bool useRevolver = false;

    [Header("CureGun")]

    public GameObject bullet4;

    public Transform FirePos4;

    public AudioClip ShotSound4;

    public static int CureDamage = 9999;

    public static float CureDelay = 7.0f;

    public static bool useCure = false;

    [Header("Shotgun")]

    public GameObject bullet5;

    public Transform FirePos5_1;
    public Transform FirePos5_2;
    public Transform FirePos5_3;
    public Transform FirePos5_4;
    public Transform FirePos5_5;
    public Transform FirePos5_6;
    public Transform FirePos5_7;

    public AudioClip ShotSound5;

    public static int ShotgunDamage = 20;

    public static float ShotgunDelay = 1.0f;

    public static bool useShotgun = false;

    /*[Header("Crossbow")]

    public GameObject Arrow;

    public Transform FirePos2;

    public AudioClip ShotSound2;

    public static int BowDamage = 75;

    public float BowDelay = 1.0f;

    public static bool useBow = false;*/

    //[Header("Another Weapon")]

    private SteamVR_TrackedObject controller;

    private AudioSource _audio;

    [Header("Shoot")]

    public SteamVR_Action_Boolean ShootAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    private Player player = null;

    private Hand pointerHand = null;

    public static float timer = 0;

    private float ShootDelay;

    public static bool useWeapon = false;

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
        if (useGun == true)
        {
            ShootDelay = GunDelay;
            useWeapon = true;
        }

        if (useBow == true)
        {
            ShootDelay = BowDelay;
            useWeapon = true;
        }

        if (useRevolver == true)
        {
            ShootDelay = RevolDelay;
            useWeapon = true;
        }

        if (useCure == true)
        {
            ShootDelay = CureDelay;
            useWeapon = true;
        }

        if (useShotgun == true)
        {
            ShootDelay = ShotgunDelay;
            useWeapon = true;
        }

        foreach (Hand hand in player.hands)
        {
            if (WasShootButtonPressed(hand))
            {
                if (useWeapon == true)
                {
                    if (useRevolver)
                    {
                        if (player.leftHand.BuildMode == false && player.rightHand.BuildMode == false && player.leftHand.ShopMode == false && player.rightHand.ShopMode == false)
                        {
                            if (timer > ShootDelay)
                            {
                                if (Revolverreloading > 0)
                                {
                                    Shoot();
                                    Revolverreloading--;
                                }
                                else
                                {
                                    RevolverReroad.Reroading();
                                    Revolverreloading = 6;
                                    timer = 0;
                                }
                            }
                        }
                    }
                    else if (useCure)
                    {
                        if (player.leftHand.BuildMode == false && player.rightHand.BuildMode == false && player.leftHand.ShopMode == false && player.rightHand.ShopMode == false)
                        {
                            if (timer > ShootDelay)
                            {
                                Shoot();
                                CureReroad.CureReroading();
                                timer = 0;
                            }
                        }
                    }
                    else if (timer > ShootDelay)
                    {
                        if (player.leftHand.BuildMode == false && player.rightHand.BuildMode == false && player.leftHand.ShopMode == false && player.rightHand.ShopMode == false)
                        {
                            Shoot();
                            timer = 0;
                        }
                    }
                }
            }
        }
        if (timer < 10.0) // 총 바꿀떄마다 생각해보기
        {
            timer += Time.deltaTime;
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
        if (useGun == true)
        {
            _audio.PlayOneShot(ShotSound1);

            Instantiate(Bullet, FirePos1.position, FirePos1.rotation);

        }

        if (useBow == true)
        {
            _audio.PlayOneShot(ShotSound2);

            Instantiate(Arrow, FirePos2.position, FirePos2.rotation);
        }

        if (useRevolver == true)
        {
            _audio.PlayOneShot(ShotSound3);

            Instantiate(Bullet3, FirePos3.position, FirePos3.rotation);
        }
        if (useCure == true)
        {
            _audio.PlayOneShot(ShotSound4);

            Instantiate(bullet4, FirePos4.position, FirePos4.rotation);
        }

        if (useShotgun == true)
        {
            _audio.PlayOneShot(ShotSound5);

            int xarrage1 = UnityEngine.Random.Range(-5, 5);
            int xarrage2 = UnityEngine.Random.Range(-5, 5);
            int xarrage3 = UnityEngine.Random.Range(-5, 5);
            int xarrage4 = UnityEngine.Random.Range(-5, 5);
            int xarrage5 = UnityEngine.Random.Range(-5, 5);
            int xarrage6 = UnityEngine.Random.Range(-5, 5);
            int xarrage7 = UnityEngine.Random.Range(-5, 5);
            int yarrage1 = UnityEngine.Random.Range(-5, 5);
            int yarrage2 = UnityEngine.Random.Range(-5, 5);
            int yarrage3 = UnityEngine.Random.Range(-5, 5);
            int yarrage4 = UnityEngine.Random.Range(-5, 5);
            int yarrage5 = UnityEngine.Random.Range(-5, 5);
            int yarrage6 = UnityEngine.Random.Range(-5, 5);
            int yarrage7 = UnityEngine.Random.Range(-5, 5);

            FirePos5_1.Rotate(xarrage1, yarrage1, 0);
            FirePos5_2.Rotate(xarrage2, yarrage2, 0);
            FirePos5_3.Rotate(xarrage3, yarrage3, 0);
            FirePos5_4.Rotate(xarrage4, yarrage4, 0);
            FirePos5_5.Rotate(xarrage5, yarrage5, 0);
            FirePos5_6.Rotate(xarrage6, yarrage6, 0);
            FirePos5_7.Rotate(xarrage7, yarrage7, 0);

            //FirePos5_1.rotation.Set(arrage1, 0, 0, 0);
            //FirePos5_2.rotation.Set(arrage2, 0, 0, 0);
            //FirePos5_3.rotation.Set(arrage3, 0, 0, 0);
            //FirePos5_4.rotation.Set(arrage4, 0, 0, 0);
            //FirePos5_5.rotation.Set(arrage5, 0, 0, 0);

            Instantiate(bullet5, FirePos5_1.position, FirePos5_1.rotation);
            Instantiate(bullet5, FirePos5_2.position, FirePos5_2.rotation);
            Instantiate(bullet5, FirePos5_3.position, FirePos5_3.rotation);
            Instantiate(bullet5, FirePos5_4.position, FirePos5_4.rotation);
            Instantiate(bullet5, FirePos5_5.position, FirePos5_5.rotation);
            Instantiate(bullet5, FirePos5_6.position, FirePos5_6.rotation);
            Instantiate(bullet5, FirePos5_7.position, FirePos5_7.rotation);

            FirePos5_1.Rotate(-xarrage1, -yarrage1, 0);
            FirePos5_2.Rotate(-xarrage2, -yarrage2, 0);
            FirePos5_3.Rotate(-xarrage3, -yarrage3, 0);
            FirePos5_4.Rotate(-xarrage4, -yarrage4, 0);
            FirePos5_5.Rotate(-xarrage5, -yarrage5, 0);
            FirePos5_6.Rotate(-xarrage6, -yarrage6, 0);
            FirePos5_7.Rotate(-xarrage7, -yarrage7, 0);
        }
    }
}