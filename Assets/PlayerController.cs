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

    public CircleCollider2D circleCollider;
    public Animator PlayerAnimator;

    public BodyController DeadBody;
    public BoundingBox DeadBodyBoundingBox;
    private DistanceJoint2D bodyJoint;

    private float IntendedRotation;

    void Start () {

        currentState = initialState;
        Velocity = Vector2.zero;

        DialogBox.OnDialogShow += () => {
            currentState = State.InDialog;
        };

        DialogBox.OnDialogHide += () => {
            if (bodyJoint == null)
            {
                currentState = State.Walking;
            }
            else
            {

                currentState = State.Dragging;
            }
        };
    }

    void UpdateCamera() {
        var position = Camera.main.transform.position;
        position.x = transform.position.x;
        position.y = transform.position.y;
        Camera.main.transform.position = position;
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

        var newPosition = transform.position.XY() + (Velocity * Time.deltaTime);

        if (!CheckCollision(newPosition)) {
            transform.position = newPosition;
            PlayerAnimator.SetFloat("MoveSpeed", MoveSpeed);
        }
        else
        {
            // Issue - can't 'slide' against walls
            PlayerAnimator.SetFloat("MoveSpeed", 0);
        }
    }


    bool CheckCollision(Vector2 position) {
        return Physics2D.CircleCast(position, circleCollider.radius, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Level"));
    }

    void GrabBody() {

        var hit = Physics2D.Linecast(transform.position, DeadBody.transform.position, LayerMask.GetMask("Body"));
        var distanceToBody = (transform.position.XY() - hit.point).magnitude;

        if (distanceToBody < DragReachDistance)
        {
            currentState = State.Dragging;
            bodyJoint = gameObject.AddComponent<DistanceJoint2D>() as DistanceJoint2D;
            bodyJoint.connectedBody = hit.collider.rigidbody2D;
            bodyJoint.distance = DragDistance;
            bodyJoint.connectedAnchor = hit.point - hit.collider.transform.position.XY();
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

        // Show or hide bounding box ...
        var hit = Physics2D.Linecast(transform.position, DeadBody.transform.position, LayerMask.GetMask("Body"));
        var distanceToBody = (transform.position.XY() - hit.point).magnitude;

        if (distanceToBody < DragReachDistance)
        {
            DeadBodyBoundingBox.Show();
        }
        else
        {
            DeadBodyBoundingBox.Hide();
        }
    }

    IEnumerator Dialog_EnterState() {
        PlayerAnimator.SetFloat("MoveSpeed", 0);
        yield return 0;
    }

    IEnumerator Dragging_EnterState() {
        print("PICKED UP BODY");
        yield return 0;
    }

    void Dragging_Update() {

        UpdateMovement();

        DeadBodyBoundingBox.Hide();

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
        UpdateCamera();
    }
}
