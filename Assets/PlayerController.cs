using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float MoveSpeed = 1.0f;

    void Start () {

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

    void Update () {
        UpdateMovement();
    }
}
