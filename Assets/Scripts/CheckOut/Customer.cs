using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private float targetCardValue;
    [SerializeField] private OctopusCard card;
    [SerializeField] private Animation animation;
    // Start is called before the first frame update
    void Awake()
    {
        card = gameObject.GetComponentInChildren<OctopusCard>();
        animation = gameObject.GetComponentInChildren<Animation>();
    }
    public bool isCompleted()
    {
        if (card.Value < -100.0f) return false;
        float difference = (targetCardValue - card.Value);
        return difference < 0.01f && difference >= -0.01f;
    }

    private void Update()
    {
        animation.CrossFade("idle");
    }

    //TODO: Check whether the scanned items can fulfill the task
}
