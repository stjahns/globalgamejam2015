using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BodyController : MonoBehaviour {

    public List<Rigidbody2D> Parts;


    void Start()
    {
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
    }

}
