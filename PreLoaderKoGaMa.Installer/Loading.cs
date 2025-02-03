

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
        public void SetState(string title, int perc)
        {
            Invoke(SetStateInternal, title, Math.Clamp(perc,0,100));

        }
        public void CloseWin()
        {
            
            Invoke(this.Close);

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
