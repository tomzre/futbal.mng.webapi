namespace Futbal.Mng.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; }

        public int Number { get; private set; }

        private Address(string street, int number)
        {
            Street = street;
            Number = number;
        }

        public static Address Create(string street, int number)
        {
            return new Address(street, number);
        }

        public override bool Equals(object obj)
        {
            Address objAddress = (Address)obj;

            if(objAddress.Street.ToLower() == Street.ToLower() && objAddress.Number == Number)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Street.GetHashCode() * 17 + Number.GetHashCode();
        }
    }
}