using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    public GameObject diceReminder;
    public GameObject attackPower;

    public GameObject victory;

    public GameObject Enemy;

    public Sprite[] candymanAnim;
    public Sprite[] knightAnim;
    public Sprite[] carAnim;
    public Sprite[] cardsAnim;
    public Sprite[] devilAnim;

    private int enemyType;

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
        // 1 candyman, 2 knight, 3 car, 4 cards, 5 devil
        enemyType = GameController.control.enemyBattleType;
        switch (enemyType)
        {
            case 1:
                Enemy.GetComponent<Animator>().Play("Candyman Anim");
                break;
            case 2:
                Enemy.GetComponent<Animator>().Play("Knight Anim");
                break;
            case 3:
                Enemy.GetComponent<Animator>().Play("Car Anim");
                break;
            case 4:
                Enemy.GetComponent<Animator>().Play("Card Anim");
                break;
            case 5:
                Enemy.GetComponent<Animator>().Play("Devil Anim");
                break;
        }

        //GameObject.Find("CandymanSheet_0").GetComponent<SpriteRenderer>().

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

        // Turn the dice reminder off at the start
        diceReminder.gameObject.SetActive(false);
        // Turn off the attack power at the start
        attackPower.gameObject.SetActive(false);
    }

    private void SpawnDice(int numDice, GameObject startDie, GameObject[] dice)
    {
        Vector3 originalPos = startDie.transform.position;
        float relativeMove = -1.1f;
        float offsetY = startDie.transform.GetComponent<RectTransform>().rect.height * relativeMove;

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
        if (!attackPower.gameObject.activeSelf)
            attackPower.gameObject.SetActive(true);

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
        // Turn off the dice reminder
        if (diceReminder.gameObject.activeSelf)
            diceReminder.gameObject.SetActive(false);

        if (rollingPlayerDice)
        {
            // Disable the start turn button while the dice are rolling
            if (startButton.gameObject.activeSelf)
                startButton.gameObject.SetActive(false);

            // Remind the player to stop their dice
            if (!diceReminder.gameObject.activeSelf)
                diceReminder.gameObject.SetActive(true);
                

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
            StartCoroutine("GameOver"); // do in coroutine for a delay
            startButton.gameObject.SetActive(false); // prevent user from continuing despite being dead
        }
        else if (enemyHealth <= 0)
        {
            victory.SetActive(true);
            startButton.gameObject.SetActive(false);

            if (enemyType == 5) // 5 = devil = final boss
            {
                victory.GetComponentInChildren(typeof(Button), true).gameObject.SetActive(false);
                StartCoroutine("Victory");
            }

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

        // Turn the start turn button back on
        startButton.gameObject.SetActive(true);
    }


    //void animateEnemy()
    //{
    //    float animRate = 1.0f;
    //    Animator.play
    //}

    private void ReturnToBoard()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private IEnumerator GameOver()
    {
        //Debug.Log("Game Over");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    private IEnumerator Victory()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }
}

