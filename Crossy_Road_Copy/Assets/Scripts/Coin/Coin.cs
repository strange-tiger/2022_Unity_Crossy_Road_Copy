using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree")
        {
            Destroy(gameObject);
            //Debug.Log("Coin Destroied");
        }
    }
}
