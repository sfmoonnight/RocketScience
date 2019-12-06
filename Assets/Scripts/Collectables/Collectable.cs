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
    public Collider2D collider2D;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        HideCollectable();
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
        //ActivateCollider();
        ShowCollectable();
    }

    public void DeactivatePickUp()
    {
        pickable = false;
        //DeactivateCollider();
        HideCollectable();
    }

    public void RemoveCollectable()
    {
        question.RemoveCollectable(this);
        question.eqTextMeshObj.GetComponent<EquationManager>().GenerateEquation();
        question.GenerateCollectables();
        question.DeactivateCollectables();
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
        collider2D.enabled = true;
    }

    void DeactivateCollider()
    {
        collider2D.enabled = false;    
    }

    public void HideCollectable()
    {
        DeactivateCollider();
        sprite.enabled = false;
    }

    public void ShowCollectable()
    {
        ActivateCollider();
        sprite.enabled = true;
    }
}
