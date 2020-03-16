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
        //List<int> inventory = Toolbox.GetInstance().GetStatManager().gameState.collected;
        if(Toolbox.GetInstance().GetStatManager().gameState.collected.Count > 0)
        {
            foreach (int id in Toolbox.GetInstance().GetStatManager().gameState.collected)
            {
                print("found in inventory " + id);
                //TODO: Reconstruct inventory system, the key items are having negative index
                print(Toolbox.GetInstance().GetStatManager().gameState.collected.Count);
                slots[id - 1].GetComponent<Image>().enabled = true;
            }
        }       
    }
}
