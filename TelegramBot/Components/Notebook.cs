using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Components
{
    class Notebook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Link { get; set; }
        public Notebook()
        {
            Id = -1;
            Details = String.Empty;
            Link = String.Empty;
        }
        public Notebook(DataRow row)
        {
            Id = Convert.ToInt32(row[0]);
            Name = row[2].ToString();
            Details = row[3].ToString();
            Link = row[4].ToString();
        }
    }
}
