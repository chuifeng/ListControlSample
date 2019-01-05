namespace test1
{
    partial class ListControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.flpListBox = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpListBox
            // 
            this.flpListBox.AutoScroll = true;
            this.flpListBox.BackColor = System.Drawing.SystemColors.Window;
            this.flpListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpListBox.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpListBox.Location = new System.Drawing.Point(0, 0);
            this.flpListBox.Margin = new System.Windows.Forms.Padding(0);
            this.flpListBox.Name = "flpListBox";
            this.flpListBox.Size = new System.Drawing.Size(148, 148);
            this.flpListBox.TabIndex = 0;
            this.flpListBox.WrapContents = false;
            this.flpListBox.Layout += new System.Windows.Forms.LayoutEventHandler(this.flpListBox_Layout);
            // 
            // ListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flpListBox);
            this.Name = "ListControl";
            this.Size = new System.Drawing.Size(148, 148);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.FlowLayoutPanel flpListBox;

    }
}
