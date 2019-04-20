using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_white
{
    [TestFixture]
    public class ContactRemovalTests: TestBase
    {
        [Test]
        public void TestContactRemoval()
        {
            app.Contacts.EnsureContactExists();
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Remove();

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
