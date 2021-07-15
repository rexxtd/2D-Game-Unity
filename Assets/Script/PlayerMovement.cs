using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;

	public float runSpeed = 40f;
	public float dashSpeed;
	public int stamina = 100;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	bool dash = false;

	[SerializeField] public float delayDashTime = 1f;
	private float canDash = 0f;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Dash") && Time.time > canDash)
		{
			dash = true;
			canDash = Time.time + delayDashTime;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} 
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		if (Input.GetButtonDown("Jump"))
		{
			if (crouch)
				jump = false;
			else 
				jump = true;
		}
	}

	void FixedUpdate()
	{	
		if (dash)
		{
			horizontalMove = Input.GetAxisRaw("Horizontal") * dashSpeed;
			stamina -= 20;
		}
		else if (controller.wallSliding)
			horizontalMove = Input.GetAxisRaw("Horizontal");
		else 
			horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
		dash = false;
	}
}
