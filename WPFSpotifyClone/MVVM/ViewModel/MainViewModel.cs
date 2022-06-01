using System;
using WPFSpotifyClone.MVVM.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using Newtonsoft.Json;

namespace WPFSpotifyClone.MVVM.ViewModel
{

    internal class MainViewModel
    {
        public ObservableCollection<Item> Songs { get; set; }

        public MainViewModel()
        {
            Songs = new ObservableCollection<Item>();
            PopulateCollection();
        }

        void PopulateCollection()
        {
            var client = new RestClient();
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("BQBQ7H1POMkx6mkdeKN61HqoZnaAd0viPTpicXhKOZjNnIzRBAS5ojbyKz-rd6qroR9gY_PLqFF4aZd0T-sSLa4wtOSHP5DnEzBJpU69IRN75BDtb3jPpgkqkskcVPSAdCno_N1riSpWY6o", "Bearer");

            var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
            request.AddHeader("Accept", "application/json");  
            request.AddHeader("Content-Type", "application/json");  

            var response = client.GetAsync(request).GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

            for (int i = 0; i < data.Albums.Limit; i++)
            {
                var track = data.Albums.Items[i];
                track.Duration = "2.32";
                Songs.Add(track);
            }
        }
    }
}
