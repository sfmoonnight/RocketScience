using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogPopUp : MonoBehaviour
{
    public enum ItemType { NotForSell, EnergyCard, Skin };
    public ItemType itemType;

    public Text numberText;
    public int amount;

    public Button buyButton;
    // Start is called before the first frame update
    void Start()
    {
        amount = 0;
        numberText.text = amount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddOne()
    {
        amount += 1;
        numberText.text = amount.ToString();
        UpdateBuyButton();
    }

    public void RemoveOne()
    {
        if(amount > 0)
        {
            amount -= 1;
        }
        numberText.text = amount.ToString();
        UpdateBuyButton();
    }

    void UpdateBuyButton()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        if (amount * 300 > gs.money)
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }
    }

    public void Purchase()
    {
        GameState gs = Toolbox.GetInstance().GetStatManager().gameState;
        if (itemType == ItemType.EnergyCard)
        {
            if(amount * 300 < gs.money)
            {
                gs.money -= amount * 300;
                gs.telescopeEnergyCard += amount;
            }
        }
        GameObject.Find("Money").GetComponent<Text>().text = gs.money.ToString();
        GameObject.Find("EnergyCard").GetComponent<Text>().text = gs.telescopeEnergyCard.ToString();
        amount = 0;
    }
}
