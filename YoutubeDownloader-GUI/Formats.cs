using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader_GUI
{
    //audio formats for conversion
    class Formats
    {
        private string name { get; set; }
        private string value { get; set; }

        public Formats(string _name, string _value)
        {
            this.name = _name;
            this.value = _value;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
