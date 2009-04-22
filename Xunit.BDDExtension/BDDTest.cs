// <copyright file="BDDTest.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Diagnostics;
using System.Reflection;
using Xunit.Sdk;

namespace Xunit.BDDExtension
{
	/// <summary>
	/// Implements an abstract base class for BDD test classes.
	/// </summary>
	/// <example>
	/// This sample shows a positive test.
	/// <code>
	/// public class Adding_a_relative_is_allowed : BDDTest
	/// {
	///		static Person person;
	///		static Person relative;
	/// 
	/// 	static Given a_new_person = () => person = new Person();
	/// 	static Given a_new_relative = () => relative = new Person();
	/// 
	/// 	static Because adding_a_relative = () => person.AddRelative(relative);
	/// 
	/// 	static It should_not_throw_an_exception = () => adding_a_relative.Invoke();
	/// 	static It should_add_the_relative = () => person.Relatives.ShouldContain(relative);
	/// }
	/// </code>
	/// This sample shows a negative test that throws an exception.
	/// <code>
	/// public class Adding_a_null_relative_is_not_allowed : BDDTest
	/// {
	///		static Person person;
	/// 
	///		static Given a_new_person = () => person = new Person();
	///		static Because adding_a_null_relative = () => person.AddRelative(null);
	///		static It should_throw_an_exception = () => adding_a_null_relative.ShouldThrow&lt;ArgumentNullException&gt;();
	/// }
	/// </code>
	/// </example>
	[RunWith(typeof(TestClassCommand))]
	public abstract class BDDTest
	{
		private readonly string outputPrefix = String.Empty;

		/// <summary>
		/// Runs this test instance by invoking <see cref="Given"/>, <see cref="Because"/> and <see cref="It"/> definitions.
		/// </summary>
		[Fact]
		public void Run()
		{
			OutputHelper.PrintLine();
			OutputHelper.PrintLine(GetType().Name.Replace("_", " "), ConsoleColor.Yellow);

			InvokeCommands(GetType());

			Trace.IndentLevel = 0;
		}

		private void InvokeCommands(Type type)
		{
			if (!typeof(BDDTest).IsAssignableFrom(type))
			{
				throw new ArgumentException(
					String.Format("type is not of type {0}", type.FullName),
					"type");
			}

			if (type != typeof(BDDTest))
			{
				InvokeCommands(type.BaseType);
			}

			FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Default | BindingFlags.NonPublic | BindingFlags.Instance);

			Trace.IndentLevel = 1;
			InvokeCommands<Given>(fieldInfos, (t) => t.Invoke());

			Trace.IndentLevel = 2;
			InvokeCommands<Because>(fieldInfos, (t) => t.Invoke());

			Trace.IndentLevel = 3;
			InvokeCommands<It>(fieldInfos, (t) => t.Invoke());
		}
        
		/// <summary>
		/// Invokes the commands found in the <paramref name="fieldInfos"/> that match <typeparamref name="TFieldDelegateType"/>.
		/// </summary>
		/// <typeparam name="TFieldDelegateType">The type of delegate that should be invoked.</typeparam>
		/// <param name="fieldInfos">The field infos.</param>
		/// <param name="invokeMethod">The invoke method of the delegate.</param>
		/// <returns>The number of invocations.</returns>
		private int InvokeCommands<TFieldDelegateType>(FieldInfo[] fieldInfos, Action<TFieldDelegateType> invokeMethod)
		{
			int invokeCount = 0;

			foreach (FieldInfo info in fieldInfos)
			{
				if (info.FieldType == typeof(TFieldDelegateType))
				{
					try
					{
						OutputHelper.Print(String.Format("{0} ", info.FieldType.Name), ConsoleColor.Green);
						OutputHelper.PrintLine(String.Format("{0}", info.Name).Replace("_", " "));

						invokeMethod.Invoke(((TFieldDelegateType)info.GetValue(this)));
						invokeCount++;
					}
					catch (Exception ex)
					{
						string message = String.Format("Unable to invoke [{0} {1}].",
						                               info.FieldType.Name,
						                               info.Name.Replace("_", " "));

						throw new BDDExtensionRuntimeException(message, ex);
					}
				}
			}

			return invokeCount;
		}
	}
}