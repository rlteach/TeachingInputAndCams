using UnityEngine;
using System.Collections;

public class PlayerCollide : MonoBehaviour {


	void	Grow() {
		transform.localScale *= 1.02f;
	}

	void	OnCollisionEnter(Collision vCol) {
		if (vCol.gameObject.tag == "Crystal") {
			Crystal tC = vCol.gameObject.GetComponent<Crystal> ();
			tC.PlayerCollided ();
			Invoke ("Grow",0.5f);
		}
	}
}
