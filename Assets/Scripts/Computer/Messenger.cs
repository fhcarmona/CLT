using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Messenger : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public static Queue<Person>[] dialogueList = new Queue<Person>[3];
    private float dialogueSpeed;
    private AudioSource notificationSound;
    private GameObject cursor;
    private static GameObject continueButton;

    [SerializeField]
    private GameObject messageLayout, bettyChat, rebeccaChat, ludvickChat;
    [SerializeField]
    private Image senderImage;

    private static GameObject bettyContent, rebeccaContent, ludvickContent;

    public enum Contact
    {
        Betty,
        Rebecca,
        Ludvick
    }

    private void Start()
    {
        bettyContent = GameObject.Find("BettyContent");
        rebeccaContent = GameObject.Find("RebeccaContent");
        ludvickContent = GameObject.Find("LudvickContent");
        continueButton = GameObject.Find("Continue");

        cursor = GameObject.Find("Cursor");
        notificationSound = GetComponent<AudioSource>();

        dialogueList[(int)Contact.Betty] = new Queue<Person>();
        dialogueList[(int)Contact.Rebecca] = new Queue<Person>();
        dialogueList[(int)Contact.Ludvick] = new Queue<Person>();

        InitialDialogue();
    }

    // Initialize which dialogue will trigger
    public void InitialDialogue()
    {
        // Dialogue
        dialogueList[(int) Contact.Betty].Enqueue(new Person("Betty", "Oi, eu sou Betty, a sua inteligencia artificial.", Color.red, false));
        dialogueList[(int) Contact.Betty].Enqueue(new Person("Betty", "Estou aqui para te auxiliar.", Color.red, false));
        dialogueList[(int) Contact.Betty].Enqueue(new Person("Betty", "Voc? foi escolhido para o novo programa da empresa.", Color.red, false));
        dialogueList[(int) Contact.Betty].Enqueue(new Person("Betty", "Na ?rea de trabalho voc? ter? tudo o que ? necess?rio, sinta-se livre para navegar.", Color.red, false));
        dialogueList[(int) Contact.Betty].Enqueue(new Person("Betty", "E n?o esque?a. 'Livre da mente. Trabalhe para a gente!'", Color.red, false));

        // Show only one chat per time
        ChangeChat("BettyChat");

        // When open the first time
        NextSentence();
    }

    public static void AddDialogue(Queue<Person> newDialogue, Contact person)
    {
        dialogueList[(int) person] = newDialogue;

        continueButton.GetComponent<Button>().interactable = false;
    }

    public static void AddDialogue(Contact person, string text, Color color, bool isRight)
    {
        if (!DialogueExist(person, text))
        {
            dialogueList[(int)person].Enqueue(new Person(person.ToString(), text, color, isRight));

            continueButton.GetComponent<Button>().interactable = true;
        }
    }

    private static bool DialogueExist(Contact person, string checkDialogue)
    {
        TextMeshProUGUI[] dialogueTextList;
        int contactIndex = (int)person;

        if (Contact.Betty == person)
        {
            dialogueTextList = bettyContent.GetComponentsInChildren<TextMeshProUGUI>();

            // Check if exist dialog in queue
            foreach (Person bettyDialogue in dialogueList[contactIndex])
            {
                if (bettyDialogue.sentence.Equals(checkDialogue))
                {
                    Debug.Log($"Dialogo duplicado bloqueado! RebeccaDialogue[{bettyDialogue.sentence}], Dialogue[{checkDialogue}]");

                    return true;
                }
            }

            // Check if exist the dialog in content
            foreach (TextMeshProUGUI bettyDialogue in dialogueTextList)
            {
                if (bettyDialogue.text.Equals(checkDialogue))
                {
                    Debug.Log($"Dialogo duplicado bloqueado! BettyDialogue[{bettyDialogue.text}], Dialogue[{checkDialogue}]");

                    return true;
                }
            }
        }
        else if (Contact.Rebecca == person)
        {
            dialogueTextList = rebeccaContent.GetComponentsInChildren<TextMeshProUGUI>();
            
            // Check if exist dialog in queue
            foreach (Person rebeccaDialogue in dialogueList[contactIndex])
            {
                if (rebeccaDialogue.sentence.Equals(checkDialogue))
                {
                    Debug.Log($"Dialogo duplicado bloqueado! RebeccaDialogue[{rebeccaDialogue.sentence}], Dialogue[{checkDialogue}]");

                    return true;
                }
            }

            // Check if exist the dialog in content
            foreach (TextMeshProUGUI rebeccaDialogue in dialogueTextList)
            {
                if (rebeccaDialogue.text.Equals(checkDialogue))
                {
                    Debug.Log($"Dialogo duplicado bloqueado! RebeccaDialogue[{rebeccaDialogue.text}], Dialogue[{checkDialogue}]");

                    return true;
                }
            }
        }
        else if (Contact.Ludvick == person)
        {
            dialogueTextList = ludvickContent.GetComponentsInChildren<TextMeshProUGUI>();

            // Check if exist dialog in queue
            foreach (Person ludvickDialogue in dialogueList[contactIndex])
            {
                if (ludvickDialogue.sentence.Equals(checkDialogue))
                {
                    Debug.Log($"Dialogo duplicado bloqueado! RebeccaDialogue[{ludvickDialogue.sentence}], Dialogue[{checkDialogue}]");

                    return true;
                }
            }

            // Check if exist the dialog in content
            foreach (TextMeshProUGUI ludvickDialogue in dialogueTextList)
            {
                if (ludvickDialogue.text.Equals(checkDialogue))
                {
                    Debug.Log($"Dialogo duplicado bloqueado! LudvickDialogue[{ludvickDialogue.text}], Dialogue[{checkDialogue}]");

                    return true;
                }
            }
        }

        return false;
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
        Person person = (Person) dialogueList[dialogueIndex].Peek();

        // Clone the message prefab
        GameObject dialogueObj = null;

        if (bettyChat.activeSelf)
            dialogueObj = Instantiate(messageLayout, bettyContent.transform);
        else if (rebeccaChat.activeSelf)
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
        dialogueList[dialogueIndex].Dequeue();

        // Disable button if there's no dialogue remaining
        if (dialogueList[dialogueIndex].Count == 0)
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
                continueButton.GetComponent<Button>().interactable = dialogueList[(int)Contact.Betty].Count != 0;
                break;
            case "RebeccaChat":
                rebeccaChat.SetActive(true);
                senderImage.sprite = Resources.Load<Sprite>("Characters/collegue");

                // Disable button if there's no dialogue remaining
                continueButton.GetComponent<Button>().interactable = dialogueList[(int)Contact.Rebecca].Count != 0;

                break;
            case "LudvickChat":
                ludvickChat.SetActive(true);
                senderImage.sprite = Resources.Load<Sprite>("Characters/boss");

                // Disable button if there's no dialogue remaining
                continueButton.GetComponent<Button>().interactable = dialogueList[(int)Contact.Ludvick].Count != 0;
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
            case 1:
                AddDialogue(person, "Vi que completou a primeira fase", color, false);
                AddDialogue(person, "Continue assim... Eles n?o devem ditar suas escolhas", color, false);
                AddDialogue(person, "Por sorte a comunica??o n?o foi bloqueada", color, false);
                AddDialogue(person, "Os n?veis s?o a solu??o, n?o esque?a disso", color, false);
                AddDialogue(person, "? ?nqs?t? ? ?n? g??i?e?", color, false);
                break;
            // After duckhunt level 05 is completed
            case 5:
                AddDialogue(person, "Eles xhpxb? ?bvvbv ?mplb comunica??o.", color, false);
                AddDialogue(person, "Voc? est? indo bem, continue ?bxbpgq xh?uq", color, false);
                AddDialogue(person, "Vamos jb?hv ?q? ?yh hwwh uvqkvb?b jb?lh", color, false);
                break;
            // After duckhunt level 15 is completed
            case 15:
                AddDialogue(person, "Eles est?o distraidos agora", color, false);
                AddDialogue(person, "Esse projeto... Essa ?x?b?ci", color, false);
                AddDialogue(person, "Eu acredito que ?znp ?? ??ysi ??cnzyqti?z", color, false);
                AddDialogue(person, "Eles ykz s?o como as propagandas indicam", color, false);
                AddDialogue(person, "Eu acredito ?d? ?znp ?? ??ysi ??cnzyqti?z", color, false);
                break;
        }
    }

    public int CountRemainingMessages()
    {
        int count = 0;

        foreach (Queue<Person> messageList in dialogueList)
            count += messageList.Count;

        return count;
    }
}
