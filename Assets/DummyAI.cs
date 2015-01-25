using UnityEngine;
using System.Collections;

public class DummyAI : TriggerBase {
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
        stop ();
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

    }

    [InputSocket]
    public void stop(){
        _Speed=0f;
    }

    [InputSocket]
    public void go(){
        _Speed=5f;
    }




}
