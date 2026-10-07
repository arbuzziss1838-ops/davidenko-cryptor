using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using ns0;

namespace NET.My
{
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.2.0.0")]
	[CompilerGenerated]
	internal sealed partial class MySettings : ApplicationSettingsBase
	{
		private static MySettings defaultInstance = ((MySettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new MySettings())));

		public static MySettings Default
		{
			get
			{
				return defaultInstance;
			}
		}
		[CompilerGenerated]
		private static class _O
		{
			public static ShutdownEventHandler _0__AutoSaveSettings;
		}
	}
}
