using UnityEngine;
using System.Collections;

public abstract	class Character : MonoBehaviour {

	protected	MeshFilter	mMeshFilter;
	protected	MeshRenderer mMeshRenderer;
	protected	Rigidbody	mRB;
	protected	Collider	mCOL;

	// Use this for initialization
	protected virtual	void Start () {
		mRB=gameObject.AddComponent<Rigidbody> ();
		mMeshFilter = gameObject.AddComponent<MeshFilter>();
		mMeshRenderer = gameObject.AddComponent<MeshRenderer> ();
		mCOL = gameObject.AddComponent<SphereCollider> ();
		mMeshFilter.mesh = Resources.Load<Mesh> ("Cube2");
		mRB.isKinematic = true;
		mMeshRenderer.material = new Material(Shader.Find("Standard"));
		mMeshRenderer.material.mainTexture = Resources.Load<Texture> ("Checker");
		mMeshRenderer.material.color = Color.black;
		name = "Base Knight";
	}
	
	// Update is called once per frame
	protected virtual	void  Update () {
	
	}
}
