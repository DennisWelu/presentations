using System;

namespace FortuneFinder.Core
{
    public interface IFortuneService
    {
        Fortunes GetFortunes(string searchText);
    }
}
