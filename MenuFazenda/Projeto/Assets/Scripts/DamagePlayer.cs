using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController1 player = other.gameObject.GetComponent<PlayerController1>();
            player.PerderVida();
            Destroy(gameObject);
        }
    }
}