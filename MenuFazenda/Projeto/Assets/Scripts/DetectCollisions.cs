using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private PlayerController1 player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animals"))
        {
            player.AdicionarPontos(1);

            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}