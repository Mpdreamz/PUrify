using System;
using Xunit;
using PUrify;
using Should;

namespace Purify.Tests
{

	public class MonoPurifierFacts
	{
		public class ThePurifyMethod
		{
			private readonly Uri _unpurifiedUri;
			private readonly Uri _purifiedUri;

			public ThePurifyMethod()
			{
				_unpurifiedUri = new Uri("http://localhost:1234/my-path");
				_purifiedUri = new Uri("http://localhost:1234/my-path").Purify();
			}

			[Fact]
			public void DoesNotMakePortPartOfThePathWithoutPurify()
			{
				_unpurifiedUri.PathAndQuery.ShouldEqual("/my-path");
			}
			[Fact]
			public void DoesNotMakePortPartOfThePathWithPury()
			{
				_purifiedUri.PathAndQuery.ShouldEqual("/my-path");
			}
		}

	}
}

