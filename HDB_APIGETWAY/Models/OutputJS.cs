namespace HDB_APIGETWAY.Models
{
    public class OutputJS
    {
        public int status { get; set; }
        public object result { get; set; }
        public OutputJS()
        {
            result = new object();

        }
        public string code { get; set; }
        public string message { get; set; }
        public double total { get; set; }
        public double page { get; set; }
    }
}
