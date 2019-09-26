using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RevolverReroad : MonoBehaviour
{
    private static Animator anim;

    static public RevolverReroad instance;

    public GameObject swiching;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {           
        anim = GetComponent<Animator>();        
    }

    void Update()
    {
        
    }    

    public static void Reroading()
    {
        RootMotion();
        anim.SetTrigger("Reroad");
        instance.StartCoroutine("Wait2");
    }

    static void  RootMotion()
    {
        if (anim.applyRootMotion)
        {
            anim.applyRootMotion = false;
        }
    }

    IEnumerator Wait2()
    {
        swiching.SetActive(false);

        yield return new
        WaitForSeconds(2f);
        yield return 0;

        swiching.SetActive(true);
    }
}
