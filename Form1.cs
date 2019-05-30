using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherSysTray0 {
    public partial class Form1 : Form {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;
        private MenuItem menuItem0;
        private MenuItem menuItem1;
        private IContainer componentSysTray;

        // https://stackoverflow.com/questions/995195/how-can-i-make-a-net-windows-forms-application-that-only-runs-in-the-system-tra

        public Form1() {
            InitializeComponent();

            this.componentSysTray = new Container();
            this.contextMenu = new ContextMenu();
            this.menuItem0 = new MenuItem();
            this.menuItem1 = new MenuItem();

            // Init context menu
            this.contextMenu.MenuItems.AddRange(new MenuItem[] { this.menuItem0, this.menuItem1 });

            // Init menu item(s)
            this.menuItem0.Index = 0;
            this.menuItem0.Text = "Exit";
            this.menuItem0.Click += new EventHandler(menuItem0_Click);

            this.menuItem1.Index = 1;
            menuItem1.Text = "Change";
            menuItem1.Click += new EventHandler(menuItem1_Click);

            // Form display
            this.ClientSize = new Size(292, 266);
            this.Text = "Notify icon example";

            // Create notify icon
            notifyIcon = new NotifyIcon(componentSysTray);
            notifyIcon.Icon = new Icon("test.ico"); // Stream can be an argument <- the key line is here

            // https://stackoverflow.com/questions/42970608/c-sharp-dynamically-change-notifyicon-image-in-tray

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

        private void menuItem0_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void menuItem1_Click(object sender, EventArgs e) {
            Bitmap bmp = new Bitmap("Image1.bmp");
            IntPtr hicon = bmp.GetHicon();
            Icon newIcon = Icon.FromHandle(hicon);
            notifyIcon.Icon = newIcon;
            DestroyIcon(newIcon.Handle);
        }

    }
}
