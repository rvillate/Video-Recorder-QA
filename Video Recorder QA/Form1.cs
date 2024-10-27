using System;
using System.IO;
using System.Windows.Forms;
using ScreenRecorderLib;
using System.Threading.Tasks;
using System.Timers;
using System.Management;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;



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
        int freeSpaceMB;

        private string rutaGrabacion = "C:\\Users\\DevraYa\\Videos\\Grabaciones";

        private bool mensajeMostrado = false;
        private Point startPoint; // Para almacenar el punto inicial del mouse
        private bool dragging = false; // Para saber si estamos arrastrando

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
            CargarVideosEnListView();
        }

        private void ObtenerEspacioLibreEnMB(string ruta)
        {
            string root = Path.GetPathRoot(ruta);

            DriveInfo driveInfo = new DriveInfo(root);

            long freeSpaceBytes = driveInfo.AvailableFreeSpace;

            // Convertir el espacio libre a MB
            freeSpaceMB = (int)(freeSpaceBytes / (1024 * 1024));

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
            CargarVideosEnListView();
        }



        private async void timer1_Tick(object sender, EventArgs e)
        {
            ObtenerEspacioLibreEnMB(txt_ruta_grabacion.Text);

            if (chk_memoria_llena.Checked == true)
            {
                if (freeSpaceMB < 500 && !mensajeMostrado)
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


        private void CargarVideosEnListView()
        {
            // Limpiar el ListView antes de agregar nuevos elementos
            listView1.Items.Clear();

            // Obtener todos los archivos .mp4 en la carpeta
            string[] archivos = Directory.GetFiles(txt_ruta_grabacion.Text, "*.mp4");

            // Obtener los FileInfo de los archivos y ordenarlos por fecha de creación de forma descendente
            var archivosOrdenados = archivos
                .Select(archivo => new FileInfo(archivo))
                .OrderByDescending(file => file.CreationTime)
                .ToList();

            // Recorrer cada archivo encontrado y agregarlo al ListView
            foreach (FileInfo file in archivosOrdenados)
            {
                // Crear un nuevo item en el ListView con el nombre del archivo
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add(GetVideoDuration(file.FullName)); // Método que ya deberías tener para obtener la duración
                item.SubItems.Add(file.CreationTime.ToString("dd/MM/yy hh:mm:ss"));
                listView1.Items.Add(item);
            }
        }


        public static string GetVideoDuration(string filePath)
        {
            using (ShellPropertyCollection properties = new ShellPropertyCollection(filePath))
            {
                foreach (IShellProperty prop in properties)
                {
                    if (prop.CanonicalName == "System.Media.Duration")
                    {
                        var durationInTicks = (ulong)prop.ValueAsObject;
                        TimeSpan duration = TimeSpan.FromTicks((long)durationInTicks);

                        string formattedDuration = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            duration.Hours,
                            duration.Minutes,
                            duration.Seconds);

                        return formattedDuration;
                    }
                }
            }
            return "00:00:00";
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            // Almacenar la posición inicial del mouse
            startPoint = e.Location;
            dragging = false; // Inicialmente no estamos arrastrando

            
        }
       
        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            // Reiniciar la bandera de arrastre cuando se suelta el mouse
            dragging = false;
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            // Verificar si se está arrastrando
            if (e.Button == MouseButtons.Left)
            {
                // Calcular la distancia desde el punto de inicio
                if (!dragging &&
                    (Math.Abs(e.X - startPoint.X) > SystemInformation.DragSize.Width ||
                     Math.Abs(e.Y - startPoint.Y) > SystemInformation.DragSize.Height))
                {
                    dragging = true; // Ahora estamos arrastrando

                    // Obtener el elemento que está debajo del cursor al iniciar el arrastre
                    ListViewItem item = listView1.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {
                        string nombreArchivo = item.Text;
                        string rutaArchivo = Path.Combine(txt_ruta_grabacion.Text, nombreArchivo);

                        // Verificar si el archivo existe
                        if (File.Exists(rutaArchivo))
                        {
                            // Crear un objeto DataObject solo para este archivo
                            DataObject data = new DataObject(DataFormats.FileDrop, new string[] { rutaArchivo });

                            // Iniciar la operación de arrastre para este archivo
                            DragDropEffects efecto = listView1.DoDragDrop(data, DragDropEffects.Copy);
                        }
                        else
                        {
                            MessageBox.Show("El archivo no existe: " + rutaArchivo);
                        }
                    }
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            // Obtener el item seleccionado
            if (listView1.SelectedItems.Count > 0)
            {
                // Habilitar la edición de la celda
                listView1.SelectedItems[0].BeginEdit();
            }
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != null) // Asegurarse de que el nombre no sea nulo
            {
                // Obtener el nuevo nombre del archivo
                string nuevoNombre = e.Label;
                string rutaActual = Path.Combine(txt_ruta_grabacion.Text, listView1.Items[e.Item].Text);
                string nuevaRuta = Path.Combine(txt_ruta_grabacion.Text, nuevoNombre);

                // Renombrar el archivo en el sistema de archivos
                if (File.Exists(rutaActual))
                {
                    try
                    {
                        File.Move(rutaActual, nuevaRuta); // Renombrar el archivo
                        e.CancelEdit = false; // Aceptar el cambio
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al renombrar el archivo: " + ex.Message);
                        e.CancelEdit = true; // Cancelar el cambio si hay un error
                    }
                }
                else
                {
                    MessageBox.Show("El archivo no existe.");
                    e.CancelEdit = true; // Cancelar el cambio si el archivo no existe
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
