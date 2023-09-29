using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Zone zone = other.GetComponent<Zone>();

        if (zone != null)
        {
            zone.EnterZone(this);
        }
    }

}
