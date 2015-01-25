using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BodyController : MonoBehaviour {

    public float tearThreshold = 0.4f;

    public List<Rigidbody2D> Parts = new List<Rigidbody2D>();

    public List<HingeJoint2D> Joints;

    void Start()
    {
        Joints = GetComponentsInChildren<HingeJoint2D>().ToList();
        Parts.ForEach((p) => p.transform.parent = null);
    }

    void Update()
    {
        // Get average position of all parts
        Vector3 position = Vector3.zero;
        foreach (var part in Parts)
        {
            position += part.transform.position;
        }

        transform.position = position / Parts.Count;

        // Check if any joints should tear!
        foreach (var joint in Joints)
        {
            var pointA = joint.transform.position + joint.transform.TransformPoint(joint.anchor);
            var pointB = joint.connectedBody.position.XY0() + joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);

            if ((pointA - pointB).magnitude > tearThreshold)
            {

                var newBody = GameObject.Instantiate(Resources.Load<GameObject>("EmptyBodyPrefab"),
                                                     joint.transform.position,
                                                     Quaternion.identity) as GameObject;

                newBody.GetComponent<BodyController>().Parts.Add(joint.rigidbody2D);

                Joints.Remove(joint);
                Parts.Remove(joint.rigidbody2D);

                Destroy(joint);
                break; // can't enumerate anymore ..
            }
        }
    }

}
