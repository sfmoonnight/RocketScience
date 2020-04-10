using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestIdentity { NoType, Collecting, TelescopeActivation};
    public QuestIdentity questIdentity;
    public List<Reward> rewards;
    public List<QuestCollectible> collectibles;
    public string text;
    public Vector2 coordinates;
    public Sprite questImage;
    //public bool completed;
    //public bool keyQuest;

    //---Normal quests have identity 0, key quests have negative identity
    //public int questIdentity;

    public Quest()
    {
        questIdentity = QuestIdentity.NoType;
        rewards = new List<Reward>();
        collectibles = new List<QuestCollectible>();
        text = "";
        coordinates = new Vector2();
        questImage = null;
        //completed = false;
    }
    public Quest(QuestIdentity questIdentity, List<Reward> rewards, List<QuestCollectible> collectibles, string text, Vector2 coordinates, Sprite image)
    {
        this.questIdentity = questIdentity;
        this.rewards = rewards;
        this.collectibles = collectibles;
        this.text = text;
        this.coordinates = coordinates;
        this.questImage = image;
        //completed = false;
    }

    /*
    public Quest(int keyQuestIdentity, Vector2 coordinates)
    {
        questIdentity = keyQuestIdentity;
        this.keyQuest = true;
        QuestCollectible kqc = new QuestCollectible(keyQuestIdentity, 0, 0, false);
        collectibles = new List<QuestCollectible>();
        this.collectibles.Add(kqc);
        this.coordinates = coordinates;
    }*/
}
