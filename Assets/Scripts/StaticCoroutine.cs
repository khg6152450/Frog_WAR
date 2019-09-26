using UnityEngine;
using System;
using System.Collections;

public class StaticCoroutine : MonoBehaviour
{
    public static StaticCoroutine Instance;

    void Start()
    {
        Instance = this;
    }  

    public IEnumerator Wait2()
    {
        Debug.Log("Reroad Start");

        yield return new
        WaitForSeconds(7f);
        yield return 0;

        Debug.Log("Reroad End");
    }
}