//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace veterinaria_ui.Logic
//{
//    public class resetVariables
//    {
//        public void ResetVariables<T>(T obj)
//        {
//            var type = typeof(T);
//            var properties = type.GetProperties();

//            foreach (var property in properties)
//            {
//                if (property.CanWrite)
//                {
//                    property.SetValue(obj, GetDefault(property.PropertyType));
//                }
//            }
//        }

//        private static object GetDefault(Type type)
//        {
//            return type.IsValueType ? Activator.CreateInstance(type) : null;
//        }
//    }
//}
