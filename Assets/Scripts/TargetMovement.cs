using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [Tooltip("Velocidade inicial em que o alvo se move")]
    public float speed = 3.0f;

    [Tooltip("Distância máxima que ele anda para a ESQUERDA antes de voltar")]
    public float maxLeftDistance = 4.0f;

    [Tooltip("Distância máxima que ele anda para a DIREITA antes de voltar")]
    public float maxRightDistance = 4.0f;

    [Header("Configurações de Dificuldade")]
    [Tooltip("Tempo em segundos para aumentar a velocidade")]
    public float speedIncreaseInterval = 6.0f;

    [Tooltip("O quanto a velocidade aumenta a cada intervalo")]
    public float speedIncreaseAmount = 1.0f;

    [Tooltip("Velocidade máxima que o alvo pode alcançar (evita bugs visuais)")]
    public float maxSpeed = 10.0f;

    private float currentMoved = 0f;

    private float direction = 1f;

    void Start()
    {
        StartCoroutine(IncreaseSpeedRoutine());
    }

    void Update()
    {
        float moveStep = speed * Time.deltaTime;
        
        transform.Translate(Vector3.left * direction * moveStep);
        
        currentMoved += moveStep * direction;

        if (currentMoved >= maxLeftDistance)
        {
            direction = -1f; // Inverte a direção (começa a ir para a direita)
        }

        else if (currentMoved <= -maxRightDistance)
        {
            direction = 1f; // Inverte a direção (começa a ir para a esquerda)
        }
    }

    IEnumerator IncreaseSpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);

            if (speed < maxSpeed)
            {
                speed += speedIncreaseAmount;
                
                if (speed > maxSpeed) 
                {
                    speed = maxSpeed;
                }
            }
        }
    }
}