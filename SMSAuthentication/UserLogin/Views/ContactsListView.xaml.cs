using System;
using System.Collections.Generic;
using PhoneBook;
using Xamarin.Forms;

namespace Views
{
	public partial class ContactsListView : ContentPage
	{
		public ContactsListView ()
		{
			InitializeComponent ();
			var phoneBookHelper = DependencyService.Get<PhoneBook.PhoneBookAbstractHelper> ();
			contactsList.ItemsSource = phoneBookHelper.GetPhoneContacts<ContactItem>();
		}

		public void SendMessage(object sender, EventArgs eventArgs) {
			System.Diagnostics.Debug.WriteLine("");
		}
	}

	public class ContactItem : Model.Contact {
		Boolean enable = false;
		public Boolean Enable {
			get {
				return enable;
			}
			set {
				enable = value;
			}
		}
	}
}

