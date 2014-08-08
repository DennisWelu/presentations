using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace FortuneFinder.Core
{
    public class Fortunes : ObservableCollection<string>
    {
        public Fortunes()
        {
        }

        public Fortunes(IEnumerable<string> fortuneStrings)
            : base(fortuneStrings)
        {
            
        }
    }
}
