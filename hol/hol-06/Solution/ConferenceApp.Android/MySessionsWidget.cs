using System.Linq;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using ConferenceApp.Contracts;
using Shiny;

namespace ConferenceApp.Droid.Widget
{
    [BroadcastReceiver(Label = "My Sessions - Conference App")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/appwidgetprovider")]
    public class MySessionsWidget : AppWidgetProvider
    {
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            var me = new ComponentName(context, Java.Lang.Class.FromType(typeof(MySessionsWidget)).Name);
            appWidgetManager.UpdateAppWidget(me, BuildRemoteViews(context, appWidgetIds));
        }

        public RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
        {
            var store = ShinyHost.Resolve<IConferenceStore>();

            var sessions = store.GetSessions().GetAwaiter().GetResult();
            var myNextSession = sessions.FirstOrDefault(s => s.IsFavorite);

            var views = new RemoteViews(context.PackageName, Resource.Layout.Widget);
            RegisterClicks(context, views, appWidgetIds);

            if (myNextSession != null)
            {
                views.SetTextViewText(Resource.Id.titleView, myNextSession.Title);
                views.SetTextViewText(Resource.Id.timeAndLocationView, $"{myNextSession.StartsAt.ToLocalTime():t} - {myNextSession.EndsAt.ToLocalTime():t} in {myNextSession.Room}");
            }
            else
            {
                views.SetTextViewText(Resource.Id.titleView, "No session available");
                views.SetTextViewText(Resource.Id.timeAndLocationView, "You have not favorited any sessions");
            }

            return views;
        }

        private void RegisterClicks(Context context, RemoteViews widgetView, int[] appWidgetIds)
        {
            var intent = new Intent(context, typeof(MySessionsWidget));
            intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            intent.PutExtra(AppWidgetManager.ExtraAppwidgetIds, appWidgetIds);

            // Register click event for the Background view of the widget
            var piBackground = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
            widgetView.SetOnClickPendingIntent(Resource.Id.widgetBackground, piBackground);
        }

    }
}
