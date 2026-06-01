using UnityEngine;
using System.Collections;

public class SistemaVidasMuñeca : MonoBehaviour
{
    public float vidaMaxima=100f;
    public float dañoPorSegundo=10f;


    public float vidaActual;
    private bool muriendo=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidaActual=vidaMaxima;
        
    }

    void OnEnable()
    {
        muriendo = false;
    }

    public void RecibirDaño(float daño){
        if(!muriendo){
            vidaActual-=daño;
             Debug.Log("Vida restante: " + vidaActual.ToString("F0"));
            if(vidaActual<=0){
                muriendo=true;
                //StartCoroutine(ParpadearyDesaparecer());
            }
        }

    }



    IEnumerator ParpadearyDesaparecer(){
        //Busca los renderes de la muñeca, como puede ser el cuerpo, ropa, pelo, etc.
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        
        for (int i = 0; i < 10; i++)
        {
            // Apaga todos los renderes
            foreach (Renderer r in renderers) r.enabled = false;
            yield return new WaitForSeconds(0.1f);
            // Enciende todos los renderes
            foreach (Renderer r in renderers) r.enabled = true;
            //Espera un segundo
            yield return new WaitForSeconds(0.1f);
        }
        
        gameObject.SetActive(false);
    }
}
