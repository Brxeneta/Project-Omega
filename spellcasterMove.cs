using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellcasterMove : MonoBehaviour
{
    public Transform firePoint;
    public GameObject orbPrefab;
    public GameObject self;
    public bool enemyAlive;
    public float range;
    public Transform player;
    bool inShootingLoop = false;
    public bool facingRight = false;
    public int hp;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = self.GetComponent<enemyHealth>().Health;

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        if (player.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();
        if (hp <= 0)
        {
            enemyAlive = false;
        }
    }

    public void Shoot()
    {
        if (!inShootingLoop)
            StartCoroutine("ShootContinuous");
        if (!enemyAlive)
            StartCoroutine("Spawn");
    }
    void Flip()
    {
        //here your flip funktion, as example
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }

    public IEnumerator ShootContinuous()
    {
        inShootingLoop = true;
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(player.position, transform.position) <= range)
        {
            //GetComponent<Animator>().SetBool("isAttacking", true);
            Instantiate(orbPrefab, firePoint.position, firePoint.rotation);
            //attackSound.Play();
        }
        else
        {
            //GetComponent<Animator>().SetBool("isAttacking", false);
        }
        yield return new WaitForSeconds(0.5f);
        inShootingLoop = false;
    }

    public IEnumerator Spawn()
    {
        self.SetActive(true);
        enemyAlive = true;
        yield return new WaitForSeconds(0.5f);
    }
}
