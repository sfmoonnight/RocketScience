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
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //HideCollectable();
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
        if (pickable)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
        }        
    }

    private void OnMouseDown()
    {
        if (pickable)
        {
            rocket = Toolbox.GetInstance().GetGameManager().GetRocket();
            rocket.MoveAndScoop(gameObject);
        }
        
        /*if(Vector2.Distance(rocket.transform.position, transform.position) < 5)
        {
            //rocket script stop moving - targetpos = rocket pos
            rocket.Scoop(gameObject);
            StartCoroutine("AddToInventory");
            
            
            //print(Vector2.Distance(rocket.transform.position, transform.position));
            //print("click");
        }    */
    }

    public void ProcessPickup()
    {
        StartCoroutine("AddToInventory");
        Toolbox.GetInstance().GetGameManager().UpdateQuestCollectible(identity);
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    public void ActivatePickUp()
    {
        pickable = true;
        ActivateCollider();
        //ShowCollectable();
    }

    public void DeactivatePickUp()
    {
        pickable = false;
        DeactivateCollider();
        //HideCollectable();
    }

    public void RemoveCollectable()
    {
        //question.RemoveCollectable(this);
        question.eqTextMeshObj.GetComponent<EquationManager>().GenerateEquation();
        question.GenerateCollectables();
        question.DeactivateCollectables();
    }

    public IEnumerator AddToInventory()
    {
        if(Toolbox.GetInstance().GetGameManager().inventory != null)
        {
            foreach (int i in Toolbox.GetInstance().GetGameManager().inventory)
            {
                if (identity == i)
                {
                    RemoveCollectable();
                    yield break;
                }
            }
        }
        
        Toolbox.GetInstance().GetGameManager().inventory.Add(identity);
        GameObject newItem = GameObject.Find("NewCollectableUI");
        HideCollectable();
        //print("new item ui" + newItem);
        yield return new WaitForSeconds(1.3f);
        newItem.GetComponent<ToggleUI>().ShowUI();
        newItem.GetComponent<ToggleUI>().ChangeImage(spriteRenderer.sprite);
        
        RemoveCollectable();
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
        spriteRenderer.enabled = false;
    }

    public void ShowCollectable()
    {
        ActivateCollider();
        spriteRenderer.enabled = true;
    }
}
