using UnityEngine;
using System.Collections;

public class ThecoupleAI : MonoBehaviour {

	public bool guy;
	public int timer;
	public int counter;

	public int _WaypointCounter;
	public bool doit;
	public bool undoit;
	public Transform[] _Waypoints;
	public float _ObjectiveGap;
	public float _Speed;

	public Vector3 _Target;
	public float _Distance;
	// Use this for initialization
	void Start () {

		_Target= _Waypoints[1].position;
	}
	void Go(){
		
	}
	// Update is called once per frame
	void Update () {
		if (!doit){
			counter++;
		}
		if (counter >= timer && !undoit){
			doit = true;                            //THIS SHOULD REALLY NOT RUN ON A TRIGGER AND SHOULD GET CALLED FROM ELSEWHERE
		}
		if(doit){
			_Distance= Vector3.Distance(transform.position,_Target);
			if (_Distance < _ObjectiveGap){
				if( _WaypointCounter < _Waypoints.Length - 1){
					_WaypointCounter++;
					_Target= _Waypoints[_WaypointCounter].position;
					//doit = false;
					//_WaypointCounter--;
				}
				else{
					Debug.Log ("HEY CODE ME MUTHSFUCKER");   // do something on a coroutine probably
															//HOwever, let.s back em up
					doit= false;
					counter = 0;
					undoit = true;
					_WaypointCounter--;
					_Target= _Target= _Waypoints[_WaypointCounter].position;
					_Distance= Vector3.Distance(transform.position,_Target);
				}

			}
			transform.position= Vector3.MoveTowards (transform.position,_Target,1f*_Speed* Time.deltaTime);
		}
		if (undoit){
			counter++;
			if (counter > timer){

				transform.position= Vector3.MoveTowards (transform.position,_Target,1f*_Speed* Time.deltaTime);
				_Distance= Vector3.Distance(transform.position,_Target);
				if (_Distance < _ObjectiveGap){
					if( _WaypointCounter > -1) {
						_WaypointCounter--;
						_Target= _Waypoints[_WaypointCounter].position;
					//doit = false;
					//_WaypointCounter--;
				}
				else {
					undoit = false;
						counter = 0;
					_WaypointCounter= 0;

					}
				}
			}
		}

	/*	_Distance=Vector3.Distance(transform.position,_Target);
		if (_Distance< _ObjectiveGap){
			if( _WaypointCounter == _Waypoints.Length - 1){
				_WaypointCounter=0;
			}
			else{
				_WaypointCounter++;
			}
			_Target= _Waypoints[_WaypointCounter].position;
		}
		transform.position= Vector3.MoveTowards (transform.position,_Target,1f*_Speed* Time.deltaTime);
		transform.position= Vector3.MoveTowards (transform.position,_Target,1f*_Speed* Time.deltaTime);
	}*/
	
	}
}
