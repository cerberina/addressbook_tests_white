using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using TestStack.White;
using TestStack.White.InputDevices;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.WindowsAPI;

namespace addressbook_tests_white
{
    public class ContactHelper: HelperBase
    {
        public static string CONTACTWINTITLE="";

        public ContactHelper(ApplicationManager manager) : base(manager)
        {

        }

        public void Remove()
        {
            Window dialog = ConfirmDeletionDialog();
            //dialog.Get<Button>("1508138").Click();
            dialog.Get(SearchCriteria.ByText("Yes")).Click();
            //dialog.Get<Button>("Yes");
        }

        private Window ConfirmDeletionDialog()
        {
            manager.MainWindow.Get<Button>("uxDeleteAddressButton").Click();
            return manager.MainWindow.ModalWindow("Question");
        }

        public void Add(ContactData newContact)
        {
            Window dialog = OpenContactsAddDialog();
            
            TextBox firstName = (TextBox)dialog.Get(SearchCriteria.ByAutomationId("ueFirstNameAddressTextBox"));
            firstName.Enter(newContact.Name);
            TextBox secondName = (TextBox)dialog.Get(SearchCriteria.ByAutomationId("ueMiddleNameAddressTextBox"));
            secondName.Enter(newContact.SecondName);

            dialog.Get<Button>("uxSaveAddressButton").Click();
        }

        public Window OpenContactsAddDialog()
        {
            manager.MainWindow.Get<Button>("uxNewAddressButton").Click();
            return manager.MainWindow.ModalWindow("Contact Editor");
        }

        public List<ContactData> GetContactList()
        {
            List<ContactData> list = new List<ContactData>();
            Table table = manager.MainWindow.Get<Table>("uxAddressGrid");
            TableRows rows = table.Rows;
            foreach ( TableRow tr in rows)
            {
                list.Add(new ContactData() { Name = tr.Cells[0].Value.ToString()});
            }
            return list;
        }

        public void EnsureContactExists()
        {
            if (IsContactListEmpty())
            {
                ContactData newContact = new ContactData()
                {
                    Name = "empty"
                };
                Add(newContact);
            }
        }

        public bool IsContactListEmpty()
        {
            return GetContactList().Count == 0;
        }
    }
}
