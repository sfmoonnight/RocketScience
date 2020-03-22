using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question : MonoBehaviour
{
    public int planetID;
    public DungeonEntrence dungeonEntrence;
    public bool openDungeon;
    
    public int capacity;
    public bool activated=false;
    //public List<Collectable> options;
    public List<int> options;

    public List<Collectable> collectables; //Collectibles generated on this planet
    public List<int> collectiblesID;

    public List<GameObject> generationPoints;
    public List<int> pointsWithCollectibles;
    public GameObject eqTextMeshObj;
    public List<GameObject> items;

    public float radius;

    float rotationSpeed = 30;

    // Start is called before the first frame update
    void Start()
    {
        Toolbox.GetInstance().GetGameManager().planets.Add(this);
        //GenerateCollectables();
        StartRotation();
        /*if (this == Toolbox.GetInstance().GetStatManager().gameState.dungeonEntered)
        {
            ActivateCollectables();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EquationManager GetEquation()
    {
        return eqTextMeshObj.GetComponent<EquationManager>();
    }

    public void UpdatePlanet()
    {
        float dungeonChance = Random.Range(0f, 1f);
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        if (dungeonChance <= 0.25 && gs.keyDungeonProgress > 0)
        {
            openDungeon = true;
            HideEquation();
            dungeonEntrence.GenerateDungeon();
        }
        else
        {
            eqTextMeshObj.GetComponent<EquationManager>().GenerateEquation();
        }
        RefreshCollectables();
        DeactivateCollectables();
    }

    public void GenerateCollectables()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        GameManager gm = Toolbox.GetInstance().GetGameManager();
        print("Planet data: " + gs.allPlanetData.Count);

        print("Generate collectables!!!");
        for (int i = 0; i < generationPoints.Count; i++)
        {
            float rate = Random.Range(0f, 1f);
            int op = options[Random.Range(0, options.Count)];

            print("option count " + options.Count);
            print("option: " + op);
            if (rate <= gm.collectibles[op - 1].rareness)
            {
                Collectable newCol = Instantiate(gm.collectibles[op - 1], generationPoints[i].transform);
                collectables.Add(newCol);
                collectiblesID.Add(newCol.identity);
                pointsWithCollectibles.Add(i);
                newCol.SetQuestion(this);
            }
        }
          
    }

    public void SetUpPlanet(PlanetData pd)
    {
        GameManager gm = Toolbox.GetInstance().GetGameManager();
        planetID = pd.planetID;
        transform.position = pd.location;
        openDungeon = pd.ifDungeonOpened;
        options = pd.collectibleOptions;
        collectiblesID = pd.collectiblesGenerated;
        pointsWithCollectibles = pd.generationPoints;

        for (int i = 0; i < collectiblesID.Count; i++)
        {
            //print("i: " + i);
            //print("collectibleID count: " + collectiblesID.Count);
            //print("ith collectibleID: " + collectiblesID[i]);
            //print("pointsWithCollectibles count: " + pointsWithCollectibles.Count);
            //print("generationPointIndex: " + pointsWithCollectibles[i]);
            Collectable col = Instantiate(gm.collectibles[collectiblesID[i] - 1], generationPoints[pointsWithCollectibles[i]].transform);
            collectables.Add(col);
            col.SetQuestion(this);
        }
    }

    public void RefreshCollectables()
    {
        GameManager gm = Toolbox.GetInstance().GetGameManager();
        ClearCollectables();
        for (int i = 0; i < generationPoints.Count; i++)
        {
            float rate = Random.Range(0f, 1f);
            int op = options[Random.Range(0, options.Count)];
            if (rate <= gm.collectibles[op - 1].rareness)
            {
                Collectable newCol = Instantiate(gm.collectibles[op - 1], generationPoints[i].transform);
                collectables.Add(newCol);
                pointsWithCollectibles.Add(i);
                collectiblesID.Add(newCol.identity);
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
        print("collectible counts: " + collectables.Count);
        foreach (Collectable col in collectables)
        {
            col.ActivatePickUp();
        }
        //ChangeColor();
        StopRotation();
        HideEquation();
        //GenerateItems();
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
        ShowEquation();
        //GetComponent<SpriteRenderer>().color = Color.white;
        activated = false;
    }

    public void ClearCollectables()
    {
        if(collectables.Count > 0)
        {
            foreach (Collectable col in collectables)
            {
                Destroy(col.gameObject);
            }
        }
        
        collectables.Clear();
        collectiblesID.Clear();
        pointsWithCollectibles.Clear();
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

    public void HideEquation()
    {
        eqTextMeshObj.GetComponent<MeshRenderer>().enabled = false;
    }

    public void ShowEquation()
    {
        eqTextMeshObj.GetComponent<MeshRenderer>().enabled = true;
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
