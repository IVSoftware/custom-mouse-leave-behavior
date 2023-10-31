The reason you're seeing the behavior (or lack thereof) is that the mouse is in a state of [Capture](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.capture?view=windowsdesktop-7.0). If you don't need it to be captured, you can un-capture it on mouse down and have it work the way you describe:

[![mouse leave message][1]][1]




```
public class PictureBoxEx : PictureBox
{
    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        Capture = false;
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
}
```
___

Alternatively, you could leave the `Capture` alone, and implement the mouse leave behavior yourself:

```
public class PictureBoxEx : PictureBox
{
    protected override void OnMouseMove(MouseEventArgs e)
    {      
        base.OnMouseMove(e);
        if ((MouseButtons == MouseButtons.Left) && !ClientRectangle.Contains(e.Location))
        {
            BeginInvoke(() =>
                MessageBox.Show($"You Leave At: {e.Location.X} --- {e.Location.Y}"));
        }
    }
}
```


  [1]: https://i.stack.imgur.com/epvuS.png