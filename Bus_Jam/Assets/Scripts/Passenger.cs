/*using UnityEngine;

public class Passenger : MonoBehaviour
{
    public int typeIndex;          // kaunsa type / human
    public float moveSpeed = 2f;   // chalne ki speed

    private Transform turnPoint;       // pehla point (aage)
    private Transform boardingPoint;   // final point (left turn ke baad)
    private bool reachedTurn = false;
    private bool reachedFinal = false; // 🔥 new flag (name change nahi, sirf add)

    // Spawner se path set hoga
    public void SetPath(Transform turn, Transform boarding)
    {
        turnPoint = turn;
        boardingPoint = boarding;
        reachedTurn = false;
        reachedFinal = false;
    }

    public void SetFinalPoint(Transform finalPoint)
    {
        boardingPoint = finalPoint;
    }

    void Update()
    {
        if (turnPoint == null || boardingPoint == null) return;
        if (reachedFinal) return; // 🔥 final pe pahunch gaya to ruk jao

        // Pehle turnPoint, phir boardingPoint
        Transform target = reachedTurn ? boardingPoint : turnPoint;

        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0f;

        float dist = toTarget.magnitude;

        if (dist > 0.05f)
        {
            Vector3 dir = toTarget.normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;

            // Face towards movement direction
            if (dir != Vector3.zero)
                transform.forward = dir;
        }
        else
        {
            // Target reached
            if (!reachedTurn)
            {
                reachedTurn = true; // ab sirf final point ki taraf jayega
            }
            else
            {
                reachedFinal = true; // 🔥 final point pe aa kar ruk jao
            }
        }
    }
}
*/


/*
using UnityEngine;

public class Passenger : MonoBehaviour
{
    public int typeIndex;
    public float moveSpeed = 2f;

    private Transform boardingPoint;   
    private bool reachedFinal = false;

    public void SetFinalPoint(Transform finalPoint)
    {
        boardingPoint = finalPoint;
        reachedFinal = false;
    }

    void Update()
    {
        if (boardingPoint == null) return;
        if (reachedFinal) return;

        Vector3 toTarget = boardingPoint.position - transform.position;
        toTarget.y = 0f;

        float dist = toTarget.magnitude;

        if (dist > 0.05f)
        {
            Vector3 dir = toTarget.normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;

            if (dir != Vector3.zero)
                transform.forward = dir;
        }
        else
        {
            reachedFinal = true;
        }
    }
}*/





using UnityEngine;

public class Passenger : MonoBehaviour
{
    public int typeIndex;
    public float moveSpeed = 2f;

    private Transform boardingPoint;
    private bool reachedZ = false;
    private bool reachedFinal = false;

    public void SetFinalPoint(Transform finalPoint)
    {
        boardingPoint = finalPoint;
        reachedZ = false;
        reachedFinal = false;
    }

    void Update()
    {
        if (boardingPoint == null || reachedFinal) return;

        Vector3 current = transform.position;
        Vector3 target = boardingPoint.position;

        // 🔹 Step 1: First align Z (vertical movement)
        if (!reachedZ)
        {
            float zDiff = target.z - current.z;

            if (Mathf.Abs(zDiff) > 0.05f)
            {
                float moveZ = Mathf.Sign(zDiff) * moveSpeed * Time.deltaTime;
                transform.position += new Vector3(0, 0, moveZ);
                transform.forward = new Vector3(0, 0, Mathf.Sign(zDiff));
            }
            else
            {
                reachedZ = true;
            }

            return;
        }

        // 🔹 Step 2: Then move in X (horizontal)
        float xDiff = target.x - transform.position.x;

        if (Mathf.Abs(xDiff) > 0.05f)
        {
            float moveX = Mathf.Sign(xDiff) * moveSpeed * Time.deltaTime;
            transform.position += new Vector3(moveX, 0, 0);
            transform.forward = new Vector3(Mathf.Sign(xDiff), 0, 0);
        }
        else
        {
            reachedFinal = true;
        }
    }
}