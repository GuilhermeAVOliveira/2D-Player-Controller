using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClimbingScript : MonoBehaviour
{
    public bool ledgeDetected;

    [Header("Ledge Info")]
    [SerializeField] public Vector2 offset1;
    [SerializeField] public Vector2 offset2;
    private Vector2 climbBeganPosition;
    private Vector2 climbOverPosition;
    private bool canGrabEdge = true;
    public bool canClimbLedge;
    public bool canClimb;

    private void Update(){
        CheckForLedge();
    }

    private void CheckForLedge(){
        if(ledgeDetected && canGrabEdge && canClimb){
            canGrabEdge = false;

            Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position;

            climbBeganPosition = ledgePosition + offset1;
            climbOverPosition = ledgePosition + offset2;

            canClimbLedge = true;
        }

        if(canClimbLedge)
            transform.position = climbBeganPosition;
        }

    private void LedgeClimbOver(){
        canClimbLedge = false;
        transform.position = climbOverPosition;
        Invoke("AllowGrabLedge", 0.1f);
    }

    private void AllowGrabLedge() => canGrabEdge = true;
}
