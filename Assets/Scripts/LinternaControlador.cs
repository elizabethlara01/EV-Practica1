using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class LinternaControlador : MonoBehaviour
{
    public Light luzLinterna;
    public InputActionReference botonLinterna;
    private bool encendida=false;
    
    private void OnEnable() //Cuando el objeto aparece en escena
    {
        botonLinterna.action.performed += encenderApagarLuz;
    }

    private void OnDisable() //Cuando el objeto dejar de aparecer en escena
    {
        botonLinterna.action.performed -= encenderApagarLuz;
    }

    private void encenderApagarLuz(InputAction.CallbackContext context){
        encendida= !encendida;
        if(encendida){
            luzLinterna.intensity=1f;
        }else{
            luzLinterna.intensity=0f;
        }

    }
}
