using UnityEngine;
using System.Collections;

public class RedKight : Character {

	Vector2	mOffset=Vector2.zero;

	float	mNumber=0f;

	// Use this for initialization
	protected override	void Start () {
		base.Start ();
		mMeshRenderer.material.color = Color.red;
		name = "Red Knight";
		RotateAround tScript=gameObject.AddComponent<RotateAround> ();
		tScript.Axis = Vector3.right;
		tScript.Speed = 100f;
	}
	
	// Update is called once per frame
	protected override	void Update () {
		base.Update ();
		mMeshRenderer.material.SetTextureOffset("_MainTex", mOffset);
		mOffset.x = Mathf.Cos (mNumber);
		mOffset.y = Mathf.Sin (mNumber);
		mNumber += Time.deltaTime*0.1f;
	}
}
