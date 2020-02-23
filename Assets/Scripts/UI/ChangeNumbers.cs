using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNumbers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(int amount)
    {
        if(amount == 1)
        {
            if(Toolbox.GetInstance().GetInventoryManager().GetPlus() > 0)
            {
                List<Number> numbers = new List<Number>();
                numbers = Toolbox.GetInstance().GetGameManager().GetRocket().GetNumbersAround();
                foreach (Number num in numbers)
                {
                    num.SetNumber(num.number + amount);
                    num.SetNumberText();
                }
                Toolbox.GetInstance().GetInventoryManager().ConsumePlus();
            }
        }
        if (amount == -1)
        {
            if (Toolbox.GetInstance().GetInventoryManager().GetMinus() > 0)
            {
                List<Number> numbers = new List<Number>();
                numbers = Toolbox.GetInstance().GetGameManager().GetRocket().GetNumbersAround();
                foreach (Number num in numbers)
                {
                    num.SetNumber(num.number + amount);
                    num.SetNumberText();
                }
                Toolbox.GetInstance().GetInventoryManager().ConsumeMinus();
            }
        }
    }
}
