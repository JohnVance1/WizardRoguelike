using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Base : MonoBehaviour
{
    public bool CanInteract { get; protected set; }
    protected Player player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanInteract = true;
            player = collision.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CanInteract = false;
        }
    }
}
