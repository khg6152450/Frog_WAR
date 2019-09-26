using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Diagnostics;

public class CompleteLevel : MonoBehaviour
{
    //public string menuSceneName = "MainMenu";

    public string nextLevel = "MainMenu2";
    //public int levelToUnlock = 2;
    
    public SceneFader sceneFader;

    void Start()
    {
        GameManager.GameIsOver = false;
        GameManager.IsDead = false;
    }

    public void Continue()
    {
        // default == 2(1레벨 이후이기 때문에)
        //PlayerPrefs.SetInt("levelReached", levelToUnlock);
        //Debug.Log("동작이 되긴하니?1");
        //sceneFader.FadeTo(nextLevel);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //GameManager.GameIsOver = false;
        //GameManager.IsDead = false;

        Process.Start(Application.dataPath + "/../FrogWar.exe");
        Application.Quit();
    }

    public void Menu()
    {        
        Application.Quit();
        //PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }
}
