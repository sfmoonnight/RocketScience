using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptainLog : MonoBehaviour
{
    public enum LogSection { TravelLog, Collectibles, KeyDungeons, Skins, Settings};
    public LogSection currentLogSection;

    public GameObject leftPage;
    public GameObject rightPage;
    public GameObject settingPage;

    public Sprite UIMask;
    public Sprite testSpriteSmall;
    public Sprite darkBackground;
    public Sprite questionMark;

    public Image[] slots;
    public Text[] slotTexts;

    public Button travelLog;
    public Button collectibles;
    public Button keyDungeons;
    public Button skins;
    public Button settings;

    public Button nextPage;
    public Button previousPage;
    public Text leftPageNumber;
    public Text rightPageNumber;

    int currentSlotIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentLogSection = LogSection.TravelLog;
        ClearPages();
        UpdatePages();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePages()
    {
        //Toolbox.GetInstance().GetStatManager().LoadState();
        ClearPages();
        UpdatePageNumber();

        if (currentLogSection == LogSection.TravelLog)
        {
            leftPage.GetComponent<ToggleUI>().ShowUI();
            rightPage.GetComponent<ToggleUI>().ShowUI();
            settingPage.GetComponent<ToggleUI>().HideUI();
            UpdateTravelLog();
        }

        if (currentLogSection == LogSection.Collectibles)
        {
            leftPage.GetComponent<ToggleUI>().ShowUI();
            rightPage.GetComponent<ToggleUI>().ShowUI();
            settingPage.GetComponent<ToggleUI>().HideUI();
            UpdateCollectibles();
        }

        if (currentLogSection == LogSection.Settings)
        {
            leftPage.GetComponent<ToggleUI>().HideUI();
            rightPage.GetComponent<ToggleUI>().HideUI();
            settingPage.GetComponent<ToggleUI>().ShowUI();
        }
        
    }

    public void ClearPages()
    {
        currentSlotIndex = 0;
        foreach(Image im in slots)
        {
            Image[] images = im.GetComponentsInChildren<Image>();
            foreach(Image i in images)
            {
                i.sprite = UIMask;
                i.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                i.rectTransform.localScale = new Vector3(1f, 1f, 1);

                Color c = Color.white;
                c.a = 0f;
                i.color = c;
                i.transform.parent.GetComponent<Image>().color = c;
            }
        }
        foreach (Text t in slotTexts)
        {
            t.text = "";
            t.rectTransform.sizeDelta = new Vector2(90, 90);
            t.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            t.rectTransform.localScale = new Vector3(1f, 1f, 1);
        }
    }

    public void NextPage()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;

        if(currentLogSection == LogSection.TravelLog)
        {
            if (gs.travelLogPageNumber < gs.firstEventOnEachPage.Count)
            {
                gs.travelLogPageNumber += 1;
                //print("Current page number after clicking next page: " + gs.travelLogPageNumber);
            }
        }

        if (currentLogSection == LogSection.Collectibles)
        {
            float num = Toolbox.GetInstance().GetGameManager().collectibles.Count / 24;
            if (gs.collectiblePageNumber < num)
            {
                gs.collectiblePageNumber += 1;
                //print("Current page number after clicking next page: " + gs.travelLogPageNumber);
            }
        }

        UpdatePages();
    }

    public void PreviousPage()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;

        if(currentLogSection == LogSection.TravelLog)
        {
            if (gs.travelLogPageNumber > 1)
            {
                gs.travelLogPageNumber -= 1;
                //print("Current page number after clicking previous page: " + gs.travelLogPageNumber);   
            }
        }

        if (currentLogSection == LogSection.Collectibles)
        {
            if (gs.collectiblePageNumber > 1)
            {
                gs.collectiblePageNumber -= 1;
                //print("Current page number after clicking next page: " + gs.travelLogPageNumber);
            }
        }

        UpdatePages();
    }

    public void SelectTravelLog()
    {
        currentLogSection = LogSection.TravelLog;
        UpdatePages();
    }

    public void SelectCollectibles()
    {
        currentLogSection = LogSection.Collectibles;
        UpdatePages();
    }

    public void SelectSettings()
    {
        currentLogSection = LogSection.Settings;
        UpdatePages();
    }

    public void UpdatePageNumber()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;

        if(currentLogSection == LogSection.TravelLog)
        {
            leftPageNumber.text = (gs.travelLogPageNumber * 2 - 1).ToString();
            rightPageNumber.text = (gs.travelLogPageNumber * 2).ToString();
        }

        if (currentLogSection == LogSection.Collectibles)
        {
            leftPageNumber.text = (gs.collectiblePageNumber * 2 - 1).ToString();
            rightPageNumber.text = (gs.collectiblePageNumber * 2).ToString();
        }
    }

    void UpdateTravelLog()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        print("-------travelLogPageNumber " + gs.travelLogPageNumber);
        for (int i = gs.firstEventOnEachPage[gs.travelLogPageNumber - 1]; i < gs.events.Count; i++)
        {
            //print("------loop through events, i = " + i);
            //print("------events count: " + gs.events.Count);
            if (gs.events[i].eventType == Event.EventType.NewCollectible)
            {
                if (currentSlotIndex > 21)
                {
                    //print("Add to first events list");
                    if (gs.travelLogPageNumber >= gs.firstEventOnEachPage.Count)
                    {
                        gs.firstEventOnEachPage.Add(i);
                    }
                    //gs.firstEventOnEachPage[gs.travelLogPageNumber] = i + 1;
                    
                    return;
                }

                //print("------new collectible from event");
                if (gs.events[i].collectibleIdentity > 0)
                {
                    slots[currentSlotIndex].sprite = Toolbox.GetInstance().GetGameManager().collectibles[gs.events[i].collectibleIdentity - 1].spriteRenderer.sprite;
                }
                if (gs.events[i].collectibleIdentity < 0)
                {
                    int n = -gs.events[i].collectibleIdentity;
                    slots[currentSlotIndex].sprite = Toolbox.GetInstance().GetGameManager().keyCollectibles[n - 1].spriteRenderer.sprite;
                }

                slots[currentSlotIndex].rectTransform.pivot = new Vector2(0.5f, 0.5f);
                slots[currentSlotIndex].rectTransform.localScale = new Vector3(0.8f, 0.8f, 1);

                Color co = Color.white;
                co.a = 1f;
                slots[currentSlotIndex].color = co;

                Image im = slots[currentSlotIndex].transform.parent.GetComponent<Image>();
                Color c = im.color;
                float numr = Random.Range(0f, 1f);
                float numg = Random.Range(0f, 1f);
                float numb = Random.Range(0f, 1f);
                c.r = numr;
                c.g = numg;
                c.b = numb;
                c.a = 0.45f;
                slots[currentSlotIndex].transform.parent.GetComponent<Image>().color = c;

                currentSlotIndex += 1;

                slots[currentSlotIndex].rectTransform.localScale = new Vector3(2f, 2f, 1);
                slots[currentSlotIndex].rectTransform.pivot = new Vector2(0f, 1f);

                slotTexts[currentSlotIndex].text = gs.events[i].time;
                
                slotTexts[currentSlotIndex].rectTransform.sizeDelta = new Vector2(180, 90);
                slotTexts[currentSlotIndex].rectTransform.pivot = new Vector2(0.5f, 0f);     
                slotTexts[currentSlotIndex].rectTransform.localScale = new Vector3(0.5f, 0.5f, 1);

                currentSlotIndex += 2;   
            }

            if (gs.events[i].eventType == Event.EventType.KeyDungeon)
            {
                print("------currentslot=" + currentSlotIndex);
                if (currentSlotIndex > 6 && currentSlotIndex <= 11)
                {
                    currentSlotIndex = 12;
                }
                else if (currentSlotIndex > 18)
                {
                    if (gs.travelLogPageNumber >= gs.firstEventOnEachPage.Count)
                    {
                        print("First pages array length: " + gs.firstEventOnEachPage.Count);
                        print("Add to first events list");

                        gs.firstEventOnEachPage.Add(i);
                        print("First pages array length: " + gs.firstEventOnEachPage.Count);
                        print("Index of the next first event: " + i);
                    }

                    return;
                }

                //print("------currentslot=" + currentSlotIndex);
                slots[currentSlotIndex].sprite = darkBackground;
                slots[currentSlotIndex].rectTransform.pivot = new Vector2(0f, 1f);
                slots[currentSlotIndex].rectTransform.localScale = new Vector3(2f, 2f, 1);
                Color c = Color.white;
                c.a = 1f;
                slots[currentSlotIndex].color = c;

                currentSlotIndex += 2;
                slotTexts[currentSlotIndex].text = gs.events[i].time;

                currentSlotIndex += 4;
            }

            if (gs.events[i].eventType == Event.EventType.NewConstellation)
            {
                print("------currentslot=" + currentSlotIndex);
                if (currentSlotIndex > 6 && currentSlotIndex <= 11)
                {
                    currentSlotIndex = 12;
                }
                else if (currentSlotIndex > 18)
                {
                    if (gs.travelLogPageNumber >= gs.firstEventOnEachPage.Count)
                    {
                        print("First pages array length: " + gs.firstEventOnEachPage.Count);
                        print("Add to first events list");

                        gs.firstEventOnEachPage.Add(i);
                        print("First pages array length: " + gs.firstEventOnEachPage.Count);
                        print("Index of the next first event: " + i);
                    }

                    return;
                }

                //print("------currentslot=" + currentSlotIndex);
                slots[currentSlotIndex].sprite = darkBackground;
                slots[currentSlotIndex].GetComponentsInChildren<Image>()[1].sprite = Toolbox.GetInstance().GetGameManager().constellationPrefabs[gs.events[i].collectibleIdentity].GetComponent<SpriteRenderer>().sprite;
                slots[currentSlotIndex].rectTransform.pivot = new Vector2(0f, 1f);
                slots[currentSlotIndex].rectTransform.localScale = new Vector3(2f, 2f, 1);
                Color c = Color.white;
                c.a = 1f;
                slots[currentSlotIndex].color = c;
                slots[currentSlotIndex].GetComponentsInChildren<Image>()[1].color = c;

                currentSlotIndex += 2;
                slotTexts[currentSlotIndex].text = gs.events[i].time;

                currentSlotIndex += 4;
            }
        }
    }

    void UpdateCollectibles()
    {
        Color c1 = Color.white;
        c1.a = 1f;

        for (int i = 0; i < Toolbox.GetInstance().GetGameManager().collectibles.Count % 24; i++)
        {
            slots[i].sprite = questionMark;
            slots[i].color = c1;

            Image im = slots[i].transform.parent.GetComponent<Image>();
            Color c = im.color;
            c.a = 0.45f;
            slots[i].transform.parent.GetComponent<Image>().color = c;
        }

        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        for (int i = (gs.collectiblePageNumber - 1) * 24;  i < (gs.collectiblePageNumber - 1) * 24 + 24; i++)
        {
            if(gs.collected.Count == 0)
            {
                return;
            }
            if(i >= Toolbox.GetInstance().GetGameManager().collectibles.Count)
            {
                return;
            }
            //print("i = " + i);
            //print("total collectibles: " + Toolbox.GetInstance().GetGameManager().collectibles.Count);
            //print("Collected item identity: " + Toolbox.GetInstance().GetGameManager().collectibles[i].identity);
            if (gs.collected.Contains(Toolbox.GetInstance().GetGameManager().collectibles[i].identity))
            {
                slots[i % 24].sprite = Toolbox.GetInstance().GetGameManager().collectibles[i].spriteRenderer.sprite;
                slots[i % 24].rectTransform.localScale = new Vector3(0.8f, 0.8f, 1);
                
                slots[i % 24].color = c1;

                Image im = slots[i % 24].transform.parent.GetComponent<Image>();
                Color c = im.color;
                float numr = Random.Range(0f, 1f);
                float numg = Random.Range(0f, 1f);
                float numb = Random.Range(0f, 1f);
                c.r = numr;
                c.g = numg;
                c.b = numb;
                slots[i % 24].transform.parent.GetComponent<Image>().color = c;

                slotTexts[i % 24].text = Toolbox.GetInstance().GetGameManager().collectibles[i].identity.ToString();
                slotTexts[i % 24].rectTransform.pivot = new Vector2(1f, 0.4f);          
            }
        }
    }

}
