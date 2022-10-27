using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDrag : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private GameObject cursor;

    void Awake()
    {
        cursor = GameObject.Find("Cursor");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.parent.position = eventData.position;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        transform.parent.parent.transform.SetAsLastSibling();
        cursor.transform.SetAsLastSibling();
    }
}
