using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField]
    Camera computerCamera;
    SpriteRenderer sRMonitor;

    public void Start()
    {
        sRMonitor = transform.parent.GetComponent<SpriteRenderer>();
    }

    public void LateUpdate()
    {
        Vector2 mousePos = Input.mousePosition;

        mousePos = GetAllowedPos(mousePos, sRMonitor);

        GetObjMouseHit(mousePos);
    }

    private Vector2 GetAllowedPos(Vector2 objWorldPos, SpriteRenderer allowedArea)
    {
        Vector3 sMinArea = allowedArea.bounds.min;
        Vector3 sMaxArea = allowedArea.bounds.max;
        Vector3 objAllowedPos = computerCamera.ScreenToWorldPoint(objWorldPos);

        if (IsAllowedPos(objAllowedPos, sMinArea, sMaxArea))
            Cursor.visible = true;
        else
            Cursor.visible = false;

        // Allowed area
        objAllowedPos.x = Mathf.Clamp(objAllowedPos.x, sMinArea.x, sMaxArea.x);
        objAllowedPos.y = Mathf.Clamp(objAllowedPos.y, sMinArea.y, sMaxArea.y);

        return objAllowedPos;
    }

    private bool IsAllowedPos(Vector3 value, Vector3 min, Vector3 max)
    {
        if (value.x < min.x || value.x > max.x)
            return false;
        else if (value.y < min.y || value.y > max.y)
            return false;

        return true;
    }

    private GameObject GetObjMouseHit(Vector2 mousePos)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

            RaycastHit2D hit = GetFrontGameObject(hits);

            // Hit someone
            switch (hit.transform.name)
            {
                case "Close":
                    CloseWindow(hit);
                    break;
                case "Moveable":
                    MoveWindow(hit);
                    break;
            }
        }

        return null;
    }

    private RaycastHit2D GetFrontGameObject(RaycastHit2D[] hits)
    {
        GameObject obj = null;
        SpriteRenderer objSR = null;
        RaycastHit2D raycastObj = new RaycastHit2D();

        foreach (RaycastHit2D hit in hits)
        {
            SpriteRenderer hitSR = hit.transform.GetComponent<SpriteRenderer>();

            if (obj == null || hitSR.sortingOrder > objSR.sortingOrder)
            {
                obj = hit.transform.gameObject;
                raycastObj = hit;
            }

            objSR = obj.GetComponent<SpriteRenderer>();
        }

        return raycastObj;
    }

    private void MoveWindow(RaycastHit2D hit)
    {
        Vector3 newPos = hit.point;
        newPos.y = hit.point.y - 0.075f;
        newPos.z = 0;

        hit.transform.parent.localPosition = newPos;
    }

    private void CloseWindow(RaycastHit2D hit)
    {
        GameObject.Destroy(hit.transform.parent.gameObject);
    }
}
