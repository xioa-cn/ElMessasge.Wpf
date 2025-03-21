namespace ElMessage.Wpf.Utils;

public static class ElMessage {
    public static void Success(string message, int duration = Utils.DEFAULT_DURATION)
        => Controls.ElMessageBase.Show(message, MessageType.Success, duration);

    public static void Warning(string message, int duration = Utils.DEFAULT_DURATION)
        => Controls.ElMessageBase.Show(message, MessageType.Warning, duration);

    public static void Error(string message, int duration = Utils.DEFAULT_DURATION)
        => Controls.ElMessageBase.Show(message, MessageType.Error, duration);

    public static void Info(string message, int duration = Utils.DEFAULT_DURATION)
        => Controls.ElMessageBase.Show(message, MessageType.Info, duration);
}