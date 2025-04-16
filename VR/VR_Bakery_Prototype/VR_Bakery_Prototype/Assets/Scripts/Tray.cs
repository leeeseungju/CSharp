using UnityEngine;
using TMPro;

public class Tray : MonoBehaviour
{
    public string targetTag = "Coffee"; // 쟁반 위에 올려야 하는 아이템의 태그
    public TextMeshProUGUI[] initialTexts; // 초기에 활성화된 텍스트들
    public TextMeshProUGUI replacementText; // 활성화되어야 할 새로운 텍스트

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // 태그가 일치하면 텍스트 교체
            ActivateReplacementText();
        }
    }

    private void ActivateReplacementText()
    {
        // 초기 텍스트 비활성화
        foreach (var text in initialTexts)
        {
            text.gameObject.SetActive(false);
        }

        // 새로운 텍스트 활성화
        replacementText.gameObject.SetActive(true);
    }
}
