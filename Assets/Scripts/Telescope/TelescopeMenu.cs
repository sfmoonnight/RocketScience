using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Toolbox.GetInstance().GetGameManager().inDungeon = true;
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;

        gs.playerPosition = GameObject.Find("Telescope").transform.position + new Vector3(0, -20, 0);
        gs.answer = Toolbox.GetInstance().GetGameManager().answer;
        Toolbox.GetInstance().GetGameManager().planets.Clear();
        Toolbox.GetInstance().GetStatManager().SaveState();

        SceneManager.LoadScene("ConstellationGame");
    }
}
