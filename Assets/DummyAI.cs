using UnityEngine;
using System.Collections;

public class DummyAI : MonoBehaviour {

	public Transform[] _Waypoints;
	public int _NumofWaypoints;
	public float _ObjectiveGap;
	public int _WaypointCounter;

	// Use this for initialization
	void Start () {
		_WaypointCounter=0;
		_Waypoints= new Transform[_NumofWaypoints] ;
	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position,_Waypoints[_WaypointCounter].position)< _ObjectiveGap){

		}
	}
}
