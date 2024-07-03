using System;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float horizontal;
    public float vertical;
    public float speed = 8f;
    private bool isFacingRight = true;

    public ClimbingScript climbing;
    public LadderScript ladder;
    public RunningScript running;
    public CrouchingScript crouch;

    void Update()
    {
        Debug.Log(speed);
        if(!ladder.canMoveY){
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        } else if(ladder.canMoveY){
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        if(!isFacingRight && horizontal > 0f){
            Flip();
        }else if(isFacingRight && horizontal < 0f){
            Flip();
        }
    }

    private void Flip(){
        isFacingRight = !isFacingRight;
        Vector3 localscale = transform.localScale;
        localscale.x *= -1f;
        transform.localScale = localscale;
        climbing.offset1.x *= -1;
        climbing.offset2.x *= -1;
    }

    public void Move(InputAction.CallbackContext context){
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    public void Climb(InputAction.CallbackContext context){
        if(context.performed){
           climbing.canClimb = true;
        }
        if(context.canceled){
            climbing.canClimb = false;
        }
    }
    public void Interact(InputAction.CallbackContext context){
        if(context.performed){
            ladder.canClimb = true;
        }
        if(context.canceled){
            ladder.canClimb = false;
        }
    }

    public void Run(InputAction.CallbackContext context){
        if(context.performed && !running.isRunning){
            if(!crouch.isCrouching) running.isRunning = true;
        } else if(context.performed && running.isRunning){
            running.isRunning = false;
        }
    }

    public void Crouch(InputAction.CallbackContext context){
         if(context.performed && !crouch.isCrouching){
            if(!running.isRunning) crouch.isCrouching = true;
        } else if(context.performed && crouch.isCrouching){
            crouch.isCrouching = false;
        }
    }
}
