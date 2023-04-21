using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : Character
{
    protected NavMeshAgent agent;
    [SerializeField]
    protected GameObject player;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    virtual public void Start()
    {

    }

    virtual public void Update()
    {

    }

    public bool InRange(Vector3 self, Vector3 target, float dist)
    {
        return Vector3.Distance(self, target) <= dist ? true : false;
    }

}
