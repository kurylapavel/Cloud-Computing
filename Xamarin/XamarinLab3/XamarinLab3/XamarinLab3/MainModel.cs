using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinLab3
{
    public class MainModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //First name 
        string firstName = "";

        public string FirstName
        {
            get => firstName;

            set
            {
                if(firstName == value)
                {
                    return;
                }
                firstName = value;
                OnPropertyChanged(nameof(firstName));
                OnPropertyChanged(nameof(DisplayName));
            }

        }

        public string DisplayName => $"Entered first name: {FirstName}";
        void OnPropertyChanged(string firstName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(firstName));
        }

        //


        //Last name
        string lastName = "";

        public string LastName
        {
            get => lastName;

            set
            {
                if (lastName == value)
                {
                    return;
                }
                lastName = value;
                OnPropertyChangedLastName(nameof(lastName));
                OnPropertyChangedLastName(nameof(DisplayLastName));
            }

        }

        public string DisplayLastName => $"Entered last name: {LastName}";

        void OnPropertyChangedLastName(string lastName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(lastName));
        }
        //

        string phoneNumber = "";

        public string PhoneNumber
        {
            get => phoneNumber;

            set
            {
                if (phoneNumber == value)
                {
                    return;
                }
                phoneNumber = value;
                OnPropertyChangedPhoneNumber(nameof(phoneNumber));
                OnPropertyChangedPhoneNumber(nameof(DisplayPhoneNumber));
            }

        }

        public string DisplayPhoneNumber => $"Entered phone number: {PhoneNumber}";
        void OnPropertyChangedPhoneNumber(string phoneNumber)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(phoneNumber));
        }


    }
}
