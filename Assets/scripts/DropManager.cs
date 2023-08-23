using UnityEngine;
using UnityEngine.UI;


public class DropManager : MonoBehaviour
{
    [SerializeField] private Item[] fishList;
    [SerializeField] private Text dropManagerText;
    [SerializeField] private Image dropManagerImage;

    private float timeBeforeExposure = 1.5f;
    private Item fish;

    public void OnSuccess()
    {
        int dice = Random.Range(1, fishList.Length) - 1;

        fish = fishList[dice];
        Collection.instance.AddItem(fish);
        OnSuccessMessage();
    }
    public void OnFailure(string reason)
    {
        if(reason == "TooEarly")
        {
            dropManagerText.gameObject.SetActive(true);
            dropManagerText.text = "Слишком рано";
            Invoke("DisableMessage", timeBeforeExposure);
        }
        if(reason == "TooLate")
        {
            dropManagerText.gameObject.SetActive(true);
            dropManagerText.text = "Рыба сорвалась";
            Invoke("DisableMessage", timeBeforeExposure);
        }
    }
    private void OnSuccessMessage()
    {
        dropManagerText.gameObject.SetActive(true);
        dropManagerImage.gameObject.SetActive(true);
        dropManagerImage.sprite = fish.icon;
        dropManagerText.text = "Вы поймали: " + fish.name;
        OnEven(fish.amount);
        Invoke("DisableMessage", timeBeforeExposure);
    }
    private void OnEven(int amount) 
    {
        if(amount == 1)
        {
            dropManagerText.text += ". Это Ваш первый раз!";
        }
        else if(amount <= 10 && amount % 5 == 0)
        {
            dropManagerText.text += $". Это Ваш {amount}ый раз!";
        }
        else if (amount % 10 == 0)
        {
            dropManagerText.text += $". Это Ваш {amount}ый раз!";
        }
    }
    private void DisableMessage()
    {
        dropManagerImage.gameObject.SetActive(false);
        dropManagerText.gameObject.SetActive(false);
    }
}
