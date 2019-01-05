using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace test1
{
    public partial class ListControlItem : UserControl
    {
        public ListControlItem()
        {
            InitializeComponent();
            _tmrMouseLeave.Interval = 10;
            _tmrMouseLeave.Tick += tmrMouseLeave_Tick;
        }
        public event SelectionChangedEventHandler SelectionChanged;

        public delegate void SelectionChangedEventHandler(object sender);

        private System.Windows.Forms.Timer _tmrMouseLeave=new Timer();

        //internal System.Windows.Forms.Timer tmrMouseLeave
        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    get
        //    {
        //        return _tmrMouseLeave;
        //    }

        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    set
        //    {
        //        if (_tmrMouseLeave != null)
        //        {
        //            _tmrMouseLeave.Tick -= tmrMouseLeave_Tick;
        //        }

        //        _tmrMouseLeave = value;
        //        if (_tmrMouseLeave != null)
        //        {
        //            _tmrMouseLeave.Tick += tmrMouseLeave_Tick;
        //        }
        //    }
        //}

        private Image mImage;
        public Image Image
        {
            get
            {
                return mImage;
            }
            set
            {
                mImage = value;
                Refresh();
            }
        }

        private string mSong = "[Song Name]";
        public string Song
        {
            get
            {
                return mSong;
            }
            set
            {
                mSong = value;
                Refresh();
            }
        }

        private string mArtist = "[Artist]";
        public string Artist
        {
            get
            {
                return mArtist;
            }
            set
            {
                mArtist = value;
                Refresh();
            }
        }

        private string mAlbum = "[Album]";
        public string Album
        {
            get
            {
                return mAlbum;
            }
            set
            {
                mAlbum = value;
                Refresh();
            }
        }

        private string _info;
        public string Info
        {
            get
            {
                return lblInfo.Text;
            }
            set
            {
                lblInfo.Text = value;
            }
        }

        private bool mSelected;
        public bool Selected
        {
            get
            {
                return mSelected;
            }
            set
            {
                mSelected = value;
                Refresh();
            }
        } 
        private enum MouseCapture
        {
            Outside,
            Inside
        }
        private enum ButtonState
        {
            ButtonUp,
            ButtonDown,
            Disabled
        }
        private ButtonState bState;
        private MouseCapture bMouse;

        private void ListControlItem_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Selected == false)
            {
                Selected = true;
                if (SelectionChanged != null)
                    SelectionChanged.Invoke(sender);
            }
        }

        private void metroRadioGroup_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) // , rdButton.MouseDown
        {
            bState = ButtonState.ButtonDown;
            Refresh();
        }

        private void metroRadioGroup_MouseEnter(object sender, System.EventArgs e)
        {
            bMouse = MouseCapture.Inside;
            _tmrMouseLeave.Start();
            Refresh();
        }

        private void metroRadioGroup_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) // , rdButton.MouseUp
        {
            bState = ButtonState.ButtonUp;
            Refresh();
        }

        private void tmrMouseLeave_Tick(System.Object sender, System.EventArgs e)
        {
            var scrPT = Control.MousePosition;
            Point ctlPT = this.PointToClient(scrPT);

            if (Song == "2")
                Debug.WriteLine(scrPT.X + "," + scrPT.Y + "\t" + ctlPT.X + "," + ctlPT.Y);

            if (ctlPT.X < 0 | ctlPT.Y < 0 | ctlPT.X > this.Width | ctlPT.Y > this.Height)
            {
                // Stop timer
                _tmrMouseLeave.Stop();
                bMouse = MouseCapture.Outside;
                Refresh();
            }
            else
                bMouse = MouseCapture.Inside;
        }


        private void Paint_DrawBackground(Graphics gfx)
        {
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            // /// Build a rounded rectangle
            GraphicsPath p = new GraphicsPath();
            const int Roundness = 5;
            p.StartFigure();
            p.AddArc(new Rectangle(rect.Left, rect.Top, Roundness, Roundness), 180, 90);
            p.AddLine(rect.Left + Roundness, 0, rect.Right - Roundness, 0);
            p.AddArc(new Rectangle(rect.Right - Roundness, 0, Roundness, Roundness), -90, 90);
            p.AddLine(rect.Right, Roundness, rect.Right, rect.Bottom - Roundness);
            p.AddArc(new Rectangle(rect.Right - Roundness, rect.Bottom - Roundness, Roundness, Roundness), 0, 90);
            p.AddLine(rect.Right - Roundness, rect.Bottom, rect.Left + Roundness, rect.Bottom);
            p.AddArc(new Rectangle(rect.Left, rect.Height - Roundness, Roundness, Roundness), 90, 90);
            p.CloseFigure();


            // /// Draw the background ///
            Color[] ColorScheme = null;
            SolidBrush brdr = null/* TODO Change to default(_) if this is not a reference type */;

            if (bState == ButtonState.Disabled)
            {
                // normal
                brdr = ColorSchemes.DisabledBorder;
                ColorScheme = ColorSchemes.DisabledAllColor;
            }
            else if (mSelected)
            {
                // Selected
                brdr = ColorSchemes.SelectedBorder;

                if (bState == ButtonState.ButtonUp & bMouse == MouseCapture.Outside)
                    // normal
                    ColorScheme = ColorSchemes.SelectedNormal;
                else if (bState == ButtonState.ButtonUp & bMouse == MouseCapture.Inside)
                    // hover 
                    ColorScheme = ColorSchemes.SelectedHover;
                else if (bState == ButtonState.ButtonDown & bMouse == MouseCapture.Outside)
                    // no one cares!
                    return;
                else if (bState == ButtonState.ButtonDown & bMouse == MouseCapture.Inside)
                    // pressed
                    ColorScheme = ColorSchemes.SelectedPressed;
            }
            else
            {
                // Not selected
                brdr = ColorSchemes.UnSelectedBorder;

                if (bState == ButtonState.ButtonUp & bMouse == MouseCapture.Outside)
                {
                    // normal
                    brdr = ColorSchemes.DisabledBorder;
                    ColorScheme = ColorSchemes.UnSelectedNormal;
                }
                else if (bState == ButtonState.ButtonUp & bMouse == MouseCapture.Inside)
                    // hover 
                    ColorScheme = ColorSchemes.UnSelectedHover;
                else if (bState == ButtonState.ButtonDown & bMouse == MouseCapture.Outside)
                    // no one cares!
                    return;
                else if (bState == ButtonState.ButtonDown & bMouse == MouseCapture.Inside)
                    // pressed
                    ColorScheme = ColorSchemes.UnSelectedPressed;
            }

            // Draw
            LinearGradientBrush b = new LinearGradientBrush(rect, Color.White, Color.Black, LinearGradientMode.Vertical);
            ColorBlend blend = new ColorBlend();
            blend.Colors = ColorScheme;
            blend.Positions = new float[] { 0.0F, 0.1F, 0.9F, 0.95F, 1.0F };
            b.InterpolationColors = blend;
            gfx.FillPath(b, p);

            // // Draw border
            gfx.DrawPath(new Pen(brdr), p);

            // // Draw bottom border if Normal state (not hovered)
            if (bMouse == MouseCapture.Outside)
            {
                rect = new Rectangle(rect.Left, this.Height - 1, rect.Width, 1);
                b = new LinearGradientBrush(rect, Color.Blue, Color.Yellow, LinearGradientMode.Horizontal);
                blend = new ColorBlend();
                blend.Colors = new Color[] { Color.White, Color.LightGray, Color.White };
                blend.Positions = new float[] { 0.0F, 0.5F, 1.0F };
                b.InterpolationColors = blend;
                // 
                gfx.FillRectangle(b, rect);
            }
        }

        private void Paint_DrawButton(Graphics gfx)
        { 
            RectangleF layoutRect;
            StringFormat SF = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
            Rectangle workingRect = new Rectangle(40, 0, this.Width - 40 - 6, this.Height);

            // Draw song name
            Font fnt = new Font("Segoe UI Light", 14);
            SizeF sz = gfx.MeasureString(mSong, fnt);
            layoutRect = new RectangleF(40, 0, workingRect.Width, sz.Height);
            gfx.DrawString(mSong, fnt, Brushes.Black, layoutRect, SF);

            // Draw artist name
            fnt = new Font("Segoe UI Light", 10);
            sz = gfx.MeasureString(mArtist, fnt);
            layoutRect = new RectangleF(42, 30, workingRect.Width, sz.Height);
            gfx.DrawString(mArtist, fnt, Brushes.Black, layoutRect, SF);

            // Draw album name
            fnt = new Font("Segoe UI Light", 10);
            sz = gfx.MeasureString(mAlbum, fnt);
            layoutRect = new RectangleF(42, 49, workingRect.Width, sz.Height);
            gfx.DrawString(mAlbum, fnt, Brushes.Black, layoutRect, SF);

            // Album Image
            if (mImage != null)
                gfx.DrawImage(mImage, new Point(7, 7));
            else
                gfx.DrawImage(imageList1.Images[0], new Point(7, 7));
        }

        private void PaintEvent(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var gfx = e.Graphics;
            // 
            Paint_DrawBackground(gfx);
            Paint_DrawButton(gfx);
        }
    }
}
