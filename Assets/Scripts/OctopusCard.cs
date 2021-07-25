using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusCard : Interactable
{
    [SerializeField] private float _value;
    public float Value { get { return _value; } }

    public void AddValue(float value)
    {
        if (value < 0.0f) return;

        _value += value;
    }
    public override void Interact() 
    {
        Debug.Log("Octopus Card Selected");
    }
    public override void Interact(Interactable other)
    {
        base.Interact(other);
        if (other.CompareTag("CardReader") == false) return;
        PointOfSale pos = GameObject.Find("Point Of Sale").GetComponent<PointOfSale>();
        if (pos.CanReadCard() == false) return;
        pos.AddCard(this);
    }
}
