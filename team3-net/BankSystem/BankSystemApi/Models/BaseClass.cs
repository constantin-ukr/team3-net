using BankSystemApi.Helpers;

namespace BankSystemApi.Models
{
    public class BaseClass
    {
        private int id;

        public int Id
        {
            set
            {
                if (value.ToString().isNatural())
                {
                    id = value;
                }
                else
                {
                    id = 0;
                }
            }
            get => id;

        }
    }
}