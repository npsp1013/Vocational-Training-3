using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{

    private void OnMouseDown()
    {
        PointOfSale pos = GetComponentInParent<PointOfSale>();
        pos.ShowRegisterScreen();
    }
}
