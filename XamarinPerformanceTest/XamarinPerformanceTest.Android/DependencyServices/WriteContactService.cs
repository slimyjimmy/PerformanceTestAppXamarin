using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinPerformanceTest.DependencyServices;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Android.Provider;
using Xamarin.Forms;


[assembly: Dependency(typeof(XamarinPerformanceTest.Droid.DependencyServices.WriteContactService))]
namespace XamarinPerformanceTest.Droid.DependencyServices
{
    class WriteContactService : IWriteContactService
    {
        public int WriteContact(Contact contact)
        {
            List<ContentProviderOperation> ops = new List<ContentProviderOperation>();
            int rawContactInsertIndex = ops.Count;

            ops.Add(ContentProviderOperation
                .NewInsert(Android.Provider.ContactsContract.RawContacts.ContentUri)
                .WithValue(Android.Provider.ContactsContract.RawContacts.InterfaceConsts.AccountType, null)
                .WithValue(Android.Provider.ContactsContract.RawContacts.InterfaceConsts.AccountName, null).Build());
            ops.Add(ContentProviderOperation
                .NewInsert(Android.Provider.ContactsContract.Data.ContentUri)
                .WithValueBackReference(Android.Provider.ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex)
                .WithValue(Android.Provider.ContactsContract.Data.InterfaceConsts.Mimetype, Android.Provider.ContactsContract.CommonDataKinds.StructuredName.ContentItemType)
                .WithValue(Android.Provider.ContactsContract.CommonDataKinds.StructuredName.DisplayName, contact.FirstName) // Name of the person
                .Build());
            ops.Add(ContentProviderOperation
                .NewInsert(Android.Provider.ContactsContract.Data.ContentUri)
                .WithValueBackReference(
                    ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex)
                .WithValue(Android.Provider.ContactsContract.Data.InterfaceConsts.Mimetype, Android.Provider.ContactsContract.CommonDataKinds.Phone.ContentItemType)
                .WithValue(Android.Provider.ContactsContract.CommonDataKinds.Phone.Number, contact.Number) // Number of the person
                .WithValue(Android.Provider.ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Type, "mobile").Build()); // Type of mobile number 
            try
            {
                Android.App.Application.Context.ContentResolver.ApplyBatch(ContactsContract.Authority, ops);
            }
            catch (Exception ex)
            {
                //
            }
            return 0;
        }
    }
}