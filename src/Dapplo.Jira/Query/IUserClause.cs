// Dapplo - building blocks for .NET applications
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

using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     The possible methods for a user clause
	/// </summary>
	public interface IUserClause
	{
		/// <summary>
		///     This allows fluent constructs like Creator.IsCurrentUser
		/// </summary>
		IFinalClause IsCurrentUser { get; }

		/// <summary>
		///     Negates the expression
		/// </summary>
		IUserClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like Creator.In("smith", "squarepants")
		/// </summary>
		IFinalClause In(params string[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.In(user1, user2)
		/// </summary>
		IFinalClause In(params User[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.InCurrentUserAnd("smith", "squarepants")
		/// </summary>
		/// <param name="users"></param>
		/// <returns></returns>
		IFinalClause InCurrentUserAnd(params string[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.InCurrentUserAnd(user)
		/// </summary>
		/// <param name="users"></param>
		/// <returns></returns>
		IFinalClause InCurrentUserAnd(params User[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.Is("smith")
		/// </summary>
		IFinalClause Is(string user);


		/// <summary>
		///     This allows fluent constructs like Creator.Is(user)
		/// </summary>
		IFinalClause Is(User user);
	}
}