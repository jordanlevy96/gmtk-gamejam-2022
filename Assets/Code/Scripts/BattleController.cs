using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Button StartBattle;
    public GameObject startingDie;

    private int numPlayerDice;
    private int numEnemyDice;
    private GameObject[] enemyDice;

    private int enemyLastRoll;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Battle Controller");
        StartBattle.onClick.AddListener(RollEnemyDice);
        numPlayerDice = GameController.control.numPlayerDice;
        numEnemyDice = GameController.control.numEnemyDice;
        SpawnEnemyDice(numEnemyDice);
    }

    private void SpawnEnemyDice(int numDice)
    {
        Vector3 originalPos = startingDie.transform.position;
        float offsetY = startingDie.transform.localPosition.y * -0.5f;

        enemyDice = new GameObject[numDice];
        enemyDice[0] = startingDie;

        for (int i = 1; i < numDice; i++)
        {
            GameObject newDie = Object.Instantiate(startingDie, startingDie.transform.parent);
            newDie.transform.Translate(new Vector3(0f, offsetY * i, 0f));
            enemyDice[i] = newDie;
        }

            }

    private void RollEnemyDice()
    {
        int total = 0;

        foreach (GameObject die in enemyDice)
        {
            DieRoller roll = die.GetComponent<DieRoller>();
            roll.TriggerRoll();
            total += roll.finalVal;
        }

        enemyLastRoll = total;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
