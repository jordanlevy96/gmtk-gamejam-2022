using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextRender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("DiceSpeedText"))
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Dice Speed: " + GameController.control.speedMod;
        }
        else if (CompareTag("DiceNumberText"))
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Number of Dice: " + GameController.control.numPlayerDice;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
