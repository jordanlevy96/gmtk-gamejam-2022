using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{

    public GameObject Board; //board parent object that holds all board spaces
    [HideInInspector]
    public PlayerController Player; // player object (dice)
    private int rollValue; // value of the dice roll

    // Array of dice sides sprites to load from Resources folder
    public Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private Image rend;

    private int finalSide;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Dice").GetComponent<PlayerController>(); // Get the dice player object
        gameObject.GetComponent<Button>().onClick.AddListener(buttonClick); // Add onClick event for the button

        // Assign Renderer component
        rend = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buttonClick()
    {
            StartCoroutine("RollTheDice");
    }

    // Generate Number and move forward
    void generateNumberAndMove()
    {

        rollValue = finalSide;

        if (GameController.control.spaceOn + rollValue > Player.boardSpaces.Count - 1)// ifout of range, set the players rollvalue to thye max space
        {
            rollValue = Player.boardSpaces.Count - 1 - GameController.control.spaceOn;
        }

        Player.GetComponent<PlayerController>().rollValue = rollValue;
    }

    private IEnumerator RollTheDice()
    {

        gameObject.GetComponent<Button>().enabled = false; // disbale button while dice is rolling. Is re-enabled in player controller


        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 iterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 6);

            // Set sprite to upper face of dice from array according to random value
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalSide = randomDiceSide + 1;

        generateNumberAndMove();
    }
}
