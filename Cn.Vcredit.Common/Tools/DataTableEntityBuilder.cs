﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Cn.Vcredit.Common.Tools
{
    public class DataTableEntityBuilder<T>
    {
        private static readonly MethodInfo getValueMethod = typeof(DataRow).GetMethod("get_Item", new Type[] { typeof(int) });
        private static readonly MethodInfo isDBNullMethod = typeof(DataRow).GetMethod("IsNull", new Type[] { typeof(int) });
        private delegate T Load(DataRow dr);

        private Load handler;
        private DataTableEntityBuilder() { }

        public T Build(DataRow dr)
        {
            return handler(dr);
        }

        public static DataTableEntityBuilder<T> CreateBuilder(DataRow dr)
        {
            DataTableEntityBuilder<T> dynamicBuilder = new DataTableEntityBuilder<T>();
            DynamicMethod method = new DynamicMethod("DynamicCreateEntity", typeof(T), new Type[] { typeof(DataRow) }, typeof(T), true);
            ILGenerator generator = method.GetILGenerator();
            LocalBuilder result = generator.DeclareLocal(typeof(T));
            generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
            generator.Emit(OpCodes.Stloc, result);

            for (int i = 0; i < dr.ItemArray.Length; i++)
            {
                //20140408 shwang修改，因存在datatable列名和类属性定义的大小写问题
                //PropertyInfo pi = typeof(T).GetProperty(dr.Table.Columns[i].ColumnName);
                PropertyInfo pi = typeof(T).GetProperty(dr.Table.Columns[i].ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                Label endIfLabel = generator.DefineLabel();
                if (pi != null && pi.GetSetMethod() != null)
                {
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, i);
                    generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                    generator.Emit(OpCodes.Brtrue, endIfLabel);
                    generator.Emit(OpCodes.Ldloc, result);
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, i);
                    generator.Emit(OpCodes.Callvirt, getValueMethod);
                    generator.Emit(OpCodes.Unbox_Any, pi.PropertyType);
                    generator.Emit(OpCodes.Callvirt, pi.GetSetMethod());
                    generator.MarkLabel(endIfLabel);
                }
            }
            generator.Emit(OpCodes.Ldloc, result);
            generator.Emit(OpCodes.Ret);
            dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
            return dynamicBuilder;
        }
    }
}
