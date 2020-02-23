using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAmount : MonoBehaviour
{
    public Text amount;
    public enum Item {Plus, Minus};
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmount();
    }

    public void UpdateAmount()
    {
        if(item == Item.Plus)
        {
            int num = Toolbox.GetInstance().GetInventoryManager().GetPlus();
            amount.text = num.ToString();
        }
        if (item == Item.Minus)
        {
            int num = Toolbox.GetInstance().GetInventoryManager().GetMinus();
            amount.text = num.ToString();
        }      
    }
}
