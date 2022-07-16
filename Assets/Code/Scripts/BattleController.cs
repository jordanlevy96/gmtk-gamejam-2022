using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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

    private int enemyHealth;
    public GameObject enemyHealthDisplay;
    private int playerHealth;
    public GameObject playerHealthDisplay;

    private void UpdateHealthField(GameObject go, int newVal)
    {
        TextMeshProUGUI textField = go.GetComponent<TextMeshProUGUI>();
        string oldText = textField.text.ToString();
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\\d+");
        string newText = regex.Replace(oldText, newVal.ToString());
        textField.text = newText;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartBattle.onClick.AddListener(RollDice);
        numPlayerDice = GameController.control.numPlayerDice;
        numEnemyDice = GameController.control.numEnemyDice;

        enemyDice = new GameObject[numEnemyDice];
        SpawnDice(numEnemyDice, enemyStartDie, enemyDice);
        enemyHealth = numEnemyDice * 6;
        UpdateHealthField(enemyHealthDisplay, enemyHealth);
        
        playerDice = new GameObject[numPlayerDice];
        SpawnDice(numPlayerDice, playerStartDie, playerDice);
        playerHealth = numPlayerDice * 6;
        UpdateHealthField(playerHealthDisplay, playerHealth);
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
