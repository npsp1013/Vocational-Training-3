using System.Collections.Generic;
using UnityEngine;
public class Customer : MonoBehaviour
{
    [SerializeField] private float _targetCardValue;
    [SerializeField] private OctopusCard _card;
    [SerializeField] private Animation _anim;
    [SerializeField] private List<string> _questText;

    public List<string> QuestText { get { return _questText; } }

    // Start is called before the first frame update
    void Awake()
    {
        _card = gameObject.GetComponentInChildren<OctopusCard>();
        _anim = gameObject.GetComponentInChildren<Animation>();
    }
    public bool isCompleted()
    {
        if (_card.Value < -100.0f) return false;
        float difference = (_targetCardValue - _card.Value);
        return difference < 0.01f && difference >= -0.01f;
    }

    private void Update()
    {
        _anim.CrossFade("idle");
    }

    private void Start()
    {
        MicrophoneCapture microphone = GameObject.Find("DialogFlow Manager").GetComponent<MicrophoneCapture>();
        StartCoroutine(microphone.StartCaptureAfterTime(0, 5));
    }

    

    //TODO: Check whether the scanned items can fulfill the task
}

