using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollListManager : MonoBehaviour
{
    [SerializeField]
    private InfiniteScrollListItem[] items;

    [SerializeField]
    private ScrollRect scrollRect;

    private object[] content;

    public void Initialize(object[] itemsContent)
    {
        if (null == items || 0 == items.Length)
        {
            Debug.LogError("Items null or empty");
            return;
        }

        content = itemsContent;

        // Set scroll list height
        var itemHeight = items[0].RectTransform.sizeDelta.y;
        var scrollHeight = itemHeight * content.Length;
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, scrollHeight);
        scrollRect.verticalNormalizedPosition = 1;

        // Initialize every item
        for (var i = 0; i < items.Length; i++)
        {
            items[i].Set(null, i);
        }
        AdjustItemsPosition();
    }

    public void AdjustItemsPosition()
    {
        if (null == items || 0 == items.Length)
        {
            Debug.LogError("Items null or empty");
            return;
        }

        if (0 == content.Length)
        {
            return;
        }

        var itemHeight = items[0].RectTransform.sizeDelta.y;
        var firstItemIndex = Mathf.FloorToInt(scrollRect.content.anchoredPosition.y / itemHeight);
        firstItemIndex = Mathf.Clamp(firstItemIndex, 0, content.Length - 1);

        if (items[0].ItemValue != content[firstItemIndex])
        {
            for (var i = 0; i < items.Length; i++)
            {
                var itemIndex = firstItemIndex + i;
                var itemPosition = itemHeight * itemIndex;
                var toSet = itemIndex < content.Length ? content[itemIndex] : null;
                var itemTransform = items[i].RectTransform;
                items[i].Set(toSet, itemIndex);
                itemTransform.anchoredPosition = new Vector2(itemTransform.anchoredPosition.x, -itemPosition);
            }
        }
    }
}
