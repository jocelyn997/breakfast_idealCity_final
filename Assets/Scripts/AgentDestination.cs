using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentDestination : MonoBehaviour
{
    public Transform home;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(home.position);
    }
}