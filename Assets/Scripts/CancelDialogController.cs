using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityCommunity.UnitySingleton;
using Unity.VisualScripting;
using System;

public class CancelDialogController : MonoSingleton<CancelDialogController>
{
    [SerializeField] private GameObject cancelControlUI;
    [SerializeField] private Vector2 spawnPos;
    [SerializeField] private const string dialogPath = "Prefabs/Cancel Dialog";

    protected override void OnInitializing()
    {
        spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        cancelControlUI = Resources.Load<GameObject>(dialogPath);
    }

    public UniTask<bool> ShowAsync(GameObject parentobject)
    {
        try
        {

            GameObject dialogInstance = Instantiate(cancelControlUI, spawnPos, Quaternion.identity);
            dialogInstance.SetActive(true);
            dialogInstance.transform.SetParent(parentobject.transform, false);
            Button[] buttonInstances = dialogInstance.GetComponentsInChildren<Button>();

            // Get the buttons from the dialog instance.
            Button acceptButtonInstance = buttonInstances[0];
            Button declineButtonInstance = buttonInstances[1];

            // Check if the buttons were found.

            var tcs = new UniTaskCompletionSource<bool>();

            // Add listeners to the buttons.
            acceptButtonInstance.onClick.AddListener(async () =>
            {

                tcs.TrySetResult(true);
                Destroy(dialogInstance);
                await Task.Delay(100);
            });

            declineButtonInstance.onClick.AddListener(async () =>
            {

                tcs.TrySetResult(false);
                Destroy(dialogInstance);
                await Task.Delay(100);
            });

            return tcs.Task;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return UniTask.FromResult(false);
        }


    }
}
