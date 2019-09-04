using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace ArtWPFHelpers.Localization
{
    /// <summary>
    /// Реализация поставщика локализованных строк через ресурсы приложения
    /// </summary>
    public class ResxLocalizationProvider : ILocalizationProvider
    {

        public ResxLocalizationProvider(ResourceManager man)
        {
            _rman = man;
        }

        private List<CultureInfo> _cultures = new List<CultureInfo>();

        private ResourceManager _rman;

        public object Localize(string key)
        {
            return _rman.GetObject(key);
        }

        public void AddCulture(string name)
        {
            _cultures.Add(new CultureInfo(name));
        }

        public IEnumerable<CultureInfo> Cultures
        {
            get
            {
                return _cultures;
            }
        }
    }
}
