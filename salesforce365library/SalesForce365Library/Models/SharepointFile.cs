using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesForce365Library.Models
{
    public class SharepointFile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Uri { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
