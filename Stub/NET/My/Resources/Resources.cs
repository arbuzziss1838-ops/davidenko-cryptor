using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace NET.My.Resources
{
	[CompilerGenerated]
	[HideModuleName]
	[DebuggerNonUserCode]
	[StandardModule]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	internal sealed class Resources
	{
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceManager_0 == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("NET.Resources", typeof(Resources).Assembly);
					Resources.resourceManager_0 = resourceManager;
				}
				return Resources.resourceManager_0;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.cultureInfo_0;
			}
			set
			{
				Resources.cultureInfo_0 = value;
			}
		}
		private static ResourceManager resourceManager_0;
		private static CultureInfo cultureInfo_0;
	}
}
