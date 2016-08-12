using UnityEngine;
using System.Collections;

public class objectSelect : MonoBehaviour {
	
	//vive button setup
	//private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	//private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	//SteamVR_TrackedObject controller;

	public GameObject righthand;

	GameObject toSend;

	Pointer rightScript;


	public void selectObj(){
		Destroy(GameObject.FindWithTag("manip"));
		rightScript.alreadyInst = false;
		rightScript.firstTime = true;
		toSend = (GameObject)Resources.Load ("Prefabs/Sphere Test");
		rightScript.placedObj = toSend;
	}

	void Start () {
		//controller = GetComponent<SteamVR_TrackedObject> ();
		rightScript = righthand.GetComponent<Pointer>();
	}
	
	// Update is called once per frame
	void Update () {
		//steamVR Controller setup
		//SteamVR_Controller.Device device = SteamVR_Controller.Input((int)controller.index);
        /*
		if (device.GetPressDown (triggerButton)) {
			Destroy(GameObject.FindWithTag("manip"));
			rightScript.alreadyInst = false;
			rightScript.firstTime = true;
			toSend = (GameObject)Resources.Load ("Prefabs/Sphere Test");
			rightScript.placedObj = toSend;
		}
		if (device.GetPressDown (gripButton)){
			Destroy(GameObject.FindWithTag("manip"));
			rightScript.alreadyInst = false;
			rightScript.firstTime = true;
			toSend = (GameObject)Resources.Load ("Prefabs/Cube Test");
			rightScript.placedObj = toSend;
		}
        */
		//keyboard testing
		if (Input.GetKeyDown (KeyCode.S)) {
			Destroy(GameObject.FindWithTag("manip"));
			rightScript.alreadyInst = false;
			rightScript.firstTime = true;
			toSend = (GameObject)Resources.Load ("Prefabs/Sphere Test");
			rightScript.placedObj = toSend;
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			Destroy(GameObject.FindWithTag("manip"));
			rightScript.alreadyInst = false;
			rightScript.firstTime = true;
			toSend = (GameObject)Resources.Load ("Prefabs/Cube Test");
			rightScript.placedObj = toSend;
		}
	}
}
