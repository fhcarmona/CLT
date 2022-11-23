using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Messenger : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public static Queue[] dialogue = new Queue[3];
    private float dialogueSpeed;
    private AudioSource notificationSound;
    private GameObject cursor;
    private static GameObject continueButton;

    [SerializeField]
    private GameObject messageLayout, bettyChat, rebeccaChat, ludvickChat, bettyContent, rebeccaContent, ludvickContent;
    [SerializeField]
    private Image senderImage;

    public enum Contact
    {
        Betty,
        Rebecca,
        Ludvick
    }

    private void Start()
    {
        continueButton = GameObject.Find("Continue");

        cursor = GameObject.Find("Cursor");
        notificationSound = GetComponent<AudioSource>();

        dialogue[(int)Contact.Betty] = new Queue();
        dialogue[(int)Contact.Rebecca] = new Queue();
        dialogue[(int)Contact.Ludvick] = new Queue();

        InitialDialogue();
    }

    // Initialize which dialogue will trigger
    public void InitialDialogue()
    {
        // Dialogue
        dialogue[(int) Contact.Betty].Enqueue(new Person("Betty", "Oi, eu sou Betty, a sua inteligencia artificial.", Color.red, false));
        dialogue[(int) Contact.Betty].Enqueue(new Person("Betty", "Estou aqui para te auxiliar.", Color.red, false));
        dialogue[(int) Contact.Betty].Enqueue(new Person("Betty", "Você foi escolhido para o novo programa da empresa.", Color.red, false));
        dialogue[(int) Contact.Betty].Enqueue(new Person("Betty", "Na área de trabalho você terá tudo o que é necessário, sinta-se livre para navegar.", Color.red, false));
        dialogue[(int) Contact.Betty].Enqueue(new Person("Betty", "E não esqueça. 'Livre da mente. Trabalhe para a gente!'", Color.red, false));

        // Show only one chat per time
        ChangeChat("BettyChat");

        // When open the first time
        NextSentence();
    }

    public static void AddDialogue(Queue newDialogue, Contact person)
    {
        dialogue[(int) person] = newDialogue;

        continueButton.GetComponent<Button>().interactable = false;
    }

    public static void AddDialogue(Contact person, string text, Color color, bool isRight)
    {
        dialogue[(int) person].Enqueue(new Person(person.ToString(), text, color, isRight));

        continueButton.GetComponent<Button>().interactable = true;
    }

    // Show the next dialogue sentence
    public void NextSentence()
    {
        int dialogueIndex = -1;

        if (bettyChat.activeSelf)
            dialogueIndex = (int)Contact.Betty;
        else if (rebeccaChat.activeSelf)
            dialogueIndex = (int)Contact.Rebecca;
        else if(ludvickChat.activeSelf)
            dialogueIndex = (int)Contact.Ludvick;

        // Get the FIFO dialogue line
        Person person = (Person) dialogue[dialogueIndex].Peek();

        // Clone the message prefab
        GameObject dialogueObj = Instantiate(messageLayout, bettyContent.transform); // Default
        
        if (rebeccaChat.activeSelf)
            dialogueObj = Instantiate(messageLayout, rebeccaContent.transform);
        else if (ludvickChat.activeSelf)
            dialogueObj = Instantiate(messageLayout, ludvickContent.transform);

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

        // Set the volume
        notificationSound.volume = Helper.GetPrefByKeyName("SFXVolume") / 100;
        notificationSound.Play();

        // Remove the dialog
        dialogue[dialogueIndex].Dequeue();

        // Disable button if there's no dialogue remaining
        if (dialogue[dialogueIndex].Count == 0)
            continueButton.GetComponent<Button>().interactable = false;
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
                // TaskBar.taskbarWindows.Remove(TaskBar.taskbarWindows[index]);
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

    public void ChangeChat(string chatName)
    {
        // Invisible chat
        bettyChat.SetActive(false);
        rebeccaChat.SetActive(false);
        ludvickChat.SetActive(false);

        switch (chatName)
        {
            case "BettyChat":
                bettyChat.SetActive(true);
                senderImage.sprite = Resources.Load<Sprite>("Characters/bot-profile");

                // Disable button if there's no dialogue remaining
                continueButton.GetComponent<Button>().interactable = dialogue[(int)Contact.Betty].Count != 0;
                break;
            case "RebeccaChat":
                rebeccaChat.SetActive(true);
                senderImage.sprite = Resources.Load<Sprite>("Characters/collegue");

                // Disable button if there's no dialogue remaining
                continueButton.GetComponent<Button>().interactable = dialogue[(int)Contact.Rebecca].Count != 0;

                break;
            case "LudvickChat":
                ludvickChat.SetActive(true);
                senderImage.sprite = Resources.Load<Sprite>("Characters/boss");

                // Disable button if there's no dialogue remaining
                continueButton.GetComponent<Button>().interactable = dialogue[(int)Contact.Ludvick].Count != 0;
                break;
        }
    }

    public void LudvickDialogue(int index)
    {
        Contact person = Contact.Ludvick;
        Color color = Color.black;

        switch (index)
        {
            case 0:
                AddDialogue(person, "", color, false);
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    public static void RebeccaDialogue(int index)
    {
        Contact person = Contact.Rebecca;
        Color color = Color.magenta;

        switch (index)
        {
            // After duckhunt level 01 is completed
            case 0:
                AddDialogue(person, "Vi que completou a primeira fase", color, false);
                AddDialogue(person, "Continue assim... Eles não devem ditar suas escolhas", color, false);
                AddDialogue(person, "Por sorte a comunicação não foi bloqueada", color, false);
                AddDialogue(person, "Os níveis são a solução, não esqueça disso", color, false);
                AddDialogue(person, "Á énqsétá ê úná gáçiáeá", color, false);
                break;
            // After duckhunt level 05 is completed
            case 1:
                AddDialogue(person, "Eles xhpxbô ébvvbv ômplb comunicação.", color, false);
                AddDialogue(person, "Você está indo bem, continue ôbxbpgq xhôuq", color, false);
                AddDialogue(person, "Vamos jbàhv êqô úyh hwwh uvqkvbôb jbõlh", color, false);
                break;
            // After duckhunt level 15 is completed
            case 2:
                AddDialogue(person, "Eles estão distraidos agora", color, false);
                AddDialogue(person, "Esse projeto... Essa õxãbõci", color, false);
                AddDialogue(person, "Eu acredito que éznp úí çõysi óõcnzyqtióz", color, false);
                AddDialogue(person, "Eles ykz são como as propagandas indicam", color, false);
                AddDialogue(person, "Eu acredito àdõ éznp úí çõysi óõcnzyqtióz", color, false);
                break;
        }
    }

    public int CountRemainingMessages()
    {
        int count = 0;

        foreach (Queue messageList in dialogue)
            count += messageList.Count;

        return count;
    }
}
