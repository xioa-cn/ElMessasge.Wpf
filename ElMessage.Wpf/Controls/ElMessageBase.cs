using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ElMessage.Wpf.Utils;

namespace ElMessage.Wpf.Controls;

public class ElMessageBase : UserControl {
    private static Panel? GetOrCreateContainer(Window targetWindow) {
        // 检查窗口是否已经有消息容器
        var existingContainer = targetWindow.Content as Panel;
        if (existingContainer != null)
        {
            var existing = existingContainer.Children.OfType<StackPanel>()
                .FirstOrDefault(p => p.Tag as string == "ElMessageContainer");
            if (existing != null)
            {
                return existing;
            }
        }

        // 创建新的消息容器
        var container = new StackPanel {
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 20, 0, 0),
            Tag = "ElMessageContainer"
        };

        // 如果窗口内容不是Panel，创建一个Grid来包装原始内容
        if (existingContainer == null)
        {
            var originalContent = targetWindow.Content;
            var grid = new Grid();

            if (originalContent != null)
            {
                grid.Children.Add(originalContent as UIElement);
            }

            grid.Children.Add(container);
            targetWindow.Content = grid;
            Panel.SetZIndex(container, 9999);
        }
        else
        {
            existingContainer.Children.Add(container);
            Panel.SetZIndex(container, 9999);
        }

        return container;
    }


    public static void Show(string message, MessageType type = MessageType.Info, int duration = Utils.Utils.DEFAULT_DURATION) {
        Application.Current.Dispatcher.Invoke(() =>
        {
            // 获取当前活动窗口
            var activeWindow = Application.Current.Windows.OfType<Window>()
                .FirstOrDefault(w => w.IsActive) ?? Application.Current.MainWindow;

            if (activeWindow == null) return;

            // 获取或创建消息容器
            var container = GetOrCreateContainer(activeWindow);

            var messageControl = new ElMessageBase();
            var border = new Border {
                Background = Utils.Utils.TypeColors[type].Background,
                BorderThickness = new Thickness(0),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(15),
                Margin = new Thickness(0, 0, 0, 10),
                Effect = new System.Windows.Media.Effects.DropShadowEffect {
                    BlurRadius = 5,
                    ShadowDepth = 0,
                    Opacity = 0.1
                },
                RenderTransform = new TranslateTransform(0, -20) // 初始位置在上方20像素
            };

            var stackPanel = new StackPanel {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0)
            };

            // 添加图标
            var iconPath = new Path {
                Data = Geometry.Parse(Utils.Utils.TypeIcons[type]),
                Width = 16,
                Height = 16,
                Stretch = Stretch.Uniform,
                Fill = Utils.Utils.TypeColors[type].Foreground,
                Margin = new Thickness(0, 0, 8, 0),
                VerticalAlignment = VerticalAlignment.Center
            };

            stackPanel.Children.Add(iconPath);

            // 添加消息文本
            var messageText = new TextBlock {
                Text = message,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 300,
                Foreground = Utils.Utils.TypeColors[type].Foreground
            };

            stackPanel.Children.Add(messageText);
            border.Child = stackPanel;
            messageControl.Content = border;

            container.Children.Add(messageControl);

            // 创建动画故事板
            var showStoryboard = new Storyboard();

            // 淡入动画
            var fadeIn = new DoubleAnimation {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(fadeIn, messageControl);
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath(OpacityProperty));

            // 位移动画
            var slideIn = new DoubleAnimation {
                From = -20,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(slideIn, border);
            Storyboard.SetTargetProperty(slideIn,
                new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));

            showStoryboard.Children.Add(fadeIn);
            showStoryboard.Children.Add(slideIn);
            showStoryboard.Begin();

            // 设置定时器移除消息
            var timer = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(duration)
            };

            timer.Tick += (s, e) =>
            {
                timer.Stop();

                var hideStoryboard = new Storyboard();

                // 淡出动画
                var fadeOut = new DoubleAnimation {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromMilliseconds(300),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                };
                Storyboard.SetTarget(fadeOut, messageControl);
                Storyboard.SetTargetProperty(fadeOut, new PropertyPath(OpacityProperty));

                // 位移动画
                var slideOut = new DoubleAnimation {
                    From = 0,
                    To = -20,
                    Duration = TimeSpan.FromMilliseconds(300),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                };
                Storyboard.SetTarget(slideOut, border);
                Storyboard.SetTargetProperty(slideOut,
                    new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));

                hideStoryboard.Children.Add(fadeOut);
                hideStoryboard.Children.Add(slideOut);
                hideStoryboard.Completed += (s2, e2) => { container.Children.Remove(messageControl); };
                hideStoryboard.Begin();
            };

            timer.Start();
        });
    }
}