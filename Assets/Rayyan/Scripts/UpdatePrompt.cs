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

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            this._appUpdateManager = new AppUpdateManager();
        }
    }

    private void Start()
    {
        StartCoroutine(CheckUpdates());
        //Initiate Class
        //_appUpdateManager = new AppUpdateManager();
    }

    IEnumerator CheckUpdates()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfo =
            _appUpdateManager.GetAppUpdateInfo();
        yield return appUpdateInfo;

        if(appUpdateInfo.Error == AppUpdateErrorCode.ErrorUnknown)
        {
            inAppUpdateStatus.text = "Some Errors Have Occured";
            print("Some Errors Have Occured");
        }
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

    IEnumerator StartImmediateUpdate(AppUpdateInfo appUpdateInfo_i, AppUpdateOptions appUpdateOptions_i)
    {
        var startUpdateRequest = _appUpdateManager.StartUpdate(
            appUpdateInfo_i,
            appUpdateOptions_i
        );
        yield return startUpdateRequest;
    }
}
