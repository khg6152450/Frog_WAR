using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    public static bool isLevel2 = false;  // enemy shared
    private bool realLevel2 = false;
    private Animator anim;

    public bool isSwim = false;

    public float startSpeed = 10f;

    public GameObject guts;
    GameObject gutsEx;
    bool smashed = false;

    //static BulletCtrl bullet1;
    //private readonly float ShootDamage = bullet1.BulletDamage;
    public int ShootDamage;

    [HideInInspector]
    public float speed;

    public float startHealth = 0;
    public float health;

    public int worth = 50;

    public bool get = false;

    public GameObject deathEffect;

    public GameObject impactEffect;

    public int waveIndex;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        if(isLevel2 == true)
        {
            realLevel2 = true;
        }
        waveIndex = PlayerStats.Rounds;
        startHealth += (waveIndex * 5);
        speed = startSpeed;
        health = startHealth;
        if(realLevel2 == true)
        {
            target = Waypoints2.points[0];
        } else
        {
            target = Waypoints.points[0];
        }        
        anim = GetComponent<Animator>();
        if(isSwim == true)
        {
            Swim();
        } else
        {
            Jump();
        }
        
    }

    public void TakeDamage (float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0 && !isDead)
        {
            Smashed();

            StartCoroutine(wait2());
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        isDead = true;

        PlayerStats.Money += worth;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
        
        Destroy(gameObject);
        //getMoney();
    }

    //void getMoney()
    //{
    //    if(get == false)
    //        PlayerStats.Money += worth;

    //    get = true;
    //}

    void Update()
    {
        LookDir();

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
            LookDir();
            //Crawl();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            //Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        
        if (realLevel2 == true)
        {
            target = Waypoints2.points[wavepointIndex];
        }
        else
        {
            target = Waypoints.points[wavepointIndex];
        }
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            GameObject hitEffect = Instantiate(impactEffect, coll.transform.position, coll.transform.rotation);

            Destroy(coll.gameObject);

            if (WeaponCtrl.useBow == true)
            {
                ShootDamage = WeaponCtrl.BowDamage;
                TakeDamage(ShootDamage);
            } else if(WeaponCtrl.useGun == true)
            {
                ShootDamage = WeaponCtrl.GunDamage;
                TakeDamage(ShootDamage);
            } else if (WeaponCtrl.useRevolver == true)
            {
                ShootDamage = WeaponCtrl.RevolDamage;
                TakeDamage(ShootDamage);
            } else if (WeaponCtrl.useCure == true)
            {
                ShootDamage = WeaponCtrl.CureDamage;
                TakeDamage(ShootDamage);
            } else if (WeaponCtrl.useShotgun == true)
            {
                ShootDamage = WeaponCtrl.ShotgunDamage;
                TakeDamage(ShootDamage);
            }
        }
    }

    void LookDir()
    {
        Vector3 vec = target.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }

    public void Swim()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Swim");
    }

    public void Crawl()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Crawl");
    }

    public void Smashed()
    {
        speed = 0;
        startSpeed = 0;


        transform.gameObject.tag = "Dead";

        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Smashed");
        Guts();
    }

    public void Jump()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Jump");
    }

    public void Guts()
    {
        Invoke("SpreadGuts", 0.1f);
    }

    void SpreadGuts()
    {
        smashed = false;
        if (!smashed)
        {
            Instantiate(guts, transform.position, transform.rotation);
            smashed = true;
        }
    }

    void RootMotion()
    {
        if (anim.applyRootMotion)
        {
            anim.applyRootMotion = false;
        }
    }


    void DestroyGuts()
    {
        gutsEx = GameObject.FindGameObjectWithTag("Guts");
        if (gutsEx != null)
        {
            Destroy(gutsEx);
            //smashed = false;
        }        
    }

    IEnumerator wait2()
    {
        yield return new
        WaitForSeconds(1f);

        yield return 0;

        Die();
    }
}
