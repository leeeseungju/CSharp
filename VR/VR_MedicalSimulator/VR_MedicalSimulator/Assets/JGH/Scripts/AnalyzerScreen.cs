using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnalyzerScreen : MonoBehaviour
{
    public TextMeshProUGUI alyzerScreen;
    public PressureXRUIController pressureXRUIController;
    public BIBSButton BIBSbutton;

    private float ScreenPressureMain;
    private float CarbonDioxide;
    private float Temperature;
    private float Humidity;

    private void Start()
    {
        StartCoroutine(HumidityText());
        StartCoroutine(TemperatureText());
        StartCoroutine(CarbonDioxideText());
    }

    private void FixedUpdate()
    {
        ScreenPressureMain = pressureXRUIController.PressureMain * 3;
        alyzerScreen.text = BIBSbutton.oxygen.ToString("F2")+ "\n"
                            + CarbonDioxide.ToString("F4") + "\n"
                            + Temperature.ToString("F1") + "\n"
                            + Humidity.ToString("F1") + "\n"
                            + ScreenPressureMain.ToString("F1");
    }

    IEnumerator HumidityText()
    {
        while (true)
        {
            Humidity = Random.Range(49.8f,50.2f);
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator TemperatureText()
    {
        while (true)
        {
            Temperature = Random.Range(21.2f, 21.6f);
            yield return new WaitForSeconds(9f);
        }
    }

    IEnumerator CarbonDioxideText()
    {
        while (true)
        {
            CarbonDioxide = Random.Range(0.4900f, 0.4920f);
            yield return new WaitForSeconds(8f);
        }
    }
}