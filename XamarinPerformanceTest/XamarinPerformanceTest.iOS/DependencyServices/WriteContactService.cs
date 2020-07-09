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
            var store = new CNContactStore();
            var contactToAdd = new CNMutableContact();
            var cellPhone = new CNLabeledValue<CNPhoneNumber>(CNLabelPhoneNumberKey.Mobile, new CNPhoneNumber(contact.Number));
            var phoneNumber = new[] { cellPhone };
            contactToAdd.PhoneNumbers = phoneNumber;
            contactToAdd.GivenName = contact.FirstName;
            var saveRequest = new CNSaveRequest();
            saveRequest.AddContact(contactToAdd, store.DefaultContainerIdentifier);
            return 1;
        }
    }
}