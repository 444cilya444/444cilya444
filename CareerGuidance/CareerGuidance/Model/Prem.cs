using System.Data;
namespace CareerGuidance.Model
{
    class Data
    {
        public static int Kod_polzovatel { get; set; }
        public static string Kod_polzovatelReport { get; set; }
        public static DataTable QestionsTable { get; set; }
        public static int Prava { get; set; }
        public static int StyleForms { get; set; }
        public static bool Admin { get; set; }
        public static bool Info { get; set; }
    }
}
