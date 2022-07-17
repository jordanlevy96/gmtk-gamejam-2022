using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startGame()
    {
        //TODO: Needs to be changed for final build to be SceneManager.LoadSceneAsync
        EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Level/Scenes/Board.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }

}
