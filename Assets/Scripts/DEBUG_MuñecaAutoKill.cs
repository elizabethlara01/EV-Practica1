using UnityEngine;

// SCRIPT TEMPORAL DE DEBUG — eliminar antes de la entrega
public class DEBUG_MuñecaAutoKill : MonoBehaviour
{
    public float intervalo = 5f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intervalo)
        {
            timer = 0f;
            SistemaVidasMuñeca muñeca = FindAnyObjectByType<SistemaVidasMuñeca>();
            if (muñeca != null && muñeca.gameObject.activeInHierarchy)
            {
                Debug.Log("[DEBUG] Matando muñeca. Ecos restantes tras esto: " + (EchoManager.Instance.echoesAlive - 1));
                muñeca.RecibirDaño(muñeca.vidaMaxima);
            }
            else
            {
                Debug.Log("[DEBUG] Muñeca no activa en escena.");
            }
        }
    }
}
