using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Image> slots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventory()
    {
        List<int> inventory = Toolbox.GetInstance().GetGameManager().inventory;
        foreach (int id in inventory)
        {
            print("found in inventory " + id);
            slots[id - 1].GetComponent<Image>().enabled = true;
        }
    }
}
