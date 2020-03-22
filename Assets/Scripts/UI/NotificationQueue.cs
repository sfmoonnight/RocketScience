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
        Notification n = new Notification(s, m);
        q.Enqueue(n);
        ShowNotification();
    }

    void ShowNotification()
    {
        if (active)
        {
            return;
        }
        active = true;
        ToggleUI tu = gameObject.GetComponent<ToggleUI>();
        Notification n = q.Dequeue();
        tu.ChangeImage(n.sprite);
        tu.ChangeText(n.message);
        tu.setCallback(ShowNext);
        tu.ShowUI();
    }

    public void ShowNext()
    {
        if (q.Count > 0)
        {
            active = false;
            ShowNotification();
        }
        active = false;
    }
}
