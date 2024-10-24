using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStatus
{
    None = 0,
    Sleep = 1,
    OnFire = 2,
};


public class BaseEnemy : Character
{
    protected NavMeshAgent agent;
    [SerializeField]
    protected GameObject player;
    protected EnemyStatus status; 

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    

    public void SetStatus(EnemyStatus s)
    {
        status = s;
    }

    public bool InRange(Vector3 self, Vector3 target, float dist)
    {
        return Vector3.Distance(self, target) <= dist ? true : false;
    }

}
