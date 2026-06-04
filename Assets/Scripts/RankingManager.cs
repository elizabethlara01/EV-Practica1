using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [Header("UI - Tiempo actual")]
    public TMP_Text textoTiempoActual;

    [Header("UI - Introducir nombre")]
    public TMP_InputField inputNombre;
    public Button btnGuardar;

    [Header("UI - Ranking")]
    public TMP_Text textoRanking;

    [Header("UI - Navegación")]
    public Button btnInicio;

    private float tiempoActual;
    private string csvPath;
    private const int MAX_ENTRADAS = 5;

    void Start()
    {
        csvPath = Path.Combine(Application.persistentDataPath, "ranking.csv");
        tiempoActual = PlayerPrefs.GetFloat("UltimoTiempo", 0f);

        textoTiempoActual.text = "Tu tiempo: " + FormatearTiempo(tiempoActual);

        btnGuardar.onClick.AddListener(GuardarEntrada);
        btnInicio.onClick.AddListener(() => SceneManager.LoadScene("MenuPrincipal"));

        MostrarRanking();
    }

    void GuardarEntrada()
    {
        string nombre = inputNombre.text.Trim();
        if (string.IsNullOrEmpty(nombre)) return;

        using (StreamWriter sw = new StreamWriter(csvPath, append: true))
            sw.WriteLine(nombre + "," + tiempoActual.ToString("F2", CultureInfo.InvariantCulture));

        // Bloquear para que no se guarde dos veces
        btnGuardar.interactable = false;
        inputNombre.interactable = false;

        MostrarRanking();
    }

    void MostrarRanking()
    {
        List<(string nombre, float tiempo)> entradas = CargarEntradas();

        if (entradas.Count == 0)
        {
            textoRanking.text = "Aún no hay puntuaciones registradas.";
            return;
        }

        var top = entradas.OrderBy(e => e.tiempo).Take(MAX_ENTRADAS).ToList();

        string texto = "─── TOP 5 ───\n\n";
        for (int i = 0; i < top.Count; i++)
            texto += $"{i + 1}.  {top[i].nombre}     {FormatearTiempo(top[i].tiempo)}\n";

        textoRanking.text = texto;
    }

    List<(string nombre, float tiempo)> CargarEntradas()
    {
        var lista = new List<(string, float)>();
        if (!File.Exists(csvPath)) return lista;

        foreach (string linea in File.ReadAllLines(csvPath))
        {
            string[] partes = linea.Split(',');
            if (partes.Length == 2 &&
                float.TryParse(partes[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float t))
                lista.Add((partes[0].Trim(), t));
        }
        return lista;
    }

    string FormatearTiempo(float segundos)
    {
        int min = (int)(segundos / 60);
        int seg = (int)(segundos % 60);
        int cs  = (int)((segundos * 100) % 100);
        return $"{min:00}:{seg:00}.{cs:00}";
    }
}
