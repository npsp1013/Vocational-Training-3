using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpinner : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * _sensitivity);
    }
}
