using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RNG : MonoBehaviour
{

    public Button generate;

    public TMPro.TextMeshProUGUI text;

    public GameObject Board;
    public PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Dice").GetComponent<PlayerController>();
        generate.onClick.AddListener(generateNumberAndMove);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Generate Number and move forward one space
    void generateNumberAndMove()
    {
        GameController.control.statRoll = Random.Range(1, 7);

        text.text = "Value: " + GameController.control.statRoll;

        if (!(GameController.control.spaceOn + 1 > Player.boardSpaces.Length - 1))
        {
            GameController.control.spaceOn++;
            Player.loadNewScene = true; //Move forward and prepare to load scene
        }
    }


}
