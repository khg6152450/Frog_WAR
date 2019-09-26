using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Vector3 positionOffEnemyset;

    public Transform spawnPoint;

    public int enemycount = 0;
    
    // Update is called once per frame
    void Update()
    {
        if(enemycount < 10)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemycount++;
        }        
    }
}
