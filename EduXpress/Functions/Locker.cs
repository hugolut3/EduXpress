using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduXpress.Functions
{
    public class Locker : ILocked
    {
        //// Fields...

        private bool _IsCanceled;

        public bool IsCanceled
        {
            get { return _IsCanceled; }
            set { _IsCanceled = value; }
        }
        public Locker()
        {
            _IsCanceled = false;
        }
    }
}
