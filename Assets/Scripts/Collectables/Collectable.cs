using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int identity;
    public float rareness;
    public GameObject planet;
    public bool pickable;

    Rocket rocket;
    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickable)
        {
            if (!collider.enabled)
            {
                ActivateCollider();
            }
        }
        else
        {
            if (collider.enabled)
            {
                DeactivateCollider();
            }
        }
    }

    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    private void OnMouseDown()
    {
        rocket = Toolbox.GetInstance().GetGameManager().GetRocket();
        if(Vector2.Distance(rocket.transform.position, transform.position) < 5)
        {
            rocket.Scoop();
            //print(Vector2.Distance(rocket.transform.position, transform.position));
            //print("click");
        }    
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ActivatePickUp()
    {
        pickable = true;
    }

    public void DeactivatePickUp()
    {
        pickable = false;
    }

    public void SetRareness(float rate)
    {
        rareness = rate;
    }

    void ActivateCollider()
    {
        collider.enabled = true;
    }

    void DeactivateCollider()
    {
        collider.enabled = false;
    }
}
