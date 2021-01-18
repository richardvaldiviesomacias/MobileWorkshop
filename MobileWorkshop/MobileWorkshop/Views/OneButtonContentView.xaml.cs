using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileWorkshop.Views
{
    [ContentProperty("MainContent")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OneButtonContentView : ContentView
    {
        public static readonly BindableProperty ContinueButtonColorProperty = BindableProperty.Create("ContinueButtonColor", typeof(Color), typeof(Color), defaultBindingMode:BindingMode.TwoWay, propertyChanged: ContinueButtonColorChanged);
        public static readonly BindableProperty ContinueButtonTextColorProperty = BindableProperty.Create("ContinueButtonTextColor", typeof(Color), typeof(Color), propertyChanged: ContinueButtonTextColorChanged);
        public static readonly BindableProperty ContinueButtonSurroundColorProperty = BindableProperty.Create("ContinueButtonSurroundColor", typeof(Color), typeof(Color), propertyChanged: ContinueButtonSurroundColorChanged);
        public static readonly BindableProperty ContinueButtonTextProperty = BindableProperty.Create("ContinueButtonText", typeof(string), typeof(string), propertyChanged: ContinueButtonTextChanged);
        public static readonly BindableProperty IsBudgetSummaryVisibleProperty = BindableProperty.Create("IsBudgetSummaryVisible", typeof(bool), typeof(bool), propertyChanged: IsBudgetSummaryVisibleChanged);

        public Action ContinueButtonAction { get; set; } = () => throw new NotImplementedException();
        public Button ContinueButton => continueButton;

        public View MainContent
        {
            get { return mainContent.Content; }
            set { mainContent.Content = value; }
        }
        
        public OneButtonContentView()
        {
            InitializeComponent();
        }
        
        public Color ContinueButtonColor
        {
            get => (Color)GetValue(ContinueButtonColorProperty);            
            set => SetValue(ContinueButtonColorProperty, value);
        }

        public Color ContinueButtonTextColor
        {
            get => (Color)GetValue(ContinueButtonTextColorProperty);
            set => SetValue(ContinueButtonTextColorProperty, value);
        }

        public Color ContinueButtonSurroundColor
        {
            get => (Color)GetValue(ContinueButtonSurroundColorProperty);
            set => SetValue(ContinueButtonSurroundColorProperty, value);
        }


        public string ContinueButtonText
        {
            get => (string)GetValue(ContinueButtonTextProperty);
            set => SetValue(ContinueButtonTextProperty, value); 
        }

        public bool IsBudgetSummaryVisible
        {
            get => (bool)GetValue(IsBudgetSummaryVisibleProperty);
            set => SetValue(IsBudgetSummaryVisibleProperty, value);
        }
        
        #region static binding property changes handlers

        private static void ContinueButtonColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (OneButtonContentView)bindable;
            view.continueButton.BackgroundColor = (Color)newValue;
        }

        private static void ContinueButtonTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (OneButtonContentView)bindable;
            view.continueButton.TextColor = (Color)newValue;
        }

        private static void ContinueButtonSurroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (OneButtonContentView)bindable;
            view.continueButtonSurroundLayout.BackgroundColor = (Color)newValue;
        }

        private static void ContinueButtonTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (OneButtonContentView)bindable;
            view.continueButton.Text = newValue.ToString();
        }
        
        private static void IsBudgetSummaryVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (OneButtonContentView)bindable;
            view.budgetSummary.IsVisible = (bool)newValue;
        }

        #endregion
        private void ContinueButton_OnClicked(object sender, EventArgs e) => ContinueButtonAction?.Invoke();

        private void ExitClick(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}