using System;
using Foundation;
using Xamarin.Forms;
using UserLogin; 

[assembly: Dependency(typeof(UserLoginAPI))]

namespace UserLogin
{
	public class UserLoginAPI : UserLoginAbstract {

		public override bool ObtainPhoneNumberFromPref (out string phone) {
			phone = NSUserDefaults.StandardUserDefaults.StringForKey (PHONE_NUMBER);
			if (!string.IsNullOrEmpty (phone))
				return true;
			return false; 
		}

		public override bool ObtainUserPasswordFromPref (out string password) {
			password = NSUserDefaults.StandardUserDefaults.StringForKey (USER_PASSWORD);
			if (!string.IsNullOrEmpty (password))
				return true;
			return false; 
		}

		public override void SavePhoneNumberToPref (string phone) {
			NSUserDefaults.StandardUserDefaults.SetString (phone, PHONE_NUMBER);
		}

		public override void SaveTokenToPref(String token) {
			NSUserDefaults.StandardUserDefaults.SetString (token, TOKEN);
		}

		public override void SaveUserPassword (string password) {
			NSUserDefaults.StandardUserDefaults.SetString (password, USER_PASSWORD);
		}

		public override bool IsUserSuccessAuthorized() {
			return NSUserDefaults.StandardUserDefaults.BoolForKey (IS_SUCCESS_AUTHORIZED);
		}

		public override void SetUserSuccessAuthorised () {
			NSUserDefaults.StandardUserDefaults.SetBool (true, IS_SUCCESS_AUTHORIZED);
		}
			
		public override void SetTargetApplication (object target, object context) {
			throw new NotImplementedException ();
		}

		public override void StartTargerApplication (){

		}
	}
}
