using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Image catColor;

    [SerializeField]
    private Image catAccessory;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private CustomEvents.UnityCardEvent cardVisible;

    private Cat cat;
    private bool visible;

    public Cat Cat
    {
        get
        {
            return cat;
        }
        set
        {
            cat = value;
            catColor.sprite = cat.Color;
            catAccessory.sprite = cat.Accessory;
            catAccessory.gameObject.SetActive(null != cat.Accessory);
            visible = false;
        }
    }

    public void FlipHidden()
    {
        if (visible)
        {
            visible = false;
            animator.SetTrigger("Flip");
        }
    }

    public void OnCardClicked()
    {
        if (!visible)
        {
            visible = true;
            animator.SetTrigger("Flip");
            cardVisible.Invoke(this);
        }
    }

    public void PairFound()
    {
        animator.SetTrigger("PairFound");
    }
}
