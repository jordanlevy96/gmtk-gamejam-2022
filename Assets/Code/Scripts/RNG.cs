using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


// Generates dice roll and sets the destination of the player
public class RNG : MonoBehaviour
{

    public Button generate; // Dice roll button

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

        

        if (GameController.control.spaceOn + rollValue > Player.boardSpaces.Count - 1)// ifout of range, set the players rollvalue to thye max space
        {

            rollValue = Player.boardSpaces.Count - 1 - GameController.control.spaceOn;


        }

        Player.GetComponent<PlayerController>().rollValue = rollValue;
    }


}
