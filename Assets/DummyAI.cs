using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DummyAI : TriggerBase {

    public Transform[] _Waypoints;
    public int _NumofWaypoints;
    public float _ObjectiveGap;
    public int _WaypointCounter;
    public float _Speed;
    public Vector3 _Target;
    public float _Distance;
    public float POVwidth = 30f;

    private GameObject Body;
    private bool _spottedBody = false;

    private Animator _animator;

    [OutputEventConnections]
    [HideInInspector]
    public List<SignalConnection> OnBodySpotted = new List<SignalConnection>();

    void Start () {

        Body = GameObject.FindGameObjectWithTag("Body");

        _WaypointCounter= 0;

        stop ();

        _Target= _Waypoints[0].position;

        _animator = GetComponent<Animator>();
    }

    bool CheckBodySight(){

        var hit = Physics2D.Linecast(transform.position, Body.transform.position, LayerMask.GetMask("Level", "Body"));

        Debug.DrawLine(transform.position, Body.transform.position);

        if ( Mathf.Abs(Vector3.Angle (Body.transform.position - transform.position, transform.up)) < POVwidth && hit.collider.CompareTag("Body")){

            return true;
        }

        return false;
    }



    void Update () {

        _animator.SetFloat("MoveSpeed", _Speed);


        if (!_spottedBody && CheckBodySight()){
            print("SPOTTED");
            _spottedBody = true;
            stop();
            OnBodySpotted.ForEach(s => s.Fire());
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

        transform.position= Vector3.MoveTowards(transform.position, _Target, _Speed * Time.deltaTime);

        var facingRotation = Quaternion.FromToRotation(Vector2.up, _Target - transform.position);
        transform.rotation = facingRotation;

    }

    [InputSocket]
    public void stop(){
        _Speed=0f;
    }

    [InputSocket]
    public void go(){
        _Speed=3f;
    }




}
