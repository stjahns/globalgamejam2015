using UnityEngine;
using System.Collections;

public class DummyAI : MonoBehaviour {
	public float myz;
	public GameObject ugly;
	public Transform[] _Waypoints;
	public int _NumofWaypoints;
	public float _ObjectiveGap;
	public int _WaypointCounter;
	public float _Speed;
	public Vector3 _Target;
	public float _Distance;
	public GameObject Body;
	public RaycastHit2D hit;
	public float POVwidth = 30f;

	// Use this for initialization
	void Start () {
		Body= GameObject.FindGameObjectWithTag("Body");
		_WaypointCounter= 0;
		go ();
		_Target= _Waypoints[0].position;
		_Distance= Vector3.Distance(transform.position,_Target);
	//	_Waypoints= new Transform[_NumofWaypoints] ;
	}
	void awake(){

	}
	void setz(float SOBADANDWRONG){
		myz = SOBADANDWRONG;
	}
	bool testsight(){
	
		if (  Mathf.Abs(Vector3.Angle (transform.position - Body.transform.position, transform.right))< POVwidth && hit.collider.CompareTag ("Body")){
		
			return true;
		}

		return false;
	}



	void Update () {
		hit = Physics2D.Linecast(transform.position,Body.transform.position, 1 << LayerMask.NameToLayer("Body"));
		Debug.DrawLine(transform.position,Body.transform.position);


		if (testsight ()){
			Debug.Log ("GAMMMMEOVER");                           //THEYSAW THE BODY
		}
	//	Debug.Log (hit.collider.name);
	//	Debug.Log (hit.collider.name);

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
		ugly.SendMessage("gimmie",_Target);
		transform.eulerAngles= new Vector3 (0f,0f,myz);// transform.eulerAngles(0f,0f, myz);
	
	
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
