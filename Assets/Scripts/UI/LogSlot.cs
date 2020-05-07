using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogSlot : MonoBehaviour
{
    public Sprite displayImage;
    public string displayText;
    public CaptainLog.LogSection logSection;
    public int ID;

    public ToggleUI logPopUp;
    // Start is called before the first frame update
    void Start()
    {
        ID = -10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopUp()
    {
        logPopUp.ChangeImage(displayImage);
        logPopUp.ChangeText1(displayText);
        logPopUp.GetComponent<LogPopUp>().itemType = LogPopUp.ItemType.NotForSell;
        //print("change type to not for sell");
        if (logSection != CaptainLog.LogSection.Store)
        {
            logPopUp.HideObjects();
        }
        else
        {
            logPopUp.ShowObjects();
            if (ID == 3)
            {
                //print("change type to energy card");
                logPopUp.GetComponent<LogPopUp>().itemType = LogPopUp.ItemType.EnergyCard;
            }
        }

        logPopUp.ShowUI();
    }
}
