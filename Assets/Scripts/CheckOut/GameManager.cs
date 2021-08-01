using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> customerPrefabs;

    [SerializeField] private GameObject _currentCustomer;

    [SerializeField] private bool isCompleted = false;

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
        if (isCompleted == true) return;
        if (_currentCustomer.GetComponent<Customer>().isCompleted() == true)
        {
            
            Destroy(_currentCustomer);
            if (customerPrefabs.Count == 0)
            {
                Debug.Log("You have completed this session!");
                isCompleted = true;
                return;
            }
            SpawnNewCustomer();
        }
    }
}

