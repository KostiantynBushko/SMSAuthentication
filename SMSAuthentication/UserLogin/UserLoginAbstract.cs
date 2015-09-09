using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Common;
using SimpleHttpClient;

namespace UserLogin
{
	public abstract class UserLoginAbstract {

		public enum RegistrationStatusCode {
			registration_complete = 0,
			alredy_registered     = 1
			//Extend with new status code if needed
		}

		public enum SMSTokenStatusCode {
			verification_success = 0
			//Extend with new status code if needed	
		}

		public static String PHONE_NUMBER = "PhoneNumber";
		public static String USER_PASSWORD = "Password";
		public static String IS_SUCCESS_AUTHORIZED = "is_success_authorized";
		public static String TOKEN = "token";

		public abstract bool ObtainPhoneNumberFromPref (out string phone);
		public abstract bool ObtainUserPasswordFromPref (out string password);
		public abstract void SavePhoneNumberToPref (string phone);
		public abstract void SaveUserPassword (string password);
		public abstract void SaveTokenToPref (string token);
		public abstract bool IsUserSuccessAuthorized ();
		public abstract void SetUserSuccessAuthorised ();

		public abstract void SetTargetApplication(Object target, Object context);
		public abstract void StartTargerApplication ();

		public static bool IsLogin = false;

		private event Action<UserRegisterResponse, System.Net.HttpStatusCode> userRegistrationComplete;
		private event Action<UserRegisterResponse, System.Net.HttpStatusCode> userAlredyRegistered;
		private event Action<UserRegisterResponse, System.Net.HttpStatusCode> messageReceived;
		private event Action<SMSTokenResponse, System.Net.HttpStatusCode> tokenVerificationComplete;

		public void SetPhoneNumberVerificationEvent(Action<UserRegisterResponse, System.Net.HttpStatusCode> phoneNumberVerificationAction) {
			userRegistrationComplete += phoneNumberVerificationAction;
		}

		public void SetTokenVerificationEvent(Action<SMSTokenResponse, System.Net.HttpStatusCode> tokenVerificationAction) {
			tokenVerificationComplete += tokenVerificationAction;
		}

		public void SetUserAlreadyRegisteredEvent(Action<UserRegisterResponse, System.Net.HttpStatusCode> userAlreadyRegisteredAction) {
			userAlredyRegistered += userAlreadyRegisteredAction;
		}

		public void SetMessageReceived(Action<UserRegisterResponse, System.Net.HttpStatusCode> messageReceivedAction) {
			messageReceived += messageReceivedAction;
		}

		public void UserRegisterVerification(String phoneNumber, String password) {
			List<KeyValuePair<String, String>> postParams = new List<KeyValuePair<String, String>> ();
			postParams.Add (new KeyValuePair<string, string> (PHONE_NUMBER, phoneNumber));
			postParams.Add (new KeyValuePair<string, string> (USER_PASSWORD, password));
			String responseResult;
			System.Net.HttpStatusCode status = new SimpleHttpClient.HttpClientHelper().HttpRequest (URL.PHONE_VERIFICATION_URL, postParams, out responseResult);

			if (status == System.Net.HttpStatusCode.OK) {
				UserRegisterResponse userRegistrationResponse = JsonConvert.DeserializeObject<UserRegisterResponse> (responseResult);
				switch (userRegistrationResponse.status) {
					// User complete registration action
					// Save phone number and password 
				  	case (int)RegistrationStatusCode.registration_complete:{
						SavePhoneNumberToPref (phoneNumber);
						SaveUserPassword (password);
						if (userRegistrationComplete != null)
							userRegistrationComplete (userRegistrationResponse,status);
						break;	
					}
					// User is already registered 
					case (int)RegistrationStatusCode.alredy_registered:{
						if (userAlredyRegistered != null)
							userAlredyRegistered (userRegistrationResponse, status);
						break;
					}
					//Add another case with specific status code
					default: {
						if (messageReceived != null)
							messageReceived (userRegistrationResponse, status);
						break;
					};
				}
			}
		}

		public void SMSTokenVerification(string token, string phoneNumber) {
			List<KeyValuePair<String,String>> postParams = new List<KeyValuePair<String,String>> ();
			postParams.Add (new KeyValuePair<string,string>(TOKEN, token));
			postParams.Add (new KeyValuePair<string, string> (PHONE_NUMBER, phoneNumber));
			String responseResult;
			System.Net.HttpStatusCode status = new SimpleHttpClient.HttpClientHelper().HttpRequest (URL.TOKEN_VERIFICATION_URL, postParams, out responseResult);

			if (status == System.Net.HttpStatusCode.OK) {
				SMSTokenResponse smsTokenResponse = JsonConvert.DeserializeObject<SMSTokenResponse> (responseResult);
				switch (smsTokenResponse.status) {
					// SMS token verification complete
					// Save token ans set user success registered
					case (int)SMSTokenStatusCode.verification_success:{
						SaveTokenToPref (token);
						SetUserSuccessAuthorised ();
						if (tokenVerificationComplete != null)
							tokenVerificationComplete (smsTokenResponse, status);
						break;
					}
					//Add another case with specific status code
					default: break;
				}
			}
		}
	}


	public class UserRegisterResponse {
		public int status;
		public String message;
	}

	public class SMSTokenResponse {
		public int status;
		public String message;
	}
}

