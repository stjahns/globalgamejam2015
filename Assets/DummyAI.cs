using UnityEngine;
using System.Collections;

public class DummyAI : MonoBehaviour {

	public Transform[] _Waypoints;
	public int _NumofWaypoints;
	public float _ObjectiveGap;
	public int _WaypointCounter;
	public float _Speed;
	public Vector3 _Target;
	public float _Distance;

	// Use this for initialization
	void Start () {
		_WaypointCounter= 0;
		go ();
		_Target= _Waypoints[0].position;
		_Distance= Vector3.Distance(transform.position,_Target);
	//	_Waypoints= new Transform[_NumofWaypoints] ;
	}
	void awake(){

	}
	// Update is called once per frame
	void Update () {
		_Distance=Vector3.Distance(transform.position,_Target);
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

	//	transform.LookAt (_Target,Vector3.);
	//	transform.Translate (Vector3.up*_Speed*Time.deltaTime);
		//transform.position.Set (transform.position.x,transform.position.y, ); 

	}
	void stop(){
		_Speed=0f;
	}
	void go(){
		_Speed=1f;
	}

	/*void TargetUpdate(){

		_Target=  _Waypoints[_WaypointCounter].position;
	//	_Direction = Vector3.

		if (_Target.y > _ObjectiveGap){
			_Direction = -Vector2.up;
			Debug.Log ("4");
		}
		else if (_Target.x > _ObjectiveGap){
			_Direction.y = 0f;
			Debug.Log ("5");
		}
		else {
			_Direction =  Vector2.up;
			Debug.Log ("6");
		}
		if (_Target.x >_ObjectiveGap){
			_Direction = -Vector2.right;
			Debug.Log ("1");
		}
		else if (_Target.x > _ObjectiveGap){
			_Direction.x = 0f;
			Debug.Log ("2");
		}
		else {
			_Direction =  Vector2.right;
			Debug.Log ("3");
		}
	}
*/
}
