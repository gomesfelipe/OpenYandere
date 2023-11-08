using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Managers.Traits;
using UnityEngine.UI;
using TMPro;
using OpenYandere.Managers;
using OpenYandere.Characters;
internal class GossipUI : Singleton<GossipUI>
{
    //[RequireComponent(CanvasGroup)]
    [SerializeField] protected Button btnLeft, btnRight;
    private CanvasGroup _canvasGroup;
    public TextMeshProUGUI StudentNameText, StudentSecret;

    //insert a event listener here
    private StudentDataManager SDM;
    private StudentGossipDataBase SGD;
    private bool isGossipOpen = false;
    private Character _currentCharacter;
    private int _currentIndex;
    void Start()
    {
        this.TryGetComponent<CanvasGroup>(out _canvasGroup);
        SGD = StudentDataManager.Instance.SGD;
        _currentCharacter = SGD.getinfo(0);

    }

    
    void Update()
    {
        if (isGossipOpen)
        {
            NavigateItems();
            displayIcon();
            displayMugShot(_currentCharacter);
            displaySecerts(_currentCharacter);

        }


    }
    private void NavigateItems()
    {
        int itemCount = SGD.getItems().Count;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentIndex = (_currentIndex + 1) % itemCount;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = itemCount - 1;
        }
        
    }
    private void displayIcon()
    {

    }

    private void displayMugShot(Character c)
    {
        StudentNameText.text = SGD.getinfo(c).club + _currentIndex ;
    }
    private void displaySecerts(Character c)
    {
        StudentSecret.text = SGD.getinfo(c).secert;
    }

    public void ActivateUI()
    {
       // GameManager.Instance.Pause();
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        isGossipOpen = true;
    }

    public void DeactivateUI()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        //  GameManager.Instance.Resume();
        isGossipOpen = false;
       // CameraManager.Instance.unlockCamera();
    }
}
