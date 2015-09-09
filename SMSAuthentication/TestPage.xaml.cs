using System;
using System.Collections.Generic;

using Xamarin.Forms;
using UserLogin;

namespace SMSAuthentication
{
	public partial class TestPage : ContentPage {
		public TestPage () {
			InitializeComponent ();
			var userLogin = DependencyService.Get<UserLoginAbstract> ();
			userLogin.GetPhoneContacts ();
		}

		public void startTestPageTwo(Object sender, EventArgs eventArgs) {
			Navigation.PushAsync (new TestPageTwo ());
		}
	}
}

