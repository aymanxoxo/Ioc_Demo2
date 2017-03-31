using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TempService3 : ITempService
    {
        public string Say(string word)
        {
            return $"Hello {word} from temp service 3";
        }
    }
}
