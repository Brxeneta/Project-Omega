using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class questGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled() 
    {
        if (goalType == GoalType.Kill)
        {
            currentAmount++;
            Debug.Log("QuestProgressed!");
        }
    }

    public void ItemCollected()
    {
        if (goalType == GoalType.Gathering)
        {
            currentAmount++;
        }
    }
}

public enum GoalType
{
    Kill,
    Gathering
}
