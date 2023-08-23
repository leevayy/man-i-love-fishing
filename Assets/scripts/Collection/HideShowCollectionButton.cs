using UnityEngine;

public class HideShowCollectionButton : MonoBehaviour
{
    [SerializeField] private GameObject collectionCanvas;
    [SerializeField] private GameObject toMenuPanel;

    private void Start()
    {
        collectionCanvas.SetActive(false);
    }

    public void HideShowInventoy()
    {       
        collectionCanvas.SetActive(!collectionCanvas.activeSelf);
        Fishing.instance.canFish = !toMenuPanel.activeSelf && !collectionCanvas.activeSelf; ;
    }
}
