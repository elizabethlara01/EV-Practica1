using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSeguirCamara : MonoBehaviour
{
    private Transform camara;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        BuscarCamara();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BuscarCamara();
    }

    void BuscarCamara()
    {
        GameObject centerEye = GameObject.Find("CenterEyeAnchor");
        if (centerEye != null)
            camara = centerEye.transform;
        else
            camara = Camera.main.transform;
    }

    void Update()
    {
        if (camara != null)
        {
            transform.position = camara.position + camara.forward * 1f;
            transform.rotation = camara.rotation;
            transform.localScale = Vector3.one * 0.01f;
        }
    }
}