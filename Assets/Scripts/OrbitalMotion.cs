using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMotion : MonoBehaviour
{
    public float speed;
    public enum rotation {World, Self};
    public rotation RotationType;
    public bool Counterclockwise;

    // Start is called before the first frame update
    void Start()
    {
        speed /= 500;
        if (Counterclockwise) {
            speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RotationType == rotation.World) {
            transform.Rotate(0, speed, 0, Space.World);
        }
        else {
            transform.Rotate(0, speed, 0, Space.Self);
        }
    }
}
