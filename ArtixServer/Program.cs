using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using ArtAuto.Common;
using ArtAuto.Data;

namespace ArtixServer
{
    public sealed class ServerApp
    {

        private NotifyIcon notifyIcon;
        private ContextMenu notificationMenu;

        #region Initialize icon and menu
        public ServerApp()
        {
            notifyIcon = new NotifyIcon();
            notificationMenu = new ContextMenu(InitializeMenu());

            notifyIcon.DoubleClick += IconDoubleClick;            
            notifyIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location); ;
            notifyIcon.ContextMenu = notificationMenu;
        }

        private MenuItem[] InitializeMenu()
        {
            MenuItem[] menu = new MenuItem[] {
                new MenuItem("About", menuAboutClick),
                new MenuItem("Exit", menuExitClick)
            };
            return menu;
        }
        #endregion


        #region Event Handlers
        private void menuAboutClick(object sender, EventArgs e)
        {
            MessageBox.Show("About This Application");
        }

        private void menuExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void IconDoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("The icon was double clicked");
            MainForm mf = new MainForm();
            mf.Show();
        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            bool isFirstInstance;
            // Please use a unique name for the mutex to prevent conflicts with other programs
            using (Mutex mtx = new Mutex(true, "testserver", out isFirstInstance))
            {
                if (isFirstInstance)
                {
                    ServerApp app = new ServerApp();
                    app.notifyIcon.Visible = true;

                    Project project = new Project("TEST", "");

                    project.Load("d:\\Work\\Автоматизация\\Тест батарей\\SRCs\\config.xml");                    


                    Application.Run();
                    app.notifyIcon.Dispose();
                }
                else
                {
                    // The application is already running
                    // TODO: Display message box or change focus to existing application instance
                }
            } // release
        }
    }
}
