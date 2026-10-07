using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;

namespace ns0
{
    [StandardModule]
    [HideModuleName]
    [GeneratedCode("MyTemplate", "11.0.0.0")]
    internal sealed class Class1
    {
        [HelpKeyword("My.Computer")]
        internal static Class0 Class0_0
        {
            get { return Class1.threadSafeObjectProvider_0.method_0(); }
        }

        [HelpKeyword("My.Application")]
        internal static Form0 Form0_0
        {
            get { return Class1.threadSafeObjectProvider_1.method_0(); }
        }

        [HelpKeyword("My.User")]
        internal static User User_0
        {
            get { return Class1.threadSafeObjectProvider_2.method_0(); }
        }

        [HelpKeyword("My.Forms")]
        internal static Class1.MyForms MyForms_0
        {
            get { return Class1.threadSafeObjectProvider_3.method_0(); }
        }

        [HelpKeyword("My.WebServices")]
        internal static Class1.MyWebServices MyWebServices_0
        {
            get { return Class1.threadSafeObjectProvider_4.method_0(); }
        }

        private static readonly Class1.ThreadSafeObjectProvider<Class0> threadSafeObjectProvider_0 =
            new Class1.ThreadSafeObjectProvider<Class0>();

        private static readonly Class1.ThreadSafeObjectProvider<Form0> threadSafeObjectProvider_1 =
            new Class1.ThreadSafeObjectProvider<Form0>();

        private static readonly Class1.ThreadSafeObjectProvider<User> threadSafeObjectProvider_2 =
            new Class1.ThreadSafeObjectProvider<User>();

        private static Class1.ThreadSafeObjectProvider<Class1.MyForms> threadSafeObjectProvider_3 =
            new Class1.ThreadSafeObjectProvider<Class1.MyForms>();

        private static readonly Class1.ThreadSafeObjectProvider<Class1.MyWebServices> threadSafeObjectProvider_4 =
            new Class1.ThreadSafeObjectProvider<Class1.MyWebServices>();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")]
        internal sealed class MyForms
        {
            public GForm2 _o_program
            {
                get
                {
                    this.gform2_0 = Class1.MyForms.smethod_0<GForm2>(this.gform2_0);
                    return this.gform2_0;
                }
                set
                {
                    bool flag = value != this.gform2_0;
                    if (flag)
                    {
                        bool flag2 = value != null;
                        if (flag2)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.method_0<GForm2>(ref this.gform2_0);
                    }
                }
            }

            public GForm0 empty
            {
                get
                {
                    this.gform0_0 = Class1.MyForms.smethod_0<GForm0>(this.gform0_0);
                    return this.gform0_0;
                }
                set
                {
                    bool flag = value != this.gform0_0;
                    if (flag)
                    {
                        bool flag2 = value != null;
                        if (flag2)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.method_0<GForm0>(ref this.gform0_0);
                    }
                }
            }

            public GForm1 loader
            {
                get
                {
                    this.gform1_0 = Class1.MyForms.smethod_0<GForm1>(this.gform1_0);
                    return this.gform1_0;
                }
                set
                {
                    bool flag = value != this.gform1_0;
                    if (flag)
                    {
                        bool flag2 = value != null;
                        if (flag2)
                        {
                            throw new ArgumentException("Property can only be set to Nothing");
                        }
                        this.method_0<GForm1>(ref this.gform1_0);
                    }
                }
            }

            private static T smethod_0<T>(T gparam_0) where T : Form, new()
            {
                bool flag = gparam_0 == null || gparam_0.IsDisposed;
                if (flag)
                {
                    bool flag2 = Class1.MyForms.hashtable_0 != null;
                    if (flag2)
                    {
                        bool flag3 = Class1.MyForms.hashtable_0.ContainsKey(typeof(T));
                        if (flag3)
                        {
                            throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate", new string[0]));
                        }
                    }
                    else
                    {
                        Class1.MyForms.hashtable_0 = new Hashtable();
                    }
                    Class1.MyForms.hashtable_0.Add(typeof(T), null);
                    TargetInvocationException ex2 = null;
                    object obj;
                    TargetInvocationException ex3;
                    bool flag4;
                    TargetInvocationException ex4;
                    bool flag5;
                    try
                    {
                        return Activator.CreateInstance<T>();
                    }
                    catch (TargetInvocationException tie)
                    {
                        if (tie.InnerException != null)
                        {
                            throw new InvalidOperationException(
                                Utils.GetResourceString("WinForms_SeeInnerException", new string[] { tie.InnerException.Message }),
                                tie.InnerException);
                        }
                    }
                    finally
                    {
                        Class1.MyForms.hashtable_0.Remove(typeof(T));
                    }
                }
                return gparam_0;
            }

            private void method_0<T>(ref T gparam_0) where T : Form
            {
                gparam_0.Dispose();
                gparam_0 = default(T);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public MyForms()
            {
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override bool Equals(object obj)
            {
                return base.Equals(RuntimeHelpers.GetObjectValue(obj));
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            internal Type method_1()
            {
                return typeof(Class1.MyForms);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override string ToString()
            {
                return base.ToString();
            }

            [ThreadStatic]
            private static Hashtable hashtable_0;

            [EditorBrowsable(EditorBrowsableState.Never)]
            public GForm2 gform2_0;

            [EditorBrowsable(EditorBrowsableState.Never)]
            public GForm0 gform0_0;

            [EditorBrowsable(EditorBrowsableState.Never)]
            public GForm1 gform1_0;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
        internal sealed class MyWebServices
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            public override bool Equals(object obj)
            {
                return base.Equals(RuntimeHelpers.GetObjectValue(obj));
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            internal Type method_0()
            {
                return typeof(Class1.MyWebServices);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override string ToString()
            {
                return base.ToString();
            }

            private static T smethod_0<T>(T gparam_0) where T : new()
            {
                bool flag = gparam_0 == null;
                T result;
                if (flag)
                {
                    result = Activator.CreateInstance<T>();
                }
                else
                {
                    result = gparam_0;
                }
                return result;
            }

            private void method_1<T>(ref T gparam_0)
            {
                gparam_0 = default(T);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public MyWebServices()
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComVisible(false)]
        internal sealed class ThreadSafeObjectProvider<T> where T : new()
        {
            internal T method_0()
            {
                bool flag = Class1.ThreadSafeObjectProvider<T>.gparam_0 == null;
                if (flag)
                {
                    Class1.ThreadSafeObjectProvider<T>.gparam_0 = Activator.CreateInstance<T>();
                }
                return Class1.ThreadSafeObjectProvider<T>.gparam_0;
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public ThreadSafeObjectProvider()
            {
            }

            internal static bool smethod_0()
            {
                return Class1.ThreadSafeObjectProvider<T>.object_0 == null;
            }

            internal static object smethod_1()
            {
                return Class1.ThreadSafeObjectProvider<T>.object_0;
            }

            [ThreadStatic]
            [CompilerGenerated]
            private static T gparam_0;

            internal static object object_0;
        }
    }
}