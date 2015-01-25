using UnityEngine;
using System.Collections;

public class UGLY : MonoBehaviour {
	public GameObject parent;
	// Use this for initialization
	void Start () {
	
		transform.position= parent.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		transform.position= parent.transform.position;
	}
	void gimmie(Vector3 target){
		transform.LookAt (target);
		sendugly (transform.rotation.eulerAngles.x);
	//	Debug.Log ("got from SPRITE");
	}
	void sendugly(float BBBBAD){
	//	Debug.Log ("SENDING to SPRITE");
		parent.SendMessage("setz", BBBBAD);
	}
}
