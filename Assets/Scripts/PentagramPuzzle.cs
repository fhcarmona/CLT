using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PentagramPuzzle : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] screensText;
    [SerializeField]
    private AudioClip[] morseClips;
    [SerializeField]
    private AudioSource[] audios;

    private Animator animator;
    
    private string solutionOrder = "";
    private string playerOrder = "";
    private AudioSource aSPentagram;

    public void Start()
    {
        animator = GetComponent<Animator>();
        aSPentagram = GetComponentInChildren<AudioSource>();

        ComputerSetup();
    }

    public void ComputerSetup()
    {
        List<string> possibilities = new List<string>(new string[] { "1", "3", "5", "2", "4" });

        int initial = Random.Range(0, possibilities.Count);
        int sorting = Random.Range(-1, 0);
        int sequenceIndex = 0;

        for (int index = initial; index < possibilities.Count; ++index)
        {
            // Incr
            if (sorting == 0)
            {
                solutionOrder += possibilities[index];
                audios[index].clip = morseClips[index];
            }
            // Decr
            else
            {
                solutionOrder += possibilities[possibilities.Count - index - 1];
                audios[index].clip = morseClips[possibilities.Count - index - 1];
            }

            if (screensText[index].text == null)
                screensText[index].text = "";

            // Number write in the cpu screen
            if(solutionOrder.Length != 6)
                screensText[index].text += (++sequenceIndex).ToString();
            else
                screensText[index].text += " - " + (++sequenceIndex).ToString();

            // When reach the final index
            if (index == 4)
                index = -1;

            if(solutionOrder.Length >= 6)
                break;
        }
    }

    public void SquareCheck(GameObject square)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (square.name)
            {
                case "Square001":
                    CheckOrder("1");
                    break;
                case "Square002":
                    CheckOrder("2");
                    break;
                case "Square003":
                    CheckOrder("3");
                    break;
                case "Square004":
                    CheckOrder("4");
                    break;
                case "Square005":
                    CheckOrder("5");
                    break;
            }

            DebugMode.SetAdditionalInfo($"Sequencia Atual[{playerOrder}], Solução[{solutionOrder}]");
        }
    }

    private void CheckPuzzleSolution()
    {
        if (solutionOrder.Equals(playerOrder))
        {
            animator.Play("PuzzleComplete");
            aSPentagram.PlayOneShot(morseClips[6]);
        }
        else
            aSPentagram.PlayOneShot(morseClips[5]);

        playerOrder = "";
    }

    private void CheckOrder(string clickedValue)
    {
        playerOrder += clickedValue;

        aSPentagram.Play();

        if (playerOrder.Length == 6)
        {
            CheckPuzzleSolution();
        }
    }
}
