using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ingamecursor : MonoBehaviour
{
    public Image ourCursor;
    public Image ourCursor2;
    public Vector3 currentMousePos;
    public float currentScaleLerp = 0;

    public void LateUpdate()
    {
        if (GameManager.Instance.currentGameSate != GameManager.gamestate.Gameplay)
        {
            Cursor.visible = true;
            if (ourCursor.enabled)
            {
                ourCursor.enabled = false;
                ourCursor2.enabled = false;
            }
            return;
        }
        else
        {
            Cursor.visible = false;
            if (ourCursor.enabled == false)
            {
                ourCursor.enabled = true;
                ourCursor2.enabled = true;
            }
            ourCursor.transform.position = Input.mousePosition;

            if (currentScaleLerp == 0)
            {
                if (ourCursor.transform.localScale != Vector3.one) ourCursor.transform.localScale = Vector3.one;
            }
            else
            {
                ourCursor.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.15f, currentScaleLerp);
                currentScaleLerp = Mathf.Clamp(currentScaleLerp -= 2 * Time.deltaTime, 0, 1);
            }
        }
    }
}
