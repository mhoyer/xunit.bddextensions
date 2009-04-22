// <copyright file="Person.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Collections.Generic;

namespace Xunit.BDDExtension.Sample
{
	/// <summary>
	/// A sample class to show the <see cref="BDDTest"/> usage.
	/// </summary>
	public class Person
	{
		#region constructor logic
		/// <summary>
		/// Initializes a new instance of the <see cref="Person"/> class.
		/// </summary>
		public Person()
		{
			Relatives = new List<Person>();
		}
		#endregion

		#region properties
		/// <summary>
		/// Gets or sets the relatives.
		/// </summary>
		/// <value>The relatives.</value>
		public List<Person> Relatives
		{
			get;
			private set;
		}
		#endregion

		#region methods
		/// <summary>
		/// Adds the relative.
		/// </summary>
		/// <param name="relative">The relative.</param>
		/// <returns>The added relative.</returns>
		/// <exception cref="ArgumentNullException">If the <paramref name="relative"/> is null.</exception>
		public Person AddRelative(Person relative)
		{
			if (relative == null)
			{
				throw new ArgumentNullException("relative");
			}

			Relatives.Add(relative);

			return relative;
		}
		#endregion
	}
}