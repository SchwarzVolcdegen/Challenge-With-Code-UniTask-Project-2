using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
public class MainController : MonoBehaviour
{
    public Button cancelButton;
    public GameObject curGameObj;
    [SerializeField] private UnityAction buttonListener;

    void Start()
    {
        buttonListener = new UnityAction(ButtonClicked);
        cancelButton.onClick.AddListener(buttonListener);
    }

    void OnDisable()
    {
        cancelButton.onClick.RemoveListener(buttonListener);
    }

    async void ButtonClicked()
    {
        var result = await CancelDialogController.Instance.ShowAsync(curGameObj);
        //Debug.Log(result);
    }
    }

