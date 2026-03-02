/*using UnityEngine;
using System.Collections;
public class PassengerSpawner : MonoBehaviour
{
    public Transform spawnPoint;                 // Start point
    public Transform turnPoint;                  // L-shape ka turn
    public PassengerQueueManager queueManager;   // Queue manager
    public float spawnInterval = 1.0f; // Har kitne sec me next nikle
    private int currentCount = 0;
    private bool isSpawning = false;
    public GameObject[] passengerTypePrefabs;    // Different human prefabs
    public int maxVisible = 20;                  // Hamesha itne dikhne chahiye

    void Start()
    {
        // Start me 20 passengers spawn karo
       // for (int i = 0; i < maxVisible; i++)
       // {
        //    SpawnPassenger();
            StartCoroutine(SpawnLoop());

       // }
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Agar line me kam hain to naya spawn karo
            if (currentCount < maxVisible && !isSpawning)
            {
                isSpawning = true;
                SpawnPassenger();
                yield return new WaitForSeconds(spawnInterval);
                isSpawning = false;
            }

            yield return null;
        }
    }



    public void SpawnPassenger()
    {
        if (passengerTypePrefabs.Length == 0) return;

        int index = Random.Range(0, passengerTypePrefabs.Length);

        GameObject p = Instantiate(
            passengerTypePrefabs[index],
            spawnPoint.position,
            Quaternion.identity
        );

        Passenger passenger = p.GetComponent<Passenger>();
        if (passenger == null)
            passenger = p.AddComponent<Passenger>();

        passenger.typeIndex = index;

        // 🔥 L-shape path set: pehle TurnPoint, baad me QueueManager slot dega
        // Abhi temporary final point same turnPoint de dete hain, QueueManager overwrite karega
        passenger.SetPath(turnPoint, turnPoint);

        // Queue me add karo (ye slot assign karega)
        queueManager.AddPassenger(passenger);
    }

    // Jab koi passenger car me baith jaaye, call karo
    public void OnPassengerBoarded()
    {
        SpawnPassenger(); // Ek naya spawn karo taaki total count maintain rahe
    }
}
*/
using UnityEngine;
using System.Collections;

public class PassengerSpawner : MonoBehaviour
{
    public Transform spawnPoint;                 // Start point
    public Transform turnPoint;                  // L-shape ka turn
    public PassengerQueueManager queueManager;   // Queue manager
    public float spawnInterval = 1.0f;           // Har kitne sec me next nikle
    private int currentCount = 0;
    private bool isSpawning = false;
    public GameObject[] passengerTypePrefabs;    // Different human prefabs
    public int maxVisible = 20;                  // Hamesha itne dikhne chahiye

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Agar line me kam hain to naya spawn karo
            if (currentCount < maxVisible && !isSpawning)
            {
                isSpawning = true;
                SpawnPassenger();
                yield return new WaitForSeconds(spawnInterval);
                isSpawning = false;
            }

            yield return null;
        }
    }

    public void SpawnPassenger()
    {
        if (passengerTypePrefabs.Length == 0) return;

        int index = Random.Range(0, passengerTypePrefabs.Length);

        GameObject p = Instantiate(
            passengerTypePrefabs[index],
            spawnPoint.position,
            Quaternion.identity
        );

        Passenger passenger = p.GetComponent<Passenger>();
        if (passenger == null)
            passenger = p.AddComponent<Passenger>();

        passenger.typeIndex = index;

        // 🔥 Pehle TurnPoint set, final slot QueueManager set karega
        passenger.SetPath(turnPoint, turnPoint);

        // Queue me add karo (ye slot assign karega)
        queueManager.AddPassenger(passenger);

        currentCount++; // ✅ yahan count badhao
    }

    // Jab koi passenger car me baith jaaye, call karo
    public void OnPassengerBoarded()
    {
        currentCount--; // ✅ pehle count ghatao
        // SpawnLoop automatically next spawn kar dega jab count < maxVisible hoga
    }
}