using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {

	[Header("Rotate around Controls")]
	public	GameObject	Target;
	public	float 		Speed;
	public	Vector3		Axis=Vector3.up;


	// Update is called once per frame
	void Update () {
		if (Target != null) {
			transform.RotateAround (Target.transform.position, Axis, Time.deltaTime*Speed);
		} else {
			transform.RotateAround (Vector3.zero, Axis, Time.deltaTime*Speed);
		}
	}
}
