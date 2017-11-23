using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    public class Toyota:ICar
    {
        public void StartEngine()
        {
            throw new NotImplementedException();
        }

        public Toyota()
        {
            
        }

        public Toyota(string model)
        {
            throw new NotImplementedException();
        }

        public Toyota(string model, int yearOfCreation)
        {

        }
    }
}
