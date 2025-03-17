using System.Collections;
using UnityEngine;

public class MovingSpikeBall : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float moveSpeed = 1f;
    public float waitingTime = 1f;
    public float waitingDuration;
    private Vector3 nextPosition;

    void Start()
    {
        waitingDuration = 0f;
        nextPosition = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        CalculateNextPosition();
    }

    void CalculateNextPosition()
    {
        if (transform.position == nextPosition && IsReadyToMove())
        {
            if (nextPosition == pointA.position)
            {
                nextPosition = pointB.position;
            }
            else
            {
                nextPosition = pointA.position;
            }
        }
    }

    bool IsReadyToMove()
    {
        if (waitingDuration < waitingTime)
        {
            waitingDuration += Time.deltaTime;
            return false;
        }
        else
        {
            waitingDuration = 0f;
            return true;

        }
    }
}
