using System.Windows;

namespace ElementPlusMessageWPF;

public partial class Window1 : Window {
    public Window1() {
        InitializeComponent();
    }

    private void Error(object sender, RoutedEventArgs e) {
        ElMessage.Wpf.Utils.ElMessage.Error("123");
    }
}