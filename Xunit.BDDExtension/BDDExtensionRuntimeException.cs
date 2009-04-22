// <copyright file="BDDExtensionRuntimeException.cs" company="Pixelplastic">
// Copyright (C) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>
using System;
using System.Runtime.Serialization;

namespace Xunit.BDDExtension
{
	public class BDDExtensionRuntimeException : Exception
	{
		public BDDExtensionRuntimeException()
		{
		}

		public BDDExtensionRuntimeException(string message)
			: base(message)
		{
		}

		public BDDExtensionRuntimeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected BDDExtensionRuntimeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}