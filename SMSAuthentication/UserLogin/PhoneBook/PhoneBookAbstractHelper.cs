using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PhoneBook
{
	public abstract class PhoneBookAbstractHelper {
		
		public abstract bool GetOwnPhoneNumber (out string phone);
		public abstract List<T> GetPhoneContacts<T> () where T : Model.Contact, new();

		public static bool IsValidPhone(string phone) {
			try {
				if (string.IsNullOrEmpty(phone))
					return false;
				var r = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
				return r.IsMatch(phone);

			} catch (Exception) {
				throw;
			}
		}
	}
}

