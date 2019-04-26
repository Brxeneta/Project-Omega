using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour
{
    public int health = 500;
    public int Experience = 0;
    public int Gold = 0;
    public int atk;
    public GameObject healthUI;
    public GameObject goldUI;
    public GameObject expUI;
    public GameObject shopHealthUI;
    public GameObject shopAttackUI;
    public GameObject shot;
    private void Start()
    {
        atk = shot.GetComponent<flameShotProperties>().damage;
        /*health = PlayerPrefs.GetInt("Health", health);
        Experience = PlayerPrefs.GetInt("Experience", Experience);
        Gold = PlayerPrefs.GetInt("Gold", Gold);
        atk = PlayerPrefs.GetInt("atk", atk);*/
    }

    // Update is called once per frame
    void Update()
    {
        healthUI.gameObject.GetComponent<Text>().text = ("Health: " + health);
        goldUI.gameObject.GetComponent<Text>().text = ("Gold: " + Gold);
        expUI.gameObject.GetComponent<Text>().text = ("Exp: " + Experience);
        shopHealthUI.gameObject.GetComponent<Text>().text = ("" + health);
        shopAttackUI.gameObject.GetComponent<Text>().text = ("" + atk);

        PlayerPrefs.SetInt("Health", health);
        PlayerPrefs.SetInt("Experience", Experience); ;
        PlayerPrefs.SetInt("Gold", Gold);
        PlayerPrefs.SetInt("atk", atk);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player Took Damage! Current Health: " + health);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy" || col.gameObject.tag == "spikes")
        {
            TakeDamage(1);
        }
        if (col.gameObject.tag == "Gold")
        {
            Gold += 1;
            Experience += 50;
            Destroy(col.gameObject);
        }
    }

    public void AddHealth()
    {
        if (Gold >= 2)
        {
            health += 100;
            Gold -= 2;
        }

    }

    public void AddAttack()
    {
        if (Gold >= 5 && Experience >= 30)
        {
            atk++;
            Gold -= 10;
            Experience -= 40;
        }
    }

}
