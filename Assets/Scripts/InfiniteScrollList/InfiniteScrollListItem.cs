using UnityEngine;

public class InfiniteScrollListItem : MonoBehaviour
{
    public RectTransform RectTransform;

    private object itemValue;

    public object ItemValue
    {
        get
        {
            return itemValue;
        }
    }
    
    public virtual void Set(object value, int index)
    {
        itemValue = value;
    }
}
