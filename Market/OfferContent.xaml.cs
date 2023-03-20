using System.Xml;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Market
{    
    public partial class OfferContent : ContentPage
    {    
        public OfferContent(Offer offer)
        {
            InitializeComponent();
            var doc = new XmlDocument();
            doc.LoadXml(offer.Content);
            Editor.Text = JsonConvert.SerializeXmlNode(doc);
        }
    }
}

