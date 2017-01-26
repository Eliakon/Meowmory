using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameBoard : MonoBehaviour
{
    [System.Serializable]
    public class UnityIntEvent : UnityEvent<int> { }

    [SerializeField]
    private Card[] cards;

    [SerializeField]
    private RandomSpritePicker colorPicker;

    [SerializeField]
    private RandomSpritePicker accessoryPicker;

    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField]
    private UnityIntEvent gameEnd;

    private Cat[] cats;
    private Card selectedCard;
    private int pairsFound;
    private int moves;

    private void OnEnable()
    {
        GenerateCats();
        Shuffle(cats);
        for (var i = 0; i < cards.Length; i++)
        {
            cards[i].Cat = cats[i];
        }

        selectedCard = null;
        pairsFound = 0;
        moves = 0;
    }

    private void GenerateCats()
    {
        cats = new Cat[cards.Length];

        for (var i = 0; i < cats.Length; i += 2)
        {
            Cat generatedCat = null;

            while (null == generatedCat)
            {
                var color = colorPicker.Pick();
                var accessory = accessoryPicker.Pick();

                if (!CatExists(color, accessory))
                {
                    generatedCat = new Cat { Color = color, Accessory = accessory };
                }
            }

            cats[i] = generatedCat;
            cats[i + 1] = generatedCat;
        }
    }

    private bool CatExists(Sprite color, Sprite accessory)
    {
        return System.Array.FindIndex(cats, cat =>
            null != cat &&
            cat.Color == color &&
            cat.Accessory == accessory) >= 0;
    }

    private void Shuffle(Cat[] items)
    {
        for (var i = 0; i < items.Length; i++)
        {
            var tmp = items[i];
            var r = Random.Range(i, items.Length);
            items[i] = items[r];
            items[r] = tmp;
        }
    }

    public void SelectCard(Card card)
    {
        if (null == selectedCard)
        {
            selectedCard = card;
            return;
        }

        eventSystem.enabled = false;
        StartCoroutine(ActionAfterDelay.DoAfterDelay(0.5f, () => { CompareCards(card); }));
    }

    private void CompareCards(Card card)
    {
        moves++;

        if (card.Cat != selectedCard.Cat)
        {
            card.FlipHidden();
            selectedCard.FlipHidden();
        }
        else
        {
            pairsFound++;
            CheckGameEnd();
        }
        selectedCard = null;
        eventSystem.enabled = true;
    }

    private void CheckGameEnd()
    {
        if (pairsFound >= cards.Length / 2)
        {
            gameEnd.Invoke(moves);
        }
    }
}
