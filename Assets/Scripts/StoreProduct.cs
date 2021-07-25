using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreProduct : MonoBehaviour
{
    [SerializeField] private string _barcode;
    [SerializeField] private float _price;
    [SerializeField] private string _name;

    public float Price { get { return _price; } }
    public string Name { get { return _name; } }

    public bool IsSameBarcode(string other)
    {
        return _barcode == other;
    }
}
