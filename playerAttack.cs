using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    public Transform firePoint;
    public Transform shieldPoint;
    public GameObject spellEffect;
    public GameObject fireShot;
    public GameObject shieldUp;

    public float attackInterval;
    public float attackIntervalAegis;
    private float elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > elapsedTime)
            {
                elapsedTime = Time.time + attackInterval;
                GetComponent<Animator>().SetBool("isAttacking", true);
                SpellEffect();
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (Time.time > elapsedTime)
            {
                elapsedTime = Time.time + attackInterval;
                GetComponent<Animator>().SetBool("isGuarding", true);
                SpellEffect2();
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("isGuarding", false);
        }
    }

    void SpellEffect()
    {
        Instantiate(spellEffect, firePoint.position, firePoint.rotation);
        Instantiate(fireShot, firePoint.position, firePoint.rotation);
    }
    void SpellEffect2()
    {
        Instantiate(shieldUp, shieldPoint.position, shieldPoint.rotation);
    }
}


