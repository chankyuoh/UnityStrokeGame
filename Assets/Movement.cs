using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
	public float m_direction = 1f;
	public float m_speed = 9f;
	public float m_jumpPower = 1f;
	public Text repCountText;

	private bool isGrounded = true;
	private bool isOverThreshold = false;
	private  float distToGround;
	private Rigidbody2D m_rigidBody;
	private Collision2D m_collision;

	private int repCounter;


	// Use this for initialization
	void Start () {
		m_rigidBody = GetComponent<Rigidbody2D> ();
		repCounter = 0;
		setCountText ();
	}
		
	
	// Update is called once per frame
	private void FixedUpdate () {
			    
		Move ();

		//print(Input.acceleration.y);
		if (Mathf.Abs(Input.acceleration.y) > .9f)
		{
			isOverThreshold = true;
			//print("JUMPING! x,y,z is: ");
			//print (Input.acceleration.x);
			print(Input.acceleration.y);
			//print(Input.acceleration.z);

			jump ();
		}

		if (isOverThreshold && Mathf.Abs(Input.acceleration.y) < .5f) {
			print (Input.acceleration.y);
			repCounter += 1;
			isOverThreshold = false;
			setCountText ();
		}


		if (Input.GetKeyDown (KeyCode.Space))
		{
			//print("JUMPING! x,y,z is: ");
			//print (Input.acceleration.x);
			//print(Input.acceleration.z);

			jump ();
		}

			

	}

	void setCountText() {
		repCountText.text = "Rep Count: " + repCounter.ToString ();
	}
		

	private void jump() {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0,100), ForceMode2D.Impulse);
		isGrounded = false;
	}


	private void Move()
	{
		// Adjust the position of the tank based on the player's input.
		//Vector2 movement = transform.forward*1* m_speed * Time.deltaTime;
		Vector2 movement;
		if (!isGrounded) 
		{
			movement = new Vector2(m_direction,0);
		} 
		else 
		{
			movement = new Vector2(m_direction,m_rigidBody.velocity.y);
		}
			
		movement *= Time.deltaTime*m_speed;
		//print (movement);
		m_rigidBody.MovePosition (m_rigidBody.position + movement);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		print (coll);
		if (coll.gameObject.tag == "RightWall" || coll.gameObject.tag == "LeftWall")
			m_direction *= -1;
		if (coll.gameObject.tag == "Ground"){
			//print ("COLLIDED WITH GROUND");
			/*
			if (!isGrounded) {
				repCounter += 1;
				print (repCounter);
			}
			*/

			isGrounded = true;
		}

	}
}
