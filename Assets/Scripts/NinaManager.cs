using UnityEngine;

public class NinaManager : MonoBehaviour
{
    public static NinaManager instancia;

    void Awake()
    {
        instancia = this;
        Desactivar();
    }

    public void MoverA(Vector3 posicion, Quaternion rotacion)
    {
        transform.position = posicion;
        transform.rotation = rotacion;
        gameObject.SetActive(true);
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
    }
}