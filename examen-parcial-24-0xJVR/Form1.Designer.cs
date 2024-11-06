namespace examen_parcial_24_0xJVR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            seatsLabel = new Label();
            seatsNumericUpDown = new NumericUpDown();
            requestButton = new Button();
            ((System.ComponentModel.ISupportInitialize)seatsNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // seatsLabel
            // 
            seatsLabel.AutoSize = true;
            seatsLabel.Location = new Point(81, 89);
            seatsLabel.Name = "seatsLabel";
            seatsLabel.Size = new Size(69, 15);
            seatsLabel.TabIndex = 0;
            seatsLabel.Text = "N. Personas";
            // 
            // seatsNumericUpDown
            // 
            seatsNumericUpDown.Location = new Point(81, 107);
            seatsNumericUpDown.Maximum = new decimal(new int[] { 150, 0, 0, 0 });
            seatsNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            seatsNumericUpDown.Name = "seatsNumericUpDown";
            seatsNumericUpDown.Size = new Size(120, 23);
            seatsNumericUpDown.TabIndex = 1;
            seatsNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // requestButton
            // 
            requestButton.Location = new Point(97, 136);
            requestButton.Name = "requestButton";
            requestButton.Size = new Size(75, 23);
            requestButton.TabIndex = 2;
            requestButton.Text = "Pedir";
            requestButton.UseVisualStyleBackColor = true;
            requestButton.Click += requestButton_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(284, 261);
            Controls.Add(requestButton);
            Controls.Add(seatsNumericUpDown);
            Controls.Add(seatsLabel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Tickets Cine";
            ((System.ComponentModel.ISupportInitialize)seatsNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Label label1;
        private TextBox textBox1;
        private Label seatsLabel;
        private NumericUpDown seatsNumericUpDown;
        private Button requestButton;
    }
}
