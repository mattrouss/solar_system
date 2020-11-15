using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSimulation : MonoBehaviour
{
    public CelestialBody[] bodies;
    public bool showTrajectories = false;
    public float trajectoryTime = 10f;
    public int trajectoryResolution = 500;

    private bool simulationStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            simulationStarted = true;


        if (simulationStarted)
            Launch();

        if (showTrajectories)
            DisplayBodyTrajectories();

    }

    void Launch()
    {
        foreach (CelestialBody body in bodies) 
        {
            body.UpdateVelocity(bodies, Time.deltaTime);
        }

        foreach (CelestialBody body in bodies)
        {
           body.UpdatePosition(Time.deltaTime);
        }

    }

    void DisplayBodyTrajectories()
    {
        float timeDelta = trajectoryTime / trajectoryResolution;
        Vector3[] positions = new Vector3[bodies.Length];
        Vector3[] previousPositions = new Vector3[bodies.Length];
        Vector3[] velocities = new Vector3[bodies.Length];

        LineRenderer[] trajectories = new LineRenderer[bodies.Length];

        for (int i = 0; i < bodies.Length; ++i)
        {
            positions[i] = bodies[i].GetPosition();
            previousPositions[i] = bodies[i].GetPosition();
            velocities[i] = bodies[i].GetVelocity();

            trajectories[i] = bodies[i].GetLineRenderer();
            trajectories[i].positionCount = trajectoryResolution;
        }

        for (int r = 0; r < trajectoryResolution; ++r)
        {
            for (int i = 0; i < bodies.Length; ++i)
            {
                Vector3 force = Vector2.zero;
                for (int j = 0; j < bodies.Length; ++j)
                {
                    if (i == j)
                        continue;

                    Vector3 direction = previousPositions[j] - previousPositions[i];
                    Vector3 forceDir = direction.normalized;
                    float sqrDist = direction.sqrMagnitude;

                    force += (Universe.gravitationalConstant * bodies[i].mass * bodies[j].mass / sqrDist) * forceDir;
                    velocities[i] += timeDelta / bodies[i].mass * force;
                    positions[i] += timeDelta * velocities[i];
                }

                trajectories[i].SetPosition(r, positions[i]);
                previousPositions[i] = positions[i];

            }
        }
        
    }
}
