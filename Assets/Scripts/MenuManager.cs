using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Slider sliderVolumen;
    public Slider sliderBrillo;
    public Image panelBrillo;

    public void CambiarVolumen(float valor)
    {
        AudioListener.volume = valor;
    }

    public void CambiarBrillo(float valor)
    {
        RenderSettings.ambientIntensity = valor;
    }

    public void CambiarCalidad(int nivel)
    {
        QualitySettings.SetQualityLevel(nivel);
    }

    public void CambiarBrilloPanel(float valor)
    {
        Color c = panelBrillo.color;
        c.a = 1 - valor;
        panelBrillo.color = c;
    }
}