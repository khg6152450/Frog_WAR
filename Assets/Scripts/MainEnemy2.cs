﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainEnemy2 : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 20;
    private Animator anim;
    public static bool clicked = false;

    public GameObject deathEffect;


    public float startSpeed = 5f;

    [HideInInspector]
    public float speed;

    int i = 0;
    
    void Start()
    {
        wavepointIndex = 20;

        speed = startSpeed;
        target = Waypoints.points[wavepointIndex];
        anim = GetComponent<Animator>();
        Jump();
    }

    void Update()
    {
        if(i == 0)
        {
            LookDir();
            i++;
        }

        if (Vector3.Distance(transform.position, target.position) >= 0.04f)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }        

        if (Vector3.Distance(transform.position, target.position) <= 0.04f)
        {
            Idle();
        }

        if(clicked == true)
        {
            Smashed();
            Guts();
            StartCoroutine(wait3());

            StartCoroutine(wait2());
        }
    }

    void LookDir()
    {
        Vector3 vec = target.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }

    public void Idle()
    {
        RootMotion();
        anim.SetTrigger("Idle");
    }

    public void Swim()
    {
        RootMotion();
        anim.SetTrigger("Swim");
    }

    public void Crawl()
    {
        RootMotion();
        anim.SetTrigger("Crawl");
    }

    public void Smashed()
    {
        speed = 0;
        startSpeed = 0;


        transform.gameObject.tag = "Dead";

        RootMotion();
        anim.SetTrigger("Smashed");
        Guts();
    }

    public void Jump()
    {
        RootMotion();
        anim.SetTrigger("Jump");
    }

    public void Guts()
    {
        Invoke("SpreadGuts", 0.1f);
    }

    void RootMotion()
    {
        if (anim.applyRootMotion)
        {
            anim.applyRootMotion = false;
        }
    }

    IEnumerator wait2()
    {
        yield return new
        WaitForSeconds(1f);

        yield return 0;

        Die();
    }

    IEnumerator wait3()
    {
        yield return new
        WaitForSeconds(0f);

        yield return 0;
        Die();
    }

    void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
        //getMoney();
    }
}
