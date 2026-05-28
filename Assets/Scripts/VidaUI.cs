using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIVida : MonoBehaviour
{
    public Slider barraVida;
    public TextMeshProUGUI textoVida;
    public SistemaVidasMuñeca sistemaVidas;

    void Update()
    {
        if (sistemaVidas != null)
        {
            barraVida.value = sistemaVidas.vidaActual / sistemaVidas.vidaMaxima;
            textoVida.text = sistemaVidas.vidaActual.ToString("F0") + " / " + sistemaVidas.vidaMaxima.ToString("F0");
        }
    }
}