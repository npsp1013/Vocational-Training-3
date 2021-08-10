using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> customerPrefabs;

    [SerializeField] private GameObject _currentCustomer;
    [SerializeField] private bool isFinished = false;
    [SerializeField] private bool _canSayClosing = true;

    [SerializeField] private int _startIndex = -1;
    public GameObject CurrentCustomer { get { return _currentCustomer; } }

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewCustomer();
    }

    private void Update()
    {
        Main();
    }

    private void SpawnNewCustomer()
    {
        if (_startIndex >= 0)
        {
            _currentCustomer = Instantiate(customerPrefabs[_startIndex]);
            customerPrefabs.Remove(customerPrefabs[_startIndex]);
            _startIndex = -1;
            return;
        }
        
        if (customerPrefabs.Count == 0)
        {
            return;
        }

        int nextCustomerID = Random.Range(0, customerPrefabs.Count);
        _currentCustomer = Instantiate(customerPrefabs[nextCustomerID]);
        customerPrefabs.Remove(customerPrefabs[nextCustomerID]);


        
    }


    private void Main()
    {
        if (isFinished == true) return;

        

        Customer customer = _currentCustomer.GetComponent<Customer>();
        if (customer.isCompleted() == true && _canSayClosing == true)
        {
            StartCoroutine(GiveClosing());
            _canSayClosing = false;
        }
        if (customerPrefabs.Count == 0)
        {
            Debug.Log("You have completed this session!");
            isFinished = true;

        }

        
    }

    public IEnumerator GiveClosing()
    {


        MicrophoneCapture microphone = GameObject.Find("DialogFlow Manager").GetComponent<MicrophoneCapture>();
       
        yield return StartCoroutine(microphone.StartCaptureAfterTime(0, 5));
       // yield return new WaitForSeconds(5);

        Debug.Log("Apple");
        Destroy(_currentCustomer);
        SpawnNewCustomer();
        _canSayClosing = true;



    }
}

