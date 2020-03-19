using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptainLog : MonoBehaviour
{
    public Sprite UIMask;
    public Sprite testSprite;
    public Image[] slots;
    public Text[] slotTexts;

    public Button travelLog;
    public Button collectibles;
    public Button keyDungeons;
    public Button skins;

    public Button nextPage;
    public Button previousPage;

    int currentSlotIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        ClearPages();
        UpdatePages();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePages()
    {
        ClearPages();

        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        foreach(Event e in gs.events)
        {
            //print("------loop through events");
            if (e.eventType == Event.EventType.NewCollectible)
            {
                print("------new collectible from event");
                if(e.collectibleIdentity > 0)
                {
                    slots[currentSlotIndex].sprite = Toolbox.GetInstance().GetGameManager().collectibles[e.collectibleIdentity - 1].spriteRenderer.sprite;
                }
                if (e.collectibleIdentity < 0)
                {
                    int i = -e.collectibleIdentity;
                    slots[currentSlotIndex].sprite = Toolbox.GetInstance().GetGameManager().keyCollectibles[i - 1].spriteRenderer.sprite;
                }

                slots[currentSlotIndex].rectTransform.pivot = new Vector2(0.5f, 0.5f);
                slots[currentSlotIndex].rectTransform.localScale = new Vector3(0.8f, 0.8f, 1);
                currentSlotIndex += 1;
                slots[currentSlotIndex].rectTransform.localScale = new Vector3(2f, 2f, 1);
                slots[currentSlotIndex].rectTransform.pivot = new Vector2(0f, 1f);
                slotTexts[currentSlotIndex].text = e.time;
                //slotTexts[currentSlotIndex].rectTransform.localScale = new Vector3(1.9f, 2f, 1);
                slotTexts[currentSlotIndex].rectTransform.sizeDelta = new Vector2(180, 90);
                slotTexts[currentSlotIndex].rectTransform.localPosition = new Vector3(0, 25, 0);
                slotTexts[currentSlotIndex].rectTransform.localScale = new Vector3(0.5f, 0.5f, 1);
                currentSlotIndex += 2;
            }

            if(e.eventType == Event.EventType.KeyDungeon)
            {
                print("------currentslot=" + currentSlotIndex);
                if (currentSlotIndex > 6 && currentSlotIndex <= 11)
                {
                    currentSlotIndex = 12;
                }
                else if(currentSlotIndex >= 21)
                {
                    return;
                }
                
                //print("------currentslot=" + currentSlotIndex);
                slots[currentSlotIndex].sprite = testSprite;
                slots[currentSlotIndex].rectTransform.pivot = new Vector2(0f, 1f);
                slots[currentSlotIndex].rectTransform.localScale = new Vector3(2f, 2f, 1);
                currentSlotIndex += 2;
                slotTexts[currentSlotIndex].text = e.time;

                currentSlotIndex += 4;          
            }
        }
    }

    public void ClearPages()
    {
        currentSlotIndex = 0;
        foreach(Image im in slots)
        {
            im.sprite = UIMask;
        }
        foreach (Text im in slotTexts)
        {
            im.text = "";
        }
    }
}
