using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using WebMatrix.Data.Resources;
namespace WebMatrix.Data
{
	public sealed class DynamicRecord : DynamicObject, ICustomTypeDescriptor
	{
		private class DynamicPropertyDescriptor : PropertyDescriptor
		{
			private static Attribute[] Empty = new Attribute[0];
			private readonly Type _type;
			public override Type ComponentType
			{
				get
				{
					return typeof(DynamicRecord);
				}
			}
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}
			public override Type PropertyType
			{
				get
				{
					return this._type;
				}
			}
			public DynamicPropertyDescriptor(string name, Type type) : base(name, DynamicRecord.DynamicPropertyDescriptor.Empty)
			{
				this._type = type;
			}
			public override bool CanResetValue(object component)
			{
				return false;
			}
			public override object GetValue(object component)
			{
				DynamicRecord dynamicRecord = component as DynamicRecord;
				if (dynamicRecord != null)
				{
					return dynamicRecord[this.Name];
				}
				return null;
			}
			public override void ResetValue(object component)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, DataResources.RecordIsReadOnly, new object[]
				{
					this.Name
				}));
			}
			public override void SetValue(object component, object value)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, DataResources.RecordIsReadOnly, new object[]
				{
					this.Name
				}));
			}
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}
		}
		public IList<string> Columns
		{
			get;
			private set;
		}
		private IDataRecord Record
		{
			get;
			set;
		}
		public object this[string name]
		{
			get
			{
				this.VerifyColumn(name);
				return DynamicRecord.GetValue(this.Record[name]);
			}
		}
		public object this[int index]
		{
			get
			{
				return DynamicRecord.GetValue(this.Record[index]);
			}
		}
		internal DynamicRecord(IEnumerable<string> columnNames, IDataRecord record)
		{
			this.Columns = columnNames.ToList<string>();
			this.Record = record;
		}
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = this[binder.Name];
			return true;
		}
		private static object GetValue(object value)
		{
			if (DBNull.Value != value)
			{
				return value;
			}
			return null;
		}
		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return this.Columns;
		}
		private void VerifyColumn(string name)
		{
			if (!this.Columns.Contains(name, StringComparer.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, DataResources.InvalidColumnName, new object[]
				{
					name
				}));
			}
		}
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return EventDescriptorCollection.Empty;
		}
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return ((ICustomTypeDescriptor)this).GetProperties();
		}
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			IEnumerable<DynamicRecord.DynamicPropertyDescriptor> source = 
				from columnName in this.Columns
				let columnIndex = this.Record.GetOrdinal(columnName)
				let type = this.Record.GetFieldType(columnIndex)
				select new DynamicRecord.DynamicPropertyDescriptor(columnName, type);
			bool readOnly = true;
			return new PropertyDescriptorCollection(source.ToArray<DynamicRecord.DynamicPropertyDescriptor>(), readOnly);
		}
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}
