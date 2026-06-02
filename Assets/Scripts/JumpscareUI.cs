using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JumpscareUI : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup panelOverlay;   // Panel negro o rojo que cubre pantalla
    public Image imagenFantasma;       // Opcional: imagen del fantasma en pantalla
    public AudioSource audioSusto;     // Sonido de jumpscare

    [Header("Configuración")]
    public float duracionFlash = 0.3f;
    public float duracionTotal = 1.8f;

    void Start()
    {
        panelOverlay.alpha = 0f;
        panelOverlay.gameObject.SetActive(false);
    }

    public void MostrarJumpscare()
    {
        StartCoroutine(AnimarJumpscare());
    }

    public void OcultarJumpscare()
    {
        panelOverlay.alpha = 0f;
        panelOverlay.gameObject.SetActive(false);
    }

    IEnumerator AnimarJumpscare()
    {
        panelOverlay.gameObject.SetActive(true);

        if (audioSusto != null)
            audioSusto.Play();

        // Flash rápido: fade in
        float t = 0f;
        while (t < duracionFlash)
        {
            panelOverlay.alpha = Mathf.Lerp(0f, 1f, t / duracionFlash);
            t += Time.deltaTime;
            yield return null;
        }
        panelOverlay.alpha = 1f;

        yield return new WaitForSeconds(duracionTotal);

        // Fade out
        t = 0f;
        while (t < duracionFlash)
        {
            panelOverlay.alpha = Mathf.Lerp(1f, 0f, t / duracionFlash);
            t += Time.deltaTime;
            yield return null;
        }

        OcultarJumpscare();
    }
}