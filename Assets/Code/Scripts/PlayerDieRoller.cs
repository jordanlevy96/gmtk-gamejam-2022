using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDieRoller : MonoBehaviour
{
    public Sprite[] diceSides;
    private bool rolling;
    private Image image;
    public int finalVal;

    // Start is called before the first frame update
    void Start()
    {
        rolling = false;
        this.GetComponent<Button>().onClick.AddListener(StopRolling);
        image = GetComponent<Image>();
        finalVal = -1;
    }

    public void ResetDie()
    {
        finalVal = -1;
    }

    private void StopRolling()
    {
        Debug.Log("Clicked!");
        rolling = false;
    }

    public void StartRolling()
    {
        StartCoroutine("HandleRoll");
    }

    public IEnumerator HandleRoll()
    {
        Debug.Log("Rolling Player die...");
        rolling = true;

        // die should loop through all values in a predictable order for the player to be able to stop
        int diceSide = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalSide = 0;

        // loop until clicked
        while (rolling)
        {
            diceSide = diceSide == 5 ? 0 : diceSide + 1;

            // Set sprite to upper face of dice from array according to next value
            image.sprite = diceSides[diceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        finalSide = diceSide + 1;

        finalVal = finalSide;
        Debug.Log("Finished rolling player die with value " + finalVal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
