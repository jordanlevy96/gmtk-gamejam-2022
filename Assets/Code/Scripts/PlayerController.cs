using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f; // How fast the dice moves to its new point
    public Transform playerMovePoint; // The transform that the dice follows

    public GameObject Board; // Object holding all the board spaces
    [HideInInspector]
    public List<GameObject> boardSpaces = new List<GameObject>(); // Array of the spaces on the board
    public SpaceController[] modifiers;
    public string spaceMovePointTag; // Tag of the point the player should move to (used to find the point hovering above the board space)
    [HideInInspector]
    public bool loadNewScene; // Flag to indicate it is time to load the next scene
    
    private int spacesMovedThisTurn = 0; // Number of spaces moved this turn
    [HideInInspector]
    public int rollValue; // value of dice roll




    // Start is called before the first frame update
    void Start()
    {

        //To keep things organized, remove parent
        playerMovePoint.parent = null;

        rollValue = -1; //No movement on scene start

        GetAllChildren(); // Populate board spaces
        //Get transforms of board spaces
        //boardSpaces = Board.GetComponentsInChildren<Transform>().Where(o => o.tag == spaceMovePointTag).ToArray(); //Using System.Linq "Where" method nicely iterates the array dn does the operations specified

        loadNewScene = false; //Set this when you are ready to load a new scene.

        playerMovePoint.position = boardSpaces[GameController.control.spaceOn].transform.position;
        transform.position = boardSpaces[GameController.control.spaceOn].transform.position; // Move the player to their last position before scene loads back in
    }

    // Update is called once per frame
    void Update()
    {
        //Gives the dice a smooth transition to the player's move point
        transform.position = Vector3.MoveTowards(transform.position, playerMovePoint.position, moveSpeed * Time.deltaTime);

        playerMovePoint.position = boardSpaces[GameController.control.spaceOn].transform.GetChild(0).position; // move the dice's move point to the board space

        if (Vector3.Distance(transform.position, playerMovePoint.position) <= .05f) // if player is close to the move point
        {

            if (spacesMovedThisTurn == rollValue)
            {
                spacesMovedThisTurn = 0;
                rollValue = -1; // this will get reset by the next dice roll

                switch (Board.transform.GetChild(GameController.control.spaceOn).gameObject.GetComponent<SpaceController>().modifierType) // Do action depending on modifier type of space
                {
                    case SpaceController.Modifier.AddDice:
                        GameController.control.numPlayerDice++;
                        break;

                    case SpaceController.Modifier.RemoveDice:
                        GameController.control.numPlayerDice--;
                        break;

                    case SpaceController.Modifier.SpeedUp:
                        GameController.control.speedMod++;
                        break;

                    case SpaceController.Modifier.SpeedDown:
                        GameController.control.speedMod--;
                        break;
                    case SpaceController.Modifier.Enemy:
                        //TODO: Needs to be changed for final build to be SceneManager.LoadSceneAsync
                        EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Level/Scenes/BattleScreen.unity", new LoadSceneParameters(LoadSceneMode.Single));
                        break;
                    case SpaceController.Modifier.Nothing:
                        break;

                }
            }

            if (spacesMovedThisTurn < rollValue)
            {
                
                GameController.control.spaceOn++;
                spacesMovedThisTurn++;
                if(Board.transform.GetChild(GameController.control.spaceOn).gameObject.GetComponent<SpaceController>().modifierType == SpaceController.Modifier.Enemy)
                {
                    spacesMovedThisTurn = rollValue; // Stop at enemy spot
                }
                
            }
            

        }
    }

    private void GetAllChildren()
    {
        for (int i = 0; i < Board.transform.childCount; i++)
        {
            boardSpaces.Add(Board.transform.GetChild(i).gameObject);
        }
    }
}
