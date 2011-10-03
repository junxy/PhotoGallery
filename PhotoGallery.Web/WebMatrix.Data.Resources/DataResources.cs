using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
namespace WebMatrix.Data.Resources
{
	[DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), CompilerGenerated]
	internal class DataResources
	{
		private static ResourceManager resourceMan;
		private static CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(DataResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("WebMatrix.Data.Resources.DataResources", typeof(DataResources).Assembly);
					DataResources.resourceMan = resourceManager;
				}
				return DataResources.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return DataResources.resourceCulture;
			}
			set
			{
				DataResources.resourceCulture = value;
			}
		}
		internal static string ConnectionStringNotFound
		{
			get
			{
				return DataResources.ResourceManager.GetString("ConnectionStringNotFound", DataResources.resourceCulture);
			}
		}
		internal static string InvalidColumnName
		{
			get
			{
				return DataResources.ResourceManager.GetString("InvalidColumnName", DataResources.resourceCulture);
			}
		}
		internal static string RecordIsReadOnly
		{
			get
			{
				return DataResources.ResourceManager.GetString("RecordIsReadOnly", DataResources.resourceCulture);
			}
		}
		internal static string UnableToDetermineDatabase
		{
			get
			{
				return DataResources.ResourceManager.GetString("UnableToDetermineDatabase", DataResources.resourceCulture);
			}
		}
		internal DataResources()
		{
		}
	}
}
