﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DummyAI : StateMachineBase {

    public Transform[] _Waypoints;
    public int _NumofWaypoints;
    public float _ObjectiveGap;
    public int _WaypointCounter;
    public float _Speed;
    public Vector3 _Target;
    public float _Distance;
    public float POVwidth = 30f;

    private GameObject[] Bodies;
    private bool _spottedBody = false;

    private Animator _animator;

    public bool RepeatWaypoints = false;
    private float LookDistance;

    public AudioClip alertSound;

    [OutputEventConnections]
    [HideInInspector]
    public List<SignalConnection> OnBodySpotted = new List<SignalConnection>();


    public enum State
    {
        FollowingWaypoints,
        Stopped
    }

    public State InitialState;

    void Start () {
        Bodies = GameObject.FindGameObjectsWithTag("Body");
        _animator = GetComponent<Animator>();

        currentState = InitialState;
        LookDistance = 10f;
    }

    IEnumerator FollowingWaypoints_EnterState()
    {
        _WaypointCounter = 0;
        _Speed= 3f;
        _Target= _Waypoints[0].position;
        yield return 0;
    }

    bool CheckBodySight(){

        foreach (var Body in Bodies)
        {
            if ((Body.transform.position - transform.position).magnitude > LookDistance)
                continue;

            var hit = Physics2D.Linecast(transform.position, Body.transform.position, LayerMask.GetMask("Level", "Body"));

            Debug.DrawLine(transform.position, Body.transform.position);

            if ( Mathf.Abs(Vector3.Angle (Body.transform.position - transform.position, transform.up)) < POVwidth && hit.collider.CompareTag("Body")){

                return true;
            }
        }

        return false;
    }

    void FollowingWaypoints_Update () {

        _Distance = Vector3.Distance(transform.position, _Target);

        if (_Distance < _ObjectiveGap){

            if( _WaypointCounter == _Waypoints.Length - 1){

                if (RepeatWaypoints)
                {
                    _WaypointCounter=0;
                }
                else
                {
                    stop();
                }
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

    protected override void Update () {

        base.Update();

        _animator.SetFloat("MoveSpeed", _Speed);

        if (!_spottedBody && CheckBodySight()) {
            _spottedBody = true;
            AudioSource.PlayClipAtPoint(alertSound, transform.position);
            stop();
            OnBodySpotted.ForEach(s => s.Fire());
        }

    }

    [InputSocket]
    public void stop(){
        _Speed=0f;
        currentState = State.Stopped;
    }

    [InputSocket]
    public void go(){
        _Speed=3f;
        currentState = State.FollowingWaypoints;
    }




}
