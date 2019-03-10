using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultManager : MonoBehaviour
{
    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;

    void Start()
    {
        musicTitleUI.text = PlayerInformation.musicTitle;
        scoreUI.text = "" + PlayerInformation.score;
        maxComboUI.text = "" + PlayerInformation.maxCombo;
    }
    
    void Update()
    {
        
    }
}
