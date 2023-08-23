using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] unlockButtons;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Text messageText;
    [SerializeField] private Text balanceText;

    private int balance = 0;
    private int currentLevel = 0;

    private void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("levelProgress");
        UpdateBalance();
        DrawLevelButtons();
    }
        
    //
    const string LevelBase = "SummerRiverLevel";
    const string Level0 = "AutumnRiverLevel";
    const string Level1 = "WinterRiverLevel";
    const string Level2 = "SpringRiverLevel";
    //

    public void UnlockLevel(int level)
    {        
        if (level >= currentLevel + 1)
        {
            messageText.text = "Unlock previous levels first";
            return;
        }
        if (balance < 100)
        {
            messageText.text = "Not enough money";
            return;
        }

        
        currentLevel += 1;
        levelButtons[level].interactable = true;
        unlockButtons[level].SetActive(false);
        PlayerPrefs.SetInt("levelProgress", currentLevel);
        PlayerPrefs.SetInt("balance", balance - 100);
        UpdateBalance();
    }

    public void AddMoney()
    {
        PlayerPrefs.SetInt("balance", 999);
        UpdateBalance();
    }
    public void Restart()
    {
        PlayerPrefs.SetInt("balance", 0);
        PlayerPrefs.SetInt("levelProgress", 0);
        UpdateBalance();
        DrawLevelButtons();
        currentLevel = 0;
    }

    public void LoadSpringRiverLevel()
    {
        SceneManager.LoadScene(Level2);
    }
    public void LoadWinterRiverLevel()
    {
        SceneManager.LoadScene(Level1);
    }
    public void LoadAutumnRiverLevel()
    {
        SceneManager.LoadScene(Level0);
    }    
    public void LoadSummerRiverLevel()
    {
        SceneManager.LoadScene(LevelBase);
    }

    private void UpdateBalance()
    {
        balance = PlayerPrefs.GetInt("balance");
        balanceText.text = Convert.ToString(balance);
    }
    private void DrawLevelButtons()
    {
        for (int i = 0; i < currentLevel; i++)
        {
            levelButtons[i].interactable = !(levelButtons[i].interactable);
            unlockButtons[i].SetActive(!unlockButtons[i].activeSelf);
        }
    }
}
