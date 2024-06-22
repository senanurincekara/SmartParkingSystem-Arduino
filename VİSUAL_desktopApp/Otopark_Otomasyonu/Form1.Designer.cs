namespace Otopark_Otomasyonu
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            araba_sayisi = new Label();
            bos_alan_sayisi = new Label();
            comboBoxPortName = new ComboBox();
            port = new Label();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            labelArabaCount = new Label();
            labelEmptyParks = new Label();
            button2 = new Button();
            label2 = new Label();
            label3 = new Label();
            dataGridView2 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.BackColor = Color.HotPink;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(0, -1);
            label1.Name = "label1";
            label1.Size = new Size(800, 44);
            label1.TabIndex = 1;
            label1.Text = "SenSem OTOPARK OTOMASYONU";
            // 
            // araba_sayisi
            // 
            araba_sayisi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            araba_sayisi.AutoSize = true;
            araba_sayisi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            araba_sayisi.Location = new Point(404, 257);
            araba_sayisi.Name = "araba_sayisi";
            araba_sayisi.Size = new Size(216, 23);
            araba_sayisi.TabIndex = 2;
            araba_sayisi.Text = "Otoparktaki Araba Sayısı:";
            // 
            // bos_alan_sayisi
            // 
            bos_alan_sayisi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            bos_alan_sayisi.AutoSize = true;
            bos_alan_sayisi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            bos_alan_sayisi.Location = new Point(404, 297);
            bos_alan_sayisi.Name = "bos_alan_sayisi";
            bos_alan_sayisi.Size = new Size(134, 23);
            bos_alan_sayisi.TabIndex = 2;
            bos_alan_sayisi.Text = "Boş Alan Sayısı:";
            // 
            // comboBoxPortName
            // 
            comboBoxPortName.FormattingEnabled = true;
            comboBoxPortName.Location = new Point(512, 71);
            comboBoxPortName.Name = "comboBoxPortName";
            comboBoxPortName.Size = new Size(151, 28);
            comboBoxPortName.TabIndex = 3;
            // 
            // port
            // 
            port.AutoSize = true;
            port.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            port.Location = new Point(411, 72);
            port.Name = "port";
            port.Size = new Size(95, 23);
            port.TabIndex = 4;
            port.Text = "Port Seçin:";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(0, 192, 0);
            button1.Font = new Font("Segoe UI Emoji", 7F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(512, 105);
            button1.Name = "button1";
            button1.Size = new Size(151, 31);
            button1.TabIndex = 5;
            button1.Text = "Bağlan";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 55);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(398, 186);
            dataGridView1.TabIndex = 7;
            // 
            // labelArabaCount
            // 
            labelArabaCount.AutoSize = true;
            labelArabaCount.Location = new Point(626, 260);
            labelArabaCount.Name = "labelArabaCount";
            labelArabaCount.Size = new Size(50, 20);
            labelArabaCount.TabIndex = 9;
            labelArabaCount.Text = "label2";
            // 
            // labelEmptyParks
            // 
            labelEmptyParks.AutoSize = true;
            labelEmptyParks.Location = new Point(550, 300);
            labelEmptyParks.Name = "labelEmptyParks";
            labelEmptyParks.Size = new Size(50, 20);
            labelEmptyParks.TabIndex = 10;
            labelEmptyParks.Text = "label2";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            button2.BackColor = Color.FromArgb(192, 0, 0);
            button2.Enabled = false;
            button2.Font = new Font("Segoe UI Emoji", 7F, FontStyle.Bold);
            button2.ForeColor = Color.White;
            button2.Location = new Point(512, 142);
            button2.Name = "button2";
            button2.Size = new Size(151, 31);
            button2.TabIndex = 11;
            button2.Text = "Bağlantıyı Kes";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(608, 448);
            label2.Name = "label2";
            label2.Size = new Size(96, 20);
            label2.TabIndex = 12;
            label2.Text = "Port Durum:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Coral;
            label3.Location = new Point(701, 448);
            label3.Name = "label3";
            label3.Size = new Size(56, 20);
            label3.TabIndex = 12;
            label3.Text = "KAPALI";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(-1, 254);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(399, 211);
            dataGridView2.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 477);
            Controls.Add(dataGridView2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(labelEmptyParks);
            Controls.Add(labelArabaCount);
            Controls.Add(button1);
            Controls.Add(port);
            Controls.Add(comboBoxPortName);
            Controls.Add(bos_alan_sayisi);
            Controls.Add(araba_sayisi);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SenSem Otoparkı";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label araba_sayisi;
        private Label bos_alan_sayisi;
        private ComboBox comboBoxPortName;
        private Label port;
        private Button button1;
        private DataGridView dataGridView1;
        private Label labelArabaCount;
        private Label labelEmptyParks;
        private Button button2;
        private Label label2;
        private Label label3;
        private DataGridView dataGridView2;
    }
}
