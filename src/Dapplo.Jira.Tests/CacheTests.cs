﻿// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2019 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

using System.Threading.Tasks;
using Dapplo.Jira.Enums;
using Dapplo.Jira.Tests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
	public class CacheTests : TestBase
	{
		public CacheTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, true)
		{
			_avatarCache = new AvatarCache(Client);
		}

		private readonly AvatarCache _avatarCache;

		[Fact]
		public async Task TestCache()
		{
			var me = await Client.User.GetMyselfAsync();
			Assert.NotEmpty(me.Name);

			var avatar = await _avatarCache.GetOrCreateAsync(me.Avatars);
			Assert.NotNull(avatar);
			Assert.True(avatar.Width > 0);

			avatar = await _avatarCache.GetAsync(me.Avatars);
			Assert.NotNull(avatar);
			Assert.True(avatar.Width > 0);

			// when changing the size, the value is no longer available.
			_avatarCache.AvatarSize = AvatarSizes.Small;
			avatar = await _avatarCache.GetAsync(me.Avatars);
			Assert.Null(avatar);
		}
	}
}