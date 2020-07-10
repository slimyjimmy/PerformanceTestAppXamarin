using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using Contacts;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinPerformanceTest.DependencyServices;

[assembly: Dependency(typeof(XamarinPerformanceTest.iOS.DependencyServices.WriteContactService))]
namespace XamarinPerformanceTest.iOS.DependencyServices
{
    class WriteContactService : IWriteContactService
    {
        public int WriteContact(Contact contact)
        {
            var contactToAdd = new CNMutableContact();
            contactToAdd.GivenName = contact.FirstName;
            var phoneNumber = new CNLabeledValue<CNPhoneNumber>(label: CNLabelKey.Home, value: new CNPhoneNumber(stringValue: contact.Number));
            contactToAdd.PhoneNumbers = new []{ phoneNumber };
            var store = new CNContactStore();
            var saveRequest = new CNSaveRequest();
            saveRequest.AddContact(contactToAdd, null);
            var error = new NSError();
            try
            {
                store.ExecuteSaveRequest(saveRequest, out error);
                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}