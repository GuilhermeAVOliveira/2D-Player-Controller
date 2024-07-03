using Unity.VisualScripting;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    [SerializeField] private ClimbingScript climbing;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float radius;
    private LadderScript ladder;
    public bool canDetect = true;

    private void Start(){
        ladder = GetComponentInParent<LadderScript>();
    }

    private void Update(){
        if(canDetect && !ladder.canDetect){
            climbing.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius, whatIsGround);
        }
        if(!canDetect){
            climbing.ledgeDetected = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground")){
            canDetect = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground")){
            canDetect = true;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
