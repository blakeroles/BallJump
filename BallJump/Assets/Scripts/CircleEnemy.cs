using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : MonoBehaviour
{
    public float speedFraction;
    public float radius;
    private float angle;

    private float x;
    private float y;
    private float speed;

    private float initialXPosition;
    private float initialYPosition;

    // Start is called before the first frame update
    void Start()
    {
        angle = 0;
        speed = (2*Mathf.PI)/speedFraction;

        initialXPosition = transform.position.x;
        initialYPosition = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        angle += speed*Time.deltaTime;
        x = Mathf.Cos(angle)*radius;
        y = Mathf.Sin(angle)*radius;

        transform.position = new Vector3(x + initialXPosition, y + initialYPosition, transform.position.z);
    }
}
