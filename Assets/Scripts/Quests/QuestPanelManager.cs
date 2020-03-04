using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateQuests();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateQuests()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        List<Quest> quests = gs.quests;
        
        if (quests.Count == 0)
        {
            // Clear panel
        }

        else
        {
            Quest currQuest = quests[gs.currQuestIndex];
            DrawQuest(currQuest);
        }

    }

    public void DrawQuest(Quest q)
    {
        List<Collectable> availableCollectibles = gameObject.GetComponent<DataContainer>().collectibles;
        Debug.Assert(availableCollectibles.Count > 0);
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (QuestCollectible qc in q.collectibles) {
            Collectable col = getCollectibleByIdentity(availableCollectibles, qc.identity);
            // Instantiate new game object and set sprite to the sprite of col
            GameObject go = new GameObject();
            Image i = go.AddComponent<Image>();
            print(i.sprite);
            print(col);
            i.sprite = col.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
            i.preserveAspect = true;
            if (!qc.collected)
            {
                i.color = Color.black;
            }
            //go.transform.SetParent(this.transform);
            go.GetComponent<RectTransform>().SetParent(this.transform);
            Vector2 v = new Vector2(qc.x, qc.y);
            go.GetComponent<RectTransform>().localPosition = v;
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            //go.transform.position = v;
            print("setting x and y " + v);
            go.SetActive(true);
        }
    }

    private Collectable getCollectibleByIdentity(List<Collectable> availableCollectibles, int identity)
    {
        foreach (Collectable col in availableCollectibles)
        {
            if (col.identity == identity)
            {
                return col;
            }
        }
        return null;
    }
}
