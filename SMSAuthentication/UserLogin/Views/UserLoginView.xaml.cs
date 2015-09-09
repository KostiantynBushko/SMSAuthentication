using System;
using Xamarin.Forms;
using UserLogin;

namespace Views
{
	public partial class UserLoginView : ContentPage
	{
		public UserLoginView ()
		{
			InitializeComponent ();

			var tapGestureRecognizer = new TapGestureRecognizer ();
			tapGestureRecognizer.Tapped += (sender, eventArgs) => Navigation.PushAsync (new ForgotPasswordView ());
			forgotPassword.GestureRecognizers.Add (tapGestureRecognizer);
		}

		public void confirmUserPassword(object sender, EventArgs eventArgs) {
			var userLogin = DependencyService.Get<UserLoginAbstract> ();
			if (userLogin == null)
				return;
			String password; 
			if(userLogin.ObtainUserPasswordFromPref(out password)) {
				if (String.Compare (password, passwordEntry.Text) == 0) {
					userLogin.StartTargerApplication ();
					UserLoginAbstract.IsLogin = true;
					return;
				}
			}
			DisplayAlert("Error:", "Incorrect password", "OK");
		}

		public void userRegistration(object sender, EventArgs eventArgs) {
			Navigation.PushAsync (new UserRegistrationView ());
		}
	}
}

