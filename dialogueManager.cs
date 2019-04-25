using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject merchant;
    public GameObject questGiver;
    public GameObject player;
    public GameObject enemy;
    public Animator animator;

    public GameObject questWindow;
    public GameObject shopWindow;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text goldText;

    public Text shopCurrentHPText;
    public Text shopCurrentATKText;

    public Quest quest;
    public playerStats stats;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.expReward.ToString();
        goldText.text = quest.goldReward.ToString();
    }

    public void OpenShopWindow()
    {
        shopWindow.SetActive(true);
        shopCurrentHPText.text = stats.health.ToString();
        shopCurrentATKText.text = stats.atk.ToString();
    }

    public void CloseShopWindow()
    {
        shopWindow.SetActive(false);
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        // give to player 
        player.GetComponent<playerController>().quest = quest;

    }

    public void StartDialogue(dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (merchant.GetComponent<dialogueTrigger>().isMerchant == true && questGiver.GetComponent<dialogueTrigger>().isMerchant == false && merchant.GetComponent<dialogueTrigger>().isQuestGiver == false)
            {
                Debug.Log("OpeningShop");
                OpenShopWindow();
            }
            if (questGiver.GetComponent<dialogueTrigger>().isQuestGiver == true && merchant.GetComponent<dialogueTrigger>().isQuestGiver == false && questGiver.GetComponent<dialogueTrigger>().isMerchant == false)
            {
                Debug.Log("OpeningQuests");
                OpenQuestWindow();
            }

            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("End of Conversation");
    }
}
