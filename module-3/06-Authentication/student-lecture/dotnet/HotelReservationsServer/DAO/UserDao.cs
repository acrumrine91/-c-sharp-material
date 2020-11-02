﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HotelReservations.Models;

namespace HotelReservations.DAO
{
    public class UserDao : IUserDao
    {
        private readonly List<User> users = new List<User>();

        public UserDao()
        {
            if (users.Count != 0)
            {
                return;
            }

            using (StreamReader dataInput = new StreamReader("./users.txt"))
            {
                while (!dataInput.EndOfStream)
                {
                    string dataLine = dataInput.ReadLine();
                    string[] lineUser = dataLine.Split(",");
                    try
                    {
                        if (lineUser.Length == 7 && int.TryParse(lineUser[0], out int parsedId))
                        {
                            User newUser = new User()
                            {
                                Id = parsedId,
                                FirstName = lineUser[1],
                                LastName = lineUser[2],
                                Username = lineUser[3],
                                PasswordHash = lineUser[4],
                                Salt = lineUser[5],
                                Role = lineUser[6]
                            };
                            users.Add(newUser);
                        }
                    }
                    catch (FormatException)
                    {
                        //skip user, bad input
                    }
                }
            }
        }

        public User GetUser(string username)
        {
            return users.SingleOrDefault(x => x.Username == username);
        }
    }
}
