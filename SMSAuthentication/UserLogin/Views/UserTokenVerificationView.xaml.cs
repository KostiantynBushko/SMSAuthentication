using System;
using UserLogin;
using Xamarin.Forms;
using SMSAuthentication;

namespace Views
{
	public partial class UserTokenVerificationView : ContentPage {
		UserLoginAbstract userLogin;
		public UserTokenVerificationView () {
			InitializeComponent ();
			userLogin = DependencyService.Get<UserLoginAbstract> ();
			userLogin.SetTokenVerificationEvent(async (response, status) => {
				if(status == System.Net.HttpStatusCode.OK) {
					if(response.status == (int)UserLoginAbstract.SMSTokenStatusCode.verification_success) {
						await DisplayAlert("Success",response.message,"Ok");
						userLogin.StartTargerApplication ();
						UserLoginAbstract.IsLogin = true;
					}
				}
			});
		}

		public void confirmSmsCode(object sender, EventArgs eventArgs) {
			String phoneNumber = null;
			if (userLogin.ObtainPhoneNumberFromPref (out phoneNumber)) {
				userLogin.SMSTokenVerification (SMSCodeEntry.Text, phoneNumber);
			}
		}
	}
}

