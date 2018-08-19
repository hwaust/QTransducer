using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTrans.Models
{
    public partial class T2018ConfigPage : UserControl
    {
        public T2018ConfigPage()
        {
            InitializeComponent();
        }

        public DateTime K0004
        {
            get { return dpDateTime.Value; }
            set { dpDateTime.Value = value; }
        }

    }
}
