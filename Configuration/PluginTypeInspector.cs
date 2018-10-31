using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeInspectors;

namespace NFive.SDK.Plugins.Configuration
{
	public class PluginTypeInspector : TypeInspectorSkeleton
	{
		private readonly ITypeInspector inspector;

		public PluginTypeInspector(ITypeInspector inspector)
		{
			this.inspector = inspector;
		}

		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container) => this.inspector
			.GetProperties(type, container)
			.Where(p => p.CanWrite)
			.Where(p => p.Name != "file_name");
	}
}
