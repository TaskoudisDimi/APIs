using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class DataModel
    {

        public static T Select<T>() where T: class, new()
        {
            
            Type t = new T().GetType();
            List<T> test = new List<T>();

            return test.FirstOrDefault();
        }

        public static T Delete<T>() where T : class, new()
        {
            string testProperty = "Test";
            PropertyInfo test2 = testProperty.GetType().GetProperty("test");

            Type t = new T().GetType();
            List<T> test = new List<T>();

            return test.FirstOrDefault();
        }

        public static T Update<T>() where T : class, new()
        {
            Type t = new T().GetType();
            List<T> test = new List<T>();

            return test.FirstOrDefault();
        }

        public static T Create<T>() where T : class, new()
        {
            Type t = new T().GetType();
            List<T> test = new List<T>();

            return test.FirstOrDefault();
        }

        


    }
}
