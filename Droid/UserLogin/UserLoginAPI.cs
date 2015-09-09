using System;
using System.Collections.Generic;
using Android.App;

using Android.Content;
using Android.OS;
using Android.Preferences;
using UserLogin;
using Xamarin.Forms;


[assembly: Dependency(typeof(UserLoginAPI))]

namespace UserLogin
{
	public class UserLoginAPI : UserLoginAbstract {

		readonly ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences (Android.App.Application.Context);
		static Intent startTargetApplicationIntent = null;
		static Activity activityContext;

		public override bool ObtainPhoneNumberFromPref (out string phone){ 
			phone = sharedPreferences.GetString (PHONE_NUMBER,"");
			if (!String.IsNullOrEmpty (phone))
				return true;
			return false; 
		}

		public override bool ObtainUserPasswordFromPref (out string password) {
			password = sharedPreferences.GetString (USER_PASSWORD,"");
			if (!String.IsNullOrEmpty (password))
				return true;
			return false; 
		}

		public override void SavePhoneNumberToPref (string phone) {
			ISharedPreferencesEditor editor = sharedPreferences.Edit ();
			editor.PutString (PHONE_NUMBER,phone);
			editor.Apply ();
		}

		public override void SaveTokenToPref(String token) {
			ISharedPreferencesEditor editor = sharedPreferences.Edit ();
			editor.PutString (TOKEN, token);
			editor.Apply ();
		}

		public override void SaveUserPassword (string password) {
			ISharedPreferencesEditor editor = sharedPreferences.Edit ();
			editor.PutString (USER_PASSWORD, password);
			editor.Apply ();
		}

		public override bool IsUserSuccessAuthorized() {
			return sharedPreferences.GetBoolean (IS_SUCCESS_AUTHORIZED, false);
		}

		public override void SetUserSuccessAuthorised () {
			ISharedPreferencesEditor editor = sharedPreferences.Edit ();
			editor.PutBoolean (IS_SUCCESS_AUTHORIZED, true);
			editor.Apply ();
		}

		public override void SetTargetApplication(Object target, Object context) {
			if ((target is Intent) && (context is Activity)) {
				startTargetApplicationIntent = (Intent)target;
				activityContext = (Activity)context;
			}
		}
			
		public override void StartTargerApplication (){
			if (startTargetApplicationIntent != null && activityContext != null) {
				activityContext.StartActivity (startTargetApplicationIntent);
				activityContext.Finish ();
			}
		}
	}
}