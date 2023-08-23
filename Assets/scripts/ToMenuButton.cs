using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenuButton : MonoBehaviour
{
    [SerializeField] private GameObject toMenuPanel;
    [SerializeField] private GameObject collectionCanvas;

    public void ToMenu()
    {
        toMenuPanel.SetActive(!toMenuPanel.activeSelf);
        Fishing.instance.canFish = !toMenuPanel.activeSelf;
    }

    public void ToMenuConfirm()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    public void ToMenuCancel()
    {
        toMenuPanel.SetActive(false);
        Fishing.instance.canFish = !toMenuPanel.activeSelf && !collectionCanvas.activeSelf;
    }
}
