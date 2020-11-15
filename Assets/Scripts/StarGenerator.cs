using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject starPrefab;
    public int numberOfStars;
    public float meanDistance;
    private float distanceInterval;

    private void Start() {
        distanceInterval = meanDistance / 50;
    }

    public void GenerateStars()
    {
        float r, theta, phi;
        Vector3 starPosition;
        for (int i = 0; i < numberOfStars; ++i)
        {
            r = Random.Range(meanDistance - distanceInterval, meanDistance + distanceInterval);
            theta = Random.Range(0, Mathf.PI);
            phi = Random.Range(-Mathf.PI, Mathf.PI);

            starPosition = new Vector3(
                r * Mathf.Sin(theta) * Mathf.Cos(phi),
                r * Mathf.Sin(theta) * Mathf.Sin(phi),
                r * Mathf.Cos(theta));

            Instantiate(starPrefab, starPosition, Quaternion.identity, transform);
        }


    }
}
