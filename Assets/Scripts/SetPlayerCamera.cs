using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetPlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = FindObjectOfType<Player>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
