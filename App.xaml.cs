using Microsoft.Extensions.DependencyInjection;

namespace mbunayd4
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new VContacto()));
        }
    }
}