using UnityEngine;
using System.Collections;

public class Door : StateMachineBase {

    public Collider2D doorCollider;

    public Transform OpenPosition;
    public Vector3 _openPosition;
    public float OpenTime;

    private Vector2 InitialPosition;
    private float openTimer;

    public enum State
    {
        Open,
        Closed,
        Opening,
        Closing
    }

    // Use this for initialization
    void Start ()
    {
        InitialPosition = transform.position;
        _openPosition = OpenPosition.position;
        currentState = State.Closed;
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
    }

    void Closing_Update()
    {
        openTimer += Time.deltaTime;
        transform.position = Vector2.Lerp(transform.position, InitialPosition, openTimer / OpenTime);

        if (openTimer > OpenTime)
        {
            currentState = State.Closed;
        }
    }

    void Opening_Update()
    {
        openTimer += Time.deltaTime;
        transform.position = Vector2.Lerp(transform.position, _openPosition, openTimer / OpenTime);

        if (openTimer > OpenTime)
        {
            currentState = State.Open;
        }
    }

    [InputSocket]
    public void Open()
    {
        doorCollider.enabled = false;
        if ((State)currentState != State.Open)
        {
            currentState = State.Opening;
            openTimer = 0;
        }
    }

    [InputSocket]
    public void Close()
    {
        doorCollider.enabled = true;
        if ((State)currentState != State.Closed)
        {
            currentState = State.Closing;
            openTimer = 0;
        }
    }
}
