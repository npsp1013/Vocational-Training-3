using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private float targetCardValue;
    [SerializeField] private OctopusCard card;

    // Start is called before the first frame update
    void Start()
    {
        card = gameObject.GetComponentInChildren<OctopusCard>();
    }
    public bool isCompleted()
    {
        if (card.Value < -100.0f) return false;
        float difference = (targetCardValue - card.Value);
        return difference < 0.01f && difference >= -0.01f;
    }

    //TODO: Check whether the scanned items can fulfill the task
}
