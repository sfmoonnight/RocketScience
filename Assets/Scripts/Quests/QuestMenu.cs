using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenu : MonoBehaviour
{
    public NewQuest newQuest;
    //public Text text;
    public QuestPanelManager questPanelManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //---Add quest to quest panel
    public void AcceptQuest()
    {
        if(newQuest.quest.questIdentity == Quest.QuestIdentity.TelescopeActivation)
        {
            Toolbox.GetInstance().GetStatManager().gameState.telescopeQuestStatus = GameState.QuestStatus.Accepted;
            GameObject.Find("Telescope").GetComponent<ActivateTelescope>().ShowTelescope(); 

        }
        GetComponent<ToggleUI>().HideUI();
        newQuest.HideNewQuest();
        Toolbox.GetInstance().GetStatManager().gameState.quests.Add(newQuest.quest);
        questPanelManager.UpdateQuests();
    }

    public void DeclineQuest()
    {
        GetComponent<ToggleUI>().HideUI();
        //TODO: Resume Game
    }
}
