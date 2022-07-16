using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform playerMovePoint;

    public GameObject Board;
    [HideInInspector]
    public Transform[] boardSpaces;
    public string spaceMovePointTag;
    [HideInInspector]
    public bool loadNewScene;

    

    // Start is called before the first frame update
    void Start()
    {
        //To keep things organized, remove parent
        playerMovePoint.parent = null;

        //Get transforms of board spaces
        boardSpaces = Board.GetComponentsInChildren<Transform>().Where(o => o.tag == spaceMovePointTag).ToArray(); //Using System.Linq "Where" method nicely iterates the array dn does the operations specified

        loadNewScene = false; //Set this when you are ready to load a new scene.

        playerMovePoint.position = boardSpaces[GameController.control.spaceOn].position;
        transform.position = boardSpaces[GameController.control.spaceOn].position; // Move the player to their last position before scene loads back in
    }

    // Update is called once per frame
    void Update()
    {
        //Gives the dice a smooth transition to the player's move point
        transform.position = Vector3.MoveTowards(transform.position, playerMovePoint.position, moveSpeed * Time.deltaTime);

        playerMovePoint.position = boardSpaces[GameController.control.spaceOn].position; // move the dice's move point to the board space

        if (Vector3.Distance(transform.position, playerMovePoint.position) <= .05f && loadNewScene)
        {

            

            
                //TODO: Needs to be changed for final build to be SceneManager.LoadSceneAsync
                EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Level/Scenes/Chris/BattleScreen.unity", new LoadSceneParameters(LoadSceneMode.Single));
            


            //if (Input.GetAxisRaw("Horizontal") == 1f && !(GameController.control.spaceOn + 1 > boardSpaces.Length - 1) && !loadNewScene)
            //{
            //    GameController.control.spaceOn++; // iterate the array of board spaces

            //    loadNewScene = true; // prepare to load scene

            //}
            //if (Input.GetAxisRaw("Horizontal") == -1f && !(GameController.control.spaceOn - 1 < 0) && !loadNewScene)
            //{
            //    GameController.control.spaceOn--;
            //}

            //if (Input.GetAxisRaw("Vertical") == 1f && !(GameController.control.spaceOn + 2 > boardSpaces.Length - 1) && !loadNewScene)
            //{
            //    GameController.control.spaceOn += 2;
            //}
            //if (Input.GetAxisRaw("Vertical") == -1f && !(GameController.control.spaceOn - 2 < 0) && !loadNewScene)
            //{
            //    GameController.control.spaceOn -= 2;
            //}
            
        }
    }
}
