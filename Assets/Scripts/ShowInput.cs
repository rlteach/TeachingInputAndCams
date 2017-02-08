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
		mMove.x = InputController.GetInput(InputController.Directions.MoveX);
		mMove.y = InputController.GetInput(InputController.Directions.MoveY);
		mRotate.x = InputController.GetInput(InputController.Directions.RotateX);
		mRotate.y = InputController.GetInput(InputController.Directions.RotateY);
		float tFire = InputController.GetInput(InputController.Directions.Fire);
		float tZoom = InputController.GetInput(InputController.Directions.Zoom);
        mText.text = string.Format("Move {0:f2}\n", mMove);
        mText.text += string.Format("Rotate {0:f2}\n", mRotate);
        mText.text += string.Format("Zoom:{0:f2} Fire:{1:f2}",tZoom,tFire);
    }
}
