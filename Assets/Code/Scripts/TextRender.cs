using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextRender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Stat Value: " + GameController.control.statRoll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
