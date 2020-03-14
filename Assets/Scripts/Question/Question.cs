﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    //public int answer;
    public DungeonEntrence dungeonEntrence;
    
    public int capacity;
    public bool activated=false;
    public List<Collectable> options;
    public List<Collectable> collectables;
    public List<GameObject> generationPoints;
    public GameObject eqTextMeshObj;
    public List<GameObject> items;

    public float radius;

    float rotationSpeed = 30;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCollectables();
        StartRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EquationManager GetEquation()
    {
        return eqTextMeshObj.GetComponent<EquationManager>();
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
        //ChangeColor();
        StopRotation();
        GenerateItems();
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
        StartRotation();
        //GetComponent<SpriteRenderer>().color = Color.white;
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

    public void GenerateItems()
    {
        foreach(GameObject go in items)
        {
            GameObject item = Instantiate(go, transform.position, Quaternion.identity);
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            Vector2 direction = new Vector2(x, y);
            item.GetComponent<Rigidbody2D>().AddForce(direction * 5, ForceMode2D.Impulse);
        }
    }

    void StartRotation()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float ran = Random.Range(0f, 1f);
        if(ran > 0.5)
        {
            rb.angularVelocity = rotationSpeed;
        }
        else
        {
            rb.angularVelocity = -rotationSpeed;
        }
        
    }

    void StopRotation()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = 0;
    }
}
