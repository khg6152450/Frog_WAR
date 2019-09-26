using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float BulletSpeed = 75.0f;
    
    private Transform target;
    public GameObject HitEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * BulletSpeed * Time.deltaTime;        
    }
}