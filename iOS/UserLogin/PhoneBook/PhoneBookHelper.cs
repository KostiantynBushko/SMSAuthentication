using System.Collections.Generic;
using AddressBook;
using Foundation;
using Xamarin.Forms;
using UserLogin;
using PhoneBook;

[assembly: Dependency(typeof(PhoneBookHelper))]

namespace PhoneBook
{
	public class PhoneBookHelper : PhoneBookAbstractHelper {

		public override bool GetOwnPhoneNumber(out string phone) {
			phone = NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)@"SBFormattedPhoneNumber").ToString(); 
			if (!string.IsNullOrEmpty (phone)) {
				new UserLoginAPI().SavePhoneNumberToPref (phone);
				return true;
			}
			return false;
		}

		public override List<T> GetPhoneContacts<T> () {
			NSError error;
			var addressBook = ABAddressBook.Create (out error);
			var status = ABAddressBook.GetAuthorizationStatus();
			var contactList = new List<T> ();
			if (status == ABAuthorizationStatus.Authorized) {
				ABPerson[] person = addressBook.GetPeople ();
				for (int i = 0; i < person.Length; i++) {
					System.Diagnostics.Debug.WriteLine (string.Format ("Name : {0} Phone : {1}", person [i].GetRelatedNames (), person [i].GetPhones ()));
					var contact = new T();
					contact.Name = person [i].FirstName;
					contact.Phone = person [i].GetPhones ().GetValues()[0];
					contactList.Add (contact);
				}
			}
			return contactList;
		}
	}
}

