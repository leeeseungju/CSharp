using UnityEngine;
using TMPro;

public class Tray : MonoBehaviour
{
    public string targetTag = "Coffee"; // ��� ���� �÷��� �ϴ� �������� �±�
    public TextMeshProUGUI[] initialTexts; // �ʱ⿡ Ȱ��ȭ�� �ؽ�Ʈ��
    public TextMeshProUGUI replacementText; // Ȱ��ȭ�Ǿ�� �� ���ο� �ؽ�Ʈ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // �±װ� ��ġ�ϸ� �ؽ�Ʈ ��ü
            ActivateReplacementText();
        }
    }

    private void ActivateReplacementText()
    {
        // �ʱ� �ؽ�Ʈ ��Ȱ��ȭ
        foreach (var text in initialTexts)
        {
            text.gameObject.SetActive(false);
        }

        // ���ο� �ؽ�Ʈ Ȱ��ȭ
        replacementText.gameObject.SetActive(true);
    }
}
