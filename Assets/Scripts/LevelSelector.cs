using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    public GameObject Selector;
    public GameObject player1;
    public GameObject player2;
    public GameObject Teleporting;
    public GameObject TeleportingArea1;
    public GameObject TeleportingArea2;
    public GameObject TeleportingArea3;
    public GameObject BuildUI1;

    public SceneFader fader;

    public Button[] levelButtons;

    string level01 = "Level01";
    string level02 = "Level02";
    string level03 = "Level03";

    void Start()
    {
        // default == 1
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    public void Select(string levelName)
    {
        Debug.Log("OK");
        //fader.FadeTo(levelName);
        StartCoroutine(wait2(levelName));
    }

    IEnumerator wait2(string levelName)
    {
        yield return new
        WaitForSeconds(1f);

        Debug.Log("OK1");
        yield return 0;

        if (levelName == level01)
        {
            Debug.Log("OK2");
            Level1.SetActive(true);
            Selector.SetActive(false);
            BuildUI.stageLevel = 1;

            player1.SetActive(true);
            player2.SetActive(false);
            Teleporting.SetActive(true);
            TeleportingArea1.SetActive(true);
            BuildUI1.SetActive(true);
            Swich.bulldmode = true;

            //ui.SetActive(false);

            Swich.GameIsOver = false;
            Swich.GameStart = true;
            Swich.time = true;            

            Swich.Toggle();
        }

        if (levelName == level02)
        {
            Debug.Log("OK2");
            Level2.SetActive(true);
            Selector.SetActive(false);
            BuildUI.stageLevel = 2;

            player1.SetActive(true);
            player2.SetActive(false);
            Teleporting.SetActive(true);
            TeleportingArea2.SetActive(true);
            BuildUI1.SetActive(true);
            Swich.bulldmode = true;

            //ui.SetActive(false);

            Swich.GameIsOver = false;
            Swich.GameStart = true;
            Swich.time = true;

            Swich.Toggle();
        }

        if (levelName == level03)
        {
            Debug.Log("OK2");
            Level3.SetActive(true);
            Selector.SetActive(false);
            BuildUI.stageLevel = 3;

            player1.SetActive(true);
            player2.SetActive(false);
            Teleporting.SetActive(true);
            TeleportingArea3.SetActive(true);
            BuildUI1.SetActive(true);
            Swich.bulldmode = true;

            //ui.SetActive(false);

            Swich.GameIsOver = false;
            Swich.GameStart = true;
            Swich.time = true;

            Swich.Toggle();
        }
    }
}
