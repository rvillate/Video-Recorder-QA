namespace Video_Recorder_QA
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
            this.components = new System.ComponentModel.Container();
            this.btn_inicia_grabacion = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_ruta_grabacion = new System.Windows.Forms.TextBox();
            this.chk_memoria_llena = new System.Windows.Forms.CheckBox();
            this.chk_audio_mic = new System.Windows.Forms.CheckBox();
            this.chk_audio_sistema = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.espacio_libre = new System.Windows.Forms.Label();
            this.txt_total_tiempo_grabado = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_inicia_grabacion
            // 
            this.btn_inicia_grabacion.Location = new System.Drawing.Point(285, 16);
            this.btn_inicia_grabacion.Name = "btn_inicia_grabacion";
            this.btn_inicia_grabacion.Size = new System.Drawing.Size(123, 23);
            this.btn_inicia_grabacion.TabIndex = 0;
            this.btn_inicia_grabacion.Text = "Iniciar grabación";
            this.btn_inicia_grabacion.UseVisualStyleBackColor = true;
            this.btn_inicia_grabacion.Click += new System.EventHandler(this.btn_inicia_grabacion_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_ruta_grabacion);
            this.groupBox1.Controls.Add(this.chk_memoria_llena);
            this.groupBox1.Controls.Add(this.chk_audio_mic);
            this.groupBox1.Controls.Add(this.chk_audio_sistema);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_inicia_grabacion);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 168);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(380, 127);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Ruta de grabación:";
            // 
            // txt_ruta_grabacion
            // 
            this.txt_ruta_grabacion.Location = new System.Drawing.Point(129, 126);
            this.txt_ruta_grabacion.Name = "txt_ruta_grabacion";
            this.txt_ruta_grabacion.Size = new System.Drawing.Size(245, 23);
            this.txt_ruta_grabacion.TabIndex = 11;
            this.txt_ruta_grabacion.Text = "C:\\Users\\DevraYa\\Videos\\Grabaciones";
            // 
            // chk_memoria_llena
            // 
            this.chk_memoria_llena.AutoSize = true;
            this.chk_memoria_llena.Checked = true;
            this.chk_memoria_llena.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_memoria_llena.Location = new System.Drawing.Point(20, 31);
            this.chk_memoria_llena.Name = "chk_memoria_llena";
            this.chk_memoria_llena.Size = new System.Drawing.Size(137, 19);
            this.chk_memoria_llena.TabIndex = 10;
            this.chk_memoria_llena.Text = "Avisar memoria llena";
            this.chk_memoria_llena.UseVisualStyleBackColor = true;
            // 
            // chk_audio_mic
            // 
            this.chk_audio_mic.AutoSize = true;
            this.chk_audio_mic.Location = new System.Drawing.Point(20, 53);
            this.chk_audio_mic.Name = "chk_audio_mic";
            this.chk_audio_mic.Size = new System.Drawing.Size(117, 19);
            this.chk_audio_mic.TabIndex = 9;
            this.chk_audio_mic.Text = "Audio microfono";
            this.chk_audio_mic.UseVisualStyleBackColor = true;
            // 
            // chk_audio_sistema
            // 
            this.chk_audio_sistema.AutoSize = true;
            this.chk_audio_sistema.Location = new System.Drawing.Point(20, 74);
            this.chk_audio_sistema.Name = "chk_audio_sistema";
            this.chk_audio_sistema.Size = new System.Drawing.Size(101, 19);
            this.chk_audio_sistema.TabIndex = 8;
            this.chk_audio_sistema.Text = "Audio sistema";
            this.chk_audio_sistema.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Grabar ultimos (s)";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(130, 96);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(64, 23);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.espacio_libre);
            this.groupBox2.Controls.Add(this.txt_total_tiempo_grabado);
            this.groupBox2.Location = new System.Drawing.Point(12, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 60);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Estado";
            // 
            // espacio_libre
            // 
            this.espacio_libre.AutoSize = true;
            this.espacio_libre.Location = new System.Drawing.Point(20, 32);
            this.espacio_libre.Name = "espacio_libre";
            this.espacio_libre.Size = new System.Drawing.Size(126, 15);
            this.espacio_libre.TabIndex = 7;
            this.espacio_libre.Text = "Espacio libre en disco: ";
            // 
            // txt_total_tiempo_grabado
            // 
            this.txt_total_tiempo_grabado.AutoSize = true;
            this.txt_total_tiempo_grabado.Location = new System.Drawing.Point(20, 14);
            this.txt_total_tiempo_grabado.Name = "txt_total_tiempo_grabado";
            this.txt_total_tiempo_grabado.Size = new System.Drawing.Size(127, 15);
            this.txt_total_tiempo_grabado.TabIndex = 5;
            this.txt_total_tiempo_grabado.Text = "Tiempo total grabado: ";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 255);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "QA - Grabación pantalla";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private Button btn_inicia_grabacion;
        private GroupBox groupBox1;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private GroupBox groupBox2;
        private Label txt_total_tiempo_grabado;
        private Label espacio_libre;
        private CheckBox chk_audio_mic;
        private CheckBox chk_audio_sistema;
        private CheckBox chk_memoria_llena;
        private Label label3;
        private TextBox txt_ruta_grabacion;
        private Button button3;
        private System.Windows.Forms.Timer timer1;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}