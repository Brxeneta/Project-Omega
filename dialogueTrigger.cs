using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public bool isMerchant;
    public bool isQuestGiver;
    public dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<dialogueManager>().StartDialogue(dialogue);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            TriggerDialogue();
        }
    }
}
