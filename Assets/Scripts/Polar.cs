using UnityEngine;

public class Polar  {
	//Helper code to map Polar to Cartesian coordinates
	//http://tutorial.math.lamar.edu/Classes/CalcIII/SphericalCoords.aspx
	//Mapping assumes Polar (1,0,0) is forwards by 1 unit

	private	float	mRadius;
	private	float	mAzimuth;
	private	float	mAttitude;


	public	float	Radius {		//Setter & Getter
		get {
			return	mRadius;
		}
		set {
			mRadius = value;
		}
	}

	public	float	Azimuth {		//Clamped to -Pi to + Pi  setter & Getter
		get {
			return	mAzimuth;
		}
		set {
			mAzimuth=Mathf.Clamp (value, -180f, 180f);
		}
	}

	public	float	Attitude {	// Clamped to -Pi/2 to + Pi/2 
		get {
			return	mAttitude;
		}

		set {
			mAttitude=Mathf.Clamp (value, -90f,90f);
		}
	}


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
