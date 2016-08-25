using UnityEngine;
using System.Collections;

public class Toggle3DCeption : MonoBehaviour {



	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.T)) {
			this.GetComponent<TBE.Components.TBE_SourceControl> ().enabled = !this.GetComponent<TBE.Components.TBE_SourceControl> ().enabled;
		}
	}
}
