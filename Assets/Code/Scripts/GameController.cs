using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //This class will be mostly for global primitive variables
    // It will need to be attached to an empty "GameController" object in whatever scene the values are needed

    public static GameController control; // Static reference to GameController to save variables between scenes
    public int spaceOn; // The current board space the player is on
    public int statRoll; // Most recent number rolled TODO: remove

    public int numPlayerDice; // player dice pool
    public int numEnemyDice; // enemy dice pool

    public int speedMod;

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
        spaceOn = 0;
        numPlayerDice = 4;
        numEnemyDice = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
