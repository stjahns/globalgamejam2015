using UnityEngine;
using System.Collections;

public class Door : TriggerBase {

    public float OpenAngleDelta = 90;

    public Collider2D doorCollider;

    private float initialAngle;
    private float openAngle;

    // Use this for initialization
    void Start ()
    {
        initialAngle = transform.rotation.eulerAngles.z;
        openAngle = initialAngle + OpenAngleDelta;
    }

    // Update is called once per frame
    void Update ()
    {
    }

    [InputSocket]
    public void Open()
    {
        transform.rotation = Quaternion.Euler(0, 0, openAngle);
        doorCollider.enabled = false;
    }

    [InputSocket]
    public void Close()
    {
        transform.rotation = Quaternion.Euler(0, 0, initialAngle);
        doorCollider.enabled = true;
    }
}
