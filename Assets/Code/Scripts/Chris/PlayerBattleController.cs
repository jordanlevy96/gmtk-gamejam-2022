using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBattleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Level/Scenes/Chris/GixTestScene.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }
    }
}
