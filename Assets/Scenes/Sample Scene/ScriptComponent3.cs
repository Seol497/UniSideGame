using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptComponent3 : MonoBehaviour
{
    public GameObject CirclePrefab;
    public Text content_text;
    public int data_value = 0;
    public void OnButton1Enter()
    {
        Instantiate(CirclePrefab);
    }
    public void OnButton2Enter(GameObject go)
    {
        Instantiate(go);
    }
    public void SetData(int value)
    {
        data_value = value;
        SetText(value);
    }
    private void SetText(int value)
    {
        content_text.text = data_value.ToString();
    }
    private void Method01()
    {

    }



}
