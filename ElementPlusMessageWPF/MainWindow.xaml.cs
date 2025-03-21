using System.Windows;


namespace ElementPlusMessageWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
        ElMessage.Wpf.Utils.ElMessage.Error("Error");
    }

    private void Success(object sender, RoutedEventArgs e) {
        ElMessage.Wpf.Utils.ElMessage.Success("Success");
    }

    private void Info(object sender, RoutedEventArgs e) {
        ElMessage.Wpf.Utils.ElMessage.Info("Info");
    }

    private void Warning(object sender, RoutedEventArgs e) {
        ElMessage.Wpf.Utils.ElMessage.Warning("Warning");
    }

    private void NewWindow(object sender, RoutedEventArgs e) {
        Window1 window1 = new Window1();
        window1.Show();
    }
}