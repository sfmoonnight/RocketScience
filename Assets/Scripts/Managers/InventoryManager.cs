using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    int item_plus;
    int item_minus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPlus()
    {
        return item_plus;
    }

    public int GetMinus()
    {
        return item_minus;
    }

    public void GainPlus()
    {
        item_plus++;
    }

    public void ConsumePlus()
    {
        if(item_plus > 0)
        {
            item_plus--;
        }
    }

    public void GainMinus()
    {
        item_minus++;
    }

    public void ConsumeMinus()
    {
        if (item_minus > 0)
        {
            item_minus--;
        }
    }
}
