using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


// Generates dice roll and sets the destination of the player
public class RNG : MonoBehaviour
{

    public Button generate; // Dice roll button

    public TMPro.TextMeshProUGUI text; // text that displays dice roll value

    public GameObject Board; //board parent object that holds all board spaces
    public PlayerController Player; // player object (dice)
    private int rollValue; // value of the dice roll

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Dice").GetComponent<PlayerController>(); // Get the dice player object
        generate.onClick.AddListener(generateNumberAndMove); // Add onClick event for the button
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Generate Number and move forward
    void generateNumberAndMove()
    {
        rollValue = Random.Range(1, 7); //"roll" a dice. Gets a number inclusivley between 1-6

        text.text = "You rolled a: " + rollValue; // Set the UI text

        if (!(GameController.control.spaceOn + rollValue > Player.boardSpaces.Count - 1)) // if not out of range, set the dice to the specified spot and indicate it is time to load the next scene
        {
            GameController.control.spaceOn += rollValue;
            switch (Board.transform.GetChild(GameController.control.spaceOn).gameObject.GetComponent<SpaceController>().modifierType) // Do action depending on modifier type of space
            {
                case SpaceController.Modifier.AddDice:
                    GameController.control.numberOfDice++;
                    break;

                case SpaceController.Modifier.RemoveDice:
                    GameController.control.numberOfDice--;
                    break;

                case SpaceController.Modifier.SpeedUp:
                    GameController.control.diceSpeed++;
                    break;

                case SpaceController.Modifier.SpeedDown:
                    GameController.control.diceSpeed--;
                    break;
                case SpaceController.Modifier.Enemy:
                    Player.loadNewScene = true;
                    break;
                case SpaceController.Modifier.Nothing:
                    break;

            }
        }
    }


}
