using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Button StartBattle;
    public GameObject enemyStartDie;
    public GameObject playerStartDie;

    private int numPlayerDice;
    private int numEnemyDice;
    private GameObject[] enemyDice;
    private GameObject[] playerDice;

    private int enemyLastRoll;
    private int playerLastRoll;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Battle Controller");
        StartBattle.onClick.AddListener(RollDice);
        numPlayerDice = GameController.control.numPlayerDice;
        numEnemyDice = GameController.control.numEnemyDice;

        enemyDice = new GameObject[numEnemyDice];
        SpawnDice(numEnemyDice, enemyStartDie, enemyDice);

        playerDice = new GameObject[numPlayerDice];
        SpawnDice(numPlayerDice, playerStartDie, playerDice);
    }

    private void SpawnDice(int numDice, GameObject startDie, GameObject[] dice)
    {
        Vector3 originalPos = startDie.transform.position;
        float offsetY = startDie.transform.localPosition.y * -0.65f;

        dice[0] = startDie;

        for (int i = 1; i < numDice; i++)
        {
            GameObject newDie = Object.Instantiate(startDie, startDie.transform.parent);
            newDie.transform.Translate(new Vector3(0f, offsetY * i, 0f));
            dice[i] = newDie;
        }

    }

    private void RollPlayerDice()
    {
        int total = 0;

        foreach (GameObject die in playerDice)
        {
            PlayerDieRoller roll = die.GetComponent<PlayerDieRoller>();
            roll.StartRolling();
            total += roll.finalVal;
        }

        playerLastRoll = total;
    }

    private void RollEnemyDice()
    {
        int total = 0;

        foreach (GameObject die in enemyDice)
        {
            EnemyDieRoller roll = die.GetComponent<EnemyDieRoller>();
            roll.TriggerRoll();
            total += roll.finalVal;
        }

        enemyLastRoll = total;
    }

    private void RollDice()
    {
        StartCoroutine("RollEnemyDice");
        // StartCoroutine("RollPlayerDice");
        RollPlayerDice();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
