using UnityEngine;
using UnityEngine.AI;

public class AgentRaycast : MonoBehaviour
{
    public float distanceRay;
    //Vector3 directionToTarget = (player.position - Transform,position).normalized;
    void FixedUpdate()
    {
        RaycastHit hit;

        //if(Physics.Raycast(transform.position + vector3.up, directionToTarger, out hitInfo, distanceToPlayer, -1. QueryTriggerInteraction.Ignore))
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanceRay, Color.white);
        }
    }
}
