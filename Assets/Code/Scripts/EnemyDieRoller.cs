using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// base code from https://www.youtube.com/watch?v=JgbJZdXDNtg

public class EnemyDieRoller : MonoBehaviour
{
    public Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private Image image;

    public int finalVal;
    public bool rolling;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void TriggerRoll()
    {
        StartCoroutine("EnemyRoll");
    }

    public void StopRolling()
    {
        rolling = false;
    }

    // Coroutine that rolls the dice for the enemy
    public IEnumerator EnemyRoll()
    {
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalSide = 0;

        rolling = true;
        // rolling stops when player is done rolling
        while (rolling)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 5);

            // Set sprite to upper face of dice from array according to random value
            image.sprite = diceSides[randomDiceSide];

            // Pause before next iteration
            yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalSide = randomDiceSide + 1;

        finalVal = finalSide;
    }
}