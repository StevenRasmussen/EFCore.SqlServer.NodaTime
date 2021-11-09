﻿using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public abstract class BaseNodaTimeMemberTranslator : IMemberTranslator
    {
        private readonly ISqlExpressionFactory _sqlExpressionFactory;
        private readonly Dictionary<string, string> _datePartMapping;
        private readonly Type _declaringType;

        public BaseNodaTimeMemberTranslator([NotNull] ISqlExpressionFactory sqlExpressionFactory, [NotNull] Type declaringType, [NotNull] Dictionary<string, string> datePartMapping)
        {
            this._sqlExpressionFactory = sqlExpressionFactory;
            this._declaringType = declaringType;
            this._datePartMapping = datePartMapping;
        }


        public SqlExpression Translate(SqlExpression instance, MemberInfo member, Type returnType, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (member.DeclaringType == this._declaringType)
            {
                var memberName = member.Name;

                if (_datePartMapping.TryGetValue(memberName, out var datePart))
                {
                    return _sqlExpressionFactory.Function(
                        "DATEPART",
                        new[] { _sqlExpressionFactory.Fragment(datePart), instance },
                        returnType);
                }
            }

            return null;
        }
    }
}
