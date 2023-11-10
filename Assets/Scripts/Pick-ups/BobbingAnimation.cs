using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{
    public float frequency;
    public float magnitude;
    public Vector3 direction;
    Pickup pickup;

    Vector3 initialPosition;

    void Start()
    {

        pickup = GetComponent<Pickup>();
        initialPosition = transform.position;

    }

    void Update()
    {
        if(pickup && !pickup.hasBeenCollected)
        {
            transform.position = initialPosition + direction * Mathf.Sin(Time.time * frequency) * magnitude;
        }        
    }
}
