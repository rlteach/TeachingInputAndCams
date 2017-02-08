//Code (C) 2017 Richard Leinfellner
//Permission given to use for educational use

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowInput : MonoBehaviour {

    Text mText;

    Vector2 mMove = Vector2.zero;
    Vector2 mRotate = Vector2.zero;

    void Start () {
        mText = GetComponent<Text>();        	
	}
	
	//Example of how to read the input from central manager
	void Update () {
        mMove.x = GameController.GetInput(GameController.Directions.MoveX);
        mMove.y = GameController.GetInput(GameController.Directions.MoveY);
        mRotate.x = GameController.GetInput(GameController.Directions.RotateX);
        mRotate.y = GameController.GetInput(GameController.Directions.RotateY);
        float tFire = GameController.GetInput(GameController.Directions.Fire);
        float tZoom = GameController.GetInput(GameController.Directions.Zoom);
        mText.text = string.Format("Move {0:f2}\n", mMove);
        mText.text += string.Format("Rotate {0:f2}\n", mRotate);
        mText.text += string.Format("Zoom:{0:f2} Fire:{1:f2}",tZoom,tFire);
    }
}
