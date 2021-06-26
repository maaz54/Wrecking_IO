using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere : MonoBehaviour
{
    public Vector3 sphereForce;
    Vector3 currPos;
    Vector3 previousPos;
    private void Update() {
        currPos = transform.position;
        sphereForce = (currPos-previousPos)/Time.deltaTime;
        previousPos = currPos;
    }
}
