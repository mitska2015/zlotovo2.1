using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class DialogueNode
{
    public string charName;
    [TextArea(2, 5)] public string speech;

    [Header("Настройки выбора")]
    public bool hasChoices;
    public string choice1Text;
    public string choice2Text;
}

public class SimpleDialogue : MonoBehaviour
{
    [Header("Текстовые поля")]
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI speechLabel;

    [Header("Настройки выбора")]
    public GameObject choicePanel;
    public TextMeshProUGUI choice1Label;
    public TextMeshProUGUI choice2Label;

    [Header("Блокировка управления")]
    public GameObject cameraObject;
    public string cameraScriptName = "MouseLook";

    [Header("Список фраз")]
    public List<DialogueNode> nodes = new List<DialogueNode>();

    public float typingSpeed = 0.05f;
    private int index = 0;
    private bool isTyping = false;

    void Awake()
    {
        if (choicePanel != null) choicePanel.SetActive(false);
    }

    void Start() {
    nameLabel.text = "";
    speechLabel.text = "";

    // Скрываем курсор, пока ждем 20 секунд
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
        // ПОВОРОТ КАМЕРЫ ВПЕРЕД ПРИ СТАРТЕ
        if (cameraObject != null)
        {
            // Устанавливаем вращение объекта в 0 по всем осям (вперед)
            cameraObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        StartCoroutine(WaitBeforeStart(20f));
}

    IEnumerator WaitBeforeStart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (nodes.Count > 0)
        {
            Button mainBtn = speechLabel.gameObject.GetComponent<Button>();
            if (mainBtn == null) mainBtn = speechLabel.gameObject.AddComponent<Button>();
            mainBtn.onClick.AddListener(OnTextClicked);
            ShowNode();
        }
    }

    public void OnTextClicked()
    {
        if (isTyping) FinishTyping();
        else if (index < nodes.Count && !nodes[index].hasChoices) NextStep();
    }

    void ShowNode()
    {
        ToggleCamera(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // ПОВОРОТ КАМЕРЫ ВПЕРЕД ПРИ СТАРТЕ
        if (cameraObject != null)
        {
            // Устанавливаем вращение объекта в 0 по всем осям (вперед)
            cameraObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (choicePanel != null) choicePanel.SetActive(false);
        nameLabel.text = nodes[index].charName;
        StopAllCoroutines();
        StartCoroutine(TypeText(nodes[index].speech));
    }

    void FinishTyping()
    {
        StopAllCoroutines();
        speechLabel.text = nodes[index].speech;
        isTyping = false;
        if (nodes[index].hasChoices) ShowChoices();
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        speechLabel.text = "";
        foreach (char letter in line.ToCharArray())
        {
            speechLabel.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        if (nodes[index].hasChoices) ShowChoices();
    }

    void ShowChoices()
    {
        if (choicePanel != null)
        {
            choicePanel.SetActive(true);
            choice1Label.text = nodes[index].choice1Text;
            choice2Label.text = nodes[index].choice2Text;
        }
    }

    public void SelectChoice(int choiceNum)
    {
        NextStep();
    }

    public void NextStep()
    {
        index++;
        if (index < nodes.Count)
        {
            ShowNode();
        }
        else
        {
            ToggleCamera(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            nameLabel.text = "";
            speechLabel.text = "";
            if (choicePanel != null) choicePanel.SetActive(false);
        }
    }

    void ToggleCamera(bool state)
    {
        if (cameraObject != null)
        {
            var script = cameraObject.GetComponent(cameraScriptName) as MonoBehaviour;
            if (script != null) script.enabled = state;
        }
    }
}