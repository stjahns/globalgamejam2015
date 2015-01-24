using UnityEngine;
using System.Collections;

public class PlayerController : StateMachineBase {

    public enum State
    {
        Walking,
        Dragging,
        InDialog
    }

    public State initialState;

    public float MoveSpeed = 1.0f;
    public float DragDistance = 1.0f;
    public float DragReachDistance = 1.0f;

    public Rigidbody2D DeadBody;
    private DistanceJoint2D bodyJoint;

    void Start () {
        currentState = initialState;
    }

    //
    // Handle character movement from input
    //
    void UpdateMovement() {

        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.DownArrow)) {
            movement -= Vector2.up;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            movement += Vector2.up;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            movement -= Vector2.right;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            movement += Vector2.right;
        }

        movement.Normalize();

        transform.position = transform.position + (movement * MoveSpeed * Time.deltaTime).XY0();
    }

    void Walking_Update() {
        UpdateMovement();
        if (Input.GetKeyDown(KeyCode.Space)) {

            var distanceToBody = (transform.position - DeadBody.transform.position).magnitude;

            if (distanceToBody < DragReachDistance)
            {
                currentState = State.Dragging;
                bodyJoint = gameObject.AddComponent<DistanceJoint2D>() as DistanceJoint2D;
                bodyJoint.connectedBody = DeadBody;
                bodyJoint.distance = DragDistance;
                bodyJoint.maxDistanceOnly = true;
            }
        }
    }

    IEnumerator Dragging_EnterState() {
        print("PICKED UP BODY");
        yield return 0;
    }

    void Dragging_Update() {

        UpdateMovement();

        if (Input.GetKeyDown(KeyCode.Space)) {

            // Stop dragging body
            Destroy(bodyJoint);
            bodyJoint = null;
            currentState = State.Walking;
        }
    }

    IEnumerator Dragging_ExitState() {
        print("DROPPED BODY");
        yield return 0;
    }

    override protected void Update () {
        base.Update();
    }
}
