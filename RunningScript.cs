using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningScript : MonoBehaviour
{
    public PlayerMovement player;
    public bool isRunning;
    private float runningSpeed = 32f;
    private float normalSpeed;

    void Start(){
        normalSpeed = player.speed;
    }

    void Update(){
        if(isRunning){
            Running();
        }else if(!isRunning){
            Walking();
        }
    }

    private void Running(){
         player.speed = runningSpeed;
    }

    private void Walking(){
        player.speed = normalSpeed;
    }
}
