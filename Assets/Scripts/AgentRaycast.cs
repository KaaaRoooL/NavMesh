using UnityEngine;
using UnityEngine.AI;

public class AgentRaycast : MonoBehaviour
{
    public float distanceRay;
    public Transform player;

    public Transform puntoInicial;

    NavMeshAgent perseguidor;
    RaycastHit hit;

    void Start()
    {
        perseguidor = GetComponent<NavMeshAgent>();
    }
    
    void FixedUpdate()
    {
        
        //Vector3 directionToTarget = (player.position - transform.position).normalized;
        //if(Physics.Raycast(transform.position + vector3.up, directionToTarger, out hitInfo, distanceToPlayer, -1, QueryTriggerInteraction.Ignore))
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,-1, QueryTriggerInteraction.Ignore) )
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);           
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanceRay, Color.red);  
        }

        DistancePlayerRay();

        
       
    }

    private void DistancePlayerRay(){
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if(distanceToPlayer < distanceRay && hit.collider.tag == "Player"){
            perseguidor.destination = player.position;
        } else {
            perseguidor.destination = puntoInicial.position;
        } 
    }
}
