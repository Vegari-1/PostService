using PostService.Model;
using PostService.Repository;
using PostService.Dto;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Npgsql;
using System.Transactions;

namespace PostService.IntegrationTests
{
    public static class DbExtensions
    {

        public static long CountTableRows(this IntegrationWebApplicationFactory<Program, AppDbContext> factory,
            string tableName)
        {
            long totalRows = -1;
            using (var connection = new NpgsqlConnection(factory.container.ConnectionString))
            {
                using (var command = new NpgsqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM \"" + tableName + "\"";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totalRows = (long)reader[0];
                        }
                    }
                }
            }
            return totalRows;
        }

        public static void InsertPost(this IntegrationWebApplicationFactory<Program, AppDbContext> factory,
            string tableName, Post post)
        {
            string insertQuery = "INSERT INTO \"" + tableName + "\" (\"Id\", \"Content\", \"TimeStamp\", \"AuthorId\", \"Likes\", \"Dislikes\") " +
                                 "VALUES (@Id, @Content, @TimeStamp, @AuthorId, @Likes, @Dislikes)";

            using (var connection = new NpgsqlConnection(factory.container.ConnectionString))
            {
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", post.Id);
                    command.Parameters.AddWithValue("@Content", post.Content);
                    command.Parameters.AddWithValue("@TimeStamp", post.TimeStamp);
                    command.Parameters.AddWithValue("@AuthorId", post.AuthorId);
                    command.Parameters.AddWithValue("@Likes", post.Likes);
                    command.Parameters.AddWithValue("@Dislikes", post.Dislikes);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void InsertConnection(this IntegrationWebApplicationFactory<Program, AppDbContext> factory,
            string tableName, Connection connectionObj)
        {
            string insertQuery = "INSERT INTO \"" + tableName + "\" (\"Id\", \"Profile1\", \"Profile2\") " +
                                 "VALUES (@Id, @Profile1, @Profile2)";

            using (var connection = new NpgsqlConnection(factory.container.ConnectionString))
            {
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", connectionObj.Id);
                    command.Parameters.AddWithValue("@Profile1", connectionObj.Profile1);
                    command.Parameters.AddWithValue("@Profile2", connectionObj.Profile2);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void InsertReaction(this IntegrationWebApplicationFactory<Program, AppDbContext> factory,
            string tableName, Reaction reaction)
        {
            string insertQuery = "INSERT INTO \"" + tableName + "\" (\"Id\", \"Positive\", \"AuthorId\", \"PostId\") " +
                                 "VALUES (@Id, @Positive, @AuthorId, @PostId)";
            using (var connection = new NpgsqlConnection(factory.container.ConnectionString))
            {
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", reaction.Id);
                    command.Parameters.AddWithValue("@Positive", reaction.Positive);
                    command.Parameters.AddWithValue("@AuthorId", reaction.AuthorId);
                    command.Parameters.AddWithValue("@PostId", reaction.PostId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void InsertComment(this IntegrationWebApplicationFactory<Program, AppDbContext> factory,
            string tableName, Comment comment)
        {
            string insertQuery = "INSERT INTO \"" + tableName + "\" (\"Id\", \"Content\", \"AuthorId\", \"PostId\") " +
                                 "VALUES (@Id, @Content, @TimeStamp, @AuthorId)";
            using (var connection = new NpgsqlConnection(factory.container.ConnectionString))
            {
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", comment.Id);
                    command.Parameters.AddWithValue("@Content", comment.Content);
                    command.Parameters.AddWithValue("@TimeStamp", comment.AuthorId);
                    command.Parameters.AddWithValue("@AuthorId", comment.PostId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void InsertProfile(this IntegrationWebApplicationFactory<Program, AppDbContext> factory,
            string tableName, Profile profile)
        {
            string insertQuery = "INSERT INTO \"" + tableName + "\" (\"Id\", \"Public\", \"Name\", \"Surname\") " +
                                 "VALUES (@Id, @Public, @Name, @Surname)";

            using (var connection = new NpgsqlConnection(factory.container.ConnectionString))
            {
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", profile.Id);
                    command.Parameters.AddWithValue("@Public", profile.Public);
                    command.Parameters.AddWithValue("@Name", profile.Name);
                    command.Parameters.AddWithValue("@Surname", profile.Surname);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteById(this IntegrationWebApplicationFactory<Program, AppDbContext> factory,
            string tableName, Guid id)
        {
            using (var connection = new NpgsqlConnection(factory.container.ConnectionString))
            {
                using (var command = new NpgsqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM \"" + tableName + "\" WHERE \"Id\" = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
