//Code (C) 2017 Richard Leinfellner
//Permission given to use for educational use

using UnityEngine;
using System.Collections;

public class Polar  {			//Helper code to map Polar to Cartesian coordinates
	//http://tutorial.math.lamar.edu/Classes/CalcIII/SphericalCoords.aspx
	//Mapping assumes Polar (1,0,0) is forwards by 1 unit

	public	float	Radius;
	public	float	Azimuth;
	public	float	Attitude;


	public	Polar(float tRadius=0.0f,float tAzimuth=0.0f,float tAttitude=0.0f) {
		Radius = tRadius;
		Azimuth = tAzimuth;
		Attitude = tAttitude;
	}

	public	Polar(Vector3 vVector) {
		Vector = vVector;
	}
		

	public	Vector3	Vector {
		get {
			Azimuth = Mathf.Clamp (Azimuth, -180f, 180f);		//Clamp Aziumuth
			Attitude = Mathf.Clamp (Attitude, -90f,90f);		//Clamp Attiude
			float	tX = Mathf.Sin (Azimuth*Mathf.Deg2Rad)*Mathf.Cos(Attitude*Mathf.Deg2Rad);
			float	tY = Mathf.Sin(Attitude*Mathf.Deg2Rad);
			float	tZ = -Mathf.Cos (Azimuth*Mathf.Deg2Rad)*Mathf.Cos(Attitude*Mathf.Deg2Rad);	
			return new Vector3 (tX, tY, tZ) * Radius;
		}
		set {
			Radius = value.magnitude;
			Azimuth = Mathf.Atan2 (value.x,-value.z)*Mathf.Rad2Deg;
			Attitude = Mathf.Asin (value.y/Radius)*Mathf.Rad2Deg;
		}
	}

	public override	string	ToString() {
		return	string.Format("({0:f2},{1:f2},{2:f2})",Radius,Azimuth,Attitude);
	}
}



public class PositionCamera : MonoBehaviour {

    [Header("Controls")]
    [Range(1f, 100f)]
    public float Sensitivity = 10f;     //Sensitivity

	float	mDistance;		//
	float	mA;		//
	float	mP;		//

	public	GameObject	Target;		//Follow this target

	Polar	mPolar;		//Polar mapping helper for Camera
		
    void Start() {
		mPolar=new Polar(transform.position-Target.transform.position);
    }

	//Update Camera so its pointing at Target, cater for Camara Zoom and Move
    void Update () {
		mPolar.Radius += GameController.GetInput(GameController.Directions.Zoom)*Time.deltaTime*Sensitivity;
		mPolar.Azimuth +=  GameController.GetInput (GameController.Directions.ShiftMoveX) * Time.deltaTime*Sensitivity*10f;
		mPolar.Attitude += GameController.GetInput (GameController.Directions.ShiftMoveY) * Time.deltaTime*Sensitivity*10f;

		mPolar.Radius = Mathf.Clamp (mPolar.Radius, 1.5f, 50f);
		mPolar.Azimuth = Mathf.Clamp (mPolar.Azimuth,-135f,135);
		mPolar.Attitude = Mathf.Clamp (mPolar.Attitude,5f,45f);

		transform.position =mPolar.Vector+Target.transform.position;	//Move camera to now location on Camera plane

        if (Target == null) {       //Keep Camera looking at Parent
			Debug.Log("No Parent to look at");
        } else {
            transform.LookAt(Target.transform.position);	//Look at parent
        }
    }
}
