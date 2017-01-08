﻿#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Confluence
// 
// Dapplo.Confluence is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Confluence is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings


#endregion

namespace Dapplo.Jira.Query
{
	/// <summary>
	/// An interface for an issue based clauses
	/// </summary>
	public interface IIssueClause
	{
		/// <summary>
		///     Negates the expression
		/// </summary>
		IIssueClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like Id.Is(12345)
		/// </summary>
		IFinalClause Is(string issueKey);


		/// <summary>
		///     This allows fluent constructs like IssueKey.InIssueHistory()
		/// </summary>
		IFinalClause InIssueHistory();

		/// <summary>
		///     This allows fluent constructs like IssueKey.InLinkedIssues(BUG-12345)
		/// </summary>
		IFinalClause InLinkedIssues(string issueKey, string linkType = null);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InVotedIssues()
		/// Find issues that you have voted for.
		/// </summary>
		IFinalClause InVotedIssues();

		/// <summary>
		///     This allows fluent constructs like IssueKey.InWatchedIssues()
		/// Find issues that you have voted for.
		/// </summary>
		IFinalClause InWatchedIssues();

		/// <summary>
		///     This allows fluent constructs like IssueKey.In(BUG-1234, FEATURE-5678)
		/// </summary>
		IFinalClause In(params string[] issueKeys);
	}

	/// <summary>
	/// A clause for content identifying values like ancestor, content, id and parent
	/// </summary>
	public class IssueClause : IIssueClause
	{
		private readonly Clause _clause = new Clause
		{
			Field = Fields.IssueKey
		};

		private bool _negate;

		/// <inheritDoc />
		public IIssueClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

		/// <inheritDoc />
		public IFinalClause Is(string issueKey)
		{
			_clause.Operator = Operators.EqualTo;
			_clause.Value = issueKey;
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause In(params string[] issueKeys)
		{
			_clause.Operator = Operators.In;
			_clause.Value = "(" + string.Join(", ", issueKeys) + ")";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		public IFinalClause InIssueHistory()
		{
			_clause.Operator = Operators.In;
			_clause.Value = "issueHistory()";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		public IFinalClause InLinkedIssues(string issueKey, string linkType = null)
		{
			_clause.Operator = Operators.In;
			var linkTypeArgument = string.IsNullOrEmpty(linkType) ? "" : $", {linkType}";

			_clause.Value = $"linkedIssues({issueKey}{linkTypeArgument})";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		public IFinalClause InVotedIssues()
		{
			_clause.Operator = Operators.In;
			_clause.Value = "votedIssues()";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		public IFinalClause InWatchedIssues()
		{
			_clause.Operator = Operators.In;
			_clause.Value = "watchedIssues()";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}
	}
}