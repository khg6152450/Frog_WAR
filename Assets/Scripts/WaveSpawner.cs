using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    public Wave[] waves;

    public Transform enemyPrefab;

    public Vector3 positionOffEnemyset;

    public Transform spawnPoint;
    public Transform spawnPoint2;

    public bool level2 = false;
    public Transform randomSpawn;
    public bool level3 = false;

    public float timeBetweenWaves = 20f;
    public static float countdown = 5f;

    public Text waveCountdownText;
    public Text waveCountdownText2;

    public GameManager gameManager;

    public static int waveIndex = PlayerStats.Rounds;

    public int roundCnt = 1;

    // Update is called once per frame

    /*void Start() // test
    {
        Swich.GameIsOver = false;
        Swich.GameStart = true;
        Swich.time = true;

        Swich.Toggle();
    }*/
    void Update()
    {
        if(waveIndex == 0)
        {
            this.enabled = true;
        }

        if (EnemiesAlive > 0)
        {
            return;
        }

        // 게임승리
        if (waveIndex == waves.Length)
        {
            gameManager.EndGame();
            // 새로운 웨이브가 나오지 않도록 함 ↓
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            if(Swich.GameStart == true)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:0.00}", countdown) ;
        waveCountdownText2.text = string.Format("{0:0.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        //Enemy spawn
        Wave wave = waves[waveIndex];

        wave.count = roundCnt;

        // 살아있는 적
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            int enemyNum = UnityEngine.Random.Range(1, 100);

            if(enemyNum >= 60)
            {
                wave.enemy = enemy1;
            } else if(enemyNum >= 35)
            {
                wave.enemy = enemy2;
            } else if(enemyNum >= 10)
            {
                wave.enemy = enemy3;
            } else if (enemyNum > 0)
            {
                wave.enemy = enemy4;
            }

            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        PlayerStats.Money += roundCnt * 50;

        roundCnt += 1;

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        if(level2 == true)
        {
            int enemyNum2 = UnityEngine.Random.Range(1, 100);
            if(enemyNum2 > 50)
            {
                randomSpawn = spawnPoint;
                Enemy.isLevel2 = false;
            } else
            {
                randomSpawn = spawnPoint2;
                Enemy.isLevel2 = true;
            }
        }
        else
        {
            randomSpawn = spawnPoint;
        }
        Instantiate(enemy, randomSpawn.position, randomSpawn.rotation);
    }
}
