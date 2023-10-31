using System.Diagnostics;

namespace custom_mouse_leave_behavior
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
    }
    public class PictureBoxEx : PictureBox
    {
#if USE_VERSION_1
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Capture) Capture = false;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (MouseButtons == MouseButtons.Left)
            {
                var client = PointToClient(MousePosition);
                BeginInvoke(() =>
                    MessageBox.Show($"You Leave At: {client.X} --- {client.Y}"));
            }
        }
#else
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Debug.WriteLine($"{e.Location}");
            base.OnMouseMove(e);
            if ((MouseButtons == MouseButtons.Left) && !ClientRectangle.Contains(e.Location))
            {
                BeginInvoke(() =>
                    MessageBox.Show($"You Leave At: {e.Location.X} --- {e.Location.Y}"));
            }
        }
#endif

    }
}