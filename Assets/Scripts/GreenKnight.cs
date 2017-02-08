using UnityEngine;
using System.Collections;

public class GreenKnight : Character {

	Vector2	mOffset=Vector2.zero;

	// Use this for initialization
	protected override	void Start () {
		base.Start ();
		mMeshRenderer.material.color = Color.green;
		name = "Green Knight";

	}
	
	// Update is called once per frame
	protected override	void Update () {
		base.Update ();
		mMeshRenderer.material.SetTextureOffset("_MainTex", mOffset);
		mOffset.x += Time.deltaTime*0.1f;
		mOffset.y = mOffset.x;
	}
}
