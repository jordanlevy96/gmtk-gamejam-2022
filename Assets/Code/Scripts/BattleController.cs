using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    public Button startButton;
    public GameObject enemyStartDie;
    public GameObject playerStartDie;

    private int numPlayerDice;
    private int numEnemyDice;
    private GameObject[] enemyDice;
    private GameObject[] playerDice;

    private bool rollingPlayerDice;

    private int enemyLastRoll;
    private int playerLastRoll;

    private int enemyHealth;
    public GameObject enemyHealthDisplay;
    private int playerHealth;
    public GameObject playerHealthDisplay;

    public GameObject playerDamage;
    public GameObject enemyDamage;

    public GameObject victory;

    private void UpdateNumberInText(GameObject go, int newVal)
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
        // make sure damage is hidden before turn starts
        playerDamage.SetActive(false);
        enemyDamage.SetActive(false);

        // set up enemy sprite
        GameObject.Find("Enemy").GetComponent<SpriteRenderer>().sprite = GameController.control.enemySprite;


        // set up buttons
        startButton.onClick.AddListener(StartTurn);
        victory.SetActive(false); // make sure victory stuff is hidden
        Button victoryButton = (Button) victory.GetComponentInChildren(typeof(Button), true);
        victoryButton.onClick.AddListener(ReturnToBoard);

        // set up dice and health
        numPlayerDice = GameController.control.numPlayerDice;
        numEnemyDice = GameController.control.numEnemyDice;

        enemyDice = new GameObject[numEnemyDice];
        SpawnDice(numEnemyDice, enemyStartDie, enemyDice);
        enemyHealth = numEnemyDice * 6;
        UpdateNumberInText(enemyHealthDisplay, enemyHealth);
        
        playerDice = new GameObject[numPlayerDice];
        SpawnDice(numPlayerDice, playerStartDie, playerDice);
        playerHealth = numPlayerDice * 6;
        UpdateNumberInText(playerHealthDisplay, playerHealth);
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
        rollingPlayerDice = true; 
        UpdateNumberInText(playerDamage, 0);
        playerDamage.SetActive(true);

        foreach (GameObject die in playerDice)
        {
            PlayerDieRoller roll = die.GetComponent<PlayerDieRoller>();
            roll.StartRolling();
        }
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
    }

    private void StartTurn()
    {
        // hide damage rolls from last turn
        playerDamage.SetActive(false);
        enemyDamage.SetActive(false);

        // start both sets of dice rolling at once
        StartCoroutine("RollEnemyDice");
        RollPlayerDice();
    }

    private void ApplyDamage()
    {
        playerHealth -= enemyLastRoll;
        UpdateNumberInText(playerHealthDisplay, playerHealth);
        enemyHealth -= playerLastRoll;
        UpdateNumberInText(enemyHealthDisplay, enemyHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (rollingPlayerDice)
        {
            int playerTotal = 0;
            int diceRolled = 0;
            foreach (GameObject die in playerDice)
            {
                PlayerDieRoller roll = die.GetComponent<PlayerDieRoller>();
                if (roll.finalVal == -1)
                {
                    break;
                }
                diceRolled++;
                playerTotal += roll.finalVal;
                UpdateNumberInText(playerDamage, playerTotal);
            }

            if (diceRolled == numPlayerDice)
            {
                rollingPlayerDice = false;
                playerLastRoll = playerTotal;
                foreach (GameObject die in playerDice)
                {
                    PlayerDieRoller roll = die.GetComponent<PlayerDieRoller>();
                    roll.ResetDie();
                }
                StartCoroutine("FinalizeTurn", enemyDice);
            }
        }

        if (playerHealth <= 0)
        {
            // game over
            // TODO: send back to main menu (which doesn't exist at time of writing)
            Debug.Log("Game over");
            startButton.gameObject.SetActive(false); // prevent user from continuing despite being dead
        }
        else if (enemyHealth <= 0)
        {
            victory.SetActive(true);
            startButton.gameObject.SetActive(false);
        }
    }

    // stops all the enemy dice rolling, then applies damage for all rolls
    private IEnumerator FinalizeTurn(GameObject[] dice)
    {
        int enemyTotal = 0;
        foreach (GameObject die in dice)
        {
            EnemyDieRoller roll = die.GetComponent<EnemyDieRoller>();
            roll.StopRolling();
            yield return new WaitForSeconds(0.2f);
            enemyTotal += roll.finalVal;
        }

        enemyLastRoll = enemyTotal;
        UpdateNumberInText(enemyDamage, enemyLastRoll);
        enemyDamage.SetActive(true);

        // apply damage after enemy logic is handled
        ApplyDamage();
    }

    private void ReturnToBoard()
    {
        EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Level/Scenes/Board.unity", new LoadSceneParameters(LoadSceneMode.Single));
    }
}
