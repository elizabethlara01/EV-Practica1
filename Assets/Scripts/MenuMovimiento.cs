using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuMovimiento : MonoBehaviour
{
    float x, y;
    [SerializeField] float velocidad;//Utilizamos SerializeField para que no pueda modificarse desde otros objetos del juego.
    [SerializeField] char boton;//Utilizamos SerializeField para que no pueda modificarse desde otros objetos del juego.
    // Start is called before the first frame update
    float posicionx, posiciony;
    void Start()
    {
        posicionx=transform.position.x;
        posiciony=transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.mousePosition.x;
        y = Input.mousePosition.y;


        switch (boton)
        {
            case 'n':
                this.GetComponent<RectTransform>().position = new Vector2((x / Screen.width) * velocidad + (Screen.width - 410), (y / Screen.height) * velocidad + (Screen.height - 450));
                break;
            case 'c':
                this.GetComponent<RectTransform>().position = new Vector2((x / Screen.width) * velocidad + (Screen.width - 410), (y / Screen.height) * velocidad + (Screen.height - 250));
                break;
            case 'a':
                this.GetComponent<RectTransform>().position = new Vector2((x / Screen.width) * velocidad + (Screen.width - 410), (y / Screen.height) * velocidad + (Screen.height - 650));
                break;
            case 's':
                this.GetComponent<RectTransform>().position = new Vector2((x / Screen.width) * velocidad + (Screen.width - 410), (y / Screen.height) * velocidad + (Screen.height - 850));
                break;
            case 'e':
                float diferenciax=Screen.width/2-posicionx;
                float diferenciay=Screen.height/2-posiciony;
                this.GetComponent<RectTransform>().position = new Vector2((x / Screen.width) * velocidad + (Screen.width/2-diferenciax), (y / Screen.height) * velocidad + (Screen.height/2-diferenciay));
               
                break;


        }

    }
}


