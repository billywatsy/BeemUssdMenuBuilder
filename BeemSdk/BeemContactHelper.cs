using BeemSDK.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BeemSDK
{
    public class BeemContactHelper
    {
        private string _username { get; set; }
        private string _password { get; set; }
        private string _baseUrl = "https://apicontacts.beem.africa";// this can on config 

        public BeemContactHelper(string username, string password)
        {
            _username = username;
            _password = password;
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            var authenticationString = $"{_username}:{_password}";

            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);
            return client;
        }

        public  KeyValuePair<string , CommonResponse> AddAddress(Address address)
        {
            var client = GetHttpClient();
            var response = client.Post<CommonResponse>($"{_baseUrl}/public/v1/address-books"  , 
                new
                {
                    addressbook = address.Addressbook ,
                    description = address.Description
                });
            return response;
        }
        public  KeyValuePair<string , CommonResponse> UpdateAddress(Address address)
        {
            var client = GetHttpClient();
            var response = client.Put<CommonResponse>($"{_baseUrl}/public/v1/address-books/{address.Id}"  , 
                new
                { 
                    addressbook = address.Addressbook ,
                    description = address.Description
                });
            return response;
        }
        public  KeyValuePair<string , AddressPaging<Address>> GetPagedAddress(string q)
        {
            var client = GetHttpClient();

            var response = client.Get<AddressPaging<Address>>($"{_baseUrl}/public/v1/address-books?q={q}");
            return response;
        }

    }
}
