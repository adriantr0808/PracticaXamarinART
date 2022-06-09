using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PracticaXamarinART.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PracticaXamarinART.Services
{
    public class ServiceApiSeries
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiSeries
(IConfiguration configuration)
        {
            this.UrlApi = configuration["ApiUrls:ApiSeries"];
            this.Header =
            new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Uri uri = new Uri(this.UrlApi + request);
                HttpResponseMessage response =
                await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/series";
            List<Serie> series =
            await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/personajes";
            List<Personaje> pers =
            await this.CallApiAsync<List<Personaje>>(request);
            return pers;
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync(int id)
        {
            string request = "/api/series/personajesserie/"+id;
            List<Personaje> pers =
            await this.CallApiAsync<List<Personaje>>(request);
            return pers;
        }

        public async Task InsertPersonaje(string nombre, string imagen, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje per = new Personaje
                {
                    IdPersonaje=0,
                    Nombre = nombre,
                    Imagen = imagen,
                    IdSerie=idserie
                };
                string json = JsonConvert.SerializeObject(per);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/api/personajes";
                Uri uri = new Uri(this.UrlApi + request);
                await client.PostAsync(uri, content);
            }
        }

        public async Task ModificarPersonaje(int idserie, int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                
                string json = JsonConvert.SerializeObject("");
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/api/personajes/"+idpersonaje+"/"+idserie;
                Uri uri = new Uri(this.UrlApi + request);
                await client.PutAsync(uri, content);
            }
        }




    }
}
