using System;
using System.Collections.Generic;
using Android.OS;
using Android.Provider;
using Android.Telephony;
using Xamarin.Forms;
using UserLogin;
using PhoneBook;

[assembly: Dependency(typeof(PhoneBookHelper))]

namespace PhoneBook
{
	public class PhoneBookHelper : PhoneBookAbstractHelper {

		public override bool GetOwnPhoneNumber(out string phone) {
			TelephonyManager telephoneManager = (TelephonyManager)Android.App.Application.Context.GetSystemService (Android.Content.Context.TelephonyService);
			phone = telephoneManager.Line1Number;
			if (!String.IsNullOrEmpty (phone)) {
				new UserLoginAPI().SavePhoneNumberToPref (phone);
				return true;
			}
			return false;
		}

		// Read contact from phone book
		public override List<T> GetPhoneContacts <T>() {
			var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;

			string[] projection = { 
				ContactsContract.Contacts.InterfaceConsts.DisplayName,
				ContactsContract.CommonDataKinds.Phone.Number
			};

			var cursor = Forms.Context.ContentResolver.Query (uri, projection, null, null, null);
			var contactList = new List<T> ();
			if (cursor.MoveToFirst ()) {
				do {
					string phone  = cursor.GetString (cursor.GetColumnIndex (projection [1]));
					phone = phone.Replace(" ", string.Empty);
					var contact = new T();
					contact.Name = cursor.GetString (cursor.GetColumnIndex (projection [0]));
					contact.Phone = cursor.GetString (cursor.GetColumnIndex (projection [1]));
					System.Diagnostics.Debug.WriteLine (string.Format ("Name : {0} Phone : {1}", contact.Name, contact.Phone));
					contactList.Add(contact);

				}  while (cursor.MoveToNext());
			}
			return contactList;
		}

	}
}

