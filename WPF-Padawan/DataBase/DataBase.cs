﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using WPF_Padawan.Models;

namespace WPF_Padawan.DataBase
{
    static class DataBase
    {
        /// <summary>
        /// Inserts a User at the DataBase
        /// </summary>
        /// <param name="Name"></param>
        /// <param adress="Adress"></param>
        /// <param phonenumber="PhoneNumber"></param>
        /// <returns></returns>
        public static bool AddUser(string Name, string Adress, string PhoneNumber)
        {
            if (Name == "" || PhoneNumber == "")
                return false;

            int Id;

            using (LiteDatabase db = new LiteDatabase("Filename=UserDB.db"))
            {
                Id = db.GetCollection<User>().Count() + 1;
                db.GetCollection<User>().Insert(new User(Id, Name, Adress, PhoneNumber));
            }

            return true;
        }

        public static bool UpdateUser(string Name, string Adress, string PhoneNumber)
        {
            if (Name.Equals(null) || Adress.Equals(null) || PhoneNumber.Equals(null))
                return false;

            using (LiteDatabase db = new LiteDatabase("Filename=UserDB.db"))
            {
                User user = db.GetCollection<User>().FindOne(x => x.Name == Name);
                user.Name = Name;
                user.Adress = Adress;
                user.PhoneNumber = PhoneNumber;

                db.GetCollection<User>().Update(user);
            }

            return true;
        }

        public static bool DeleteUser(string Name)
        {
            if (Name.Equals(null))
                return false;

            using (LiteDatabase db = new LiteDatabase("Filename=UserDB.db"))
            {
                User user = db.GetCollection<User>().FindOne(x => x.Name == Name);

                db.GetCollection<User>().Delete(user.Id);
            }

            return true;
        }
    }
}