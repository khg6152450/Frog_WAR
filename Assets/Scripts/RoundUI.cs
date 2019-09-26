using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundUI : MonoBehaviour
{
    public Text RoundText;

    // Update is called once per frame
    void Update()
    {
        RoundText.text = PlayerStats.Rounds.ToString() + " R"; 
    }
}
