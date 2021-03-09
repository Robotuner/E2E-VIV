using OneVote.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneVote.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenericEntryField : ContentView
    {
        public string FieldLabel
        {
            get { return GetFieldLabel(this); }
            set { SetFieldLabel(this, value); }
        }
        #region FieldLabel
        public static readonly BindableProperty FieldLabelProperty = BindableProperty.CreateAttached(
                propertyName: "FieldLabel", returnType: typeof(string), declaringType: typeof(GenericEntryField), defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnFieldLabelChanged);

        private static void OnFieldLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GenericEntryField thisctrl = (GenericEntryField)bindable;
            if (thisctrl != null && oldValue != newValue)
            {
                if (newValue != null)
                {
                    thisctrl.fieldLabel.Text = newValue.ToString();
                }
                else
                {
                    thisctrl.fieldLabel.Text = null;
                }
            }
        }

        public static string GetFieldLabel(BindableObject target)
        {
            if (FieldLabelProperty != null)
            {
                object result = target.GetValue(FieldLabelProperty);
                if (result != null) return (string)result;
            }
            return null;
        }

        public static void SetFieldLabel(BindableObject target, string value)
        {
            target.SetValue(FieldLabelProperty, value);
        }
        #endregion
        public string PlaceHolder
        {
            get { return GetPlaceHolder(this); }
            set { SetPlaceHolder(this, value); }
        }
        #region PlaceHolder
        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.CreateAttached(
                propertyName: "PlaceHolder", returnType: typeof(string), declaringType: typeof(GenericEntryField), defaultValue: "Enter Value Here",
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnPlaceHolderChanged);

        private static void OnPlaceHolderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GenericEntryField thisctrl = (GenericEntryField)bindable;
            if (thisctrl != null && oldValue != newValue)
            {
                if (newValue != null)
                {
                    thisctrl.fieldValue.Placeholder = (string)newValue;
                }
            }
        }

        public static string GetPlaceHolder(BindableObject target)
        {
            if (PlaceHolderProperty != null)
            {
                object result = target.GetValue(PlaceHolderProperty);
                if (result != null) return (string)result;
            }
            return null;
        }

        public static void SetPlaceHolder(BindableObject target, string value)
        {
            target.SetValue(PlaceHolderProperty, value);
        }
        #endregion
        public string PlaceKeyboardTypeHolder
        {
            get { return GetKeyboardType(this); }
            set { SetKeyboardType(this, value); }
        }
        #region KeyboardType
        public static readonly BindableProperty KeyboardTypeProperty = BindableProperty.CreateAttached(
                propertyName: "KeyboardType", returnType: typeof(string), declaringType: typeof(GenericEntryField), defaultValue: "Default",
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnKeyboardTypeChanged);

        private static void OnKeyboardTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GenericEntryField thisctrl = (GenericEntryField)bindable;
            if (thisctrl != null && oldValue != newValue)
            {
                if (newValue != null)
                {
                    switch (newValue.ToString())
                    {
                        case "Chat":
                            thisctrl.fieldValue.Keyboard = Xamarin.Forms.Keyboard.Chat;
                            break;
                        case "Default":
                            thisctrl.fieldValue.Keyboard = Xamarin.Forms.Keyboard.Default;
                            break;
                        case "Email":
                            thisctrl.fieldValue.Keyboard = Xamarin.Forms.Keyboard.Email;
                            break;
                        case "Numeric":
                            thisctrl.fieldValue.Keyboard = Xamarin.Forms.Keyboard.Numeric;
                            break;
                        case "Plain":
                            thisctrl.fieldValue.Keyboard = Xamarin.Forms.Keyboard.Plain;
                            break;
                        case "Telephone":
                            thisctrl.fieldValue.Keyboard = Xamarin.Forms.Keyboard.Telephone;
                            break;
                        case "Text":
                            thisctrl.fieldValue.Keyboard = Xamarin.Forms.Keyboard.Text;
                            break;
                        case "Url":
                            thisctrl.fieldValue.Keyboard = Xamarin.Forms.Keyboard.Url;
                            break;
                        default:
                            thisctrl.fieldValue.Keyboard = Xamarin.Forms.Keyboard.Default;
                            break;
                    }
                }
            }
        }

        public static string GetKeyboardType(BindableObject target)
        {
            if (KeyboardTypeProperty != null)
            {
                object result = target.GetValue(KeyboardTypeProperty);
                if (result != null) return (string)result;
            }
            return null;
        }

        public static void SetKeyboardType(BindableObject target, string value)
        {
            target.SetValue(KeyboardTypeProperty, value);
        }
        #endregion
        public string FieldValue
        {
            get { return GetFieldValue(this); }
            set { SetFieldValue(this, value); }
        }
        #region FieldValue
        public static readonly BindableProperty FieldValueProperty = BindableProperty.CreateAttached(
                propertyName: "FieldValue", returnType: typeof(string), declaringType: typeof(GenericEntryField), defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnFieldValueChanged);

        private static void OnFieldValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GenericEntryField thisctrl = (GenericEntryField)bindable;
            if (thisctrl != null && oldValue != newValue)
            {
                thisctrl.fieldValue.Text = newValue?.ToString();
                thisctrl.fieldValueDisplay.Text = newValue?.ToString();
                if (newValue == null)
                {
                    thisctrl.fieldValueDisplay.Text = thisctrl.PlaceHolder;
                    thisctrl.fieldValueDisplay.TextColor = Color.Gray;
                }
                else
                {
                    thisctrl.fieldValueDisplay.TextColor = Color.Default;
                }
            }
        }

        public static string GetFieldValue(BindableObject target)
        {
            return target.GetValue(FieldValueProperty).ToString();
        }

        public static void SetFieldValue(BindableObject target, string value)
        {
            target.SetValue(FieldValueProperty, value);
        }
        #endregion

        public Color BkgColor
        {
            get { return GetBkgColor(this); }
            set { SetBkgColor(this, value); }
        }
        #region BkgColor
        public static readonly BindableProperty BkgColorProperty = BindableProperty.CreateAttached(
                propertyName: "BkgColor", returnType: typeof(Color), declaringType: typeof(GenericEntryField), defaultValue: Color.Transparent,
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnBkgColorChanged);

        private static void OnBkgColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GenericEntryField thisctrl = (GenericEntryField)bindable;
            if (thisctrl != null && oldValue != newValue && newValue != null)
            {
                thisctrl.fieldPanel.BackgroundColor = (Color)newValue;
            }
        }

        public static Color GetBkgColor(BindableObject target)
        {
            object result = target.GetValue(BkgColorProperty);
            return (Color)result;
        }

        public static void SetBkgColor(BindableObject target, Color value)
        {
            target.SetValue(BkgColorProperty, value);
        }
        #endregion


        public bool AsPassword
        {
            get { return GetAsPassword(this); }
            set { SetAsPassword(this, value); }
        }
        #region AsPassword
        public static readonly BindableProperty AsPasswordProperty = BindableProperty.CreateAttached(
                propertyName: "AsPassword", returnType: typeof(bool), declaringType: typeof(GenericEntryField), defaultValue: false,
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnAsPasswordChanged);

        private static void OnAsPasswordChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GenericEntryField thisctrl = (GenericEntryField)bindable;
            if (thisctrl != null && oldValue != newValue)
            {
                thisctrl.fieldValue.IsPassword = (bool)newValue;
            }
        }

        public static bool GetAsPassword(BindableObject target)
        {
            object result = target.GetValue(AsPasswordProperty);
            return (bool)result;
        }

        public static void SetAsPassword(BindableObject target, bool value)
        {
            target.SetValue(AsPasswordProperty, value);
        }
        #endregion

        public bool ShowRequiredFlag
        {
            get { return GetShowRequiredFlag(this); }
            set { SetShowRequiredFlag(this, value); }
        }
        #region ShowRequiredFlag
        public static readonly BindableProperty ShowRequiredFlagProperty = BindableProperty.CreateAttached(
                propertyName: "ShowRequiredFlag", returnType: typeof(bool), declaringType: typeof(GenericEntryField), defaultValue: false,
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnShowRequiredFlagChanged);

        private static void OnShowRequiredFlagChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GenericEntryField thisctrl = (GenericEntryField)bindable;
            if (thisctrl != null && oldValue != newValue)
            {
                thisctrl.required.IsVisible = (bool)newValue;
            }
        }

        public static bool GetShowRequiredFlag(BindableObject target)
        {
            object result = target.GetValue(ShowRequiredFlagProperty);
            return (bool)result;
        }

        public static void SetShowRequiredFlag(BindableObject target, bool value)
        {
            target.SetValue(ShowRequiredFlagProperty, value);
        }
        #endregion

        public int? MaxLength
        {
            get { return GetMaxLength(this); }
            set { SetMaxLength(this, value); }
        }
        #region MaxLength
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.CreateAttached(
                propertyName: "MaxLength", returnType: typeof(int?), declaringType: typeof(GenericEntryField), defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnMaxLengthChanged);

        private static void OnMaxLengthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GenericEntryField thisctrl = (GenericEntryField)bindable;
            if (thisctrl != null && oldValue != newValue)
            {
                //thisctrl.MaxLength.Text = (int?)newValue;
            }
        }

        public static int? GetMaxLength(BindableObject target)
        {
            object result = target.GetValue(MaxLengthProperty);
            return (int?)result;
        }

        public static void SetMaxLength(BindableObject target, int? value)
        {
            target.SetValue(MaxLengthProperty, value);
        }
        #endregion



        public bool IsReadOnly
        {
            get { return GetIsReadOnly(this); }
            set { SetIsReadOnly(this, value); }
        }
        #region IsReadOnly
        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.CreateAttached(
                propertyName: "IsReadOnly", returnType: typeof(bool), declaringType: typeof(GenericEntryField), defaultValue: false,
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnIsReadOnlyChanged);

        private static void OnIsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GenericEntryField thisctrl = (GenericEntryField)bindable;
            if (thisctrl != null && oldValue != newValue)
            {
                thisctrl.fieldValue.IsReadOnly = (bool)newValue;
                thisctrl.fieldValue.IsVisible = !(bool)newValue;
                thisctrl.fieldValueDisplay.IsVisible = (bool)newValue;
            }
        }

        public static bool GetIsReadOnly(BindableObject target)
        {
            object result = target.GetValue(IsReadOnlyProperty);
            return (bool)result;
        }

        public static void SetIsReadOnly(BindableObject target, bool value)
        {
            target.SetValue(IsReadOnlyProperty, value);
        }
        #endregion


        public GenericEntryField()
        {
            InitializeComponent();
            this.required.IsVisible = false;
            this.fieldValue.IsReadOnly = false;
            this.fieldValue.IsVisible = true;
            this.fieldValueDisplay.Text = this.PlaceHolder;
            this.fieldValueDisplay.TextColor = Color.Gray;
            this.fieldValueDisplay.IsVisible = this.fieldValue.IsReadOnly;
            this.fieldPanel.BackgroundColor = Color.Transparent;
        }

        protected void OnEntryChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry)
            {
                string val = entry.Text;
                if (this.MaxLength.HasValue && val.Length > this.MaxLength)
                {
                    val = val.Remove(val.Length - 1);
                    this.FieldValue = entry.Text = val;
                    string msg = string.Format("Max Length of {0} reached", this.MaxLength);
                    MessagingCenter.Send<Entry, string>(entry, MessagingEvents.MaxLengthReached, msg);
                    return;
                }

                this.FieldValue = val;
            }
        }
    }
}