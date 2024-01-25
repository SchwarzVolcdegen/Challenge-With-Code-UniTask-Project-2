using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityCommunity.UnitySingleton;

public class CancelDialogController : MonoSingleton<CancelDialogController>
{
    public GameObject cancelControlUI;
    [SerializeField] private Vector2 spawnPos;

    void OnInitialized()
    {
        spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
    }

    public UniTask<bool> ShowAsync(GameObject parentobject)
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


}