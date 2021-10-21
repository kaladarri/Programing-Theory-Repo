using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CameraMove : MonoBehaviour
{
    protected NavMeshAgent m_Agent;
    private float PanSpeed = 50.0f;
    protected void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = PanSpeed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void GoTo(Vector3 position)
    {
        //we don't have a target anymore if we order to go to a random point.
        //m_Target = null;
        m_Agent.SetDestination(position);
        m_Agent.isStopped = false;
    }
}
