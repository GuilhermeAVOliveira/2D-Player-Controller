using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingScript : MonoBehaviour
{
    public PlayerMovement player;
    public bool isCrouching;
    private float crouchSpeed = 2;
    private float normalSpeed;

    public BoxCollider2D standCol;

    void Start()
    {
        normalSpeed = player.speed;
    }

    void Update()
    {
        if(isCrouching){
            Crouching();
        }else if(!isCrouching){
            Standing();
        }
    }

    private void Standing(){
         player.speed = normalSpeed;
         standCol.enabled = true;
    }

    private void Crouching(){
        player.speed = crouchSpeed;
        standCol.enabled = false;
    }
}
