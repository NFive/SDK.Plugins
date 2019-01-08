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
	public class PluginTypeInspector : TypeInspectorSkeleton
	{
		private readonly ITypeInspector inspector;

		/// <summary>
		/// Initializes a new instance of the <see cref="PluginTypeInspector"/> class.
		/// </summary>
		/// <param name="inspector">The type inspector.</param>
		public PluginTypeInspector(ITypeInspector inspector)
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
			.Where(p => p.CanWrite) // Writable
			.Where(p => p.Name != "file_name"); // Exclude "FileName"
	}
}
