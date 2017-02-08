//Code (C) 2017 Richard Leinfellner
//Permission given to use for educational use

using UnityEngine;
using System.Collections;

public class LookAtAndFollowCam : MonoBehaviour {

    [Header("Look and follow controls")]
    [Range(1f, 100f)]
    public 	float 	Sensitivity = 10f;     //Sensitivity
	public	bool	Look=true;
	public	bool	Follow=true;

	public	GameObject	Target;		//Follow this target

	Polar	mPolar;		//Polar mapping helper for Camera
		
    void Start() {
		if (Target == null) {       //Keep Camera looking at Parent
			Debug.Log ("No Target to look at");
		} else {
			mPolar = new Polar (transform.position - Target.transform.position);
		}
    }

	//Update Camera so its pointing at Target, cater for Camara Zoom and Move
    void Update () {
		Vector3 tTargetPosition=Vector3.zero;
		if (Target != null) {       //Keep Camera looking at Parent, if set up
			tTargetPosition= Target.transform.position;
		}
		if(Follow) {
			mPolar.Radius += InputController.GetInput(InputController.Directions.Zoom)*Time.deltaTime*Sensitivity;
			mPolar.Azimuth +=  InputController.GetInput (InputController.Directions.ShiftMoveX) * Time.deltaTime*Sensitivity*10f;
			mPolar.Attitude += InputController.GetInput (InputController.Directions.ShiftMoveY) * Time.deltaTime*Sensitivity*10f;

			mPolar.Radius = Mathf.Clamp (mPolar.Radius, 1.5f, 50f);
			mPolar.Azimuth = Mathf.Clamp (mPolar.Azimuth,-135f,135);
			mPolar.Attitude = Mathf.Clamp (mPolar.Attitude,5f,45f);

			transform.position = mPolar.Vector + tTargetPosition;	//Move camera to now location on Camera plane
		}
		if(Look) {
			transform.LookAt (tTargetPosition);	//Look at parent
		}
    }
}
