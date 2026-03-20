using System.Collections.Generic;
using UnityEngine;

public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> otherObjects;
    private Rigidbody rb;
    const float G = 0.006673f;

    [SerializeField] bool planet = false;
    [SerializeField] float orbitSpeed = 1000f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjects == null)
        {
            otherObjects = new List<Gravitation>();
        }

        otherObjects.Add(this);

        // orbiting
        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }

    void FixedUpdate()
    {
        foreach (Gravitation obj in otherObjects)
        {
            if (obj != this)
            {
                AttractionForce(obj);
            }
        }
    }

    void AttractionForce(Gravitation other)
    {
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;

        if (distance == 0f) return;

        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distance, 2));
        Vector3 gravitationalForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravitationalForce);
    }
}