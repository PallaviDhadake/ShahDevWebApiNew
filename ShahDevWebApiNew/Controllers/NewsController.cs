using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShahDevWebApiNew.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ShahDevWebApiNew.Controllers
{
    public class NewsController : ApiController
    {
        // GET api/news
        iClass c = new iClass();

        //Add security key
        [HttpGet]
        [ActionName("GetAll")]
        public IHttpActionResult Get()
        {
            var apiKey = HttpContext.Current.Request.Headers["Api-Key"];

            if (apiKey != "2356")
            {
                return Unauthorized();
            }

            List<NewsData> newslist = new List<NewsData>();
            using (DataTable dtnews = c.GetDataTable("spSelectNews"))
            {
                foreach (DataRow row in dtnews.Rows)
                {
                    NewsData news = new NewsData
                    {
                        NewsId = Convert.ToInt32(row["newsId"]),
                        NewsTitle = row["newsTitle"].ToString(),
                        NewsDescription = row["newsInfo"].ToString()
                    };
                    newslist.Add(news);
                }
            }

            return Ok (newslist);

            // Your existing logic to fetch news data
        }



        //[HttpGet]
        //[ActionName("GetAll")]
        //public IEnumerable<NewsData> Get()
        //{

        //    List<NewsData> newslist = new List<NewsData>();
        //    using (DataTable dtnews = c.GetDataTable("spSelectNews"))
        //    {
        //        foreach (DataRow row in dtnews.Rows)
        //        {
        //            NewsData news = new NewsData
        //            {
        //                NewsId = Convert.ToInt32(row["newsId"]),
        //                NewsTitle = row["newsTitle"].ToString(),
        //                NewsDescription = row["newsInfo"].ToString()
        //            };
        //            newslist.Add(news);
        //        }
        //    }

        //    return newslist;
        //}

        


        // GET api/news/5
        [HttpGet]
        [ActionName("GetById")]
        public IHttpActionResult Get(int id)
        {

            var apiKey = HttpContext.Current.Request.Headers["Api-Key"];

            if (apiKey != "2356")
            {
                return Unauthorized();
            }

            NewsData news = null;
            using (DataTable dtnews = c.GetDataTable("spSelectNews"))
            {
                foreach (DataRow row in dtnews.Rows)
                {
                    if (Convert.ToInt32(row["newsId"]) == id)
                    {
                        news = new NewsData
                        {
                            NewsId = Convert.ToInt32(row["NewsId"]),
                            NewsTitle = row["newsTitle"].ToString(),
                            NewsDescription = row["newsInfo"].ToString()
                        };
                        break; // Exit the loop once the news item is found
                    }
                }
            }

            return Ok(news);
        }

        //[HttpPost]
        //[ActionName("AddNew")]
        //public IHttpActionResult CreateNews(NewsData obj)
        //{
        //    var apiKey = HttpContext.Current.Request.Headers["Api-Key"];

        //    if (apiKey != "2356")
        //    {
        //        return Unauthorized();
        //    }

        //    //using (SqlConnection conn = new SqlConnection(c.OpenConnection()))
        //    //{
        //    //    conn.Open();

        //    // Create a SqlCommand object for the stored procedure

        //    SqlCommand cmdInsertNews = new SqlCommand(Convert.ToString(c.ExecuteQuery("spInsertNews"))).ToString();
        //    // Add parameters to the stored procedure
        //    cmdInsertNews.Parameters.AddWithValue("@newsTitle", obj.NewsTitle);
        //    cmdInsertNews.Parameters.AddWithValue("@newsInfo", obj.NewsDescription);
        //    c.ExecuteQuery("spInsertNews");
        //    cmdInsertNews.CommandType = CommandType.StoredProcedure;
        //    // Execute the stored procedure
        //    // cmdInsertNews.ExecuteNonQuery();

        //    //}

        //    return Ok();
        //}


        [HttpPost]
        [ActionName("AddNew")]
        public IHttpActionResult CreateNews(NewsData obj)
        {
            var apiKey = HttpContext.Current.Request.Headers["Api-Key"];

            if (apiKey != "2356")
            {
                return Unauthorized();
            }

            using (SqlConnection conn = new SqlConnection(c.OpenConnection()))
            {
                conn.Open();

                // Create a SqlCommand object for the stored procedure
                //c.ExecuteQuery("spInsertNews");
                SqlCommand cmdInsertNews = new SqlCommand("spInsertNews", conn);
                cmdInsertNews.CommandType = CommandType.StoredProcedure;

                // Add parameters to the stored procedure
                cmdInsertNews.Parameters.AddWithValue("@newsTitle", obj.NewsTitle);
                cmdInsertNews.Parameters.AddWithValue("@newsInfo", obj.NewsDescription);

                // Execute the stored procedure
                cmdInsertNews.ExecuteNonQuery();

            }

            return Ok();
        }

        // PUT api/news/5
        [HttpPut]
        [ActionName("Update")]
        public IHttpActionResult UpdateNews(NewsData obj)
        {
            var apiKey = HttpContext.Current.Request.Headers["Api-Key"];

            if (apiKey != "2356")
            {
                return Unauthorized();
            }

            using (SqlConnection conn = new SqlConnection(c.OpenConnection()))
            {
                conn.Open();

                // Create a SqlCommand object for the stored procedure
               //c.ExecuteQuery("spInsertNews");
                SqlCommand cmdUpdateNews = new SqlCommand("spUpdateNews", conn);
                cmdUpdateNews.CommandType = CommandType.StoredProcedure;

                // Add parameters to the stored procedure
                cmdUpdateNews.Parameters.AddWithValue("@newsId", obj.NewsId);
                cmdUpdateNews.Parameters.AddWithValue("@newsTitle", obj.NewsTitle);
                cmdUpdateNews.Parameters.AddWithValue("@newsInfo", obj.NewsDescription);

                // Execute the stored procedure
                cmdUpdateNews.ExecuteNonQuery();

            }

            return Ok();
        }

        // DELETE api/news/5
        [HttpDelete]
        [ActionName("Delete")]
        public IHttpActionResult DeleteNews(NewsData obj)
        {
            var apiKey = HttpContext.Current.Request.Headers["Api-Key"];

            if (apiKey != "2356")
            {
                return Unauthorized();
            }

            using (SqlConnection conn = new SqlConnection(c.OpenConnection()))
            {
                conn.Open();

                // Create a SqlCommand object for the stored procedure
                //c.ExecuteQuery("spInsertNews");
                SqlCommand cmdDeleteNews = new SqlCommand("spDeleteNews", conn);
                cmdDeleteNews.CommandType = CommandType.StoredProcedure;

                // Add parameters to the stored procedure
                cmdDeleteNews.Parameters.AddWithValue("@newsId", obj.NewsId);
                //cmdInsertNews.Parameters.AddWithValue("@newsTitle", obj.NewsTitle);
                //cmdInsertNews.Parameters.AddWithValue("@newsInfo", obj.NewsDescription);

                // Execute the stored procedure
                cmdDeleteNews.ExecuteNonQuery();

            }

            return Ok();
        }

        
    }

    
}
