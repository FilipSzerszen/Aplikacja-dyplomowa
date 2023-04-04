
namespace Aplikacja_dyplomowa
{
    partial class Form5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            this.label1 = new System.Windows.Forms.Label();
            this.nazwaPliku = new System.Windows.Forms.TextBox();
            this.ZapiszDoKalendarza = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Podaj nazwę pliku:";
            // 
            // nazwaPliku
            // 
            this.nazwaPliku.Location = new System.Drawing.Point(6, 28);
            this.nazwaPliku.Name = "nazwaPliku";
            this.nazwaPliku.Size = new System.Drawing.Size(270, 20);
            this.nazwaPliku.TabIndex = 1;
            this.nazwaPliku.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nazwaPliku_KeyUp);
            // 
            // ZapiszDoKalendarza
            // 
            this.ZapiszDoKalendarza.Enabled = false;
            this.ZapiszDoKalendarza.Location = new System.Drawing.Point(104, 54);
            this.ZapiszDoKalendarza.Name = "ZapiszDoKalendarza";
            this.ZapiszDoKalendarza.Size = new System.Drawing.Size(75, 23);
            this.ZapiszDoKalendarza.TabIndex = 3;
            this.ZapiszDoKalendarza.Text = "Zapisz";
            this.ZapiszDoKalendarza.UseVisualStyleBackColor = true;
            this.ZapiszDoKalendarza.Click += new System.EventHandler(this.ZapiszDoKalendarza_Click);
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 86);
            this.Controls.Add(this.ZapiszDoKalendarza);
            this.Controls.Add(this.nazwaPliku);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form5";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Nazwa pliku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nazwaPliku;
        private System.Windows.Forms.Button ZapiszDoKalendarza;
    }
}