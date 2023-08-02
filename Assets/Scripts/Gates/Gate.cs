using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public int num;
    public int scene;
    public Transform GateSpawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(scene);
            collision.GetComponent<Player>().gateNum = num;
        }
    }


}
