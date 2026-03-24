using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;



public class GameSettings : MonoBehaviour
{
private bool Ghostverdadeiro = false;

public GameObject player;

    void Start()
    {
 
    }


    void Update()
    {
        if (Ghostverdadeiro)
        {
            player.gameObject.SetActive(false);
             StartCoroutine("Ghost");
        }
        }
    IEnumerator Ghost()
    {
        
        yield return new WaitForSeconds (2);
        Debug.Log("Objeto volta");
       player.gameObject.SetActive(true);
       Ghostverdadeiro =false;
    }

       public void Parametro(bool Valor)
    {
        Ghostverdadeiro = Valor;
    }

}
