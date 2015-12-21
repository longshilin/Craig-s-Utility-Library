﻿/*
Copyright (c) 2014 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System;
using System.ComponentModel;

namespace Utilities.DataTypes
{
    /// <summary>
    /// Extensions converting between types, checking if something is null, etc.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TypeConversionExtensions
    {
        /// <summary>
        /// Calls the object's ToString function passing in the formatting
        /// </summary>
        /// <param name="input">Input object</param>
        /// <param name="format">Format of the output string</param>
        /// <returns>The formatted string</returns>
        public static string FormatToString(this object input, string format)
        {
            if (input == null)
                return "";
            return !string.IsNullOrEmpty(format) ? input.Call<string>("ToString", format) : input.ToString();
        }

        /// <summary>
        /// Sets up a mapping between two types
        /// </summary>
        /// <param name="LeftType">Left type</param>
        /// <param name="RightType">Right type</param>
        /// <returns>The type mapping</returns>
        public static ITypeMapping MapTo(this Type LeftType, Type RightType)
        {
            Contract.Requires<ArgumentNullException>(LeftType != null);
            Contract.Requires<ArgumentNullException>(RightType != null);
            var TempManager = IoC.Manager.Bootstrapper.Resolve<Utilities.DataTypes.DataMapper.Manager>();
            if (TempManager == null)
                return null;
            return TempManager.Map(LeftType, RightType);
        }

        /// <summary>
        /// Sets up a mapping between two types
        /// </summary>
        /// <typeparam name="Left">Left type</typeparam>
        /// <typeparam name="Right">Right type</typeparam>
        /// <param name="Object">Object to set up mapping for</param>
        /// <returns>The type mapping</returns>
        public static ITypeMapping<Left, Right> MapTo<Left, Right>(this Left Object)
        {
            var TempManager = IoC.Manager.Bootstrapper.Resolve<Utilities.DataTypes.DataMapper.Manager>();
            if (TempManager == null)
                return null;
            return TempManager.Map<Left, Right>();
        }

        /// <summary>
        /// Sets up a mapping between two types
        /// </summary>
        /// <typeparam name="Left">Left type</typeparam>
        /// <typeparam name="Right">Right type</typeparam>
        /// <param name="ObjectType">Object type to set up mapping for</param>
        /// <returns>The type mapping</returns>
        public static ITypeMapping<Left, Right> MapTo<Left, Right>(this Type ObjectType)
        {
            var TempManager = IoC.Manager.Bootstrapper.Resolve<Utilities.DataTypes.DataMapper.Manager>();
            if (TempManager == null)
                return null;
            return TempManager.Map<Left, Right>();
        }

        /// <summary>
        /// Attempts to convert the object to another type and returns the value
        /// </summary>
        /// <typeparam name="T">Type to convert from</typeparam>
        /// <typeparam name="R">Return type</typeparam>
        /// <param name="Object">Object to convert</param>
        /// <param name="DefaultValue">
        /// Default value to return if there is an issue or it can't be converted
        /// </param>
        /// <returns>
        /// The object converted to the other type or the default value if there is an error or
        /// can't be converted
        /// </returns>
        public static R To<T, R>(this T Object, R DefaultValue = default(R))
        {
            return Manager.To(Object, DefaultValue);
        }

        /// <summary>
        /// Attempts to convert the object to another type and returns the value
        /// </summary>
        /// <typeparam name="T">Type to convert from</typeparam>
        /// <param name="ResultType">Result type</param>
        /// <param name="Object">Object to convert</param>
        /// <param name="DefaultValue">
        /// Default value to return if there is an issue or it can't be converted
        /// </param>
        /// <returns>
        /// The object converted to the other type or the default value if there is an error or
        /// can't be converted
        /// </returns>
        public static object To<T>(this T Object, Type ResultType, object DefaultValue = null)
        {
            return Manager.To(Object, ResultType, DefaultValue);
        }
    }
}