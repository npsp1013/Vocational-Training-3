using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> customerPrefabs;

    [SerializeField] private GameObject currentCustomer;

    [SerializeField] private bool isCompleted = false;

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
        currentCustomer = Instantiate(customerPrefabs[nextCustomerID]);
        customerPrefabs.Remove(customerPrefabs[nextCustomerID]);
    }

    private void Main()
    {
        if (isCompleted == true) return;
        if (currentCustomer.GetComponent<Customer>().isCompleted() == true)
        {
            
            Destroy(currentCustomer);
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

