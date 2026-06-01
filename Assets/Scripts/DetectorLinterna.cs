using UnityEngine;

public class DetectorLinterna : MonoBehaviour
{
    public Animator animator;
    public float tiempoParaCorrer = 2f;
    public float tiempoParaDesaparecer = 2f;
    public float velocidad = 5f;
    public AudioClip risaCorrer; // Arrastra el audio de la risa aquí
    
    private float tiempoApuntando = 0f;
    private bool corriendo = false;
    private Transform jugador;
    private Vector3 direccionFija;
    private AudioSource audioSourceRisa;

    void Start()
    {
        jugador = GameObject.FindWithTag("MainCamera").transform;
        audioSourceRisa = gameObject.AddComponent<AudioSource>();
        audioSourceRisa.spatialBlend = 1f;
        audioSourceRisa.clip = risaCorrer;
        audioSourceRisa.loop = true; // Se repite mientras corre
    }

    void Update()
    {
        if (corriendo)
        {
           transform.position += direccionFija * velocidad * Time.deltaTime;
        }else{
            Vector3 direccion = jugador.position - transform.position;
            direccion.y = 0; // Para que no se incline
            
            if (direccion != Vector3.zero){
                //float angulo = Vector3.SignedAngle(transform.forward, direccion, Vector3.up);
                //animator.SetFloat("giro", angulo / 180f);
                Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 3f);
            }
        }

        if (EstaApuntandoLinterna())
        {
            tiempoApuntando += Time.deltaTime;

            //Vida muñeca
            SistemaVidasMuñeca vida= GetComponent<SistemaVidasMuñeca>();
            if(vida!=null){
                vida.RecibirDaño(vida.dañoPorSegundo*Time.deltaTime);
            }

            if (tiempoApuntando >= tiempoParaCorrer && !corriendo)
            {
                if (animator.GetBool("sentada") || animator.GetBool("llorando"))
                {
                    Desaparecer();
                }else{
                    corriendo = true;
                    audioSourceRisa.time = 4f;
                    audioSourceRisa.Play();
                    Vector3 dir = (jugador.position - transform.position);
                    dir.y = 0;
                    direccionFija = dir.normalized;
                    animator.SetBool("corriendo", true);
                    Invoke("Desaparecer", tiempoParaDesaparecer);
                }
            }
        }
        else
        {
            tiempoApuntando = 0f;
        }
    }

    private bool EstaApuntandoLinterna()
    {
        RaycastHit hit;
        GameObject linterna = GameObject.FindWithTag("Linterna");
        
        if (linterna == null) return false;
        Light luz = linterna.GetComponentInChildren<Light>();
        if (luz == null || luz.intensity == 0) return false;
        
        Vector3 origen = luz.transform.position;
        Vector3 direccion = luz.transform.forward;

        int layerMuñeca = LayerMask.GetMask("Muñeca");
        
        if (Physics.Raycast(origen, direccion, out hit, 20f, layerMuñeca))
        {
            return hit.transform == transform;
        }
        return false;
    }
    
    private void Desaparecer()
    {
        float tiempoActual = audioSourceRisa.time;
        GameObject audioObj = new GameObject("RisaLinterna");
        AudioSource audio = audioObj.AddComponent<AudioSource>();
        audio.clip = risaCorrer;
        audio.spatialBlend = 0f; // 2D para que se escuche bien
        audio.time = tiempoActual;
        audio.Play();
        Destroy(audioObj, risaCorrer.length);
        gameObject.SetActive(false);
    }
}