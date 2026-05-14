using UnityEngine;
public class DetectorLinterna : MonoBehaviour
{
    public Animator animator;
    public float tiempoParaCorrer = 2f;
    public float tiempoParaDesaparecer = 2f;
    
    private float tiempoApuntando = 0f;
    private bool corriendo = false;
    void Update()
    {
        if (EstaApuntandoLinterna())
        {
            tiempoApuntando += Time.deltaTime;
            if (tiempoApuntando >= tiempoParaCorrer && !corriendo)
            {
                corriendo = true;
                animator.SetBool("corriendo", true);
                Invoke("Desaparecer", tiempoParaDesaparecer);
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
        if (Physics.Raycast(origen, direccion, out hit, 20f))
        {
            return hit.transform == transform;
        }
        return false;
    }
    private void Desaparecer()
    {
        gameObject.SetActive(false);
    }
}
