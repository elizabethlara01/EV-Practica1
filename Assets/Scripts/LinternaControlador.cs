using UnityEngine;

public class LinternaControlador : MonoBehaviour
{
    public Light luzLinterna;
    public OVRHand manoDerecha; // Asígnala directamente en el Inspector
    
    private bool encendida = false;
    private bool puñoCerradoAntes = false;
    private bool pinchAntes = false;

    void Update()
    {
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
        {
            // ---- MODO MANDOS ----
            /*
            float agarre = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
            bool puñoCerrado = agarre > 0.9f;

            if (puñoCerrado && !puñoCerradoAntes)
            {
                ToggleLinterna();
            }

            puñoCerradoAntes = puñoCerrado;
            */
            // ---- MODO MANDOS ----
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                ToggleLinterna();
            }
        }
        else if (manoDerecha != null && manoDerecha.IsTracked)
        {
            // ---- MODO HAND TRACKING ----
            //bool pinch = manoDerecha.GetFingerIsPinching(OVRHand.HandFinger.Index);
            bool pinch = manoDerecha.GetFingerIsPinching(OVRHand.HandFinger.Index);
            if (pinch && !pinchAntes)
            {
                ToggleLinterna();
            }

            pinchAntes = pinch;
        }
    }

    private void ToggleLinterna()
    {
        encendida = !encendida;
        luzLinterna.intensity = encendida ? 1f : 0f;
    }
}