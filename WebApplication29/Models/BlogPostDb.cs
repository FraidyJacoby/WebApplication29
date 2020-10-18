using Microsoft.AspNetCore.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication29.Models
{
    public class BlogPostDb
    {
        private string _connectionString;

        public BlogPostDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<BlogPost> Get5LatestBlogPosts()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT TOP 5 * FROM BlogPosts ORDER BY Id DESC";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<BlogPost> posts = new List<BlogPost>();
                while (reader.Read())
                {
                    posts.Add(new BlogPost
                    {
                        Id = (int)reader["Id"],
                        Title = (string)reader["Title"],
                        Body = (string)reader["Body"],
                        DatePosted = (DateTime)reader["DatePosted"],
                        Comments = GetComments((int)reader["Id"])
                    });
                }
                return posts;
            }  
        }

        public BlogPost GetBlogPostById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT * FROM BlogPosts WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                BlogPost post = new BlogPost
                    {
                        Id = (int)reader["Id"],
                        Title = (string)reader["Title"],
                        Body = (string)reader["Body"],
                        DatePosted = (DateTime)reader["DatePosted"],
                        Comments = GetComments(id)
                    };
                
                return post;
            }
        }

        public string GetBodyById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Body FROM BlogPosts WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return (string)cmd.ExecuteScalar();
            }
        }

        public List<Comment> GetComments(int blogPostid)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT * FROM Comments WHERE BlogPostId = @id";
                cmd.Parameters.AddWithValue("@id", blogPostid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Comment> result = new List<Comment>();
                while (reader.Read())
                {
                    result.Add(new Comment
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        CommentText = (string)reader["Comment"],
                        DatePosted = (DateTime)reader["DatePosted"]
                    });
                }
                return result;
            }
        }

        public void AddComment(Comment comment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO Comments (BlogPostId, Name, Comment, DatePosted)
                                     VALUES (@blogPostId, @name, @comment, @datePosted)";
                cmd.Parameters.AddWithValue("@blogPostId", comment.BlogPostId);
                cmd.Parameters.AddWithValue("@name", comment.Name);
                cmd.Parameters.AddWithValue("@comment", comment.CommentText);
                cmd.Parameters.AddWithValue("@datePosted", DateTime.Now);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int AddBlogPost(BlogPost post)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString)) 
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO BlogPosts (Title, Body, DatePosted)
                                    VALUES (@title, @body, @datePosted)";
                cmd.Parameters.AddWithValue("@title", post.Title);
                cmd.Parameters.AddWithValue("@body", post.Body);
                cmd.Parameters.AddWithValue("@datePosted", DateTime.Now);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT TOP 1 Id FROM BlogPosts ORDER BY Id DESC";
                conn.Open();
                int id = (int)cmd.ExecuteScalar();
                return id;
            }

        }
    }
}
