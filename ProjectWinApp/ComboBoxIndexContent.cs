using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinApp
{
    public class ComboBoxIndexContent
    {

        public int Id { get; set; }
        public string Content { get; set; }

        public ComboBoxIndexContent(int id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}
