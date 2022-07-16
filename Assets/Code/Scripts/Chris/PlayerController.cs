using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform movePoint;
    public int spaceOn;

    public UnityEngine.GameObject Board;
    private Transform[] boardSpaces;

    // Start is called before the first frame update
    void Start()
    {
        //To keep things organized, remove parent
        movePoint.parent = null;

        //Get transforms of board spaces
        boardSpaces = Board.GetComponentsInChildren<Transform>().Where(o => o.tag == "MovePoint").ToArray(); //Using System.Linq "Where" method nicely iterates the array dn does the operations specified

        spaceOn = 0;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {


            if (Input.GetAxisRaw("Horizontal") == 1f && !(spaceOn + 1 > boardSpaces.Length - 1))
            {
                spaceOn++;
                movePoint.position = boardSpaces[spaceOn].position;
            }
            if (Input.GetAxisRaw("Horizontal") == -1f && !(spaceOn - 1 < 0))
            {
                spaceOn--;
                movePoint.position = boardSpaces[spaceOn].position;
            }

            if (Input.GetAxisRaw("Vertical") == 1f && !(spaceOn + 2 > boardSpaces.Length - 1))
            {
                spaceOn += 2;
                movePoint.position = boardSpaces[spaceOn].position;
            }
            if (Input.GetAxisRaw("Vertical") == -1f && !(spaceOn - 2 < 0))
            {
                spaceOn -= 2;
                movePoint.position = boardSpaces[spaceOn].position;
            }
        }
    }
}
