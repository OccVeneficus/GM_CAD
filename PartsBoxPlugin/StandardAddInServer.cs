using Inventor;
using Microsoft.Win32;
using PartsBox;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace PartsBoxPlugin
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("1736870b-0921-49e4-9bff-b5b098099461")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.
        private Inventor.Application m_inventorApplication;

        private ButtonDefinition _button;

        private Window _window = new MainWindow();

        public StandardAddInServer()
        {
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
            m_inventorApplication = addInSiteObject.Application;
            _button = m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition(
                "Test", "Test", CommandTypesEnum.kShapeEditCmdType, getGUID());
            var partRibbon = m_inventorApplication.UserInterfaceManager.Ribbons["Part"];
            var toolsTab = partRibbon.RibbonTabs["id_TabTools"];
            var customPanel = toolsTab.RibbonPanels.Add("TestName", "Test", getGUID());
            _button.OnExecute += Button_OnExecute;
            customPanel.CommandControls.AddButton(_button);
            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.
        }

        private void Button_OnExecute(NameValueMap Context)
        {
            _window = new MainWindow();
            _window.Show();
            //MessageBox.Show("Lol", "I DID IT", MessageBoxButton.OK);
        }

        private string getGUID()
        {
            GuidAttribute addInCLSID = (GuidAttribute)GuidAttribute.GetCustomAttribute(
                typeof(StandardAddInServer),
                typeof(GuidAttribute));
            return "{" + addInCLSID.Value + "}";
        }



        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            m_inventorApplication = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

        #endregion

    }
}
