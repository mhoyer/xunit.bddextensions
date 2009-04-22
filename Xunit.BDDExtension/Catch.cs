// <copyright file="Catch.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;

namespace Xunit.BDDExtension
{
	/// <summary>
	/// Implements a helper class to catch expected exceptions in <see cref="Because"/> statements.
	/// </summary>
	public static class Catch
	{
		/// <summary>
		/// Captures the throwing action and returns the exception.
		/// </summary>
		/// <param name="throwingAction">The throwing action.</param>
		/// <returns>The <see cref="System.Exception"/> or null if <paramref name="throwingAction"/> does not throw.</returns>
		public static Exception Exception(Action throwingAction)
		{
			try
			{
				throwingAction();
			}
			catch (Exception exception)
			{
				return exception;
			}

			return null;
		}
	}
}