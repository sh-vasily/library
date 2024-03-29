﻿using Dapper;
using Npgsql;
using NReco.Csv;

namespace Parser;

internal sealed class Importer(string connectionString)
{
    internal async Task ImportUsers()
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        var insertQuery = "insert into users (first_name, last_name, birthday, address, gender) values(@FirstName, @LastName, @Birthday, @Address, @Gender::gender);";
        using var reader = new StreamReader("./users.csv");
        var csv = new CsvReader(reader, ",");
        var firstLine = true;
        while (csv.Read())
        {
            if (firstLine)
            {
                firstLine = false;
                continue;
            }

            var name = csv[1].Split(' ');

            if (!DateTime.TryParse(csv[3], out var birthDay))
            {
                return;
            }

            var address = $"{csv[6]}, {csv[5]}";

            var param = new
            {
                FirstName = name[0],
                LastName = name[1],
                Birthday = birthDay,
                Address = address,
                Gender = csv[2].ToLower()
            };
        
            await connection.ExecuteAsync(insertQuery, param);   
        }               
    }
    
    internal async Task ImportBooks()
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        var insertBookQuery = "insert into books (title, author, isbn) values(@Title, @Author, @ISBN);";
        using var reader = new StreamReader("./books.csv");
        var csv = new CsvReader(reader, ";");
        var firstLine = true;
        var random = new Random();
        while (csv.Read())
        {
            if (firstLine)
            {
                firstLine = false;
                continue;
            }

            if (!int.TryParse(csv[0], out var isbn))
            {
                continue;
            }
            
            var param = new
            {
                Title = csv[1],
                Author = csv[2],
                ISBN = isbn,
            };
            await connection.ExecuteAsync(insertBookQuery, param);
        }
    }

    internal async Task GenerateBookInstances()
    {
        var selectBooksQuery = "select id from books;";
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        var bookIds = await connection.QueryAsync<int>(selectBooksQuery);
        var random = new Random();
        
        foreach (var bookId in bookIds)
        {
            var insertBookInstanceQuery = "insert into books_instances(book_id) values(@bookId)";
            var countInstances = random.Next(1, 100);
            for (int i = 0; i < countInstances; i++)
            {
                await connection.ExecuteAsync(insertBookInstanceQuery, new { bookId });
            }
        }
    }
}