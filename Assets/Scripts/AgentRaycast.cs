using UnityEngine;
using UnityEngine.AI;

public class AgentRaycast : MonoBehaviour
{
    public float distanceRay;
    public Transform player;
    public Transform puntoInicial;
    private float anguloVision;
    Vector3 directionToTarget;
    NavMeshAgent perseguidor;
    RaycastHit hit;
    AgentState state;

    void Start()
    {
        perseguidor = GetComponent<NavMeshAgent>();
        state = AgentState.Parado;
        anguloVision = Vector3.Angle(transform.forward, directionToTarget);
        directionToTarget = player.transform.position - transform.position;
    }
    
    void FixedUpdate()
    {
        
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        
        if (Physics.Raycast(transform.position, directionToPlayer, out hit,distanceRay,-1, QueryTriggerInteraction.Ignore) )
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);           
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanceRay, Color.red);  
        }

        //InRange(); 

   
       switch (state) {
            case AgentState.Parado:
                if(InRange())
                    SetState(AgentState.Persiguiendo);
                break;
            case AgentState.Persiguiendo:
                if(InRange()) {
                    perseguidor.destination = player.position;                 
                } else {
                    SetState(AgentState.Volviendo);
                }
                break;
            case AgentState.Volviendo:
                if(InRange()) {
                    SetState(AgentState.Persiguiendo);
                } else if (perseguidor.isStopped){
                    SetState(AgentState.Parado);
                }
                break;
       } 
       
        
    }

    
    private bool IsFollow(){
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if(distanceToPlayer < distanceRay && hit.collider.tag == "Player"){
            perseguidor.destination = player.position;
            return true;
        } else {
            perseguidor.destination = puntoInicial.position;
            return false;
        } 
        
    }

    private bool InRange(){
         if(anguloVision > -20 && anguloVision < 20){
            if (hit.collider.tag == "Player") {
                IsFollow();
                return true;
            } else {
                perseguidor.destination = puntoInicial.position;
                return false;
            }
        }     
        return true; 
    }  


    public void SetState(AgentState newState) {
        if(newState != state) {
            state = newState;
            switch (newState){
                case AgentState.Parado:
                break;
                case AgentState.Persiguiendo:
                break;
                case AgentState.Volviendo:
                    perseguidor.destination = puntoInicial.position;
                break;
            }
        }
    }

    public enum AgentState {
        Parado, 
        Persiguiendo, 
        Volviendo, 
    }
}
