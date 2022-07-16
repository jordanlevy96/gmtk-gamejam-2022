using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Button StartBattle;
    public GameObject dice;

    private int playerDice;
    private int enemyDice;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Battle Controller");
        StartBattle.onClick.AddListener(dice.GetComponent<DieRoller>().TriggerRoll);
        playerDice = GameController.control.numPlayerDice;
        enemyDice = GameController.control.numEnemyDice;
        SpawnEnemyDice(enemyDice);
    }

    void SpawnEnemyDice(int numDice)
    {
        Vector3 originalPos = dice.transform.position;
        float offsetY = dice.transform.localPosition.y;

        GameObject newDie = Object.Instantiate(dice);
        newDie.transform.Translate(new Vector3(originalPos.x, offsetY, originalPos.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
