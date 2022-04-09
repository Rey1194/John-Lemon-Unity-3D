using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Nav Mesh Agent reference
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
	// Editor Reference
	public NavMeshAgent navMeshAgent;
	public Transform[] wayPoints;
	// Varaibles
	private int m_CurrentWaypointIndex;
	
  // Start is called before the first frame update
  void Start()
  {
	  // configurar el destino inicial del Nav Mesh Agent.
	  navMeshAgent.SetDestination(wayPoints[0].position);
  }

  // Update is called once per frame
  void Update()
	{
		// saber si el Nav Mesh Agent ha llegado a su destino
	  if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance) {
	  	// actualizar el índice actual
	  	m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % wayPoints.Length;
	  	// Configurar el nuevo destino del Nav Mesh Agent
	  	navMeshAgent.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
	  }
  }
}
