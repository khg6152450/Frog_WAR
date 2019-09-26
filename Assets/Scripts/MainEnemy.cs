using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainEnemy : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    private Animator anim;    

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    Random rnum = new Random();
    
    void Start()
    {
        wavepointIndex = Random.Range(1, 10);

        speed = startSpeed;
        target = Waypoints.points[wavepointIndex];
        anim = GetComponent<Animator>();
        Jump();
    }

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
        if (wavepointIndex >= 11)
        {
            EndPath();
            //Destroy(gameObject);
            return;
        }

        wavepointIndex += 10;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        Destroy(gameObject);
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
}
