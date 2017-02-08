using UnityEngine;
using System.Collections;

public class GrowCrystal : MonoBehaviour {

	public	GameObject	Player;

	public	GameObject	Crystal;

	private	Terrain	mT;

	// Use this for initialization
	void Start () {
		mT = GetComponent<Terrain> ();
		InvokeRepeating ("Grow", 0, 2f);
	}

	void	Grow() {
		Vector3	tSpawnPoint = new Vector3 (Random.Range (0.1f, 1f), 0f,Random.Range (0.1f, 1f));
		float	tDistance = 10f;
		tSpawnPoint = tSpawnPoint.normalized*tDistance+Player.transform.position;
		tSpawnPoint.y = mT.SampleHeight (tSpawnPoint);
		GameObject	mGO = Instantiate (Crystal);
		mGO.transform.position = tSpawnPoint;
	}

}
