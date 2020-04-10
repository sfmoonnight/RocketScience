using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    public enum RewardType { Currency, EnergyCard, Collectible, Skin};
    public RewardType rewardType;
    public int number;

    public static Reward currency100 = new Reward(RewardType.Currency, 100);
    public Reward(RewardType rewardType, int number)
    {
        this.rewardType = rewardType;
        this.number = number;
    }

    /*public static Reward normalCollectingReward()
    {
        Reward reward = new Reward(RewardType.Currency, 100);
        return reward;
    }*/

    public void CollectReward()
    {
        switch (rewardType)
        {
            case RewardType.Currency:
                Toolbox.GetInstance().GetStatManager().gameState.money += number;
                break;
            case RewardType.EnergyCard:
                Toolbox.GetInstance().GetStatManager().gameState.telescopeEnergyCard += number;
                break;
            case RewardType.Collectible:
                Toolbox.GetInstance().GetStatManager().gameState.collected.Add(number);
                Toolbox.GetInstance().GetStatManager().gameState.notCollected.Remove(number);
                break;
            case RewardType.Skin:
                //TODO:
                break;
        }
    }
}
