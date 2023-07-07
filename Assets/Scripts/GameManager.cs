using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> gates;


    void OnEnable()
    {
        SceneManager.sceneLoaded += SetPlayerPos;
    }


    void SetPlayerPos(Scene scene, LoadSceneMode mode)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
