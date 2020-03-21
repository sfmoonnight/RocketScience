using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    public ToggleUI scoreBorad;

    public bool finished = false;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public void ExitDungeon()
    {
        Toolbox.GetInstance().GetGameManager().inDungeon = false;
        
        print("Count: " + Toolbox.GetInstance().GetStatManager().gameState.events.Count);

        SceneManager.LoadScene("Main");
        
    }

    public virtual void Finished()
    {
        finished = true;
        scoreBorad.ShowUI();
    }
}
