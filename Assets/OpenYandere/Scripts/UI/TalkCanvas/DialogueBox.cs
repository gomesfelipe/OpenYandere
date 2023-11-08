using System;
using TMPro;
using UnityEngine;
using OpenYandere.Managers;
using OpenYandere.Characters;
using System.Collections.Generic;
using OpenYandere.Characters.NPC;
using OpenYandere.Characters.Player;
using DG.Tweening;
using TMPro;

namespace OpenYandere.UI.TalkCanvas
{
    public class DialogueBox : MonoBehaviour
    {
        [Header("References:")]
        private Player _player;
        private NPC _interactingWithNPC;
        [SerializeField] private Animator _animator, _choicesAnimator;
        [SerializeField] private TextMeshProUGUI _characterName, _dialogueText;
        [SerializeField] private RectTransform _DialogChoices;
        [SerializeField] private RectTransform _FavorChoices;
        [SerializeField] private GameObject btnChoiceTemplate;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        private Dictionary<NPC,ActivityBase.EndActivity> taskDelegate;

        private Vector3 _originalPosition;

        protected struct DialogueEntry
        {
            public string CharacterName;
            public string Text;
        }

        private bool _isVisible, _areChoicesVisible;

        private readonly Queue<DialogueEntry> _dialogueEntries = new();

        private void Awake()
	    {
            taskDelegate = new Dictionary<NPC, ActivityBase.EndActivity>();
		    _player = GameManager.Instance.PlayerManager.Player.GetComponent<Player>();
            _rectTransform = GetComponent<RectTransform>();
            if (!TryGetComponent<CanvasGroup>(out _canvasGroup))
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
           
            _canvasGroup.alpha = 0;
            _originalPosition = _rectTransform.anchoredPosition;
        }

	    private void Update()
	    {
		    if (!_isVisible) return;
			
		    if (Input.GetMouseButtonDown(0) && !_areChoicesVisible)
		    {
			    if (_dialogueEntries.Count > 0)
			    {
				    DialogueEntry dialogueEntry = _dialogueEntries.Dequeue();
					_characterName.SetText(dialogueEntry.CharacterName);
					_dialogueText.SetText(dialogueEntry.Text);// TODO: Animate the text.
			    }
			    else
			    {
                    HideBox();
			    }
		    }
	    }

	    public void Initialise(NPC interactingWithNPC)
	    {
		    _interactingWithNPC = interactingWithNPC;
	    }

        public void ShowBox()
        {
            _canvasGroup.alpha = 0;
            if (_rectTransform != null)
            {
                _rectTransform.anchoredPosition = new Vector2(0, -Screen.height);
            }
            _canvasGroup.DOFade(1, 0.5f);
            _rectTransform.DOAnchorPos(_originalPosition, 0.5f).SetEase(Ease.OutQuad);
            _isVisible = true;
            _animator.SetBool("Visible", _isVisible);
            GameManager.Instance.PlayerManager.PlayerMovement.BlockMovement();
        }
        private void switchToFavorBox()
        {
            ActivityBase.EndActivity task;

            Debug.Log(_interactingWithNPC.characterName);
                if (!taskDelegate.TryGetValue(_interactingWithNPC, out task))
                {
                     TrackingTargetActivity t = ScriptableObject.CreateInstance<TrackingTargetActivity>();
                    //TrackingTargetActivity t = new TrackingTargetActivity();
                   // t.Tracktarget = _interactingWithNPC.player.transform;
                    _interactingWithNPC.addRequest(t);
                    //t.Tracktarget = _interactingWithNPC.player.gameObject.transform;
                    t.TrackInterval = 60;
                    t.Distance = 2;
                    taskDelegate.Add(_interactingWithNPC, t.getEndActivityDelegate());
                } else
                {
                    task.DynamicInvoke(_interactingWithNPC);
                }
          
           // _FavorChoices.DOMove(_DialogChoices.position, 0.5f).SetEase(Ease.InBounce);
           // _DialogChoices.DOMove(Vector3.one*100, 0.5f).SetEase(Ease.InBounce);
        }
        public void HideBox()
        {
            HideChoices();
            transform.DOKill();
            transform.DOMove(_originalPosition + new Vector3(0, -50f, 0), 0.5f).SetEase(Ease.InQuad).OnComplete(() =>
            {

                _isVisible = false;
            });

            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.DOFade(0, 0.5f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                GameManager.Instance.PlayerManager.PlayerMovement.UnblockMovement();
            });
        }
        public void ShowChoices()
        {
	        _choicesAnimator.SetBool("Visible", true);
	        _areChoicesVisible = true;
        }

	    public void HideChoices()
	    {
		    _choicesAnimator.SetBool("Visible", false);
		    _areChoicesVisible = false;
	    }
	    
	    public void Queue(string characterName, string dialogueText)
	    {
		    _dialogueEntries.Enqueue(new DialogueEntry
		    {
			    CharacterName = characterName,
			    Text = dialogueText
		    });
	    }

	    public void SetText(string characterName, string dialogueText)
	    {
		    _characterName.SetText(characterName);
		    _dialogueText.SetText(dialogueText);
	    }
        
        public void OnComplimentButtonClicked()
        {
	        HideChoices();
	        
	        SetText(_player.characterName, "I just wanted to tell you that you look lovely today!");

	        if (_player.Reputation >= 50)
	        {
		        Queue(_interactingWithNPC.characterName, "Wow! That means a lot coming from you! Thank you so much!");
	        }
	        else if(_player.Reputation < 0)
	        {
		        Queue(_interactingWithNPC.characterName, "Umm...thanks, I guess...?...");
	        }
	        else
	        {
                Queue(_interactingWithNPC.characterName, "Really? That's so nice of you to say!");
	        }
            _interactingWithNPC.trustLevel += 1;
	        _player.Reputation += 1;
            CameraManager.Instance.unlockCamera();
        }
		
        public void OnGossipButtonClicked()
        {
            // TODO
            GossipUI.Instance.ActivateUI();
        }
		
        public void OnPerformTaskButtonClicked()
        {
            // TODO\
            switchToFavorBox();
            //CameraManager.Instance.unlockCamera();
        }
		
        public void OnAskForFavorButtonClicked()
        {
            // TODO
            //CameraManager.Instance.unlockCamera();
           
        }
		
        public void OnExitButtonClicked()
        {
            HideBox();
            CursorManager.Instance.lockCursor();
            CameraManager.Instance.unlockCamera();
	        // TODO
        }

        public GameObject btnFactory(string msg)
        {
          GameObject label= Instantiate(btnChoiceTemplate);


            TextMeshPro text = label.GetComponentInChildren<TextMeshPro>();
            text.text = msg;
            return label;
        }
    }
}