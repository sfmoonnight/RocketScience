using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TelescopeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterTelescope()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        if(gs.telescopeEnergyCard > 0)
        {
            Toolbox.GetInstance().GetGameManager().inDungeon = true;
            gs.playerPosition = GameObject.Find("Telescope").transform.position + new Vector3(0, -20, 0);
            gs.answer = Toolbox.GetInstance().GetGameManager().answer;
            Toolbox.GetInstance().GetGameManager().planets.Clear();
            gs.telescopeEnergyCard -= 1;
            GameObject.Find("EnergyCard").GetComponent<Text>().text = gs.telescopeEnergyCard.ToString();

            Toolbox.GetInstance().GetStatManager().SaveState();

            SceneManager.LoadScene("ConstellationGame");
        }
        else
        {
            GetComponent<ToggleUI>().HideUI();
            GameObject newItem = GameObject.Find("NotificationUI");
            newItem.GetComponent<NotificationQueue>().AddToQueue(null, "You don't have any energy card. You can order some in your Captain log.");
        }    
    }
}
