namespace mbunayd4;

public partial class vDespLiegaDatos : ContentPage
{
    private static string RutaArchivo =>
        Path.Combine(FileSystem.AppDataDirectory, "contacto.txt");

    public vDespLiegaDatos(string tipo, string cedula, string ruc, string pasaporte, string nombres, string apellidos, string fecha, string correo, string salario)
    {
        InitializeComponent();

        lblTipo.Text = $"Tipo de contacto: {tipo}";
        lblCedula.Text = $"Cédula: {cedula}";
        lblRuc.Text = $"RUC: {ruc}";
        lblPasaporte.Text = $"Pasaporte: {pasaporte}";
        lblNombres.Text = $"Nombres: {nombres}";
        lblApellidos.Text = $"Apellidos: {apellidos}";
        lblFecha.Text = $"Fecha: {fecha}";
        lblCorreo.Text = $"Correo: {correo}";
        lblSalario.Text = $"Salario: {salario}";

        if (decimal.TryParse(salario, out decimal sueldo))
        {
            decimal aporte = sueldo * 0.0945m;
            lblAporteIess.Text = $"Aporte IESS: {aporte:F2}";
        }
        else
        {
            lblAporteIess.Text = "Aporte IESS: 0.00";
        }
    }

    private async void OnGuardarTxtClicked(object? sender, EventArgs e)
    {
        string contenido = string.Join(Environment.NewLine,
            lblTipo.Text,
            lblCedula.Text,
            lblRuc.Text,
            lblPasaporte.Text,
            lblNombres.Text,
            lblApellidos.Text,
            lblFecha.Text,
            lblCorreo.Text,
            lblSalario.Text,
            lblAporteIess.Text);

        try
        {
            File.WriteAllText(RutaArchivo, contenido);

            bool abierto = await Launcher.Default.OpenAsync(new OpenFileRequest
            {
                Title = "contacto.txt",
                File = new ReadOnlyFile(RutaArchivo)
            });

            if (!abierto)
            {
                await DisplayAlertAsync("Archivo guardado", $"El archivo se guardó, pero no hay una aplicación para abrirlo.\n{RutaArchivo}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }
}
