using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    public LayerMask interactableMask;
    Camera cam;
    public Interactable focus;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        OnRightMouseButtonDown();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, interactableMask))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    public void OnRightMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, interactableMask))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable == null) return;
                if (focus == null) return;
                focus.Interact(interactable);
            }
            RemoveFocus();
        }
    }

    public void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus && focus != null) focus.OnDefocused();
        if (newFocus != focus) focus = newFocus;
        newFocus.OnFocused();
    }

    public void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
    }
}
