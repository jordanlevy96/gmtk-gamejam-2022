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
        //TODO: Needs to be changed for final build to be SceneManager.LoadSceneAsync
        SceneManager.LoadScene(1); // why async?
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
