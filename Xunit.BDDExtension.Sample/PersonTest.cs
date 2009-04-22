// <copyright file="PersonTest.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Collections.Generic;
using Xunit.Extensions.AssertExtensions;

namespace Xunit.BDDExtension.Sample
{
	public abstract class PeoplePool : BDDTest
	{
		protected static List<Person> people;

		Given a_pool_of_people =
			() =>
				{
					people = new List<Person>();

					for (int i = 0; i < 10; i++)
					{
						people.Add(new Person());
					}
				};
	}

	public class When_adding_a_relative_from_people_pool : PeoplePool
	{
		static Person person, relative;
		
		Given a_person_from_people_pool = () => person = people[0];
		Given a_relative_from_people_pool = () => relative = people[1];

		Because adding_a_relative = () => person.AddRelative(relative);

		It should_add_the_relative = () => person.Relatives.ShouldContain(relative);
	}

	public class When_adding_a_null_relative : PeoplePool
	{
		static Person person;
		static Exception exception;

		// Arrange
		Given a_new_person = () => person = new Person();

		// Act
		Because adding_a_null_relative = () => exception = Catch.Exception(() => person.AddRelative(null));

		// Assert
		It should_throw_an_ArgumentNullException = () => exception.ShouldBeType<ArgumentNullException>();
		It should_leave_the_relatives_list_empty = () => person.Relatives.ShouldBeEmpty();
	}

	public class When_adding_a_valid_relative : PeoplePool
	{
		static Person person;
		static Person relative;

		Given a_new_person = () => person = new Person();
		Given a_new_relative = () => relative = new Person();

		Because adding_a_relative = () => person.AddRelative(relative);

		It should_add_the_relative = () => person.Relatives.ShouldContain(relative);
	}
}