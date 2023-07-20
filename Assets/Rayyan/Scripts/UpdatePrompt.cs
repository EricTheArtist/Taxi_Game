using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//google libraries
using  Google.Play.AppUpdate;
using  Google.Play.Common;
using  Google.Play.Core;

public class UpdatePrompt : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inAppUpdateStatus;

    private AppUpdateManager _appUpdateManager;

    private void Start()
    {
        StartCoroutine(CheckUpdates());
    }

    IEnumerator CheckUpdates()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfo =
            _appUpdateManager.GetAppUpdateInfo();
        yield return appUpdateInfo;

        if (appUpdateInfo.IsSuccessful)
        {
            var appUpdateResult = appUpdateInfo.GetResult();
            
            if(appUpdateResult.UpdateAvailability== UpdateAvailability.UpdateAvailable)
            {
                inAppUpdateStatus.text = UpdateAvailability.UpdateAvailable.ToString();
            }
            else
            {
                inAppUpdateStatus.text = "We Still Working On The Next Update";
            }

            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
            StartCoroutine(StartImmediateUpdate(appUpdateResult, appUpdateOptions));
        }
    }

    IEnumerator StartImmediateUpdate(AppUpdateInfo _appUpdateInfo, AppUpdateOptions appUpdateOptions)
    {
        var startUpdareRequest = _appUpdateManager.StartUpdate(
            _appUpdateInfo,
            appUpdateOptions
        );
        yield return startUpdareRequest;
    }
}
