using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherSysTray0 {
    public partial class Form1 : Form {
        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;
        private MenuItem menuItem;
        private IContainer componentSysTray;

        public Form1() {
            InitializeComponent();

            this.componentSysTray = new Container();
            this.contextMenu = new ContextMenu();
            this.menuItem = new MenuItem();

            // Init context menu
            this.contextMenu.MenuItems.AddRange(new MenuItem[] { this.menuItem });

            // Init menu item(s)
            this.menuItem.Index = 0;
            this.menuItem.Text = "Exit";
            this.menuItem.Click += new EventHandler(menuItem_Click);

            // Form display
            this.ClientSize = new Size(292, 266);
            this.Text = "Notify icon example";

            // Create notify icon
            notifyIcon = new NotifyIcon(componentSysTray);
            notifyIcon.Icon = new Icon("test.ico");

            // Context menu right click
            notifyIcon.ContextMenu = this.contextMenu;

            // Tool tip text
            notifyIcon.Text = "Notify example";
            notifyIcon.Visible = true;

            // Double click handler to activate form
            notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);                   
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void menuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

    }
}
