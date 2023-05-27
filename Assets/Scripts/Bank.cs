using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;

    [SerializeField] TextMeshProUGUI dislayBalance;

    public int CurrentBalance { get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }
    public void Deposit(int amt)
    {
        currentBalance += Mathf.Abs(amt);
        UpdateDisplay();

    }
    public void Withdraw(int amt)
    {
        currentBalance -= Mathf.Abs(amt);
        UpdateDisplay();
        if (currentBalance<0)
        {
            //Lose
            Reload();
        }
    }
    void UpdateDisplay()
    {
        dislayBalance.text = "Gold :"+currentBalance;
    }
    public void Reload()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
