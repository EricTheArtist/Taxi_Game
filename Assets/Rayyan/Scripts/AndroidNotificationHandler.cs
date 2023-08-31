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
        notification.Title = "Howzit Bra!";
        notification.Text = "Your daily skaftin is waiting for you! Come find it on the road.";
        notification.FireTime = System.DateTime.Now.AddMinutes(2); //sent within 24 hours of exiting app
        notification.SmallIcon = "my_custom_icon_id";
        notification.LargeIcon = "my_custom_large_icon_id";

        //Send Notification
        var id= AndroidNotificationCenter.SendNotification(notification, "channel_id");

        
        //setup for the actual notification that is going to be sent
        var notification2 = new AndroidNotification();
        notification2.Title = "Hey man!";
        notification2.Text = "Passengers are needing a ride and will pay you!";
        notification2.FireTime = System.DateTime.Now.AddMinutes(4); //sent within 24 hours of exiting app
        notification2.SmallIcon = "my_custom_icon_id";
        notification2.LargeIcon = "my_custom_large_icon_id";

        //Send Notification
        var id2 = AndroidNotificationCenter.SendNotification(notification2, "channel_id2");

        
        //setup for the actual notification that is going to be sent
        var notification3 = new AndroidNotification();
        notification3.Title = "What's up!";
        notification3.Text = "You need to drive your taxi soon or the battery is going to run flat!";
        notification3.FireTime = System.DateTime.Now.AddMinutes(6); //sent within 24 hours of exiting app
        notification3.SmallIcon = "my_custom_icon_id";
        notification3.LargeIcon = "my_custom_large_icon_id";

        //Send Notification
        var id3 = AndroidNotificationCenter.SendNotification(notification3, "channel_id3");
        
        //if the script is run and a message is already scheduled, cancel it and reschedule another message
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelNotification(id);
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
        
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id2) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelNotification(id2);
            AndroidNotificationCenter.SendNotification(notification2, "channel_id2");
        }
        
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id3) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelNotification(id3);
            AndroidNotificationCenter.SendNotification(notification3, "channel_id3");
        }
        
    }
}
