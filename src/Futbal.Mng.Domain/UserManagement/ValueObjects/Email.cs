using System.Text.RegularExpressions;

namespace Futbal.Mng.Domain.UserManagement.ValueObjects
{
    public class Email
    {
        private readonly string _value;

        private Email(string value)
        {
            _value = value;
        }

        public static (Email Email, string Result)  Create(string email)
        {
            if(!string.IsNullOrWhiteSpace(email))
                return (null, "E-mail cannot be empty");

            if (email.Length > 100)
                return (null, "E-mail is too long");
 
            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                return (null, "E-mail is invalid");

            return (new Email(email), "Success");
        }

        public static implicit operator string(Email email)
        {
            return email._value;
        }

        public static explicit operator Email(string email)
        {
            return Create(email).Email;
        }
    }

    public class Skip
    {
        private readonly Skippability _value;

        private Skip(Skippability value)
        {
            _value = value;
        }

        public static Skip Create(Skippability skippability)
        {
            return new Skip(skippability);
        }

        public static Skip Create(bool? skippability)
        {
            switch (skippability)
            {
                case false:
                return new Skip(Skippability.NoSkip);
                case true:
                return new Skip(Skippability.Skip);
                default:
                return new Skip(Skippability.Unknown);
            }
        }

        public static Skip Create(int? skippability)
        {
            switch (skippability)
            {
                case 0:
                return new Skip(Skippability.NoSkip);
                case 1:
                return new Skip(Skippability.Skip);
                default:
                return new Skip(Skippability.Unknown);
            }
        }

        public static int? ToInteger(Skip skip)
        {
            if(skip._value == Skippability.Unknown) return null;
            if(skip._value == Skippability.Skip)
                return 1;
            if(skip._value == Skippability.NoSkip)
                return 0;

            return null;
        }

        public static bool? ToBoolean(Skip skip)
        {
            switch (skip._value)
            {
                case Skippability.NoSkip:
                return false;
                case Skippability.Skip:
                return true;
                default:
                return null;
            }

            // return skip._value switch =>
            // {
            //     Skippability.NoSkip => false;
            //     _ => false;
            // };

        }

        public static explicit operator Skip(bool? skip)
        {
            return Create(skip);
        }

        public static implicit operator Skip(int? skip)
        {
            return Create(skip);
        }

        public static explicit operator Skip(Skippability skip)
        {
            return Create(skip);
        }

        public static implicit operator bool?(Skip skip)
        {
            return Create(skip._value);
        }

        public static implicit operator int?(Skip skip)
        {
            return ToInteger(skip);
        }
        public static implicit operator string(Skip skip)
        {
            return skip._value.ToString();
        }

        public static implicit operator Skippability(Skip skip)
        {
            return (Skippability)ToInteger(skip);
        }
    }

    public enum Skippability
    {
        Unknown,
        Skip,
        NoSkip
    }
}