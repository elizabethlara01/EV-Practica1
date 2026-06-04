using UnityEngine;

public class GestorApariciones : MonoBehaviour
{
    public static GestorApariciones instancia;

    [System.Serializable]
    public class Habitacion
    {
        public string nombre;
        public Transform centro;
        public SpawnPoint[] spawnPoints;
    }

    [System.Serializable]
    public class SpawnPoint
    {
        public Transform punto;
        public bool llorar, sentada;
    }

    public Habitacion[] habitaciones;
    public GameObject niña;
    //public float distanciaDeteccion = 15f;
    public float probabilidadAparicion = 1f;
    public float tiempoEntreApariciones = 0f; // Tiempo mínimo entre apariciones

    private Transform jugador;
    private Habitacion habitacionActual = null;
    private float tiempoUltimaAparicion = 0f;
    

    
    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        jugador = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Habitacion habitacionMasCercana = GetHabitacionMasCercana();

        if (habitacionMasCercana != null && Time.time - tiempoUltimaAparicion >= tiempoEntreApariciones)
        {
            if (habitacionMasCercana != habitacionActual || !niña.activeSelf)
            {
                habitacionActual = habitacionMasCercana;
                float random = Random.value;
                Debug.Log("Habitacion: " + habitacionActual.nombre + " Random: " + random);
                if (random <= probabilidadAparicion)
                {
                    tiempoUltimaAparicion = Time.time;
                    Aparecer(habitacionActual);
                }
                else
                {
                  //  tiempoUltimaAparicion = Time.time;
                }
            }
        }
    }

    Habitacion GetHabitacionMasCercana()
    {
        Habitacion masCercana = null;
        float distanciaMinima = float.MaxValue;

        foreach (Habitacion h in habitaciones)
        {
            float distancia = Vector3.Distance(h.centro.position, jugador.position);
            Debug.Log(h.nombre + " distancia: " + distancia.ToString("F1"));
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                masCercana = h;
            }
        }

        return masCercana;
    }

    void Aparecer(Habitacion habitacion)
    {
            Debug.Log("Intentando aparecer en: " + habitacion.nombre + " con " + habitacion.spawnPoints.Length + " spawnpoints");

        float randomDetras = Random.value;

        if (randomDetras < 0.3f)
        {
            Vector3 detras = jugador.position - jugador.forward * 1.5f;
            detras.y = niña.transform.position.y;
            niña.transform.position = detras;
            niña.transform.rotation = Quaternion.LookRotation(jugador.forward);
            Debug.Log("Aparece detrás del jugador");

            Animator anim = niña.GetComponent<Animator>();
            anim.SetBool("sentada", false);
            anim.SetBool("llorando", false);

            AudioMuñeca audio = niña.GetComponent<AudioMuñeca>();
            if (audio != null)
                audio.ReproducirRespiracion();
        }
        else
        {
            int indice = Random.Range(0, habitacion.spawnPoints.Length);
            SpawnPoint spawn = habitacion.spawnPoints[indice];
            niña.transform.position = spawn.punto.position;
            niña.transform.rotation = spawn.punto.rotation;
            Debug.Log("Aparece en: " + spawn.punto.name);
/*
            Vector3 direccion = habitacion.centro.position - spawn.punto.position;
            direccion.y = 0;
            niña.transform.rotation = Quaternion.LookRotation(direccion);
*/
            Animator anim = niña.GetComponent<Animator>();
            anim.SetBool("sentada", false);
            anim.SetBool("llorando", false);

            if (spawn.llorar)
                anim.SetBool("llorando", true);
            else if(spawn.sentada)
                anim.SetBool("sentada", true);
        }
        niña.SetActive(true);
    }
}