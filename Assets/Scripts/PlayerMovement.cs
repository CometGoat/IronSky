using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerAnimation playerAnim;

    private void Start()
    {
        playerAnim = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerAnim.TurnLeft();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.ReturnLeft();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            playerAnim.TurnRight();
        }

        if(Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.ReturnRight();
        }

    }
}
