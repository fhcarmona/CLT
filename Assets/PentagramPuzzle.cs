using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramPuzzle : MonoBehaviour
{
    private Animator animator;

    private string solutionOrder = "413524";
    private string playerOrder = "";

    public void Start()
    {
        animator = GetComponent<Animator>();
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
        }
        
        playerOrder = "";
    }

    private void CheckOrder(string clickedValue)
    {
        playerOrder += clickedValue;

        if (playerOrder.Length == 6)
        {
            CheckPuzzleSolution();
        }
    }
}
