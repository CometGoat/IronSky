using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // VARIABLES

    PlayerAnimation playerAnim;
    Vector2 movementDirection;
    [SerializeField] float speed = 5f;

    bool turningLeft;
    bool turningRight;

    Rigidbody2D rb;

    // EXECUTION FUNCTIONS

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        GetInput();

        /*
        if (Input.GetKeyDown(KeyCode.A)) playerAnim.TurnLeft();
        if (Input.GetKeyUp(KeyCode.A)) playerAnim.ReturnLeft();

        if(Input.GetKeyDown(KeyCode.D)) playerAnim.TurnRight();
        if(Input.GetKeyUp(KeyCode.D)) playerAnim.ReturnRight();
        */
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + (movementDirection * speed) * Time.fixedDeltaTime);
    }

    // METHODS

    void GetInput() {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");

        if (movementDirection.x < 0 && !turningLeft) {
            playerAnim.TurnLeft();
            turningLeft = true;
        }
        if (movementDirection.x == 0 && turningLeft) {
            playerAnim.ReturnLeft();
            turningLeft = false;
        }

        if (movementDirection.x > 0 && !turningRight) {
            playerAnim.TurnRight();
            turningRight = true;
        }
        if (movementDirection.x == 0 && turningRight) {
            playerAnim.ReturnRight(); 
            turningRight = false;
        }
    }

    void Movement() {
        
    }
}
