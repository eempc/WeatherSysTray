using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Timers;

namespace WeatherSysTray0 {
    class Main : ApplicationContext {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;
        private MenuItem menuItem0;
        private MenuItem menuItem1;
        //private List<MenuItem> menuItems;
        private IContainer componentSysTray;

        //private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public double temperature;

        public Main() {
            //menuItems = new List<MenuItem>();
            componentSysTray = new Container();
            contextMenu = new ContextMenu();
            menuItem0 = new MenuItem();
            menuItem1 = new MenuItem();

            // Init context menu with an array as argument
            contextMenu.MenuItems.AddRange(new MenuItem[] { menuItem0, menuItem1 });

            // Init menu item(s)
            menuItem0.Index = 0;
            menuItem0.Text = "Exit";
            menuItem0.Click += new EventHandler(menuItem0_Click);

            menuItem1.Index = 1;
            menuItem1.Text = "Change";
            menuItem1.Click += new EventHandler(menuItem1_Click);

            // Form display
            //this.ClientSize = new Size(292, 266);
            //this.Text = "Notify icon example";

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

        public void UpdateTemperatureTimer() {

        }


        private void notifyIcon_DoubleClick(object sender, EventArgs e) {
            //if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
            //this.Activate();
        }

        private void menuItem0_Click(object sender, EventArgs e) {
           Application.Exit();
        }

        // Also dynamic change icon
        private void menuItem1_Click(object sender, EventArgs e) {
            DrawStringBmp();
        }

        private void DrawStringBmp() {
            string txt = WeatherFeed.ExtractTemperature().KelvinToCelsius().ToString();
            //int fontSize = 32;
            int bmpSize = 48;
            Font font = new Font("Arial", 30, GraphicsUnit.Pixel);
            Color myColour = Color.FromName("White");

            Bitmap bmp = new Bitmap(bmpSize, bmpSize);
            Graphics gfx = Graphics.FromImage(bmp);

            Brush brush = new SolidBrush(myColour);
            gfx.Clear(Color.Transparent);
            //gfx.FillRectangle(new SolidBrush(Color.Orange), 0, 0, bmpSize, bmpSize);
            //gfx.FillEllipse(new SolidBrush(Color.LightBlue), 2, 2, bmpSize-4, bmpSize-4);
            gfx.FillPie(new SolidBrush(Color.Turquoise), new Rectangle(0, 0, bmpSize - 1, bmpSize - 1), 315f, 270f);
            gfx.DrawString(txt, font, brush, 1, 1);

            //for (int i = 0; i < bmp.Width; i++) {
            //    bmp.SetPixel(i, 1, Color.White);
            //    bmp.SetPixel(i, bmpSize-2, Color.White);
            //}

            Icon myIcon = Icon.FromHandle(bmp.GetHicon());
            notifyIcon.Icon = myIcon;

            DestroyIcon(myIcon.Handle);
        }

        private void BmpToIco() {
            Bitmap bmp = new Bitmap("Image1.bmp");
            IntPtr hicon = bmp.GetHicon();
            Icon newIcon = Icon.FromHandle(hicon);
            notifyIcon.Icon = newIcon;
            DestroyIcon(newIcon.Handle);
        }

        public static void Exit(object sender, EventArgs e) {
            Application.Exit();
        }

    }
}
