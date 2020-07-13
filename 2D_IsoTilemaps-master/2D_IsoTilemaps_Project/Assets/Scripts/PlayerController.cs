using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[System.Serializable]
	public enum StateMachine
	{
		Idle, Walk, Run, Attack, Block
	}

	private StateMachine movementState;

    private Vector3 lookDirection;
	private float horizontalInput;
    private float verticalInput;
    
	private Vector3 moveVector;
	public bool canMove;

	private Animator anim;
    
	// Start is called before the first frame update
    void Start()
    {
        // Initialize variables.
        moveVector = Vector3.zero;
        horizontalInput = 0;
        verticalInput = 0;

		lookDirection = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        anim = GetComponent<Animator>();
    }

	// Update is called once per frame
    void Update()
    {
		verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
		//Debug.Log("X: " + horizontalInput + " | Y: " + verticalInput);
		Move();
        Animate();

		//transform.LookAt(lookDirection);
    }

	void Move()
	{
		if (movementState == StateMachine.Run && canMove == true)
		{
			//lookDirection = new Vector3(transform.position.x + horizontalInput, transform.position.y + verticalInput, transform.position.z);
			// Set player to face the current direction of movement.
		}
	}

	void Animate()
	{
		// Player is moving.
        if (horizontalInput != 0 || verticalInput != 0 /*&& canMove == true*/) 
        {
			SetMovementState(StateMachine.Run);
			
			/*
			FACING DIRECTIONS
			N = 0* | NE = 45* | E = 90* | SE = 135* | S = 180* | SW = 225* | W = 270* | NW = 315*
			*/
			
			// N
			if (horizontalInput == 0 && verticalInput > 0)
			{
				transform.rotation = Quaternion.Euler(-30, 0, 0);
			}
			
			// NE
			if (horizontalInput > 0 && verticalInput > 0)
			{
				transform.rotation = Quaternion.Euler(-15, 45, -15);
			}

			// E
			if (horizontalInput > 0 && verticalInput == 0)
			{
				transform.rotation = Quaternion.Euler(0, 90, -30);
			}

			// SE
			if (horizontalInput > 0 && verticalInput < 0)
			{
				transform.rotation = Quaternion.Euler(15, 135, -15);
			}

			// S
			if (horizontalInput == 0 && verticalInput < 0)
			{
				transform.rotation = Quaternion.Euler(30, 180, 0);
			}

			// SW
			if (horizontalInput < 0 && verticalInput < 0)
			{
				transform.rotation = Quaternion.Euler(15, 225, 15);
			}

			// W
			if (horizontalInput < 0 && verticalInput == 0)
			{
				transform.rotation = Quaternion.Euler(0, 270, 30);
			}

			// NW
			if (horizontalInput < 0 && verticalInput > 0)
			{
				transform.rotation = Quaternion.Euler(-15, 315, 15);
			}

        }
		else
		{
			// No motion detected.
			SetMovementState(StateMachine.Idle);
		}
		anim.SetBool("IsRunning", movementState == StateMachine.Run);
	}
	
	private void SetMovementState(StateMachine newState)
	{
		// Base case.
		if (movementState == newState)
		{
			return;
		}

		// Set the current state to the new desired state.
		movementState = newState;
	}
	
	// Getter used if an outside script wants to modify the state.
	public StateMachine GetMovementState()
	{
		return movementState;
	}
}
