using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Messenger : MonoBehaviour, IDragHandler
{
    private Queue dialogue;
    private float dialogueSpeed;

    [SerializeField]
    private GameObject messageLayout, contentPanel;

    private void Start()
    {
        StartDialogue();
    }

    // Initialize which dialogue will trigger
    public void StartDialogue()
    {
        dialogue = new Queue();

        // Dialogue
        dialogue.Enqueue(new Person("Chefe", "Precisa cumprir o horário", Color.red, false));
        dialogue.Enqueue(new Person("Chefe", "Por isso, ficará pós-expediente", Color.red, false));
        dialogue.Enqueue(new Person("Jogador", "Eu tenho compromisso...", Color.cyan, true));
        dialogue.Enqueue(new Person("Chefe", "Se quiser ainda trabalhar", Color.red, false));
        dialogue.Enqueue(new Person("Chefe", "Desmarque os compromissos", Color.red, false));
        dialogue.Enqueue(new Person("Jogador", "Está certo", Color.cyan, true));
        dialogue.Enqueue(new Person("Chefe", "Ficarei te observando", Color.red, false));
        dialogue.Enqueue(new Person("Chefe", "Ao trabalho!", Color.red, false));

        Debug.Log("Dialogue Initialize!");
    }

    // Show the next dialogue sentence
    public void NextSentence()
    {
        // No dialogue left
        if (dialogue.Count == 0)
            return;

        // Get the FIFO dialogue line
        Person person = (Person) dialogue.Peek();

        // Clone the message prefab
        GameObject dialogueObj = Instantiate(messageLayout, contentPanel.transform);

        // Get child
        TextMeshProUGUI senderName = dialogueObj.transform.Find("SenderName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI senderMessage = dialogueObj.transform.Find("SenderMessage").GetComponent<TextMeshProUGUI>();

        // Set message info
        senderName.text = person.name;
        senderName.color = person.color;
        senderMessage.text = person.sentence;

        // Align message
        if (person.isRightAlign)
        {
            senderName.alignment = TextAlignmentOptions.MidlineRight;
            senderMessage.alignment = TextAlignmentOptions.MidlineRight;
        }

        // Remove the dialog
        dialogue.Dequeue();
    }

    public void MinimizeWindow()
    {
        gameObject.SetActive(false);
    }

    public void CloseWindow()
    {
        // Search by taskbar list of open apps
        for (int index = 0; index < TaskBar.taskbarWindows.Count; index++)
            if (TaskBar.taskbarWindows[index].program = gameObject)
            {
                // Destroy app
                Destroy(TaskBar.taskbarWindows[index].taskbarProgram);
                Destroy(TaskBar.taskbarWindows[index].program);

                // Remove app from list
                TaskBar.taskbarWindows.Remove(TaskBar.taskbarWindows[index]);
            }
    }

    // Write dialogue per string
    private IEnumerator DialogueByString()
    {
        yield return new WaitForSeconds(dialogueSpeed);
    }

    // Write dialogue per char
    private IEnumerator DialogueByChar()
    {
        yield return new WaitForSeconds(dialogueSpeed);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.position);

        transform.position = eventData.position;
    }
}
