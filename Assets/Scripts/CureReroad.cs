using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CureReroad : MonoBehaviour
{
    static public CureReroad instance;

    private static Animator anim;

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

    public static void CureReroading()
    {
        
        RootMotion();
        anim.SetTrigger("CureRoad");
        instance.StartCoroutine("Wait2");
    }

    static void RootMotion()
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
        WaitForSeconds(7f);
        yield return 0;

        swiching.SetActive(true);
    }
}
