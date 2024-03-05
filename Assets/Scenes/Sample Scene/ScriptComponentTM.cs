using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptComponentTM : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI; // 텍스트 매쉬프로를 사용하느 UI
    public TMP_Text tmp; // 텍스트 매쉬프로 중에서 텍스트면 다 가능


    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>(); 
        textMeshProUGUI.text = "기존 텍스트와 \n큰 차이가 없음.";
    }

    public void OnButtonEnter(TextMeshProUGUI text)
    {
        SetBold(text.text);
    }
    public string SetBold(string data)
    {
        return $"<b>{data}</b>";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
