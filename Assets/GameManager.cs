using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public static bool IsDead = false;

    void Start()
    {
        GameIsOver = false;
        IsDead = false;        
    }

    void Update()
    {
        if(PlayerStats.Lives <= 0 && IsDead == false)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Over!!");
        IsDead = true;
        GameIsOver = true;
    }
}
