using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    public PlayerMovement player;
    private Rigidbody2D rb;

    private LayerMask whatIsLadder;
    public bool canDetect = false;
    public bool canGrabLadder = true;
    public bool canClimb;
    public bool canMoveY;

    public float radius;

     [Header("Ladder Info")]
    [SerializeField] public Vector2 offset1;
    [SerializeField] public Vector2 offset2;
    private Vector2 climbBegan;
    private Vector2 climbOver;
    private Vector2 ladderPosition;
    private GameObject ladder;

    public void Start(){
        canGrabLadder = true;
        whatIsLadder = LayerMask.GetMask("Ladder");
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update(){
        CheckForLadder();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ladder")){
            canDetect = true;
            ladderPosition = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ladder")){
            canDetect = false;
            ladderPosition = Vector2.zero;
        }
    }

    private void CheckForLadder(){
        bool up = Physics2D.OverlapCircle(transform.position + new Vector3(0, (float)-0.85), radius, whatIsLadder);
        bool down = Physics2D.OverlapCircle(transform.position + new Vector3(0, (float)-1.9), radius, whatIsLadder);

        if(canDetect && canClimb && canGrabLadder){
            canGrabLadder = false;

            climbBegan = ladderPosition + offset1;
            climbOver = ladderPosition + offset2;

            canMoveY = true;
            player.speed = 4;
            rb.isKinematic = true;
        }

        if(canMoveY){
            if(!up && player.vertical > 0 || !down && player.vertical < 0){
                FinishClimb();
                return;
            }
        }

    }

    private void FinishClimb(){
        canGrabLadder = true;
        canMoveY = false;
        player.speed = 8;
        rb.isKinematic = false;
    }

    private void OnDrawGizmos(){

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + new Vector3(0, (float)-0.85), radius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, (float)-1.9), radius);
    }
}
