using System;

namespace Model
{
	public class Contact {
		
		private String name = null;
		private String phone = null;

		public String Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		public String Phone {
			get {
				return phone;
			}
			set {
				phone = value;
			}
		}
	}
}

