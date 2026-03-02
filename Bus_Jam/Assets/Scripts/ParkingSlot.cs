using UnityEngine;

public class ParkingSlot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Car car = other.GetComponent<Car>();

        if (car != null)
        {
            car.TryBoardPassenger();
        }
    }
}