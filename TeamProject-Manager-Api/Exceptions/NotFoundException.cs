using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Exceptions
{
    public class NotFoundException : Exception {
        public NotFoundException(string message) : base(message) {

        }
    }
}
