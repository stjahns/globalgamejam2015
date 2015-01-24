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

    public float MoveSpeed = 0.0f;
    public float MaxMoveSpeed = 1.0f;
    public float Acceleration = 1.0f;
    public float TurnSpeed = 0.3f;
    public Vector2 Velocity;


    public float DragDistance = 1.0f;
    public float DragReachDistance = 1.0f;

    public Animator PlayerAnimator;

    public Rigidbody2D DeadBody;
    private DistanceJoint2D bodyJoint;

    private float IntendedRotation;

    void Start () {
        currentState = initialState;
        Velocity = Vector2.zero;

        DialogBox.OnDialogShow += () => {
            currentState = State.InDialog;
        };

        DialogBox.OnDialogHide += () => {
            currentState = State.Walking;
        };
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

        if (movement.magnitude > 0f) {
            MoveSpeed += Acceleration * Time.deltaTime;
        } else {
            MoveSpeed -= Acceleration * Time.deltaTime;
        }

        MoveSpeed = Mathf.Clamp(MoveSpeed, 0, MaxMoveSpeed);

        var moveDirection = Velocity.normalized;

        if (movement.magnitude > 0) {
            moveDirection = movement;
            IntendedRotation = Quaternion.FromToRotation(Vector2.up, moveDirection).eulerAngles.z;
        } else {
            moveDirection = Velocity.normalized;
        }

        transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, IntendedRotation, TurnSpeed));

        Velocity = moveDirection * MoveSpeed;

        transform.position = transform.position.XY() + (Velocity * Time.deltaTime);

        PlayerAnimator.SetFloat("MoveSpeed", MoveSpeed);
    }


    void GrabBody() {

        var hit = Physics2D.Linecast(transform.position, DeadBody.transform.position, LayerMask.GetMask("Body"));
        var distanceToBody = (transform.position.XY() - hit.point).magnitude;

        if (distanceToBody < DragReachDistance)
        {
            currentState = State.Dragging;
            bodyJoint = gameObject.AddComponent<DistanceJoint2D>() as DistanceJoint2D;
            bodyJoint.connectedBody = DeadBody;
            bodyJoint.distance = DragDistance;
            bodyJoint.connectedAnchor = hit.point - DeadBody.transform.position.XY();
            bodyJoint.maxDistanceOnly = true;
        }
    }

    void DropBody() {
        // Stop dragging body
        Destroy(bodyJoint);
        bodyJoint = null;
        currentState = State.Walking;
    }


    void Walking_Update() {
        UpdateMovement();
        if (Input.GetKeyDown(KeyCode.Space)) {
            GrabBody();
        }
    }

    IEnumerator Dragging_EnterState() {
        print("PICKED UP BODY");
        yield return 0;
    }

    void Dragging_Update() {

        UpdateMovement();

        if (Input.GetKeyDown(KeyCode.Space)) {
            DropBody();
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
