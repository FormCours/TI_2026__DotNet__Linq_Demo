using System.Text.RegularExpressions;

namespace Demo_Linq.Models
{
    internal class Shelf
    {
        private string _code;

        public int Id { get; set; }
        public string Code
        {
            get { return _code; }
            set
            {
                if (Regex.IsMatch(value, "^[0-35-9][0-9]*$"))
                {
                    _code = value;
                }
                else
                {
                    throw new ArgumentException("The code is invalid");
                }
            }
        }
        public int CDU
        {
            get
            {
                return Convert.ToInt32(_code[0].ToString());
                //return (int.Parse(_code[0].ToString()));
            }
        }
        public int Slots { get; set; }
        public string Color { get; set; }
        public Shelf(int id, string code, int slots, string color)
        {
            Id = id;
            Code = code;
            Slots = slots;
            Color = color;
        }


        public override string ToString()
        {
            return $"{Id} - {Code} {Slots}";
        }
    }
}
