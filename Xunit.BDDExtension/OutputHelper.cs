// <copyright file="OutputHelper.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Diagnostics;
using System.IO;

namespace Xunit.BDDExtension
{
	public class OutputHelper
	{
		private static readonly StreamWriter console;
		private static bool indent = true;

		static OutputHelper()
		{
			console = new StreamWriter(Console.OpenStandardOutput());
			console.AutoFlush = true;
			Trace.AutoFlush = true;
		}

		public static void Print(string message)
		{
			Indent();
			console.Write(message);
			Trace.Write(message);
		}

		public static void Print(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;

			Print(message);

			Console.ResetColor();
		}

		public static void PrintLine()
		{
			Print(Environment.NewLine);
		}

		public static void PrintLine(string message)
		{
			Print(string.Format("{0}{1}", message, Environment.NewLine));
			indent = true;
		}

		public static void PrintLine(string message, ConsoleColor foreground)
		{
			Print(string.Format("{0}{1}", message, Environment.NewLine), foreground);
			indent = true;
		}

		private static void Indent()
		{
			if (!indent)
			{
				return;
			}

			indent = false;

			for (int i = 0; i < Trace.IndentLevel; i++)
			{
				Print("  ");
			}
		}
	}
}