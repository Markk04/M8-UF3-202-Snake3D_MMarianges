using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bodySpeed;
    [SerializeField] private float steerSpeed;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject bodyParent;
    [SerializeField] private GameManager _gm;

    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> positionHistory = new List<Vector3>();
    private bool _gameOver;
    private PlayerController _playerController;
    private Gyroscope _gyroscope;
    void Start()
    {
        // Verificar si el dispositivo admite el giroscopio
        if (SystemInfo.supportsGyroscope)
        {
            // Obtener el giroscopio actual
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true; // Habilitar el giroscopio

            Debug.Log("Giroscopio habilitado.");
        }
        else
        {
            Debug.LogError("El dispositivo no admite el giroscopio.");
        }

        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();

        _gameOver = false;
        _playerController = GetComponent<PlayerController>();

        InvokeRepeating("UpdatePositionHistory", 0f, 0.01f);

    }

    void Update()
    {
        if (!_gameOver)
        {

            // Acceder a los datos del giroscopio
            if (_gyroscope != null)
            {
                Debug.Log("GYROSCOPE ACTIU");
                Vector3 rotationRate = _gyroscope.rotationRate; // Velocidad de rotación
                Quaternion attitude = _gyroscope.attitude; // Orientación del dispositivo

                // Aplicar la rotación al objeto (serpiente o jugador)
                transform.Rotate(Vector3.up * rotationRate.y * steerSpeed * Time.deltaTime);


                // Usa los datos según tus necesidades (por ejemplo, para rotar un objeto)
                // Ejemplo: transform.Rotate(rotationRate * Time.deltaTime);
            }
            //move forward
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            //steer
            float steerDirection = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

            int index = 0;
            foreach (GameObject body in bodyParts)
            {
                Vector3 point = positionHistory[Math.Min(index * 10, positionHistory.Count - 1)];
                Vector3 moveDirection = point - body.transform.position;
                body.transform.position += moveDirection * bodySpeed * Time.deltaTime;

                body.transform.LookAt(point);

                index++;
            }
        }

    }

    void UpdatePositionHistory()
    {
        Debug.Log("UpdatePositionHistory");
        // Añadir la posición actual al inicio de la lista
        positionHistory.Insert(0, transform.position);

        // Si la lista excede el número máximo de posiciones, elimina la última
        if (positionHistory.Count > 500)
        {
            positionHistory.RemoveAt(positionHistory.Count - 1);
        }
    }

    private void GrowSnake()
    {
        //  Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject body = Instantiate(bodyPrefab);
        bodyParts.Add(body);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            Debug.Log("Food detected");
            GrowSnake();
            _gm.addScore();
        }

        if (other.CompareTag("Boundary") || other.CompareTag("Body"))
        {
            _gameOver = true;
            Destroy(_playerController);
            _gm.setUpGameOver();


        }
    }
}
