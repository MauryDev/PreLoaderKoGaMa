using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreLoaderKoGaMa.Installer
{
    public partial class Loading : Form
    {
        public int Step { get; set; } = 0;
        public int TotalStep { get; set; }
        public Action? RunCallback = null;
        public Loading()
        {
            InitializeComponent();
        }
        public void NextState(string title)
        {
            Step++;
            Invoke(SetStateInternal, title, 100 / TotalStep * Step);

        }

        void SetStateInternal(string title, int progress)
        {
            label1.Text = title;
            progressBar1.Value = progress;
        }

        private void Loading_Shown(object sender, EventArgs e)
        {
            RunCallback?.Invoke();
            if (Owner != null && StartPosition == FormStartPosition.WindowsDefaultLocation)
            {
                int offset = Owner.OwnedForms.Length * 38;  // approx. 10mm
                Point p = new Point(Owner.Left + Owner.Width / 2 - Width / 2 + offset, Owner.Top + Owner.Height / 2 - Height / 2 + offset);
                this.Location = p;
            }
        }
    }
}
