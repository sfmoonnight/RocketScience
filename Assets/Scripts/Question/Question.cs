using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    //public int answer;
    
    public int capacity;
    public bool activated=false;
    public List<Collectable> options;
    public List<Collectable> collectables;
    public List<GameObject> generationPoints;
    public GameObject eqTextMeshObj;

    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCollectables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateCollectables()
    {
        ClearCollectables();
        foreach(GameObject gp in generationPoints)
        {
            float rate = Random.Range(0f, 1f);
            int op = Random.Range(0, options.Count);
            if(rate <= options[op].rareness)
            {
                Collectable newCol = Instantiate(options[op], gp.transform);
                collectables.Add(newCol);
                newCol.SetQuestion(this);
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
        if(collectables.Count > 0)
        {
            foreach (Collectable col in collectables)
            {
                col.DeactivatePickUp();
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        activated = false;
    }

    public void ClearCollectables()
    {
        foreach(Collectable col in collectables)
        {
            Destroy(col.gameObject);
        }
        collectables.Clear();
    }

    public void RemoveCollectable(Collectable c)
    {
        collectables.Remove(c);
        Destroy(c.gameObject);
    }

    void ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }
}
