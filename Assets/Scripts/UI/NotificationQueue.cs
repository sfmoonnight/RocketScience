using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationQueue : MonoBehaviour
{
    bool active;
    Queue<Notification> q;
    // Start is called before the first frame update
    void Start()
    {
        q = new Queue<Notification>();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //[MethodImpl(MethodImplOptions.Synchronized)]
    public void AddToQueue(Sprite s, string m)
    {
        //print("Enqueue " + m);
        Notification n = new Notification(s, m);
        q.Enqueue(n);
        if (!active)
        {
            ShowNotification();
        }
    }

    void ShowNotification()
    {
        active = true;
        ToggleUI tu = gameObject.GetComponent<ToggleUI>();
        Notification n = q.Dequeue();
        //print("Dequeue" + n.message + ". New q size" + q.Count);
        tu.ChangeImage(n.sprite);
        tu.ChangeText1(n.message);
        tu.setCallback(ShowNext);
        tu.ShowUI();
    }

    public void ShowNext()
    {
        //print("In show next, q size is " + q.Count);
        if (q.Count > 0)
        {
            //print("Calling show notification");
            //active = false;
            ShowNotification();
        }
        else
        {
            active = false;
        }
    }
}
