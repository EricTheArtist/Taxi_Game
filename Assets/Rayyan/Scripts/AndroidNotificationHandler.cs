using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour
{
  
  private void Start()
  {
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

    #endregion
    
    //setup for the actual notification that is going to be sent
    var notification = new AndroidNotification();
    notification.Title = "Howzit Bra!";
    notification.Text = "Your daily skaftin is waiting for you! Come find it on the road.";
    notification.FireTime = System.DateTime.Now.AddHours(24); //sent within 24 hours of exiting app
    notification.SmallIcon = "my_custom_icon_id";
    notification.LargeIcon = "my_custom_large_icon_id";

    //Send Notification
    var id= AndroidNotificationCenter.SendNotification(notification, "channel_id");
    
    //if the script is run and a message is already scheduled, cancel it and reschedule another message
    if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
    {
      AndroidNotificationCenter.CancelAllNotifications();
      AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
  }
}
