using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int identity;
    public float rareness;
    public bool pickable;

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
