using Android.App;
using Android.OS;
using Android.Content.PM;
using Android.Content;
using Xamarin.Forms;


namespace UserLogin
{
	[Activity (Label = "UserLogin", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]			
	public class UserLoginActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity {

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);
			global::Xamarin.Forms.Forms.Init (this, bundle);

			Intent intent = new Intent (this, typeof(SMSAuthentication.Droid.MainActivity));
			LoadApplication (new UserLoginApp ());

			if (!UserLoginAbstract.IsLogin) {
				var userLogin = DependencyService.Get<UserLoginAbstract> ();
				userLogin.SetTargetApplication (intent, this);
			} else {
				StartActivity (intent);
				Finish ();
			}
		}

		protected override void OnDestroy() {
			base.OnDestroy ();
		}
	}
}

