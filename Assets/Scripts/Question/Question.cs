using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    //public int answer;
    
    public int capacity;
    public bool activated;
    public List<Collectable> options;
    public List<Collectable> collectables;
    public GameObject eqTextMeshObj;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VerifyAnswer()
    {
        /*int currAnswer = eqTextMeshObj.GetComponent<EquationManager>().equation.answer;
        if (Toolbox.GetInstance().GetGameManager().answer == currAnswer)
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
        }*/
    }

    void GenerateCollectables()
    {
        foreach(Collectable col in options)
        {
            float rate = Random.Range(0f, 1f);
            if(rate <= col.rareness)
            {
                Collectable newCol = Instantiate(col, transform);
                collectables.Add(newCol);
            }
        }
    }

    public void ActivateCollectables()
    {
        if (activated)
        {
            return;
        }
        foreach (Collectable col in collectables)
        {
            col.ActivatePickUp();
        }
        ChangeColor();
        activated = true;
    }

    public void DeactivateCollectables()
    {
        if (!activated)
        {
            return;
        }
        foreach (Collectable col in collectables)
        {
            col.DeactivatePickUp();
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        activated = false;
    }

    void ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }
}
