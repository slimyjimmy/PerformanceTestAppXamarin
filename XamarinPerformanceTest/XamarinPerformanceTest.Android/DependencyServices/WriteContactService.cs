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


//using static Android.Content.ContentProviderOperation;
//using static Android.Content.OperationApplicationException;
//using static Android.Provider.ContactsContract;
//using static Android.Provider.ContactsContract.CommonDataKinds.Phone;
//using static Android.Provider.ContactsContract.CommonDataKinds.StructuredName;
//using static Android.Provider.ContactsContract.Data;
//using static Android.Provider.ContactsContract.RawContacts;
using Xamarin.Forms;


[assembly: Dependency(typeof(XamarinPerformanceTest.Droid.DependencyServices.WriteContactService))]
namespace XamarinPerformanceTest.Droid.DependencyServices
{
    class WriteContactService : IWriteContactService
    {
        public int WriteContact(Contact contact)
        {
            var activity = Android.App.Application.Context as Activity;
            var intent = new Intent(Intent.ActionInsert);
            intent.SetType(ContactsContract.Contacts.ContentType);
            intent.PutExtra(ContactsContract.Intents.Insert.Name, contact.FirstName);
            intent.PutExtra(ContactsContract.Intents.Insert.Phone, contact.Number);
            activity.StartActivity(intent);
            return 0;
            //ContentValues contentValues = new ContentValues();
            //Android.Net.Uri rawContactUri = GetContentResolver().Insert(ContactsContract.RawContacts.ContentUri, contentValues);
            //long rawContactId = ContentUris.ParseId(rawContactUri);

            //ContentValues[] contactDetails = new ContentValues[2];
            //contentValues = new ContentValues();
            //contentValues.Put(ContactsContract.Data.RAW_CONTACT_ID, rawContactId);
            //contentValues.Put(ContactsContract.Data.MIMETYPE, ContactsContract.CommonDataKinds.StructuredName.CONTENT_ITEM_TYPE);
            //contentValues.Put(ContactsContract.CommonDataKinds.StructuredName.GivenName, "mobile" + ((int)(Math.random() * 1000)));
            //contactDetails[0] = contentValues;

            //ContentValues contentValuesPhone = new ContentValues();
            //contentValuesPhone.Put(ContactsContract.Data.RAW_CONTACT_ID, rawContactId);
            //contentValuesPhone.Put(ContactsContract.Data.MIMETYPE, ContactsContract.CommonDataKinds.Phone.CONTENT_ITEM_TYPE);
            //contentValuesPhone.Put(ContactsContract.CommonDataKinds.Phone.TYPE, ContactsContract.CommonDataKinds.Phone.TYPE_HOME);
            //contentValuesPhone.Put(ContactsContract.CommonDataKinds.Phone.Number, "123456789");
            //contactDetails[1] = contentValuesPhone;

            //getContentResolver().BulkInsert(ContactsContract.Data.CONTENT_URI, contactDetails);
        }


        //private void WriteContact(String displayName, String number)
        //{
        //    //ArrayList contentProviderOperations = new ArrayList();
        //    var contentProviderOperations = new List<ContentProviderOperation>();
        //    //insert raw contact using RawContacts.CONTENT_URI
        //    contentProviderOperations.Add(ContentProviderOperation.NewInsert(RawContacts.ContentUri)
        //        .WithValue(RawContacts.ACCOUNT_TYPE, null).withValue(RawContacts.ACCOUNT_NAME, null).build());
        //    //insert contact display name using Data.CONTENT_URI
        //    contentProviderOperations.Add(ContentProviderOperation.NewInsert(ContactsContract.Data.CONTENT_URI)
        //        .withValueBackReference(ContactsContract.Data.RAW_CONTACT_ID, 0).withValue(ContactsContract.Data.MIMETYPE, StructuredName.CONTENT_ITEM_TYPE)
        //        .withValue(StructuredName.DISPLAY_NAME, displayName).build());
        //    //insert mobile number using Data.CONTENT_URI
        //    contentProviderOperations.Add(ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri)
        //        .withValueBackReference(ContactsContract.Data.RAW_CONTACT_ID, 0).withValue(ContactsContract.Data.MIMETYPE, Phone.CONTENT_ITEM_TYPE)
        //        .withValue(Phone.NUMBER, number).withValue(Phone.TYPE, Phone.TYPE_MOBILE).build());
        //    try
        //    {
        //        getApplicationContext().getContentResolver().
        //            applyBatch(ContactsContract.AUTHORITY, contentProviderOperations);
        //    }
        //    catch (RemoteException e)
        //    {
        //        e.printStackTrace();
        //    }
        //    catch (OperationApplicationException e)
        //    {
        //        e.printStackTrace();
        //    }
        //}
    }
}