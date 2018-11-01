using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeInspectors;

namespace NFive.SDK.Plugins.Configuration
{
	public class BasePluginTypeInspector : TypeInspectorSkeleton
	{
		private readonly ITypeInspector inspector;

		public BasePluginTypeInspector(ITypeInspector inspector)
		{
			this.inspector = inspector;
		}

		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container) => this.inspector
			.GetProperties(type, container)
			.Where(p => p.Name != "full_name");
	}
}
