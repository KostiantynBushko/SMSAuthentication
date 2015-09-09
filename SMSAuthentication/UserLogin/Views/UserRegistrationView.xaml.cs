using System;
using System.Text.RegularExpressions;
using PhoneBook;
using UserLogin;
using Xamarin.Forms;


namespace Views
{
	public partial class UserRegistrationView : ContentPage
	{
		PhoneBookAbstractHelper phoneBookHelper;
		UserLoginAbstract userLogin;

		public UserRegistrationView ()
		{
			InitializeComponent ();

			userLogin = DependencyService.Get<UserLoginAbstract> ();
			phoneBookHelper = DependencyService.Get<PhoneBookAbstractHelper> ();
			if (phoneBookHelper != null) {
				string phoneNumber;
				if (phoneBookHelper.GetOwnPhoneNumber (out phoneNumber)) {
					phoneNumberEntry.Text = phoneNumber;
					buttonProceedAuthentication.IsEnabled = true;
				} else {
					String phone;
					if (userLogin.ObtainPhoneNumberFromPref (out phone)) {
						phoneNumberEntry.Text = phone;
						buttonProceedAuthentication.IsEnabled |= PhoneBookAbstractHelper.IsValidPhone (phone);
					}
				}
				String pass;
				if (userLogin.ObtainUserPasswordFromPref (out pass)) {
					userPassword.Text = pass;
				}
			}

			phoneNumberEntry.TextChanged += (object sender, TextChangedEventArgs e) => {
				if(PhoneBookAbstractHelper.IsValidPhone (e.NewTextValue)) {
					buttonProceedAuthentication.IsEnabled = true;
				} else {
					buttonProceedAuthentication.IsEnabled = false;
				}
			};

			userLogin.SetPhoneNumberVerificationEvent(async (response, status) => {
				if(status == System.Net.HttpStatusCode.OK) {
					if(response.status == (int)UserLoginAbstract.RegistrationStatusCode.registration_complete) {
						await DisplayAlert("Complete",response.message,"Ok");
						await Navigation.PushAsync (new UserTokenVerificationView ());
					}
				} else {
					await DisplayAlert ("Backend Problem", "Did not receive successful response from backend.", "OK");
				}
			});
		}

		public void proceedAuthentication(object sender, EventArgs eventArgs) {
			userLogin.UserRegisterVerification (phoneNumberEntry.Text, userPassword.Text);
		}
	}
}

