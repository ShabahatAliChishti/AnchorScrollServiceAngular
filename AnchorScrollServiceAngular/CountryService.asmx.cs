using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace AnchorScrollServiceAngular
{
    /// <summary>
    /// Summary description for CountryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CountryService : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetData()
        {
            List<Country> listCountries = new List<Country>();

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                
                SqlCommand cmd = new SqlCommand("Select * from tblCountry;Select * from tblCity", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                //we have two tables in dataset 1)tblcountry and tblcity
                da.Fill(ds);
                // it contains city table
                DataView dataView = new DataView(ds.Tables[1]);
                //looping through each row in country table
                foreach (DataRow countryDataRow in ds.Tables[0].Rows)
                {
                    Country country = new Country();
                    country.Id = Convert.ToInt32(countryDataRow["Id"]);
                    country.Name = countryDataRow["Name"].ToString();
                    //give me all countries where country id = country.id
                    dataView.RowFilter = "CountryId = '" + country.Id + "'";

                    List<City> listCities = new List<City>();
                    //looping through all dataview that is present in above row filtered dataview
                    foreach (DataRowView cityDataRowView in dataView)
                    {
                        //to get  the underlying data row that is present within the data row view object we use the row property
                        DataRow cityDataRow = cityDataRowView.Row;

                        City city = new City();
                        city.Id = Convert.ToInt32(cityDataRow["Id"]);
                        city.Name = cityDataRow["Name"].ToString();
                        city.CountryId = Convert.ToInt32(cityDataRow["CountryId"]);
                        listCities.Add(city);
                    }
                    //when we done looping through all the cities of a given country object we  are setting that as the value for the cities property of the country object and finally we are adding the country object to list of countries so we have the list of countries right here 
                    country.Cities = listCities;
                    listCountries.Add(country);
                }
            }


            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listCountries));
        }
    }
}