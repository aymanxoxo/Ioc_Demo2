using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TempService1 : ITempService
    {
        public string Say(string word)
        {
            return $"Hello {word} from Temp Service 1";
        }
    }
}
