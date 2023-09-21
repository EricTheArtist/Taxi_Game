using UnityEngine.Android;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour
{
  
  private void Start()
  {

    if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
    {
        Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
    }

    //remove notis already displayed:
    AndroidNotificationCenter.CancelAllDisplayedNotifications();
    //setting up a notification channel to send message

    #region Notification Channel

    var channel = new AndroidNotificationChannel()
    {
      Id = "channel_id",
      Name = "Notification Channel",
      Importance = Importance.Default,
      Description = "Reminder notifications",
    };
    AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var channel2 = new AndroidNotificationChannel()
        {
            Id = "channel_id2",
            Name = "Notification Channel 2",
            Importance = Importance.Default,
            Description = "Reminder notifications 2",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel2);

        var channel3 = new AndroidNotificationChannel()
        {
            Id = "channel_id3",
            Name = "Notification Channel 3",
            Importance = Importance.Default,
            Description = "Reminder notifications 3",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel3);
        #endregion

        //setup for the actual notification that is going to be sent
        var notification = new AndroidNotification();
        notification.Title = "Hai kawan!";
        notification.Text = "Kotak makan tengah hari harian anda sedang menunggu untuk anda! Datang cari di jalan raya.";
        notification.FireTime = System.DateTime.Now.AddHours(24); //sent within 24 hours of exiting app
        notification.SmallIcon = "my_custom_icon_id";
        notification.LargeIcon = "my_custom_large_icon_id";

        //Send Notification
        var id= AndroidNotificationCenter.SendNotification(notification, "channel_id");

        
        //setup for the actual notification that is going to be sent
        var notification2 = new AndroidNotification();
        notification2.Title = "Hai kawan!";
        notification2.Text = "Penumpang memerlukan perjalanan dan akan membayar anda!";
        notification2.FireTime = System.DateTime.Now.AddDays(3); //sent within 24 hours of exiting app
        notification2.SmallIcon = "my_custom_icon_id";
        notification2.LargeIcon = "my_custom_large_icon_id";

        //Send Notification
        var id2 = AndroidNotificationCenter.SendNotification(notification2, "channel_id2");

        
        //setup for the actual notification that is going to be sent
        var notification3 = new AndroidNotification();
        notification3.Title = "Hai kawan!";
        notification3.Text = "Anda perlu memandu teksi anda tidak lama lagi atau bateri akan kehabisan!";
        notification3.FireTime = System.DateTime.Now.AddDays(7); //sent within 24 hours of exiting app
        notification3.SmallIcon = "my_custom_icon_id";
        notification3.LargeIcon = "my_custom_large_icon_id";

        //Send Notification
        var id3 = AndroidNotificationCenter.SendNotification(notification3, "channel_id3");
        
        //if the script is run and a message is already scheduled, cancel it and reschedule another message
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
            AndroidNotificationCenter.SendNotification(notification2, "channel_id2");
            AndroidNotificationCenter.SendNotification(notification3, "channel_id3");
        }
    
        
 
        
        
   }
}
