using UnityEngine;
using System.Collections;

public class PowerupRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.AddTorque(new Vector3 (0, 40, 0));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
