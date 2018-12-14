using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeInspectors;

namespace NFive.SDK.Plugins.Configuration
{
	/// <summary>
	/// Yaml inspector to deserialize a plugin configuration.
	/// </summary>
	/// <seealso cref="TypeInspectorSkeleton" />
	public class BasePluginTypeInspector : TypeInspectorSkeleton
	{
		private readonly ITypeInspector inspector;

		/// <summary>
		/// Initializes a new instance of the <see cref="BasePluginTypeInspector"/> class.
		/// </summary>
		/// <param name="inspector">The type inspector.</param>
		public BasePluginTypeInspector(ITypeInspector inspector)
		{
			this.inspector = inspector;
		}

		/// <summary>
		/// Gets the filtered properties from the specified type container.
		/// </summary>
		/// <param name="type">The type to inspect.</param>
		/// <param name="container">The container to extract properties from.</param>
		/// <returns>Valid deserialized properties.</returns>
		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container) => this.inspector
			.GetProperties(type, container)
			.Where(p => p.Name != "file_name"); // Exclude "FileName"
	}
}
