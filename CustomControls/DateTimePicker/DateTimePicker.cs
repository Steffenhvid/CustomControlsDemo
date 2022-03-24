using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomControls
{
    public class DateTimePicker : Control, INotifyPropertyChanged
    {
        // For manipulation
        private DateTime temporaryDateTime = DateTime.Now.AddDays(3);
        private bool ShowSeconds = false;

        //Design Elements
        private Calendar? calendar;
        private Button? okButton;
        private Button? cancelButton;
        private Button? todayButton;
        private Button? tommorrowButton;
        private TextBox? hourTextBox;
        private TextBox? minuteTextBox;
        private TextBox? secondTextBox;
        private TextBlock? collonTextBox;

        public DateTime TemporaryDateTime 
        { 
            get => temporaryDateTime; 
            set
            {
                temporaryDateTime = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        static DateTimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimePicker), new FrameworkPropertyMetadata(typeof(DateTimePicker)));
        }

        public override void OnApplyTemplate()
        {
            LoadComponents();
            SetupEvents();
            SetupCalendar();
            SetupTimeBoxes();

            base.OnApplyTemplate();
        }

        private void SetupTimeBoxes()
        {
            if(!ShowSeconds) 
            {
                collonTextBox.Visibility = Visibility.Collapsed;
                secondTextBox.Visibility = Visibility.Collapsed;
            }

            secondTextBox.Text = TemporaryDateTime.Second.ToString();
            minuteTextBox.Text = TemporaryDateTime.Minute.ToString();
            hourTextBox.Text = TemporaryDateTime.Hour.ToString();
        }

        private void SetupEvents()
        {
            tommorrowButton.Click += TommorrowButton_Click;
            todayButton.Click += TodayButton_Click;
        }

        private void SetupCalendar()
        {
            calendar.IsTodayHighlighted = false;
            calendar.SelectedDate = TemporaryDateTime;
        }

        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            calendar.SelectedDate = DateTime.Today;
        }

        private void TommorrowButton_Click(object sender, RoutedEventArgs e)
        {
            calendar.SelectedDate = DateTime.Today.AddDays(1);
        }

        public void LoadComponents() 
        {
            calendar = Template.FindName("PART_Calendar", this) as Calendar;
            okButton = Template.FindName("PART_OkButton", this) as Button;
            cancelButton = Template.FindName("PART_CancelButton", this) as Button;
            todayButton = Template.FindName("PART_TodayButton", this) as Button;
            tommorrowButton = Template.FindName("PART_TomorrowButton", this) as Button;
            hourTextBox = Template.FindName("PART_HourTextBox", this) as TextBox;
            minuteTextBox = Template.FindName("PART_MinuteTextBox", this) as TextBox;
            secondTextBox = Template.FindName("PART_SecondTextBox", this) as TextBox;
            collonTextBox = Template.FindName("PART_CollonTextBox", this) as TextBlock;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
