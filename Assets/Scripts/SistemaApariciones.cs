using UnityEngine;

public class SistemaApariciones : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float probabilidadAparicion = 0.7f;
    public GameObject niña;
    public float distanciaDeteccion = 6f; // Radio de detección

    private bool yaAparecio = false;
    private Transform jugador;

    void Start()
    {
        jugador = GameObject.FindWithTag("MainCamera").transform;
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);
        Debug.Log("La distancia es "+distancia);
        if (distancia <= distanciaDeteccion && !yaAparecio)
        {
            Debug.Log("Debería de estar dentro");
            yaAparecio = true;
            float random = Random.value;
            if (random <= probabilidadAparicion)
            {
                Aparecer();
            }
        }
        else if (distancia > distanciaDeteccion)
        {
            yaAparecio = false;
            Debug.Log("Debería de estar fuera");
        }
    }

    void Aparecer()
    {
        float randomDetras = Random.value;
        if (randomDetras > 0.3f)
        {
             Debug.Log("Debería de estar en un punto");
            int indice = Random.Range(0, spawnPoints.Length);
            Transform spawn = spawnPoints[indice];
            niña.transform.position = spawn.position;
            niña.transform.rotation = spawn.rotation;
        }
        else
        {
            Debug.Log("Debería de estar detrás");
            Vector3 detras = jugador.position - jugador.forward * 1.5f;
            detras.y = niña.transform.position.y;
            niña.transform.position = detras;
            niña.transform.rotation = Quaternion.LookRotation(jugador.forward);
        }
        niña.SetActive(true);
    }
}