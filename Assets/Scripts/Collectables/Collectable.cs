using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int identity;
    public float rareness;
    public Question question;
    public bool pickable;

    Rocket rocket;
    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        DeactivateCollider();
        pickable = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
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
        }*/
    }

    private void OnMouseOver()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
    }

    private void OnMouseDown()
    {
        rocket = Toolbox.GetInstance().GetGameManager().GetRocket();
        if(Vector2.Distance(rocket.transform.position, transform.position) < 5)
        {
            //rocket script stop moving - targetpos = rocket pos
            rocket.Scoop(gameObject);
            RemoveCollectable();
            //print(Vector2.Distance(rocket.transform.position, transform.position));
            //print("click");
        }    
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    public void ActivatePickUp()
    {
        pickable = true;
        ActivateCollider();
    }

    public void DeactivatePickUp()
    {
        pickable = false;
        DeactivateCollider();
    }

    public void RemoveCollectable()
    {
        question.RemoveCollectable(this);
    }

    public void SetRareness(float rate)
    {
        rareness = rate;
    }

    public void SetQuestion(Question q)
    {
        question = q;
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
