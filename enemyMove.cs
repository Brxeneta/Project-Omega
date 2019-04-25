using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public float enemySpeed;
    public float stoppingDistance;
    public int xMoveDirection;
    public int damage = 1;


    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;
    }

    void Flip()
    {
        if (xMoveDirection > 0)
        {
            xMoveDirection = -1;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            xMoveDirection = 1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "flipWall")
        {
            Flip();
        }
    }
}

