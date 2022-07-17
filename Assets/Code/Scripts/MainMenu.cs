using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject MainCanvas;
    public GameObject TutorialCanvas;

    // Start is called before the first frame update
    void Start()
    {
        MainCanvas.SetActive(true);
        TutorialCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // reset start of game values
        GameController.control.spaceOn = 0;
        GameController.control.numPlayerDice = 3;
        GameController.control.numEnemyDice = 1;
        GameController.control.enemySeed = Guid.NewGuid().GetHashCode();


        SceneManager.LoadScene(1);
    }  
    
    public void GoToCredits()
    {
        SceneManager.LoadScene(5);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleTutorial()
    {
        bool tutorialState = TutorialCanvas.activeSelf;
        MainCanvas.SetActive(tutorialState);
        TutorialCanvas.SetActive(!tutorialState);
    }

}
