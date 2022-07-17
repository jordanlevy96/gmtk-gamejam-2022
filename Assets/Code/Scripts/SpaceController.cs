using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{

    public Modifier modifierType;
    public Enemies enemyType;

    // Sprites for Enemies
    public Sprite devil;
    public Sprite candyman;
    public Sprite knight;
    public Sprite car;
    public Sprite cards;

    // Sprites for Other Status Effects
    public Sprite plusDice;
    public Sprite minusDice;
    public Sprite spdUp;
    public Sprite spdDown;

    public static System.Random rand = new System.Random(GameController.control != null ? GameController.control.enemySeed : 0);

    public enum Modifier { AddDice, RemoveDice, SpeedUp, SpeedDown, Nothing, Enemy };
    public enum Enemies { None, Candyman, Knight, Car, Cards, Devil }

    // Start is called before the first frame update
    void Start()
    {
        Transform curSprite = gameObject.transform.Find("SpotSprite");




        // Set the sprite of the space depending on the enum
        switch (modifierType)
        {
            case Modifier.AddDice:
                scaleNonEnemySprites();
                curSprite.GetComponent<SpriteRenderer>().sprite = plusDice;
                break;
            case Modifier.RemoveDice:
                scaleNonEnemySprites();
                curSprite.GetComponent<SpriteRenderer>().sprite = minusDice;
                break;
            case Modifier.SpeedDown:
                break;
            case Modifier.SpeedUp:
                break;
            case Modifier.Enemy:
                if (enemyType == Enemies.None)
                {
                    enemyType = randomizeEnemy();
                }

                switch (enemyType)
                {
                    case Enemies.Candyman:
                        curSprite.GetComponent<SpriteRenderer>().sprite = candyman;
                        break;
                    case Enemies.Knight:
                        curSprite.GetComponent<SpriteRenderer>().sprite = knight;
                        break;
                    case Enemies.Car:
                        curSprite.GetComponent<SpriteRenderer>().sprite = car;
                        break;
                    case Enemies.Cards:
                        curSprite.GetComponent<SpriteRenderer>().sprite = cards;
                        break;
                    case Enemies.Devil:
                        curSprite.GetComponent<SpriteRenderer>().sprite = devil;
                        break;
                }
                break;
            case Modifier.Nothing:
                break;
            default:
                break;
        }
    }

    void scaleNonEnemySprites()
    {
        Transform curSprite = gameObject.transform.Find("SpotSprite");
        curSprite.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        curSprite.transform.localPosition = new Vector3(0.0f, 0.75f, -0.01f);
    }

    // Set an enemy type if the spot is set to enemy and no type is predefined
    Enemies randomizeEnemy()
    {
        Enemies outType = (Enemies)rand.Next(1, 5);
        return outType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
