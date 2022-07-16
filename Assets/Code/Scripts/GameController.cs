using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //This class will be mostly for global primitive variables
    // It will need to be attached to an empty "GameController" object in whatever scene the values are needed

    public static GameController control; // Static reference to GameController to save variables between scenes
    public int spaceOn; // The current board space the player is on

    public int numPlayerDice; // number of dice the player has
    public int speedMod; // Speed at which the dice rolls

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //spaceOn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
