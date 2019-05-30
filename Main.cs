using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace WeatherSysTray0 {
    class Main : ApplicationContext {


        public Main() {
        }



        public static void Exit(object sender, EventArgs e) {
            Application.Exit();
        }

    }
}
