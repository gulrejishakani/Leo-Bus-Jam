using UnityEngine;

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