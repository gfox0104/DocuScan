using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuScan
{
    class Destination
    {
        private string _alias;
        private string _targetPath;

        public string Alias { get; set; }

        public string TargetPath { get; set; }

        public Destination(string alias, string targetPath)
        {
            this.Alias = alias;
            this.TargetPath = targetPath;
        }
    }
}
