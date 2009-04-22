// <copyright file="Because.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

namespace Xunit.BDDExtension
{
	/// <summary>
	/// Represents a delegate to define a <c>because</c> statement for BDD pattern (Act).
	/// </summary>
	/// <remarks>
	/// Make sure that at least one <see cref="It"/> definition is invoking this <c>when</c> statement.
	/// </remarks>
	public delegate void Because();
}