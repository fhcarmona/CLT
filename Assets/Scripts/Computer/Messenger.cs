using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Messenger : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private Queue dialogue;
    private float dialogueSpeed;
    private GameObject cursor;

    [SerializeField]
    private GameObject messageLayout, contentPanel;

    private void Start()
    {
        cursor = GameObject.Find("Cursor");

        StartDialogue();
    }

    // Initialize which dialogue will trigger
    public void StartDialogue()
    {
        dialogue = new Queue();

        // Dialogue
        dialogue.Enqueue(new Person("Betty", "Oi, eu sou Betty, a sua inteligencia artificial.", Color.red, false));
        dialogue.Enqueue(new Person("Betty", "Estou aqui para te auxiliar.", Color.red, false));
        dialogue.Enqueue(new Person("Betty", "Você foi escolhido para o novo programa da empresa.", Color.red, false));
        dialogue.Enqueue(new Person("Betty", "Na área de trabalho você terá tudo o que é necessário, sinta-se livre para navegar.", Color.red, false));
        dialogue.Enqueue(new Person("Betty", "E não esqueça. 'Livre da mente. Trabalhe para a gente!'", Color.red, false));
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
            if (TaskBar.taskbarWindows[index].program.name.Equals(gameObject.name))
            {
                // Destroy app
                TaskBar.taskbarWindows[index].taskbarProgram.SetActive(false);
                TaskBar.taskbarWindows[index].program.SetActive(false);

                // Remove app from list
                //TaskBar.taskbarWindows.Remove(TaskBar.taskbarWindows[index]);
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
        transform.position = eventData.position;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        cursor.transform.SetAsLastSibling();
    }
}
