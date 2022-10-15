using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        Debug.Log("Moving"); // todo: fix
        point.y = agent.nextPosition.y;
        agent.SetDestination(point);
    }

    public void MoveX(int )
    {
        point.y = agent.nextPosition.y;
        agent.SetDestination(point);
    }
}
