using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController1 : MonoBehaviour
{
    public GameSettings Gamesettings;
    
    public float speed = 20f;
    private float xRange = 20f;
    public GameObject projectilePrefab;
    private float horizontalInput;
    public int vida = 3;
    public TMP_Text textoVida;
    public GameObject painelGameOver;
    public GameObject buttonSair;
    public int pontos = 0;
    public TMP_Text textoPontos;
    public bool CtrlVerdadeiro = false;
    private int ultimateCargas = 0;
    private const int ultimateMax = 10;
    public TMP_Text textoUltimate;

    public InputActionAsset InputActions;
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction pausaActionPlayer;
    private InputAction playerFantasma;
    private InputAction pausaActionUI;
    private InputAction especialAction;
    private float tempo = 1f;
    public GameObject painel;
    public GameObject botoesHUD;

    // Update is called once per frame  
    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable(); 
    }
    
    private void Awake()
    {

        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Jump");
        pausaActionPlayer = InputSystem.actions.FindAction("Pausa");
        playerFantasma = InputSystem.actions.FindAction("Ghost");
        pausaActionUI = InputSystem.actions.FindAction("Despausa");
        especialAction = InputSystem.actions.FindAction("Especial");
    }
    void Start()
    {
        AtualizarHUDVida();
        AtualizarHUDPontos();
        AtualizarHUDUltimate();
        painelGameOver.SetActive(false);
    }
    void Update()
    {
        float horizontalInput = moveAction.ReadValue<Vector2>().x;
        // movimenta o player para esquerda e direita a partir da entrada do usu�rio
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);
        // mant�m o player dentro dos limites do jogo (eixo x)
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.y);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.y);
        }
        if(fireAction.WasPressedThisFrame())
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);

        }

    
        if (especialAction.WasPressedThisFrame() && ultimateCargas >= ultimateMax)
        {
            
            StartCoroutine("SpecialCorroutine");
        }

        if (playerFantasma.WasPressedThisFrame())
        {
            
            CtrlVerdadeiro = true;
            Gamesettings.Parametro(CtrlVerdadeiro);
        }
        PauseGame();
        
       
}
 
 void Special()
    {
            Quaternion baseRot = projectilePrefab.transform.rotation;
            Instantiate(projectilePrefab, transform.position, baseRot);
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 45, 0) * baseRot);
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, -45, 0) * baseRot);
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, -23, 0) * baseRot);
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 23, 0) * baseRot);
            AtualizarHUDUltimate();
    }
    IEnumerator SpecialCorroutine()
    {
        
    Special();
     yield return new WaitForSeconds (tempo);
    Special();
     yield return new WaitForSeconds (tempo);
    Special();
     yield return new WaitForSeconds (tempo);
    Special();


       ultimateCargas = 0;
    }

  
void PauseGame()
    {
         if (pausaActionPlayer.WasPressedThisFrame())
        {
            painel.SetActive(true);
            botoesHUD.SetActive(false);
            InputActions.FindActionMap("Player").Disable();
            InputActions.FindActionMap("UI").Enable();
            Time.timeScale = 0f;
        }
        if (pausaActionUI.WasPressedThisFrame())
        {
            painel.SetActive(false);
            botoesHUD.SetActive(true);
            InputActions.FindActionMap("Player").Enable();
            InputActions.FindActionMap("UI").Disable();
            Time.timeScale = 1f;
        }
    }
public void PerderVida()
    {
        vida--;

        if (vida < 0)
        {
            vida = 0;
        }

        AtualizarHUDVida();

        if (vida == 0)
        {
            GameOver();
        }
    }
public void AdicionarPontos(int quantidade)
    {
        pontos += quantidade;
        AtualizarHUDPontos();
        if (ultimateCargas < ultimateMax)
        {
            ultimateCargas += quantidade;
            if(ultimateCargas <= 0)
            {
                ultimateCargas = 0;
            }
            if (ultimateCargas > ultimateMax) ultimateCargas = ultimateMax;
            AtualizarHUDUltimate();
        }
    }

void AtualizarHUDVida()
    {
        textoVida.text = "Vidas: " + vida;
    }

void AtualizarHUDPontos()
    {
        textoPontos.text = "Pontos: " + pontos;
        if(pontos <= -5)
        {
            GameOver();
        }
    }

void AtualizarHUDUltimate()
    {
        textoUltimate.text = "Ultimate " + ultimateCargas + "/" + ultimateMax;
    }
void GameOver()
    {
         painelGameOver.SetActive(true);
         buttonSair.SetActive(true);
         Time.timeScale = 0f;
         botoesHUD.SetActive(false);
         
    }

    }