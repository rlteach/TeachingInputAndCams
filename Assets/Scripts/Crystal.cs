using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour {

	public	float	KillTime=10.0f;

	Animator	mANI;
	MeshRenderer mMR;

	public	Color	FromColour;
	public	Color	ToColour;
	float	mColour=0.0f;

	// Use this for initialization
	void 	Start () {
		mANI = GetComponent<Animator> ();
		mMR = GetComponent<MeshRenderer> ();
		mANI.SetFloat("Speed",Random.Range(0.1f,1.5f));
	}


	public	void	ColourCycle(float vTime) {		//Colour Cycle Crystals
		mMR.material.color = Vector4.Lerp (FromColour, ToColour, Mathf.Sin (vTime * Mathf.PI * 2f));
	}


	public	void	PlayerCollided() {
		GetComponent<Collider> ().enabled = false;
		mANI.SetTrigger ("Eaten");
	}

	public	void	Eaten() {
		Destroy (gameObject);
	}
}
