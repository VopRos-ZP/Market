using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;

namespace Market
{
    public class OfferViewModel : INotifyPropertyChanged
    {

        private List<Offer> _offers;
        public List<Offer> Offers
        {
            get => _offers;
            set
            {
                _offers = value;
                OnPropertyChanged();
            }
        }
        
        private const string Url = "http://partner.market.yandex.ru/pages/help/YML.xml";

        public OfferViewModel()
        {
            LoadOffers();
        }

        private void LoadOffers()
        {
            Task.Run(async () =>
            {
                Offers = await GetOffers();
            });
        }
        
        private async Task<List<Offer>> GetOffers()
        {
            var offers = new List<Offer>();
            /* Http request */
            var client = new HttpClient();
            var response = await client.GetStringAsync(Url);
            /* Parse string to Xml */
            var doc = new XmlDocument();
            doc.LoadXml(response);
            /* Convert Xml to ViewModels */
            foreach (var offer in doc.DocumentElement.Cast<XmlElement>()
                         .SelectMany(shop => shop.Cast<XmlElement>()
                             .Where(offer => offer.Name == "offers")))
            {
                offers.AddRange(from XmlNode node in offer.ChildNodes 
                    select new Offer
                    {
                        Id = node.Attributes.GetNamedItem("id").Value, 
                        Content = node.OuterXml
                    });
            }
            return await Task.FromResult(offers);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}