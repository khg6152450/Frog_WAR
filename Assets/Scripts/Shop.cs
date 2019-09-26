using UnityEngine;
using UnityEngine.UI;



public class Shop : MonoBehaviour
{
    public TurretBlueprint stadnardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    [Header("Gun")]
    public GameObject gun;
    public int GunPrice = 500;
    public GameObject GunDetail;
    public Text GunDetailText;
    public Text GunBuyText;
    public GameObject GunCrossHair;
    public int GunText1 = 30;
    public float GunText2 = 0.1f;
    public int GunText3 = 500;
    public static bool GetGun = false;


    [Header("Bow")]
    public GameObject bow;
    public int BowPrice = 200;
    public GameObject BowDetail;
    public Text BowBuyText;
    public GameObject BowCrossHair;
    public Text BowDetailText;
    public int BowText1 = 75;
    public float BowText2 = 1.0f;
    public int BowText3 = 200;
    public static bool GetBow = false;

    [Header("Revolver")]
    public GameObject revolver;
    public int RevolverPrice = 100;
    public GameObject RevolverDetail;
    public Text RevolverBuyText;
    public GameObject RevolverCrossHair;
    public Text RevolverDetailText;
    public int RevolverText1 = 19;
    public int RevolverText3 = 100;
    public static bool GetRevolver = false;

    [Header("Cure")]
    public GameObject cure;
    public int CurePrice = 1000;
    public GameObject CureDetail;
    public Text CureBuyText;
    public GameObject CureGunCrossHair;
    public Text CureDetailText;
    public float CureText2 = 7.0f;
    public int CureText3 = 1000;
    public static bool GetCure = false;

    [Header("Shotgun")]
    public GameObject shotgun;
    public int ShotgunPrice = 1000;
    public GameObject ShotgunDetail;
    public Text ShotgunBuyText;
    public GameObject ShotgunCrossHair;
    public Text ShotgunDetailText;
    public int ShotgunText1 = 20;
    public float ShotgunText2 = 1.0f;
    public int ShotgunText3 = 500;
    public static bool GetShotgun = false;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStanderdTureet()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(stadnardTurret);
    }

    public void SelectMissileTureet()
    {
        Debug.Log("Missile Turret Selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserTureet()
    {
        Debug.Log("Laser Turret Selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }

    //gunbuy

    public void GunChange()
    {
        if(GetGun == false)
        {
            if (PlayerStats.Money >= GunPrice)
            {
                PlayerStats.Money -= GunPrice;
                GetGun = true;
                GunBuyText.text = "Upgrade";
                GunDetailText.text = string.Format("    Damage : "+GunText1+ "\n    Delay: " + GunText2 + "s\n    Upgrade:\n    $" + GunText3);
            }
        }
        else
        {
            if (PlayerStats.Money >= GunText3)
            {
                PlayerStats.Money -= GunText3;
                GunText1 += 1;
                WeaponCtrl.GunDamage += 1;
                GunText3 += 50;
                GunDetailText.text = string.Format("    Damage : " + GunText1 + "\n    Delay: " + GunText2 + "s\n    Upgrade:\n    $" + GunText3);
            }
        }
    }

    public void GunAplly()
    {
        if (GetGun == false)
        {
            //구매 안했엉
        }
        else
        {
            gun.SetActive(true);
            bow.SetActive(false);
            revolver.SetActive(false);
            cure.SetActive(false);
            shotgun.SetActive(false);
            WeaponCtrl.useBow = false;
            WeaponCtrl.useGun = true;
            WeaponCtrl.useRevolver = false;
            WeaponCtrl.useCure = false;
            WeaponCtrl.useShotgun = false;
            GunCrossHair.SetActive(true);
            BowCrossHair.SetActive(false);
            RevolverCrossHair.SetActive(false);
            CureGunCrossHair.SetActive(false);
            ShotgunCrossHair.SetActive(false);
        }
    }

    public void GunDetailView()
    {
        GunDetail.SetActive(true);
        BowDetail.SetActive(false);
        RevolverDetail.SetActive(false);
        CureDetail.SetActive(false);
        ShotgunDetail.SetActive(false);
    }

    //bowbuy

    public void BowChange()
    {
        if (GetBow == false)
        {
            if (PlayerStats.Money >= BowPrice)
            {
                PlayerStats.Money -= BowPrice;
                GetBow = true;
                BowBuyText.text = "Upgrade";
            }
        }
        else
        {
            if (PlayerStats.Money >= BowText3)
            {
                PlayerStats.Money -= BowText3;
                BowText1 += 5;
                WeaponCtrl.BowDamage += 5;
                if (BowText2 >= 0.5f)
                {
                    BowText2 -= 0.1f;
                    WeaponCtrl.BowDelay -= 0.1f;
                }
                BowText3 += 100;
                BowDetailText.text = string.Format("    Damage : " + BowText1 + "\n    Delay: 0." + Mathf.Round(BowText2 * 10) + "s\n    Upgrade:\n    $" + BowText3);
            }
        }
    }

    public void BowApply()
    {
        if (GetBow == false)
        {
            //구매해
        }
        else
        {
            gun.SetActive(false);
            bow.SetActive(true);
            revolver.SetActive(false);
            cure.SetActive(false);
            shotgun.SetActive(false);
            WeaponCtrl.useBow = true;
            WeaponCtrl.useGun = false;
            WeaponCtrl.useRevolver = false;
            WeaponCtrl.useCure = false;
            WeaponCtrl.useShotgun = false;
            GunCrossHair.SetActive(false);
            BowCrossHair.SetActive(true);
            RevolverCrossHair.SetActive(false);
            CureGunCrossHair.SetActive(false);
            ShotgunCrossHair.SetActive(false);
        }
    }

    public void BowDetailView()
    {
        GunDetail.SetActive(false);
        BowDetail.SetActive(true);
        RevolverDetail.SetActive(false);
        CureDetail.SetActive(false);
        ShotgunDetail.SetActive(false);
    }

    //revolverbuy

    public void RevolverChange()
    {
        if (GetRevolver == false)
        {
            if (PlayerStats.Money >= RevolverPrice)
            {
                PlayerStats.Money -= RevolverPrice;
                GetRevolver = true;
                RevolverBuyText.text = "Upgrade";
            }
        }
        else
        {
            if (PlayerStats.Money >= RevolverText3)
            {
                PlayerStats.Money -= RevolverText3;
                RevolverText1 += 1;
                WeaponCtrl.RevolDamage += 1;
                RevolverText3 += 100;
                RevolverDetailText.text = string.Format("    Damage : "+RevolverText1+"\n    Delay: 0.1s\n    Reroad: 1.5s\n    Price: $"+ RevolverText3);
            }
        }
    }

    public void RevolverApply()
    {
        if (GetRevolver == false)
        {
            //구매해
        }
        else
        {
            gun.SetActive(false);
            bow.SetActive(false);
            revolver.SetActive(true);
            cure.SetActive(false);
            shotgun.SetActive(false);
            WeaponCtrl.useBow = false;
            WeaponCtrl.useGun = false;
            WeaponCtrl.useRevolver = true;
            WeaponCtrl.useCure = false;
            WeaponCtrl.useShotgun = false;
            GunCrossHair.SetActive(false);
            BowCrossHair.SetActive(false);
            RevolverCrossHair.SetActive(true);
            CureGunCrossHair.SetActive(false);
            ShotgunCrossHair.SetActive(false);
        }
    }

    public void RevolverDetailView()
    {
        GunDetail.SetActive(false);
        BowDetail.SetActive(false);
        RevolverDetail.SetActive(true);
        CureDetail.SetActive(false);
        ShotgunDetail.SetActive(false);
    }

    //curebuy

    public void CureChange()
    {
        if (GetCure == false)
        {
            if (PlayerStats.Money >= CurePrice)
            {
                PlayerStats.Money -= CurePrice;
                GetCure = true;
                CureBuyText.text = "Hold";
            }
        }
        else
        {
            
        }
    }

    public void CureApply()
    {
        if (GetCure == false)
        {
            //구매해
        }
        else
        {
            gun.SetActive(false);
            bow.SetActive(false);
            revolver.SetActive(false);
            cure.SetActive(true);
            shotgun.SetActive(false);
            WeaponCtrl.useBow = false;
            WeaponCtrl.useGun = false;
            WeaponCtrl.useRevolver = false;
            WeaponCtrl.useCure = true;
            WeaponCtrl.useShotgun = false;
            GunCrossHair.SetActive(false);
            BowCrossHair.SetActive(false);
            RevolverCrossHair.SetActive(false);
            CureGunCrossHair.SetActive(true);
            ShotgunCrossHair.SetActive(false);

            WeaponCtrl.timer = 7f;
        }
    }

    public void CureDetailView()
    {
        GunDetail.SetActive(false);
        BowDetail.SetActive(false);
        RevolverDetail.SetActive(false);
        CureDetail.SetActive(true);
        ShotgunDetail.SetActive(false);
    }

    public void ShotgunChange()
    {
        if (GetShotgun == false)
        {
            if (PlayerStats.Money >= ShotgunPrice)
            {
                PlayerStats.Money -= ShotgunPrice;
                GetShotgun = true;
                ShotgunBuyText.text = "Upgrade";
            }
        }
        else
        {
            if (PlayerStats.Money >= ShotgunText3)
            {
                PlayerStats.Money -= ShotgunText3;
                ShotgunText1 += 1;
                WeaponCtrl.ShotgunDamage += 1;
                if (ShotgunText2 >= 0.3f)
                {
                    ShotgunText2 -= 0.1f;
                    WeaponCtrl.ShotgunDelay -= 0.1f;
                }
                ShotgunText3 += 100;
                ShotgunDetailText.text = string.Format("    Damage : " + ShotgunText1 + "\n    Delay: 0." + Mathf.Round(ShotgunText2 * 10) + "s\n    Upgrade:\n    $" + ShotgunText3);
            }
        }
    }

    public void ShotgunApply()
    {
        if (GetShotgun == false)
        {
            //구매해
        }
        else
        {
            gun.SetActive(false);
            bow.SetActive(false);
            revolver.SetActive(false);
            cure.SetActive(false);
            shotgun.SetActive(true);
            WeaponCtrl.useBow = false;
            WeaponCtrl.useGun = false;
            WeaponCtrl.useRevolver = false;
            WeaponCtrl.useCure = false;
            WeaponCtrl.useShotgun = true;
            GunCrossHair.SetActive(false);
            BowCrossHair.SetActive(false);
            RevolverCrossHair.SetActive(false);
            CureGunCrossHair.SetActive(false);
            ShotgunCrossHair.SetActive(true);
        }
    }

    public void ShotgunDetailView()
    {
        GunDetail.SetActive(false);
        BowDetail.SetActive(false);
        RevolverDetail.SetActive(false);
        CureDetail.SetActive(false);
        ShotgunDetail.SetActive(true);
    }
}
