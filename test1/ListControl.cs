using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test1
{
    public partial class ListControl : UserControl
    {
        public ListControl()
        {
            InitializeComponent();
        }

        public void Add(Control c)
        {
            flpListBox.Controls.Add(c);
            SetupAnchors();
        }
        public void Add(string account,string caption)
        {
            ListControlItem item=new ListControlItem();
            item.Song = account;
            item.Album = caption;
            item.Info = caption;

            item.SelectionChanged += SelectionChanged;
            //item.Click += ItemClicked;

            flpListBox.Controls.Add(item);
            SetupAnchors();
        }

        public void Remove(string name)
        {
            var item = flpListBox.Controls[name] as ListControlItem;
            flpListBox.Controls.Remove(item);


            item.SelectionChanged -= SelectionChanged;
            //item.Click -= ItemClicked;

            item.Dispose();

            SetupAnchors();
        }

        public void Clear()
        {
            for (int i = 0; i < flpListBox.Controls.Count; i++)
            {
                Control c = flpListBox.Controls[i];
                flpListBox.Controls.Remove(c);
                c.Dispose();
            }
        }

        public  int Count
        {
            get { return flpListBox.Controls.Count; }
        }
        //布局控件大小
        private void flpListBox_Layout(object sender, LayoutEventArgs e)
        {
            if (flpListBox.Controls.Count > 0)
            {
                flpListBox.Controls[0].Width = flpListBox.Size.Width - SystemInformation.VerticalScrollBarWidth;
            }
        }

        private void SetupAnchors()
        {
            for (int i = 0; i < flpListBox.Controls.Count; i++)
            {
                Control c = flpListBox.Controls[i];

                if (i == 0)
                {
                    //首个控件不能使用Left+Right
                    c.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    c.Width = flpListBox.Width - SystemInformation.VerticalScrollBarWidth;
                }
                else
                {
                    //使宽度适应第一个控件的宽度
                    c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                }
            }
        }

        private ListControlItem mLastSelected = null;
        private void SelectionChanged(object sender)
        {
            if (mLastSelected != null)
                mLastSelected.Selected = false;
            mLastSelected = sender as ListControlItem;
        }

        public event ItemClickEventHandler ItemClick;

        public delegate void ItemClickEventHandler(object sender, int Index);
        private void ItemClicked(object sender, System.EventArgs e)
        {
            if (ItemClick != null)
                ItemClick.Invoke(this, flpListBox.Controls.IndexOfKey((sender as ListControlItem).Name));
        }
    }
}
