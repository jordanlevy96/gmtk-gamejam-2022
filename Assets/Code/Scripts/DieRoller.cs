using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// base code from https://www.youtube.com/watch?v=JgbJZdXDNtg

public class DieRoller : MonoBehaviour
{

    // Array of dice sides sprites to load from Resources folder
    public Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private Image image;

    public int finalVal;

    // Use this for initialization
    private void Start()
    {

        // Assign Renderer component
        image = GetComponent<Image>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        // diceSides = Resources.LoadAll<Sprite>("DieSides/");
    }

    // If you left click over the dice then RollTheDice coroutine is started
    public void TriggerRoll()
    {
        StartCoroutine("EnemyRoll");
    }

    // Coroutine that rolls the dice for the enemy
    public IEnumerator EnemyRoll()
    {
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalSide = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 5);

            // Set sprite to upper face of dice from array according to random value
            image.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalSide = randomDiceSide + 1;

        // Show final dice value in Console
        Debug.Log(finalSide);

        finalVal = finalSide;
    }
}