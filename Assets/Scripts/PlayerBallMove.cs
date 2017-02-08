using UnityEngine;
using System.Collections;

public class PlayerBallMove : MonoBehaviour {

	[Header("Settings")]
	public	float	Sensitivity=5f;


	Rigidbody	mRB;
	Vector3		mForce=Vector3.zero;

	// Use this for initialization
	void Start () {
		mRB = GetComponent<Rigidbody> ();		//Get Reference to RB to move it
	}
	
	// FixedUpdate is called once per physics frame, this is locked to a fixed framerate
	void FixedUpdate () {
		mForce.x = InputController.GetInput (InputController.Directions.MoveX);		
		mForce.z = InputController.GetInput (InputController.Directions.MoveY);
		mRB.AddForce (mForce*Sensitivity);
		mForce.x = mForce.z = 0f;
		mForce.y = InputController.GetInput (InputController.Directions.Jump);
		mRB.AddForce (mForce,ForceMode.Impulse);
	}
}
