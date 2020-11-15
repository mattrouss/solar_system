using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSimulation : MonoBehaviour
{
    public CelestialBody[] bodies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
