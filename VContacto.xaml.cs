namespace mbunayd4;

public partial class VContacto : ContentPage
{
    public VContacto()
    {
        InitializeComponent();

        pickerTipo.ItemsSource = new List<string>
        {
            "Cédula",
            "RUC",
            "Pasaporte"
        };



        // Aquí ya funcionará sin error:
        dpFechaNacimiento.MaximumDate = DateTime.Today;
        dpFechaNacimiento.Date = DateTime.Today.AddYears(-18);
    }

    private void OnTipoSeleccionado(object? sender, EventArgs e)
    {
        txt_cedula.IsEnabled = false;
        txt_ruc.IsEnabled = false;
        txt_pasaporte.IsEnabled = false;

        txt_cedula.Text = string.Empty;
        txt_ruc.Text = string.Empty;
        txt_pasaporte.Text = string.Empty;

        switch (pickerTipo.SelectedItem?.ToString())
        {
            case "Cédula":
                txt_cedula.IsEnabled = true;
                break;
            case "RUC":
                txt_ruc.IsEnabled = true;
                break;
            case "Pasaporte":
                txt_pasaporte.IsEnabled = true;
                break;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string tipo = pickerTipo.SelectedItem?.ToString() ?? "No seleccionado";

        await Navigation.PushAsync(new vDespLiegaDatos(
            tipo,
            txt_cedula.Text ?? string.Empty,
            txt_ruc.Text ?? string.Empty,
            txt_pasaporte.Text ?? string.Empty,
            txt_nombres.Text ?? string.Empty,
            txt_apellidos.Text ?? string.Empty,
            $"{dpFechaNacimiento.Date:dd/MM/yyyy}",
            txt_correo.Text ?? string.Empty,
            txt_salario.Text ?? string.Empty));
    }

    private void Button_Clicked_1(object? sender, EventArgs e)
    {
        pickerTipo.SelectedIndex = -1;

        txt_cedula.Text = string.Empty;
        txt_ruc.Text = string.Empty;
        txt_pasaporte.Text = string.Empty;
        txt_cedula.IsEnabled = false;
        txt_ruc.IsEnabled = false;
        txt_pasaporte.IsEnabled = false;

        txt_nombres.Text = string.Empty;
        txt_apellidos.Text = string.Empty;
        txt_correo.Text = string.Empty;
        txt_salario.Text = string.Empty;

        dpFechaNacimiento.Date = DateTime.Today.AddYears(-18);
    }
}
