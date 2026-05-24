using UnityEngine;
using UnityEngine.InputSystem;

public class CogerLinterna : MonoBehaviour
{
    public Transform rightHandAnchor;
    public float distanciaParaCoger = 0.5f;

    private bool cogida = false;

    void Update()
    {
        if (!cogida)
        {
            float distancia = Vector3.Distance(transform.position, rightHandAnchor.position);

            if (distancia < distanciaParaCoger && Mouse.current.leftButton.wasPressedThisFrame)
            {
                CogerObjeto();
            }
        }
        else
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                SoltarObjeto();
            }
        }
    }

    private void CogerObjeto()
    {
        cogida = true;
        transform.SetParent(rightHandAnchor);
        transform.localPosition = new Vector3(0, -0.1f, 0.1f);
        transform.localRotation = Quaternion.Euler(90, 0, 0);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void SoltarObjeto()
    {
        cogida = false;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}