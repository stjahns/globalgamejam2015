using UnityEngine;
using System.Collections;

public class ThecoupleAI : MonoBehaviour {

  public float myz;
  public bool guy;
  public int timer;
  public int counter;
  public GameObject Body;
  public int _WaypointCounter;
  public bool doit;
  public bool undoit;
  public Transform[] _Waypoints;
  public float _ObjectiveGap;
  public float _Speed;
  public RaycastHit2D hit;

  public Vector3 _Target;
  public float _Distance;
  // Use this for initialization
  void Start () {
     Body= GameObject.FindGameObjectWithTag("Body");
    _Target= _Waypoints[1].position;
  }
  void Go(){

  }
  void setz(float SOBADANDWRONG){
    myz = SOBADANDWRONG;
  }
  // Update is called once per frame

  void Update () {
    hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y),transform.rigidbody2D.position - Body.transform.position.XY());
//		Debug.Log (hit.collider.name);
//		Debug.DrawLine(transform.position,Body.transform.position);
//		if (testsight()){}                  //THEY SAW THE BODY!!!!

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

      var facingRotation = Quaternion.FromToRotation(Vector2.up, _Target - transform.position);
      transform.rotation = facingRotation;

    }
    if (undoit){
      counter++;
      if (counter > timer){

        transform.position= Vector3.MoveTowards (transform.position,_Target,1f*_Speed* Time.deltaTime);

        var facingRotation = Quaternion.FromToRotation(Vector2.up, _Target - transform.position);
        transform.rotation = facingRotation;

        _Distance= Vector3.Distance(transform.position,_Target);
        if (_Distance < _ObjectiveGap){
          if( _WaypointCounter > 0) {
            _WaypointCounter--;
            _Target= _Waypoints[_WaypointCounter].position;
        }
        else {
          undoit = false;
            counter = 0;
          _WaypointCounter= 0;

          }
        }
      }
    }

  }
}
