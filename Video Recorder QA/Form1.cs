using System;
using System.IO;
using System.Windows.Forms;
using ScreenRecorderLib;
using System.Threading.Tasks;
using System.Timers;
using System.Management;

namespace Video_Recorder_QA
{
    public partial class Form1 : Form
    {
        private Recorder _rec;
        private int _sequenceId = 1;
        private System.Timers.Timer _timer;
        private bool _isRecording = false;

        int espacioEnGB;
        int espacioEnMB;

        private TimeSpan _totalTiempoGrabado = TimeSpan.Zero;

        private string rutaGrabacion = "C:\\Users\\DevraYa\\Videos\\Grabaciones";

        private System.Timers.Timer _tiempoGrabadoTimer;

        private bool mensajeMostrado = false;


        public Form1()
        {
            InitializeComponent();
        }

        private async void btn_inicia_grabacion_Click(object sender, EventArgs e)
        {
            if (btn_inicia_grabacion.Text.Equals("Iniciar grabación"))
            {
                ObtenerEspacioLibreEnMB("C:\\");
                if (espacioEnGB * 1024 + espacioEnMB < 512)
                {
                    DialogResult result = MessageBox.Show(@"Poco espacio en disco C:\. Este disco se usa para guardar un archivo temporal de grabación, ¿seguro que desea continuar?", "Poco espacio en disco", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                // Iniciar la grabación
                CreateRecording();
                btn_inicia_grabacion.Text = "Detener grabación";
            }
            else if (btn_inicia_grabacion.Text == "Detener grabación")
            {
                // Detener la grabación cuando se detenga manualmente
                await EndRecordingAsync();
                btn_inicia_grabacion.Text = "Iniciar grabación";
            }
        }

        private void ObtenerEspacioLibreEnMB(string ruta)
        {
            string root = Path.GetPathRoot(ruta);

            DriveInfo driveInfo = new DriveInfo(root);

            long freeSpaceBytes = driveInfo.AvailableFreeSpace;

            // Convertir el espacio libre a MB
            int freeSpaceMB = (int)(freeSpaceBytes / (1024 * 1024));

            espacioEnGB = Convert.ToInt32(freeSpaceMB / 1024);
            espacioEnMB = Math.Abs((espacioEnGB * 1024) - freeSpaceMB);
            espacio_libre.Text = $"Espacio libre en disco {espacioEnGB} GB - {espacioEnMB} MB";

        }
       
        void CreateRecording()
        {
            _isRecording = true;
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string videoFilePath = Path.Combine(txt_ruta_grabacion.Text, $"{timestamp}_seq{_sequenceId}.mp4"); // Ruta del archivo con el nombre dinámico

            _rec = Recorder.CreateRecorder(new RecorderOptions
            {
                AudioOptions = new AudioOptions
                {
                    IsAudioEnabled = chk_audio_mic.Checked == true || chk_audio_sistema.Checked == true ? true : false,
                    IsInputDeviceEnabled = chk_audio_mic.Checked,
                    IsOutputDeviceEnabled = chk_audio_sistema.Checked
                }
            });
            //_rec.OnRecordingComplete += Rec_OnRecordingComplete;
            _rec.OnRecordingFailed += Rec_OnRecordingFailed;
            _rec.OnStatusChanged += Rec_OnStatusChanged;

            _rec.Record(videoFilePath);

            _sequenceId++;

        }

        async Task EndRecordingAsync()
        {
            if (_rec != null && _isRecording)
            {
                await Task.Run(() => _rec.Stop());
                _isRecording = false;
            }
        }

        private void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            string error = e.Error;
            Console.WriteLine($"Error en la grabación: {error}");
        }

        private void Rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            RecorderStatus status = e.Status;
            Console.WriteLine($"Estado de la grabación: {status}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            txt_ruta_grabacion.Text = rutaGrabacion;
        }



        private async void timer1_Tick(object sender, EventArgs e)
        {
            ObtenerEspacioLibreEnMB(txt_ruta_grabacion.Text);

            if (chk_memoria_llena.Checked == true)
            {
                if (espacioEnMB < 500 && !mensajeMostrado)
                {
                    DialogResult result = MessageBox.Show("El disco donde se están guardando los videos tiene menos de 500 MB de memoria, ¿desea continuar? Se deshabilitará el check de avisar memoria llena; en caso contrario, se detendrá el video.", "Memoria llena", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    mensajeMostrado = true;

                    if (result == DialogResult.Yes)
                    {
                        chk_memoria_llena.Checked = false;
                    }
                    else if (result == DialogResult.No)
                    {
                        await EndRecordingAsync();
                        btn_inicia_grabacion.Text = "Iniciar grabación";
                    }
                }
                else if (espacioEnMB >= 500)
                {
                    mensajeMostrado = false;
                }
            }
        }

        private async void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_isRecording)
            {
                // Detener la grabación actual
                await EndRecordingAsync();

                // Pausar brevemente para asegurar que el archivo se guarda correctamente
                await Task.Delay(1000);
            }
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isRecording)
            {
                // Detener la grabación actual
                await EndRecordingAsync();

                // Pausar brevemente para asegurar que el archivo se guarda correctamente
                await Task.Delay(1000);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_ruta_grabacion.Text = folder.SelectedPath;
            }
        }
    }
}
