﻿using System;
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

        private IContainer componentSysTray;

        //public int timerLength = 15 * 60 * 1000; // minutes * seconds * milliseconds

        public System.Timers.Timer myTimer;

        DateTimeOffset dtos;

        public Main() {
            try {
                WeatherFeed.MakeApiCall();
            } catch {

            }

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

            // Create notify icon
            notifyIcon = new NotifyIcon(componentSysTray); 

            // Context menu right click
            notifyIcon.ContextMenu = contextMenu;

            // Tool tip text
            dtos = DateTimeOffset.FromUnixTimeSeconds(WeatherFeed.epochTime);
            notifyIcon.Text = "UTC" + dtos.ToString("yyyy-MM-dd HH:mm:ss");

            // Set true
            notifyIcon.Visible = true;

            // Double click handler
            notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);

            DrawStringBmp();
            StartApiTimer();
        }

        public void StartApiTimer() {
            myTimer = new System.Timers.Timer(1200000);
            myTimer.Elapsed += MakeApiCallTimer;
            myTimer.AutoReset = true;
            myTimer.Enabled = true;
        }

        public void MakeApiCallTimer(object sender, ElapsedEventArgs e) {
            try {
                WeatherFeed.MakeApiCall();                
            } catch {

            }
            
            dtos = DateTimeOffset.FromUnixTimeSeconds(WeatherFeed.epochTime);
            notifyIcon.Text = "UTC" + dtos.ToString("yyyy-MM-dd HH:mm:ss");
            DrawStringBmp();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e) {

        }

        // Exit menu item
        private void menuItem0_Click(object sender, EventArgs e) {
           QuitApp();
        }

        // Second menu item
        private void menuItem1_Click(object sender, EventArgs e) {
        }

        private void DrawStringBmp() {
            string txt = Math.Round(WeatherFeed.temperatureKelvin.KelvinToCelsius()).ToString();
            //int fontSize = 32;
            int bmpSize = 48;
            Font font = new Font("Arial", 36, GraphicsUnit.Pixel);
            Color myColour = Color.FromName("White");

            Bitmap bmp = new Bitmap(bmpSize, bmpSize);
            Graphics gfx = Graphics.FromImage(bmp);

            Brush brush = new SolidBrush(myColour);

            Color backgroundColour = (WeatherFeed.rainExists) ? Color.Blue : Color.Green;

            gfx.Clear(backgroundColour);
            //gfx.FillRectangle(new SolidBrush(Color.Orange), 0, 0, bmpSize, bmpSize);
            //gfx.FillEllipse(new SolidBrush(Color.LightBlue), 2, 2, bmpSize-4, bmpSize-4);
            //gfx.FillPie(new SolidBrush(Color.Turquoise), new Rectangle(0, 0, bmpSize - 1, bmpSize - 1), 315f, 270f);
            gfx.DrawString(txt, font, brush, 1, 1);

            //for (int i = 0; i < bmp.Width; i++) {
            //    bmp.SetPixel(i, 1, Color.White);
            //    bmp.SetPixel(i, bmpSize-2, Color.White);
            //}

            Icon myIcon = Icon.FromHandle(bmp.GetHicon());
            notifyIcon.Icon = myIcon;

            DestroyIcon(myIcon.Handle);

            
        }

        // Exit
        public void QuitApp() {
            myTimer.Stop();
            myTimer.Dispose();
            Application.Exit();
        }

    }
}
