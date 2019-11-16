using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public int answer;
    public int capacity;
    public bool activate;
    public List<Collectable> options;
    public List<Collectable> collectables;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if(Toolbox.GetInstance().GetGameManager().answer == answer)
        {
            if (!activate)
            {
                ActivateCollectables();
            }
            activate = true;
        }
        else
        {
            if (activate)
            {
                DeactivateCollectables();
            }
            activate = false;
        }
    }

    void GenerateCollectables()
    {
        foreach(Collectable col in options)
        {
            float rate = Random.Range(0f, 1f);
            if(rate < col.rareness)
            {
                Collectable newCol = Instantiate(col);
                collectables.Add(newCol);
            }
        }
    }

    void ActivateCollectables()
    {
        foreach (Collectable col in collectables)
        {
            col.ActivatePickUp();
        }
    }

    void DeactivateCollectables()
    {
        foreach (Collectable col in collectables)
        {
            col.DeactivatePickUp();
        }
    }
}
