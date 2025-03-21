using System.Windows.Media;

namespace ElMessage.Wpf.Utils;
public enum MessageType
{
    Success,
    Warning,
    Info,
    Error
}
public static class Utils {
    public const int DEFAULT_DURATION = 3000; // 默认显示时间3秒
    public static readonly Dictionary<MessageType, (SolidColorBrush Foreground, SolidColorBrush Background)> TypeColors = new()
    {
        { 
            MessageType.Success, 
            (
                new SolidColorBrush(Color.FromRgb(103, 194, 58)),   // 亮绿色文字
                new SolidColorBrush(Color.FromRgb(45, 57, 34))      // 深绿色背景
            )
        },
        { 
            MessageType.Warning, 
            (
                new SolidColorBrush(Color.FromRgb(230, 162, 60)),   // 橙色文字
                new SolidColorBrush(Color.FromRgb(58, 47, 31))      // 深棕色背景
            )
        },
        { 
            MessageType.Info, 
            (
                new SolidColorBrush(Color.FromRgb(144, 147, 153)),  // 灰色文字
                new SolidColorBrush(Color.FromRgb(42, 42, 42))      // 深灰色背景
            )
        },
        { 
            MessageType.Error, 
            (
                new SolidColorBrush(Color.FromRgb(245, 108, 108)),  // 红色文字
                new SolidColorBrush(Color.FromRgb(58, 34, 34))      // 深红色背景
            )
        }
    };

    public static readonly Dictionary<MessageType, string> TypeIcons = new()
    {
        { MessageType.Success, "M12 21.6C17.3019 21.6 21.6 17.3019 21.6 12C21.6 6.69807 17.3019 2.4 12 2.4C6.69807 2.4 2.4 6.69807 2.4 12C2.4 17.3019 6.69807 21.6 12 21.6ZM11.0182 15.7683L16.7443 9.91831L15.8557 9.08169L10.9818 14.0317L8.14434 11.3183L7.25566 12.1817L11.0182 15.7683Z" },
        { MessageType.Warning, "M12 21.6C17.3019 21.6 21.6 17.3019 21.6 12C21.6 6.69807 17.3019 2.4 12 2.4C6.69807 2.4 2.4 6.69807 2.4 12C2.4 17.3019 6.69807 21.6 12 21.6ZM11.4 13.2V7.2H12.6V13.2H11.4ZM11.4 16.8V15.6H12.6V16.8H11.4Z" },
        { MessageType.Info, "M12 21.6C17.3019 21.6 21.6 17.3019 21.6 12C21.6 6.69807 17.3019 2.4 12 2.4C6.69807 2.4 2.4 6.69807 2.4 12C2.4 17.3019 6.69807 21.6 12 21.6ZM11.4 16.8V10.8H12.6V16.8H11.4ZM11.4 8.4V7.2H12.6V8.4H11.4Z" },
        { MessageType.Error, "M12 21.6C17.3019 21.6 21.6 17.3019 21.6 12C21.6 6.69807 17.3019 2.4 12 2.4C6.69807 2.4 2.4 6.69807 2.4 12C2.4 17.3019 6.69807 21.6 12 21.6ZM8.70711 16.7071L12 13.4142L15.2929 16.7071L16.7071 15.2929L13.4142 12L16.7071 8.70711L15.2929 7.29289L12 10.5858L8.70711 7.29289L7.29289 8.70711L10.5858 12L7.29289 15.2929L8.70711 16.7071Z" }
    };
}