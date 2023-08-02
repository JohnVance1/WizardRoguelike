using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> gates;
    public Player player;
    public List<ScriptableObject> persistData;


    void OnEnable()
    {
        SceneManager.sceneLoaded += SetPlayerPos;
        player = FindObjectOfType<Player>();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetPlayerPos;
    }


    void SetPlayerPos(Scene scene, LoadSceneMode mode)
    {
        foreach(GameObject go in gates)
        {
            if(go.GetComponent<Gate>().num == player.gateNum)
            {
                player.transform.position = go.GetComponent<Gate>().GateSpawn.position;
            }
        }

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
