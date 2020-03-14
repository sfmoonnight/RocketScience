using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    public ToggleUI scoreBorad;
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
        SceneManager.LoadScene("Main");
    }

    public virtual void Finished()
    {
        scoreBorad.ShowUI();
    }
}
