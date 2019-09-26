using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    public Color color = Color.green;

    public float radius = 0.02f;

    void OnDrawGizmos()
    {
        Gizmos.color = color;

        Gizmos.DrawSphere(transform.position, radius);
    }
}