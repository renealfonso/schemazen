﻿using System;
using System.Collections.Generic;

namespace model {
	public class Column {
		public string Name;
		public int Position;
		public Default Default;
		public bool IsNullable;
		public string Type;
		public int Length;
		public byte Precision;
		public int Scale;
		public Identity Identity;

		private string IsNullableText {
			get {
				if (IsNullable) return "NULL";
				return "NOT NULL";
			}
		}

		public string DefaultText {
			get {
				if (this.Default == null) return "";
				return "\r\n      " + this.Default.Script();
			}
		}

		public string IdentityText {
			get {
				if (this.Identity == null) return "";
				return "\r\n      " + this.Identity.Script();
			}
		}

		public Column() {

		}

		public Column(string name, string type, bool @null, Default @default) {
			this.Name = name;
			this.Type = type;
			this.Default = @default;
		}

		public Column(string name, string type, int length, bool @null, Default @default)
			: this(name, type, @null, @default) {
			this.Length = length;
		}

		public Column(string name, string type, byte precision, int scale, bool @null, Default @default)
			: this(name, type, @null, @default) {
			this.Precision = precision;
			this.Scale = scale;
		}

		public ColumnDiff Compare(Column c) {
			return new ColumnDiff(this, c);
		}

		public string Script() {
			switch (Type) {
				case "bigint":
				case "bit":
				case "date":
				case "datetime":
				case "float":
				case "image":
				case "int":
				case "money":
				case "ntext":
				case "real":
				case "smalldatetime":
				case "smallint":
				case "smallmoney":
				case "sql_variant":
				case "text":
				case "timestamp":
				case "tinyint":
				case "uniqueidentifier":
				case "xml":

					return string.Format("[{0}] [{1}] {2} {3} {4}", Name, Type, IsNullableText, DefaultText, IdentityText);
				case "binary":
				case "char":
				case "nchar":
				case "nvarchar":
				case "varbinary":
				case "varchar":
					string lengthString = Length.ToString();
					if (lengthString == "-1") lengthString = "max";

					return string.Format("[{0}] [{1}]({2}) {3} {4}", Name, Type, lengthString, IsNullableText, DefaultText);
				case "decimal":
				case "numeric":

					return string.Format("[{0}] [{1}]({2},{3}) {4} {5}", Name, Type, Precision, Scale, IsNullableText, DefaultText);
				default:
					throw new NotSupportedException("SQL data type " + Type + " is not supported.");
			}
		}
	}

	public class ColumnDiff {
		public ColumnDiff(Column target, Column source) {
			this.Source = source;
			this.Target = target;
		}
		public Column Source;
		public Column Target;
		public bool IsDiff {
			get { return Source.DefaultText != Target.DefaultText || Source.IsNullable != Target.IsNullable || Source.Length != Target.Length || Source.Position != Target.Position || Source.Type != Target.Type || Source.Precision != Target.Precision || Source.Scale != Target.Scale; }
		}

		public string Script() {
			return Target.Script();
		}
	}

}
