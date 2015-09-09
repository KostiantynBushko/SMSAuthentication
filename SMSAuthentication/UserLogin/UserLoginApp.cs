using System;
using Xamarin.Forms;
using Views;

namespace UserLogin
{
	public class UserLoginApp : Application {
		public UserLoginApp () {
			MainPage = new NavigationPage (new UserLoginView ());
		}
	}
}

