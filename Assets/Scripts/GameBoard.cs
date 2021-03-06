﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private string boardKey;

    [SerializeField]
    private Card[] cards;

    [SerializeField]
    private RandomSpritePicker colorPicker;

    [SerializeField]
    private RandomSpritePicker accessoryPicker;

    [SerializeField]
    private Text movesCount;

    [SerializeField]
    private Text newMovesCount;

    [SerializeField]
    private Animator movesCountAnimator;

    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField]
    private CustomEvents.UnityStringIntEvent gameEnd;

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
        UpdateMovesCount();
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
        UpdateMovesCount();

        if (card.Cat != selectedCard.Cat)
        {
            card.FlipHidden();
            selectedCard.FlipHidden();
        }
        else
        {
            pairsFound++;
            card.PairFound();
            selectedCard.PairFound();
            CheckGameEnd();
        }
        selectedCard = null;
        eventSystem.enabled = true;
    }

    private void CheckGameEnd()
    {
        if (pairsFound >= cards.Length / 2)
        {
            gameEnd.Invoke(boardKey, moves);
        }
    }

    private void UpdateMovesCount()
    {
        if (moves == 0)
        {
            movesCount.text = moves.ToString();
            newMovesCount.text = moves.ToString();
            return;
        }

        newMovesCount.text = moves.ToString();
        movesCount.text = (moves - 1).ToString();
        movesCountAnimator.SetTrigger("UpdateMovesCount");
    }
}
