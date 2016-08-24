using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour {
	
	//Vive button setup
	//private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	//private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	//private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	//SteamVR_TrackedObject controller;

	public GameObject placedObj;

	public GameObject leftHand;

	public float rotationSpeed = 1.0f;

	public float laserWidth = 0.01F;
	public Material LaserEffect;

	public GameObject manipObj;
	GameObject[] existObj;
    GameObject[] layerObj;

	public bool alreadyInst = false;

	LineRenderer myLine;
	RaycastHit hitPos;
	Quaternion rotation;

	Vector3 placingPos;
	Vector3 placingExt;
	Vector3 shiftDir;

	int layerMask = 1 << 8;

	public bool firstTime = true;



	void Start () {

		//controller = GetComponent<SteamVR_TrackedObject> ();
		myLine = gameObject.AddComponent<LineRenderer> ();
		myLine.SetWidth (laserWidth, laserWidth);
		myLine.enabled = false;
		myLine.material = LaserEffect;
		layerMask = ~layerMask;


	}

	void Update () {
		//steamVR Controller setup
		//SteamVR_Controller.Device device = SteamVR_Controller.Input((int)controller.index);

		//raycasting/laser

		Ray pointerRay = new Ray (this.transform.position, this.transform.forward);
		bool hit = Physics.Raycast (pointerRay, out hitPos, 100, layerMask);
		if (hit) {
            myLine.enabled = true;
            myLine.SetPosition(0, transform.position);
            myLine.SetPosition(1, hitPos.point);
            leftHand.GetComponent<objectSelect>().currentPoint = hitPos.transform.gameObject;
        } else {
			myLine.enabled = false;
		}

        //send current point to left hand
        


        //if there's a object selected, it's not been instantiated, raycast hits something, and what it's hitting is environment
        if (placedObj) {
			if (alreadyInst == false) {
				if (hit) {
					if (hitPos.collider.gameObject.layer != 5) {
						if (firstTime) {
							shift ();
							manipObj = (GameObject)Instantiate (placedObj, shift (), Quaternion.identity);
							firstTime = false;
						} else {
							manipObj = (GameObject)Instantiate (placedObj, shift (), rotation);
						}

						manipObj.tag = "manip";
						alreadyInst = true;
					} 
				}
			} else if (!hit || hitPos.collider.gameObject.layer == 5) {//if it's not hitting anything, save rotation, destroy it and reinstantiate it when it hits something again (dont place on ui either)
				rotation = manipObj.transform.rotation;
				Destroy (manipObj);
				alreadyInst = false;
			} else {//repositioning

				reposition ();
                
				/*
				//rotate vive
				Vector2 touchPos = device.GetAxis();
				if (touchPos.x > 0.5 && device.GetPress (touchpad)) {
					manipObj.transform.Rotate (Vector3.down * rotationSpeed, Space.Self);
				}
				if (touchPos.x < -0.5 && device.GetPress (touchpad)) {
					manipObj.transform.Rotate (Vector3.up * rotationSpeed, Space.Self);
				}

				//Place the object! Vive
				if (device.GetPressDown (triggerButton)) {
					manipObj.tag = "object";
					manipObj.layer = 0;
					firstTime = true;
					placedObj = null;
					manipObj = null;
					alreadyInst = false;
				}
                */

				//rotate keyboard
				if (Input.GetKey (KeyCode.Q)) {
					manipObj.transform.Rotate (Vector3.up * rotationSpeed, Space.Self);
				}
				if (Input.GetKey (KeyCode.E)) {
					manipObj.transform.Rotate (Vector3.down * rotationSpeed, Space.Self);
				}

				//Place the object! Keyboard
				if (Input.GetKeyDown (KeyCode.P)) {
					manipObj.layer = 9;
					firstTime = true;
					placedObj = null;
					manipObj = null;
					alreadyInst = false;
				}
			}
				
		}


		//delete the objects
        /*
		if (device.GetPressDown (gripButton)){
			deleteObjs ();
		}
        */
		if (Input.GetKeyDown (KeyCode.D)) {
			deleteObjs ();
		}




	}


	Vector3 shift(){
		placingPos = hitPos.point;
		placingExt = placedObj.GetComponent<MeshRenderer> ().bounds.extents;
		shiftDir = hitPos.normal;
		Vector3 temp;
		int roundedx = (int)Mathf.RoundToInt (shiftDir.x);
		int roundedy = (int)Mathf.RoundToInt (shiftDir.y);
		int roundedz = (int)Mathf.RoundToInt (shiftDir.z);

		if (roundedx != 0) {
			if (roundedx == 1) {
				//Debug.Log ("right");
				temp = placingPos;
				temp.x = placingPos.x + placingExt.x;
				return(temp);
			} else {
				//Debug.Log ("left");
				temp = placingPos;
				temp.x = placingPos.x - placingExt.x;
				return(temp);
			}
		} else if (roundedy != 0) {
			if (roundedy == 1) {
				//Debug.Log ("up");
				temp = placingPos;
				temp.y = placingPos.y + placingExt.y;
				return(temp);
			} else {
				//Debug.Log ("down");
				temp = placingPos;
				temp.y = placingPos.y - placingExt.y;
				return(temp);
			}
		} else {
			if (roundedz == 1) {
				//Debug.Log ("forward");
				temp = placingPos;
				temp.z = placingPos.z + placingExt.z;
				return(temp);
			} else {
				//Debug.Log ("back");
				temp = placingPos;
				temp.z = placingPos.z - placingExt.z;
				return(temp);
			}
		}

	}

	void reposition (){
		manipObj.transform.position = shift();
	}

	void deleteObjs(){
        existObj = FindObjectsOfType(typeof(GameObject)) as GameObject[];

		for (int i = 0; i < existObj.Length; i++) {
            if(existObj[i].layer == 9) {
                Destroy(existObj[i]);
            }
		}
	}
}
