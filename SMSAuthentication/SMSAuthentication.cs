using System;
using Xamarin.Forms;
using UserLogin;
using Views;

namespace SMSAuthentication
{
	public class SMSAuthentication : Application {
		public SMSAuthentication () {
			MainPage = new NavigationPage (new ContactsListView());
		}

		protected override void OnStart () {
			// Handle when your app starts
		}

		protected override void OnSleep () {
			// Handle when your app sleeps
		}

		protected override void OnResume () {
			// Handle when your app resumes
		}
	}
}

