﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;

    public GameObject TutorialMenuPanel;

    public GameObject LevelSelectPanel;

    public GameObject CreditsPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu()
    {
        MainMenuPanel.SetActive(true);
        TutorialMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void LoadTutorialMenu()
    {
        MainMenuPanel.SetActive(false);
        TutorialMenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void LoadLevelSelectMenu()
    {
        MainMenuPanel.SetActive(false);
        TutorialMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }
}
