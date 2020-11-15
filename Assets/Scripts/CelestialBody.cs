using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public float mass;
    public float radius;
    public Vector3 initialVelocity;
    private Vector3 currentVelocity;


    public Material material;
    private Rigidbody rigidBody;
    private LineRenderer lineRenderer;

    private bool has_collided = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        material = GetComponent<MeshRenderer>().material;
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startColor = material.color;
        lineRenderer.endColor =Color.white;

        currentVelocity = initialVelocity;
    }

    void OnTriggerEnter(Collider other)
    {
        currentVelocity = Vector3.zero;
        has_collided = true;
    }

    public void UpdateVelocity(CelestialBody[] allBodies, float timeDelta)
    {
        Vector3 force = Vector2.zero;
        foreach (CelestialBody body in allBodies)
        {
            if (this == body)
                continue;

            Vector3 direction = body.rigidBody.position - rigidBody.position;
            Vector3 forceDir = direction.normalized;
            float sqrDist = direction.sqrMagnitude;

            force += (Universe.gravitationalConstant * mass * body.mass / sqrDist) * forceDir;
        }
        if (!has_collided)
            currentVelocity += timeDelta / mass * force;
    }

    public void UpdatePosition(float timeDelta)
    {
       rigidBody.position += timeDelta * currentVelocity;
    }

    public LineRenderer GetLineRenderer()
    {
        return lineRenderer;
    }

    public Vector3 GetVelocity()
    {
        return currentVelocity;
    }

    public Vector3 GetPosition()
    {
        return rigidBody.position;
    }
}
