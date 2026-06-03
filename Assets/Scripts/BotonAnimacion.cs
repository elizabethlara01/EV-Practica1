using UnityEngine;
using UnityEngine.EventSystems;

public class BotonAnimacion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        animator.SetTrigger("Highlighted");
    }

    public void OnPointerExit(PointerEventData data)
    {
        animator.SetTrigger("Normal");
    }

    public void OnPointerClick(PointerEventData data)
    {
        animator.SetTrigger("Pressed");
    }
}