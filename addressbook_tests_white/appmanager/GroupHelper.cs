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
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.WindowsAPI;

namespace addressbook_tests_white
{
    public class GroupHelper: HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public GroupHelper (ApplicationManager manager): base(manager)
            {
             
            }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            Window dialog = OpenGroupsDialog();
            Tree tree = dialog.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach(TreeNode item in root.Nodes)
            {
                list.Add(new GroupData() { Name = item.Text });
            }
            CloseGroupsDialog(dialog);
            return list;
        }

        public void Add(GroupData newGroup)
        {
            Window dialog = OpenGroupsDialog();
            dialog.Get<Button>("uxNewAddressButton").Click();
            TextBox textBox = (TextBox) dialog.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textBox.Enter(newGroup.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
            CloseGroupsDialog(dialog);
        }

        private void CloseGroupsDialog(Window dialog)
        {
            dialog.Get<Button>("uxCloseAddressButton").Click();
        }

        private Window OpenGroupsDialog()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }

        public Window OpenDeletionGroupDialog(Window dialog)
        {
            dialog.Get<Button>("uxDeleteAddressButton").Click();
            return dialog.ModalWindow("Delete group");
        }

        public void Remove()
        {
            //select group
            List<GroupData> list = new List<GroupData>();
            Window dialog = OpenGroupsDialog();
            Tree tree = dialog.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            TreeNode group = root.Nodes[1];
            group.Select();
            //click delete button
            //dialog.Get<Button>("uxDeleteAddressButton").Click();
            Window removeDialog = OpenDeletionGroupDialog(dialog);
            //confirm deletion
            removeDialog.Get<Button>("uxOKAddressButton").Click();
            CloseGroupsDialog(dialog);
        }
    }
}