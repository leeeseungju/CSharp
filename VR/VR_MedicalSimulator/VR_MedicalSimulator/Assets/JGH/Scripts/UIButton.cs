using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Content.Interaction;

public class UIButton : MonoBehaviour
{
    public TextMeshPro stepText;
    public TextMeshPro processText;
    public BIBSButton bIBSButton;
    public PressureXRUIController pressureXRUIController;
    public LSJXRKnob pressurePlusWheel;
    public LSJXRKnob pressureMinusWheel;
    public Patient patient;
    public GameObject step0_0_Button, step0_Button, step1_Button, step2_Button, step3_Button, step4_Button, step5_Button;
    public AudioSource typingSource;
    public AudioClip typing;
    IEnumerator C2, C3, C4;
    public bool step1, step2_1, step2_2, step2_3, step2_4, step2_5, step3, step4, step5 = false;
    private string currentText = "";
    public float delayLetters = 0.03f;
    public float count = 15f;
    public float barText;

    private void Start()
    {
        C2 = Step2Process();
        C3 = Step3Process();
        C4 = Step4Process();
    }

    public void StartTyping()
    {
        currentText = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (var letter in processText.text)
        {
            currentText += letter;
            processText.text = currentText;
            typingSource.PlayOneShot(typing);
            yield return new WaitForSeconds(delayLetters);
        }

        processText.text = currentText;
    }

    public void Step0()
    {
        step0_Button.SetActive(true);
        step0_0_Button.SetActive(false);
        stepText.text = "Step 1";
        processText.text = "고압산소치료기 시뮬레이션을 시작합니다.";
        StartTyping();
    }


    public void Step1()
    {
        step1 = true;
        step0_Button.SetActive(false);

        stepText.text = "Step 1";
        processText.text = "환자 옆으로 이동하세요.";
        StartTyping();
    }

    public void Step2_1()
    {
        step1 = false;
        step1_Button.SetActive(false);
        step2_1 = true;

        stepText.text = "Step 2";
        processText.text = "컨트롤 콘솔 앞으로 이동하세요.";
        StartTyping();
    }

    public void Step2_2()
    {
        step2_1 = false;
        step2_2 = true;
        processText.text = "주격실 가압밸브를 좌측으로 돌려 압력을 1.5bar ~ 2.5bar 사이로 맞춰주세요. 현재 압력 " + "0.0" + "bar";
        StartTyping();
        StartStep2Process();
    }

    public void StartStep2Process()
    {
        StartCoroutine(C2);
    }

    public void StopStep2Process()
    {
        StopCoroutine(C2);
    }

    private IEnumerator Step2Process()
    {
        yield return new WaitForSeconds(2.5f);

        while (step2_2 == true)
        {
            processText.text = "주격실 가압밸브를 좌측으로 돌려 압력을 1.5bar ~ 2.5bar 사이로 맞춰주세요. 현재 압력 " + barText.ToString("F1") + "bar";
            yield return new WaitForSeconds(0.1f);
        }

        processText.text = "주격실 가압밸브를 우측으로 돌려 잠가주세요." + "\n" + "현재 압력 " + barText.ToString("F1") + "bar";
        StartTyping();

        yield return new WaitForSeconds(2f);

        while (step2_3 == true)
        {
            processText.text = "주격실 가압밸브를 우측으로 돌려 잠가주세요." + "\n" + "현재 압력 " + barText.ToString("F1") + "bar";
            yield return new WaitForSeconds(0.1f);
        }

        processText.text = "가압밸브를 우측으로 돌려 잠그고 감압밸브를 좌측으로 돌려 압력을 낮춰주세요. 현재 압력 " + barText.ToString("F1") + "bar";
        StartTyping();

        yield return new WaitForSeconds(2.5f);

        while (step2_4 == true)
        {
            processText.text = "가압밸브를 우측으로 돌려 잠그고 감압밸브를 좌측으로 돌려 압력을 낮춰주세요. 현재 압력 " + barText.ToString("F1") + "bar";
            yield return new WaitForSeconds(0.1f);
        }

        processText.text = "주격실 감압밸브를 우측으로 돌려 잠가주세요." + "\n" + "현재 압력 " + barText.ToString("F1") + "bar";
        StartTyping();

        yield return new WaitForSeconds(2f);

        while (step2_5 == true)
        {
            processText.text = "주격실 감압밸브를 우측으로 돌려 잠가주세요." + "\n" + "현재 압력 " + barText.ToString("F1") + "bar";
            yield return new WaitForSeconds(0.1f);
        }

        StartStep2Process();
    }

    public void Step3()
    { 
        step2_3 = false;
        step2_5 = false;
        step2_Button.SetActive(false);
        StopStep2Process();
        step3 = true;
        stepText.text = "Step 3";
        processText.text = "주격실 BIBS MASK를 O2로 돌려주세요." + "\n" + "현재 산소 " + bIBSButton.oxygen.ToString("F2") + "%";
        StartTyping();
        StartStep3Process();
    }

    public void StartStep3Process()
    {
        StartCoroutine(C3);
    }

    public void StopStep3Process()
    {
        StopCoroutine(C3);
    }

    private IEnumerator Step3Process()
    {
        yield return new WaitForSeconds(2.5f);

        while (step3 == true)
        {
            processText.text = "주격실 BIBS MASK를 O2로 돌려주세요." + "\n" + "현재 산소 " + bIBSButton.oxygen.ToString("F2") + "%";
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Step4()
    {
        step3 = false;
        step3_Button.SetActive(false);
        StopStep3Process();
        step4 = true;
        stepText.text = "Step 4";
        processText.text = "치료 중입니다." + "\n" + "남은 시간 " + count.ToString("F0") + "초";
        StartTyping();
        StartStep4Process();
    }

    public void Step4Count()
    {
        if (count > 0f)
        {
            count -= 1 * Time.fixedDeltaTime;
        }
    }

    public void StartStep4Process()
    {
        StartCoroutine(C4);
    }

    public void StopStep4Process()
    {
        StopCoroutine(C4);
    }

    private IEnumerator Step4Process()
    {
        yield return new WaitForSeconds(1f);

        while (step4 == true)
        {
            processText.text = "치료 중입니다." + "\n" + "남은 시간 " + count.ToString("F0") + "초";
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Step5()
    {
        step4 = false;
        step4_Button.SetActive(false);
        StopStep4Process();
        step5 = true;
        step5_Button.SetActive(true);
        stepText.text = "Step 5";
        processText.text = "치료가 완료되었습니다!";
        StartTyping();
    }

    public void Step6()
    {
        SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        barText = pressureXRUIController.PressureMain * 3;

        if (step2_2 == true && barText > 1.5f && barText < 2.5f)
        {
            step2_2 = false;
            step2_3 = true;
        }

        if (step2_3 == true && pressurePlusWheel.value == 0)
        {
            step2_Button.SetActive(true);
        }
        else if (step2_3 == true && barText > 2.5f)
        {
            step2_3 = false;
            step2_Button.SetActive(false);
            step2_4 = true;
        }

        if (step2_4 == true && barText < 2.5f)
        {
            step2_4 = false;
            step2_5 = true;
        }

        if (step2_5 == true && pressureMinusWheel.value == 0)
        {
            step2_Button.SetActive(true);
        }
        else if (step2_5 == true && barText < 1.5f)
        {
            step2_5 = false;
            step2_2 = true;
            step2_Button.SetActive(false);
        }

        if (step3 == true && bIBSButton.oxygen == 99.47f)
        {
            step3_Button.SetActive(true);
        }
        else
        {
            step3_Button.SetActive(false);
        }

        if (step4 == true)
        {
            Step4Count();
        }

        if (step4 == true && count < 0.2f)
        {
            step4_Button.SetActive(true);
        }
    }

    public void StartSit()
    {
        patient.StartCoroutineSit();
    }
}
