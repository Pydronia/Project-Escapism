using UnityEngine;
using System.Collections;

public class objectSelect : MonoBehaviour {
	
	//vive button setup
	//private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	//private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	//SteamVR_TrackedObject controller;

	public GameObject righthand;
	public GameObject currentPoint;

    public GameObject UI;

    public GameObject leftSpeaker;
    //public GameObject rightSpeaker;

    public GameObject effect1;
   // public GameObject effect2;

	Pointer rightScript;


	public void selectObj(){
		Destroy(GameObject.FindWithTag("manip"));
		rightScript.alreadyInst = false;
		rightScript.firstTime = true;
		rightScript.placedObj = effect1;
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

        if (Input.GetKeyDown(KeyCode.C)) {
            if(GameObject.FindWithTag("ui") != null) {
                Destroy(GameObject.FindWithTag("ui"));
            } else {
                Vector3 localPlace = this.transform.position;
                Vector3 offset = new Vector3(0, 0.5f, 0.5f);
                Vector3 toPlace = localPlace + offset;
                Instantiate(UI, toPlace, this.transform.rotation);
            }
            
        }

		//keyboard testing
		if (currentPoint) {
			if (currentPoint.tag == "speaker") {
				if (Input.GetKeyDown (KeyCode.S)) {
                    rightScript.manipObj = null;
					rightScript.alreadyInst = false;
					rightScript.firstTime = true;
					rightScript.placedObj = leftSpeaker;
				}
			}

			if (currentPoint.tag == "effect1") {
				if (Input.GetKeyDown (KeyCode.S)) {
                    rightScript.manipObj = null;
                    rightScript.alreadyInst = false;
					rightScript.firstTime = true;
					rightScript.placedObj = effect1;
				}
				
			}
		}




	}
}
