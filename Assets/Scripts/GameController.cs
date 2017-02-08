//Code (C) 2017 Richard Leinfellner
//Permission given to use for educational use
using UnityEngine;
using System.Collections;


//Adds a static GameController Input module to the Scene
//Can be attached to a GameObject or will add itself if not


public class GameController : MonoBehaviour {

    public enum Directions { 
        None = 0
        , MoveX
        , MoveY
		, ShiftMoveX
		, ShiftMoveY
        , RotateX
        , RotateY
        , Zoom
        , Fire
		, Jump
        , Count
    }

    private Vector3 mLastPosition;      //Use this to work out changes in mouse position

    private float[] mInputs;        //Array of inputs

    //static reference to Game Controller
    static GameController GC;

    public static uint InputCount {       //Get number of inputs
        get {
            return (uint)Directions.Count;
        }
    }

    //Create Singleton
    void Awake() {
        if (GC == null) {      //If first time keep static reference for easy access
            GC = this;
            DontDestroyOnLoad(gameObject);      //Keep it around during scene loads
            mInputs = new float[InputCount];        //Make Array for all the inputs I am using
        } else if (GC != this) {       //if duplicate, kill it
            Destroy(gameObject);
        }
    }

    static public float GetInput(Directions vFlag) {       //Read Specific normalised value
		if (GC == null || GC.gameObject==null) {			//If this has not been added to the scene, add it
			GameObject	tGO = new GameObject ("GameController");
			tGO.AddComponent <GameController>();		//Add this script to the GameObject
			Debug.Log("Auto added GameController on first use");
		}

        uint tIndex = (uint)vFlag;
        if (tIndex < InputCount) {
            return GC.mInputs[tIndex];           //Get last read value
        }
        return 0f;      //Default if invalid index
    }

    private void SetInput(Directions vFlag, float vValue) {       //Write Specific normalised value
        uint tIndex = (uint)vFlag;
        if (tIndex < InputCount) {
            mInputs[tIndex] = vValue;           //Set Value
        }
    }

    private void UpdateInput(Directions vFlag, float vValue) {        //Update Specific normalised value, if update is zero then damp it, to float to zero
        uint tIndex = (uint)vFlag;
        if (tIndex < InputCount) {
            if (Mathf.Abs(vValue) > Mathf.Epsilon) {
                mInputs[tIndex] = Mathf.Clamp(mInputs[tIndex] + vValue, -1f, 1f);   //Set Value with clamp
            } else {
                mInputs[tIndex] = FloatToZero(mInputs[tIndex]);
            }
        }
    }

    static float FloatToZero(float vValue) {        //Gradually damp a number to zero, regardless of sign
        float tAbsValue = System.Math.Abs(vValue);
        if (tAbsValue > Mathf.Epsilon) {
            return vValue * 0.9f;
        } else {
            return  0f;
        }
    }

    void Update() {
        UpdateInput();
    }
    //Map input to game movements, can be accessed via a static method
    //Read using static public float GetInput(Directions vFlag)
    void UpdateInput() {        //Update Input Array which can be read by all clients


		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			if (Input.GetKey (KeyCode.UpArrow)) {        //Map control to game input
				SetInput (Directions.ShiftMoveY, 1.0f);
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				SetInput (Directions.ShiftMoveY, -1.0f);
			} else {
				SetInput (Directions.ShiftMoveY, 0f);
			}


			if (Input.GetKey (KeyCode.LeftArrow)) {
				SetInput (Directions.ShiftMoveX, -1.0f);
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				SetInput (Directions.ShiftMoveX, 1.0f);
			} else {
				SetInput (Directions.ShiftMoveX, 0f);
			}
		} else {
			if (Input.GetKey(KeyCode.UpArrow)) {        //Map control to game input
				SetInput(Directions.MoveY,1.0f);
			} else if (Input.GetKey(KeyCode.DownArrow)) {
				SetInput(Directions.MoveY, -1.0f);
			} else {
				SetInput(Directions.MoveY, 0f);
			}


			if (Input.GetKey(KeyCode.LeftArrow)) {
				SetInput(Directions.MoveX, -1.0f);
			} else if (Input.GetKey(KeyCode.RightArrow)) {
				SetInput(Directions.MoveX, 1.0f);
			} else {
				SetInput(Directions.MoveX, 0f);
			}
		}



        if (Input.GetKey(KeyCode.Period)) {
            SetInput(Directions.Zoom, -1.0f);
        } else if (Input.GetKey(KeyCode.Comma)) {
            SetInput(Directions.Zoom, 1.0f);
        } else {
            SetInput(Directions.Zoom, 0f);
        }

		if (Input.GetMouseButton(0)) {
			SetInput(Directions.Fire, 1.0f);
		} else {
			SetInput(Directions.Fire, 0f);
		}

		if (Input.GetKey(KeyCode.Return)) {
            SetInput(Directions.Jump, 1.0f);
        } else {
            SetInput(Directions.Jump, 0f);
        }
        Vector3 tCurrentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);      //Map Mouse to rotation
        if (tCurrentPosition.x >= 0 && tCurrentPosition.x <= 1f && tCurrentPosition.y >= 0f && tCurrentPosition.y < 1f) {       //Only Tumble camera if mouse in viewport
            Vector3 tMouseDelta = tCurrentPosition - mLastPosition;
            UpdateInput(Directions.RotateX, tMouseDelta.x);
            UpdateInput(Directions.RotateY, tMouseDelta.y);
        } else {
            SetInput(Directions.RotateX, 0f);
            SetInput(Directions.RotateY, 0f);
        }
        mLastPosition = tCurrentPosition;       //If outside view still update old position so it does not snap on reentering view
    }
}
