namespace myspace
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_start = new System.Windows.Forms.Button();
            this.tb_time = new System.Windows.Forms.TextBox();
            this.lb_time = new System.Windows.Forms.Label();
            this.lb_status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bt_start
            // 
            this.bt_start.Location = new System.Drawing.Point(12, 70);
            this.bt_start.Name = "bt_start";
            this.bt_start.Size = new System.Drawing.Size(260, 50);
            this.bt_start.TabIndex = 3;
            this.bt_start.Text = "GET started";
            this.bt_start.UseVisualStyleBackColor = true;
            this.bt_start.Click += new System.EventHandler(this.bt_start_Click);
            // 
            // tb_time
            // 
            this.tb_time.Location = new System.Drawing.Point(12, 30);
            this.tb_time.Multiline = true;
            this.tb_time.Name = "tb_time";
            this.tb_time.Size = new System.Drawing.Size(260, 20);
            this.tb_time.TabIndex = 2;
            // 
            // lb_time
            // 
            this.lb_time.AutoSize = true;
            this.lb_time.Location = new System.Drawing.Point(12, 12);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(260, 17);
            this.lb_time.TabIndex = 6;
            this.lb_time.Text = "Waiting time to open the model (minutes):";
            // 
            // lb_status
            // 
            this.lb_status.AutoSize = true;
            //this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_status.Location = new System.Drawing.Point(12, 50);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(20, 15);
            this.lb_status.TabIndex = 7;
            this.lb_status.Text = ".cs";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.lb_time);
            this.Controls.Add(this.lb_status);
            this.Controls.Add(this.tb_time);
            this.Controls.Add(this.bt_start);
            this.Name = "Form1";
            this.Text = "Sharing model downloading";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button bt_start;
        private System.Windows.Forms.TextBox tb_time;
        private System.Windows.Forms.Label lb_time;
        private System.Windows.Forms.Label lb_status;
    }
}