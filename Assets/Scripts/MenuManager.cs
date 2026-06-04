using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Slider sliderVolumen;
    private Slider sliderBrillo;

    private Image panelBrillo;

    void Start()
{
    GameObject obj = GameObject.Find("PanelBrillo");
    if (obj != null)
        panelBrillo = obj.GetComponent<Image>();

    float volumen = PlayerPrefs.GetFloat("Volumen", 1f);
    float brillo = PlayerPrefs.GetFloat("Brillo", 1f);

    AudioListener.volume = volumen;
    
    if (panelBrillo != null)
    {
        Color c = panelBrillo.color;
        c.a = 1 - brillo;
        panelBrillo.color = c;
    }
}

    public void CambiarVolumen(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("Volumen", valor);
    }

    public void CambiarCalidad(int nivel)
    {
        QualitySettings.SetQualityLevel(nivel);
    }

    public void CambiarBrilloPanel(float valor)
    {
            Debug.Log("Valor recibido: " + valor);

        if (panelBrillo == null)
            panelBrillo = GameObject.Find("PanelBrillo").GetComponent<Image>();
        
        Color c = panelBrillo.color;
        c.a = (1 - valor) * 0.8f;
        panelBrillo.color = c;
        PlayerPrefs.SetFloat("Brillo", valor);
    }

    public void PantallaCompleta(bool valor)
    {
        Screen.fullScreen = valor;
    }

    public void Salir()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EscenaAjustes")
        {
            GameObject objBrillo = GameObject.Find("Slider");
            if (objBrillo != null) sliderBrillo = objBrillo.GetComponent<Slider>();

            GameObject objVolumen = GameObject.Find("SliderVolumen");
            if (objVolumen != null) sliderVolumen = objVolumen.GetComponent<Slider>();

            if (sliderVolumen != null) sliderVolumen.value = PlayerPrefs.GetFloat("Volumen", 1f);
            if (sliderBrillo != null) sliderBrillo.value = PlayerPrefs.GetFloat("Brillo", 1f);
        }
    }
}