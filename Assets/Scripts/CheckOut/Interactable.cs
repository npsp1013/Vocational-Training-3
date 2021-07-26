using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool hasInteracted = false;
    [SerializeField] public bool isFocus = false;

    void Update()
    {
        WhenFocused();
    }

    public void WhenFocused()
    {
        if (isFocus == false) return;
        if (hasInteracted == true) return;
        Interact();
        hasInteracted = true;
    }

    public void OnFocused()
    {
        isFocus = true;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        hasInteracted = false;
    }

    public virtual void Interact()
    {
        Debug.Log("INTERACTing with " + transform.name);
    }

    public virtual void Interact(Interactable other)
    {
        Debug.Log("INTERACTing with " + transform.name + " " + other.transform.name);
    }
}
