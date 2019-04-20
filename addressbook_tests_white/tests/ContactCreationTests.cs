using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_tests_white
{
    [TestFixture]
    public class ContactCreationTests:TestBase
    {
        [Test]
        public void TestContactCreation ()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            ContactData newContact = new ContactData()
            {
                Name = "white",
                SecondName = "black"
            };
            app.Contacts.Add(newContact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(newContact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

        }
    }
}
