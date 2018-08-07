﻿using SmartSql.Abstractions;
using SmartSql.Abstractions.Cache;
using SmartSql.Configuration.Maps;
using SmartSql.Configuration.Tags;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace SmartSql.Configuration.Statements
{
    [Flags]
    public enum SqlCommandType
    {
        Unknown = 0,
        Insert = 1 << 0,
        Update = 1 << 1,
        Delete = 1 << 2,
        Select = 1 << 3
    }
    public class Statement
    {
        [XmlIgnore]
        public SmartSqlMap SmartSqlMap { get; internal set; }
        [XmlAttribute]
        public String Id { get; set; }
        [XmlIgnore]
        public SqlCommandType SqlCommandType { get; set; } = SqlCommandType.Unknown;
        [XmlAttribute]
        public CommandType? CommandType { get; set; }
        [XmlAttribute]
        public DataSourceChoice? SourceChoice { get; set; }
        public String FullSqlId => $"{SmartSqlMap.Scope}.{Id}";
        public IList<ITag> SqlTags { get; set; }
        public Cache Cache { get; set; }
        public ResultMap ResultMap { get; set; }
        public ParameterMap ParameterMap { get; set; }
        public MultipleResultMap MultipleResultMap { get; set; }


        public void BuildSql(RequestContext context)
        {
            foreach (ITag tag in SqlTags)
            {
                tag.BuildSql(context);
            }
        }


    }
}
