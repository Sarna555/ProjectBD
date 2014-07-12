using Security;

namespace Admin.View
{
    partial class Mainwindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private IUserCtx _userCtx;
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zalogujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wylogujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zakończToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.użytkownicyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajUżytkownikaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zobaczListęToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grupyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajGrupęToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zobaczListęToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.uprawnieniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajNoweUprawnienieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.użytkownicyToolStripMenuItem,
            this.grupyToolStripMenuItem,
            this.uprawnieniaToolStripMenuItem,
            this.pomocToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(491, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zalogujToolStripMenuItem,
            this.wylogujToolStripMenuItem,
            this.zakończToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // zalogujToolStripMenuItem
            // 
            this.zalogujToolStripMenuItem.Name = "zalogujToolStripMenuItem";
            this.zalogujToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.zalogujToolStripMenuItem.Text = "Zaloguj";
            this.zalogujToolStripMenuItem.Click += new System.EventHandler(this.zalogujToolStripMenuItem_Click);
            // 
            // wylogujToolStripMenuItem
            // 
            this.wylogujToolStripMenuItem.Name = "wylogujToolStripMenuItem";
            this.wylogujToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.wylogujToolStripMenuItem.Text = "Wyloguj";
            // 
            // zakończToolStripMenuItem
            // 
            this.zakończToolStripMenuItem.Name = "zakończToolStripMenuItem";
            this.zakończToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.zakończToolStripMenuItem.Text = "Zakończ";
            this.zakończToolStripMenuItem.Click += new System.EventHandler(this.zakończToolStripMenuItem_Click);
            // 
            // użytkownicyToolStripMenuItem
            // 
            this.użytkownicyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dodajUżytkownikaToolStripMenuItem,
            this.zobaczListęToolStripMenuItem});
            this.użytkownicyToolStripMenuItem.Name = "użytkownicyToolStripMenuItem";
            this.użytkownicyToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.użytkownicyToolStripMenuItem.Text = "Użytkownicy";
            // 
            // dodajUżytkownikaToolStripMenuItem
            // 
            this.dodajUżytkownikaToolStripMenuItem.Name = "dodajUżytkownikaToolStripMenuItem";
            this.dodajUżytkownikaToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.dodajUżytkownikaToolStripMenuItem.Text = "Dodaj użytkownika";
            this.dodajUżytkownikaToolStripMenuItem.Click += new System.EventHandler(this.dodajUżytkownikaToolStripMenuItem_Click);
            // 
            // zobaczListęToolStripMenuItem
            // 
            this.zobaczListęToolStripMenuItem.Name = "zobaczListęToolStripMenuItem";
            this.zobaczListęToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.zobaczListęToolStripMenuItem.Text = "Zobacz listę ";
            this.zobaczListęToolStripMenuItem.Click += new System.EventHandler(this.zobaczListęToolStripMenuItem_Click);
            // 
            // grupyToolStripMenuItem
            // 
            this.grupyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dodajGrupęToolStripMenuItem,
            this.zobaczListęToolStripMenuItem1});
            this.grupyToolStripMenuItem.Name = "grupyToolStripMenuItem";
            this.grupyToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.grupyToolStripMenuItem.Text = "Grupy";
            // 
            // dodajGrupęToolStripMenuItem
            // 
            this.dodajGrupęToolStripMenuItem.Name = "dodajGrupęToolStripMenuItem";
            this.dodajGrupęToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dodajGrupęToolStripMenuItem.Text = "Dodaj grupę";
            this.dodajGrupęToolStripMenuItem.Click += new System.EventHandler(this.dodajGrupęToolStripMenuItem_Click);
            // 
            // zobaczListęToolStripMenuItem1
            // 
            this.zobaczListęToolStripMenuItem1.Name = "zobaczListęToolStripMenuItem1";
            this.zobaczListęToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.zobaczListęToolStripMenuItem1.Text = "Zobacz listę";
            this.zobaczListęToolStripMenuItem1.Click += new System.EventHandler(this.zobaczListęToolStripMenuItem1_Click);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oProgramieToolStripMenuItem});
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            // 
            // oProgramieToolStripMenuItem
            // 
            this.oProgramieToolStripMenuItem.Name = "oProgramieToolStripMenuItem";
            this.oProgramieToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.oProgramieToolStripMenuItem.Text = "O programie";
            this.oProgramieToolStripMenuItem.Click += new System.EventHandler(this.oProgramieToolStripMenuItem_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(13, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(150, 199);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(170, 28);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(150, 199);
            this.checkedListBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 233);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Modyfikuj zaznaczenie";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(170, 233);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Usuń zaznaczenie";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(327, 28);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(150, 199);
            this.checkedListBox2.TabIndex = 5;
            // 
            // uprawnieniaToolStripMenuItem
            // 
            this.uprawnieniaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dodajNoweUprawnienieToolStripMenuItem});
            this.uprawnieniaToolStripMenuItem.Name = "uprawnieniaToolStripMenuItem";
            this.uprawnieniaToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.uprawnieniaToolStripMenuItem.Text = "Uprawnienia";
            // 
            // dodajNoweUprawnienieToolStripMenuItem
            // 
            this.dodajNoweUprawnienieToolStripMenuItem.Name = "dodajNoweUprawnienieToolStripMenuItem";
            this.dodajNoweUprawnienieToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.dodajNoweUprawnienieToolStripMenuItem.Text = "Dodaj nowe uprawnienie";
            this.dodajNoweUprawnienieToolStripMenuItem.Click += new System.EventHandler(this.dodajNoweUprawnienieToolStripMenuItem_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(327, 233);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(152, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Zapisz zmiany";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Mainwindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 327);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.checkedListBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(348, 365);
            this.Name = "Mainwindow";
            this.Text = "Security Admnistrator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zalogujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wylogujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zakończToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem użytkownicyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajUżytkownikaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zobaczListęToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grupyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajGrupęToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zobaczListęToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oProgramieToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem uprawnieniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajNoweUprawnienieToolStripMenuItem;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.Button button3;

      

    }
}

