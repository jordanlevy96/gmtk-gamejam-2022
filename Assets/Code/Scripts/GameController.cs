using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //This class will be mostly for global primitive variables
    // It will need to be attached to an empty "GameController" object in whatever scene the values are needed

    public static GameController control; // Static reference to GameController to save variables between scenes
    public int spaceOn; // The current board space the player is on

    public int numPlayerDice; // player dice pool
    public int numEnemyDice; // enemy dice pool

    public int speedMod;

    public int enemySeed;

    [HideInInspector]
    public Sprite enemySprite;
    [HideInInspector]
    public int enemyBattleType;

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
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicHandler>().PlayMusic();
        spaceOn = 0;
        numPlayerDice = 3;
        numEnemyDice = 1; // will get incremented, so first enemy has 2, next has 3, etc.
        enemySeed = Guid.NewGuid().GetHashCode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
