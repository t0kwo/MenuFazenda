using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController1 : MonoBehaviour
{
    public GameSettings Gamesettings;
    
    public float speed = 20f;
    public float xRange = 15f;
    public GameObject projectilePrefab;
    private float horizontalInput;
    public int vida = 3;
    public TMP_Text textoVida;
    public GameObject painelGameOver;
    public GameObject buttonSair;
    public int pontos = 0;
    public TMP_Text textoPontos;
    public bool CtrlVerdadeiro = false;
    private float tripleShootCooldown = 0f;

    public InputActionAsset InputActions;
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction pausaActionPlayer;
    private InputAction playerFantasma;
    private InputAction pausaActionUI;
    public GameObject painel;

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
    }
    void Start()
    {
        AtualizarHUDVida();
        AtualizarHUDPontos();
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

        // Cooldown do triple-shot
        if (tripleShootCooldown > 0f)
            tripleShootCooldown -= Time.deltaTime;

        // Triple-shot com Shift (frente + duas diagonais)
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && tripleShootCooldown <= 0f)
        {
            Quaternion baseRot = projectilePrefab.transform.rotation;
            Instantiate(projectilePrefab, transform.position, baseRot);
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 45, 0) * baseRot);
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, -45, 0) * baseRot);
            tripleShootCooldown = 10f;
        }

        if (playerFantasma.WasPressedThisFrame())
        {
            
            CtrlVerdadeiro = true;
            Gamesettings.Parametro(CtrlVerdadeiro);
        }
        PauseGame();
        
       
}
 
  
void PauseGame()
    {
         if (pausaActionPlayer.WasPressedThisFrame())
        {
            painel.SetActive(true);
            InputActions.FindActionMap("Player").Disable(); 
            InputActions.FindActionMap("UI").Enable();
            Time.timeScale = 0f;
        }
        if (pausaActionUI.WasPressedThisFrame())
        {
            painel.SetActive(false);
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
    }

void AtualizarHUDVida()
    {
        textoVida.text = "Vidas: " + vida;
    }

void AtualizarHUDPontos()
    {
        textoPontos.text = "Pontos: " + pontos;
    }
void GameOver()
    {
         painelGameOver.SetActive(true);
         buttonSair.SetActive(true);
         Time.timeScale = 0f;
         
    }

    }