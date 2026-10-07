using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ns0
{
	internal class Class11
	{
		internal static object[] smethod_0()
		{
			return new object[1];
		}
		internal static object[] smethod_1<T>(int int_2, object object_1, object object_2, ref T gparam_0)
		{
			object obj = Class11.object_0;
			lock (obj)
			{
				bool flag2 = !Class11.bool_0;
				if (flag2)
				{
					Class11.bool_0 = true;
					Class11.smethod_4();
				}
			}
			Class11.Class17 @class = null;
			bool flag3 = Class11.class17_0[int_2] != null;
			if (flag3)
			{
				@class = Class11.class17_0[int_2];
			}
			else
			{
				Class11.binaryReader_0.BaseStream.Position = (long)Class11.int_0[int_2];
				@class = new Class11.Class17();
				Module module = typeof(Class11).Module;
				int metadataToken = Class11.smethod_6(Class11.binaryReader_0);
				int num = Class11.smethod_6(Class11.binaryReader_0);
				int num2 = Class11.smethod_6(Class11.binaryReader_0);
				int num3 = Class11.smethod_6(Class11.binaryReader_0);
				@class.object_0 = module.ResolveMethod(metadataToken);
				ParameterInfo[] parameters = ((MethodBase)@class.object_0).GetParameters();
				@class.class13_0 = new Class11.Class13[parameters.Length];
				for (int i = 0; i < parameters.Length; i++)
				{
					Type type = parameters[i].ParameterType;
					Class11.Class13 class2 = new Class11.Class13();
					class2.bool_0 = type.IsByRef;
					class2.int_0 = i;
					@class.class13_0[i] = class2;
					bool isByRef = type.IsByRef;
					if (isByRef)
					{
						type = type.GetElementType();
					}
					Class11.Enum1 enum1_ = (!(type == typeof(string))) ? ((!(type == typeof(byte))) ? ((type == typeof(sbyte)) ? ((Class11.Enum1)1) : ((!(type == typeof(short))) ? ((!(type == typeof(ushort))) ? ((!(type == typeof(int))) ? ((!(type == typeof(uint))) ? ((!(type == typeof(long))) ? ((!(type == typeof(ulong))) ? ((!(type == typeof(float))) ? ((!(type == typeof(double))) ? ((!(type == typeof(bool))) ? ((!(type == typeof(IntPtr))) ? ((!(type == typeof(UIntPtr))) ? ((type == typeof(char)) ? ((Class11.Enum1)15) : ((Class11.Enum1)0)) : ((Class11.Enum1)13)) : ((Class11.Enum1)12)) : ((Class11.Enum1)11)) : ((Class11.Enum1)10)) : ((Class11.Enum1)9)) : ((Class11.Enum1)8)) : ((Class11.Enum1)7)) : ((Class11.Enum1)6)) : ((Class11.Enum1)5)) : ((Class11.Enum1)4)) : ((Class11.Enum1)3))) : ((Class11.Enum1)2)) : ((Class11.Enum1)14);
					class2.enum1_0 = enum1_;
				}
				@class.list_1 = new List<Class11.Class14>(num);
				for (int j = 0; j < num; j++)
				{
					int num4 = Class11.smethod_6(Class11.binaryReader_0);
					Class11.Class14 class3 = new Class11.Class14();
					class3.type_0 = null;
					bool flag4 = num4 >= 0 && num4 < 50;
					if (flag4)
					{
						class3.enum1_0 = (Class11.Enum1)(num4 & 31);
						class3.bool_0 = ((num4 & 32) > 0);
					}
					class3.int_0 = j;
					@class.list_1.Add(class3);
				}
				@class.list_2 = new List<Class11.Class15>(num2);
				for (int k = 0; k < num2; k++)
				{
					int num5 = Class11.smethod_6(Class11.binaryReader_0);
					int num6 = Class11.smethod_6(Class11.binaryReader_0);
					Class11.Class15 class4 = new Class11.Class15();
					class4.int_0 = num5;
					class4.int_1 = num6;
					Class11.Class16 class5 = class4.class16_0 = new Class11.Class16();
					num5 = Class11.smethod_6(Class11.binaryReader_0);
					num6 = Class11.smethod_6(Class11.binaryReader_0);
					int num7 = Class11.smethod_6(Class11.binaryReader_0);
					class5.int_0 = num5;
					class5.int_1 = num6;
					class5.int_3 = num7;
					int num8 = num7;
					int num9 = num8;
					if (num9 != 0)
					{
						if (num9 != 1)
						{
							Class11.smethod_6(Class11.binaryReader_0);
						}
						else
						{
							class5.int_2 = Class11.smethod_6(Class11.binaryReader_0);
						}
					}
					else
					{
						class5.type_0 = module.ResolveType(Class11.smethod_6(Class11.binaryReader_0));
					}
					@class.list_2.Add(class4);
				}
				@class.list_2.Sort((Class11.Class15 x, Class11.Class15 y) => x.class16_0.int_0.CompareTo(y.class16_0.int_0));
				@class.list_0 = new List<Class11.Class12>(num3);
				for (int l = 0; l < num3; l++)
				{
					Class11.Class12 class6 = new Class11.Class12();
					byte b = (byte)(class6.enum3_0 = (Class11.Enum3)Class11.binaryReader_0.ReadByte());
					bool flag5 = b < 176;
					if (!flag5)
					{
						throw new Exception();
					}
					int num10 = (int)Class11.byte_0[(int)b];
					bool flag6 = num10 == 0;
					if (flag6)
					{
						class6.object_0 = null;
					}
					else
					{
						object obj2;
						switch (num10)
						{
						case 1:
							obj2 = Class11.smethod_6(Class11.binaryReader_0);
							break;
						case 2:
							obj2 = Class11.binaryReader_0.ReadInt64();
							break;
						case 3:
							obj2 = Class11.binaryReader_0.ReadSingle();
							break;
						case 4:
							obj2 = Class11.binaryReader_0.ReadDouble();
							break;
						case 5:
						{
							int num11 = Class11.smethod_6(Class11.binaryReader_0);
							int[] array = new int[num11];
							for (int m = 0; m < num11; m++)
							{
								array[m] = Class11.smethod_6(Class11.binaryReader_0);
							}
							obj2 = array;
							break;
						}
						default:
							throw new Exception();
						}
						class6.object_0 = obj2;
					}
					@class.list_0.Add(class6);
				}
				Class11.class17_0[int_2] = @class;
			}
			Class11.Class20 class7 = new Class11.Class20();
			class7.class17_0 = @class;
			ParameterInfo[] parameters2 = ((MethodBase)@class.object_0).GetParameters();
			bool flag7 = false;
			int num12 = 0;
			bool flag8 = @class.object_0 is MethodInfo && ((MethodInfo)@class.object_0).ReturnType != typeof(void);
			if (flag8)
			{
				flag7 = true;
			}
			bool isStatic = ((MethodBase)@class.object_0).IsStatic;
			if (isStatic)
			{
				class7.class22_0 = new Class11.Class22[parameters2.Length];
				for (int n = 0; n < parameters2.Length; n++)
				{
					Type parameterType = parameters2[n].ParameterType;
					class7.class22_0[n] = Class11.Class22.smethod_1(parameterType, ((object[])object_1)[n]);
					bool isByRef2 = parameterType.IsByRef;
					if (isByRef2)
					{
						num12++;
					}
				}
			}
			else
			{
				class7.class22_0 = new Class11.Class22[parameters2.Length + 1];
				bool isValueType = ((MemberInfo)@class.object_0).DeclaringType.IsValueType;
				if (isValueType)
				{
					class7.class22_0[0] = new Class11.Class33(new Class11.Class34(object_2), ((MemberInfo)@class.object_0).DeclaringType);
				}
				else
				{
					class7.class22_0[0] = new Class11.Class34(object_2);
				}
				for (int num13 = 0; num13 < parameters2.Length; num13++)
				{
					Type parameterType2 = parameters2[num13].ParameterType;
					bool isByRef3 = parameterType2.IsByRef;
					if (isByRef3)
					{
						class7.class22_0[num13 + 1] = Class11.Class22.smethod_1(parameterType2, ((object[])object_1)[num13]);
						num12++;
					}
					else
					{
						class7.class22_0[num13 + 1] = Class11.Class22.smethod_1(parameterType2, ((object[])object_1)[num13]);
					}
				}
			}
			class7.class22_1 = new Class11.Class22[@class.list_1.Count];
			for (int num14 = 0; num14 < @class.list_1.Count; num14++)
			{
				Class11.Class14 class8 = @class.list_1[num14];
				switch (class8.enum1_0)
				{
				case (Class11.Enum1)0:
					class7.class22_1[num14] = null;
					break;
				case (Class11.Enum1)1:
				case (Class11.Enum1)2:
				case (Class11.Enum1)3:
				case (Class11.Enum1)4:
				case (Class11.Enum1)5:
				case (Class11.Enum1)6:
				case (Class11.Enum1)11:
				case (Class11.Enum1)15:
					class7.class22_1[num14] = new Class11.Class24(0, class8.enum1_0);
					break;
				case (Class11.Enum1)7:
				case (Class11.Enum1)8:
					class7.class22_1[num14] = new Class11.Class25(0L, class8.enum1_0);
					break;
				case (Class11.Enum1)9:
				case (Class11.Enum1)10:
					class7.class22_1[num14] = new Class11.Class27(0.0, class8.enum1_0);
					break;
				case (Class11.Enum1)12:
					class7.class22_1[num14] = new Class11.Class26(IntPtr.Zero);
					break;
				case (Class11.Enum1)13:
					class7.class22_1[num14] = new Class11.Class26(UIntPtr.Zero);
					break;
				case (Class11.Enum1)14:
					class7.class22_1[num14] = null;
					break;
				case (Class11.Enum1)16:
					class7.class22_1[num14] = new Class11.Class34(null);
					break;
				}
			}
			try
			{
				class7.method_0();
			}
			finally
			{
				class7.method_1();
			}
			int num15 = 0;
			bool flag9 = flag7;
			if (flag9)
			{
				num15 = 1;
			}
			num15 += num12;
			object[] array2 = new object[num15];
			bool flag10 = flag7;
			if (flag10)
			{
				array2[0] = null;
			}
			bool flag11 = @class.object_0 is MethodInfo;
			if (flag11)
			{
				MethodInfo methodInfo = (MethodInfo)@class.object_0;
				bool flag12 = methodInfo.ReturnType != typeof(void) && class7.class22_2 != null;
				if (flag12)
				{
					array2[0] = class7.class22_2.vmethod_4(methodInfo.ReturnType);
				}
			}
			bool flag13 = num12 > 0;
			if (flag13)
			{
				int num16 = 0;
				bool flag14 = flag7;
				if (flag14)
				{
					num16++;
				}
				for (int num17 = 0; num17 < parameters2.Length; num17++)
				{
					Type type2 = parameters2[num17].ParameterType;
					bool flag15 = !type2.IsByRef;
					if (!flag15)
					{
						type2 = type2.GetElementType();
						bool flag16 = class7.class22_0[num17] != null;
						if (flag16)
						{
							bool isStatic2 = ((MethodBase)@class.object_0).IsStatic;
							if (isStatic2)
							{
								array2[num16] = class7.class22_0[num17].vmethod_4(type2);
							}
							else
							{
								array2[num16] = class7.class22_0[num17 + 1].vmethod_4(type2);
							}
						}
						else
						{
							array2[num16] = null;
						}
						num16++;
					}
				}
			}
			bool flag17 = !((MethodBase)@class.object_0).IsStatic && ((MemberInfo)@class.object_0).DeclaringType.IsValueType;
			if (flag17)
			{
				gparam_0 = (T)((object)class7.class22_0[0].vmethod_4(((MemberInfo)@class.object_0).DeclaringType));
			}
			return array2;
		}
		internal static object[] smethod_2(int int_2, object object_1, object object_2)
		{
			return Class11.smethod_1<int>(int_2, object_1, object_2, ref Class11.int_1);
		}
		internal static object[] smethod_3<T>(int int_2, object object_1, ref T gparam_0)
		{
			return Class11.smethod_1<T>(int_2, object_1, gparam_0, ref gparam_0);
		}
		internal static void smethod_4()
		{
			bool flag = Class11.int_0 == null;
			if (flag)
			{
				BinaryReader binaryReader = new BinaryReader(typeof(Class11).Assembly.GetManifestResourceStream("p\u008cd\u009f\u009au\u008f\u008et\u008d\u0088jy\u0088\u0086re4.b2v\u009d5d\u0090\u0099b7\u008d\u0094\u008c\u009f8\u0091j\u009c"));
				binaryReader.BaseStream.Position = 0L;
				byte[] byte_ = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
				binaryReader.Close();
				Class11.smethod_5(byte_);
			}
		}		internal static void smethod_5(byte[] byte_1)
		{
			Class11.binaryReader_0 = new BinaryReader(new MemoryStream(byte_1));
			Class11.byte_0 = new byte[255];
			int num = Class11.smethod_6(Class11.binaryReader_0);
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)Class11.binaryReader_0.ReadByte();
				Class11.byte_0[num2] = Class11.binaryReader_0.ReadByte();
			}
			num = Class11.smethod_6(Class11.binaryReader_0);
			Class11.list_0 = new List<string>(num);
			for (int j = 0; j < num; j++)
			{
				Class11.list_0.Add(Encoding.Unicode.GetString(Class11.binaryReader_0.ReadBytes(Class11.smethod_6(Class11.binaryReader_0))));
			}
			num = Class11.smethod_6(Class11.binaryReader_0);
			Class11.class17_0 = new Class11.Class17[num];
			Class11.int_0 = new int[num];
			for (int k = 0; k < num; k++)
			{
				Class11.class17_0[k] = null;
				Class11.int_0[k] = Class11.smethod_6(Class11.binaryReader_0);
			}
			int num3 = (int)Class11.binaryReader_0.BaseStream.Position;
			for (int l = 0; l < num; l++)
			{
				int num4 = Class11.int_0[l];
				Class11.int_0[l] = num3;
				num3 += num4;
			}
		}		internal static int smethod_6(BinaryReader binaryReader_1)
		{
			bool flag = false;
			uint num = (uint)binaryReader_1.ReadByte();
			uint num2 = 0U | (num & 63U);
			bool flag2 = (num & 64U) > 0U;
			if (flag2)
			{
				flag = true;
			}
			bool flag3 = num < 128U;
			int result;
			if (flag3)
			{
				bool flag4 = !flag;
				if (flag4)
				{
					result = (int)num2;
				}
				else
				{
					result = (int)(~(int)num2);
				}
			}
			else
			{
				int num3 = 0;
				for (;;)
				{
					uint num4 = (uint)binaryReader_1.ReadByte();
					num2 |= (num4 & 127U) << 7 * num3 + 6;
					bool flag5 = num4 < 128U;
					if (flag5)
					{
						break;
					}
					num3++;
				}
				bool flag6 = !flag;
				if (flag6)
				{
					result = (int)num2;
				}
				else
				{
					result = (int)(~(int)num2);
				}
			}
			return result;
		}

		internal static Class11.Class17[] class17_0 = null;
		internal static int[] int_0 = null;
		internal static List<string> list_0;
		private static BinaryReader binaryReader_0;
		private static byte[] byte_0;
		private static bool bool_0 = false;
		private static object object_0 = 1;
		private static int int_1;
		[StructLayout(LayoutKind.Explicit)]
		public struct Struct1
		{			[FieldOffset(0)]
			public byte byte_0;
			[FieldOffset(0)]
			public sbyte sbyte_0;
			[FieldOffset(0)]
			public ushort ushort_0;
			[FieldOffset(0)]
			public short short_0;
			[FieldOffset(0)]
			public uint uint_0;
			[FieldOffset(0)]
			public int int_0;
		}
		private class Class24 : Class11.Class23
		{			internal override void vmethod_10(Class11.Class22 class22_0)
			{
				this.struct1_0 = ((Class11.Class24)class22_0).struct1_0;
				this.enum1_0 = ((Class11.Class24)class22_0).enum1_0;
			}
			internal override void vmethod_2(Class11.Class22 class22_0)
			{
				this.vmethod_10(class22_0);
			}
			public Class24(bool bool_0)
			{
				this.enum4_0 = (Class11.Enum4)1;
				bool flag = !bool_0;
				if (flag)
				{
					this.struct1_0.int_0 = 0;
				}
				else
				{
					this.struct1_0.int_0 = 1;
				}
				this.enum1_0 = (Class11.Enum1)11;
			}
			public Class24(Class11.Class24 class24_0)
			{
				this.enum4_0 = class24_0.enum4_0;
				this.struct1_0.int_0 = class24_0.struct1_0.int_0;
				this.enum1_0 = class24_0.enum1_0;
			}
			public override Class11.Class23 vmethod_74()
			{
				return new Class11.Class24(this);
			}
			public Class24(int int_0)
			{
				this.enum4_0 = (Class11.Enum4)1;
				this.struct1_0.int_0 = int_0;
				this.enum1_0 = (Class11.Enum1)5;
			}
			public Class24(uint uint_0)
			{
				this.enum4_0 = (Class11.Enum4)1;
				this.struct1_0.uint_0 = uint_0;
				this.enum1_0 = (Class11.Enum1)6;
			}
			public Class24(int int_0, Class11.Enum1 enum1_1)
			{
				this.enum4_0 = (Class11.Enum4)1;
				this.struct1_0.int_0 = int_0;
				this.enum1_0 = enum1_1;
			}
			public Class24(uint uint_0, Class11.Enum1 enum1_1)
			{
				this.enum4_0 = (Class11.Enum4)1;
				this.struct1_0.uint_0 = uint_0;
				this.enum1_0 = enum1_1;
			}
			public override bool vmethod_11()
			{
				Class11.Enum1 @enum = this.enum1_0;
				Class11.Enum1 enum2 = @enum;
				switch (enum2)
				{
				case (Class11.Enum1)1:
				case (Class11.Enum1)3:
				case (Class11.Enum1)5:
				case (Class11.Enum1)7:
					goto IL_4F;
				case (Class11.Enum1)2:
				case (Class11.Enum1)4:
				case (Class11.Enum1)6:
					break;
				default:
					if (enum2 == (Class11.Enum1)11 || enum2 == (Class11.Enum1)15)
					{
						goto IL_4F;
					}
					break;
				}
				return this.struct1_0.uint_0 == 0U;
				IL_4F:
				return this.struct1_0.int_0 == 0;
			}
			public override bool vmethod_12()
			{
				return !this.vmethod_11();
			}
			public override Class11.Class22 vmethod_13(Class11.Enum1 enum1_1)
			{
				if (!true)
				{
				}
				Class11.Class23 result;
				switch (enum1_1)
				{
				case (Class11.Enum1)1:
					result = this.vmethod_15();
					goto IL_B9;
				case (Class11.Enum1)2:
					result = this.vmethod_16();
					goto IL_B9;
				case (Class11.Enum1)3:
					result = this.vmethod_17();
					goto IL_B9;
				case (Class11.Enum1)4:
					result = this.vmethod_18();
					goto IL_B9;
				case (Class11.Enum1)5:
					result = this.vmethod_19();
					goto IL_B9;
				case (Class11.Enum1)6:
					result = this.vmethod_20();
					goto IL_B9;
				case (Class11.Enum1)11:
					result = this.vmethod_14();
					goto IL_B9;
				case (Class11.Enum1)15:
					result = this.method_7();
					goto IL_B9;
				case (Class11.Enum1)16:
					result = this.vmethod_74();
					goto IL_B9;
				}
				throw new Exception(((Class11.Enum5)4).ToString());
				IL_B9:
				if (!true)
				{
				}
				return result;
			}
			internal override object vmethod_4(Type type_0)
			{
				bool flag = type_0 != null && type_0.IsByRef;
				if (flag)
				{
					type_0 = type_0.GetElementType();
				}
				bool flag2 = !(type_0 == null) && !(type_0 == typeof(object));
				object result;
				if (flag2)
				{
					bool flag3 = type_0 == typeof(int);
					if (flag3)
					{
						result = this.struct1_0.int_0;
					}
					else
					{
						bool flag4 = !(type_0 == typeof(uint));
						if (flag4)
						{
							bool flag5 = !(type_0 == typeof(short));
							if (flag5)
							{
								bool flag6 = !(type_0 == typeof(ushort));
								if (flag6)
								{
									bool flag7 = type_0 == typeof(byte);
									if (flag7)
									{
										result = this.struct1_0.byte_0;
									}
									else
									{
										bool flag8 = !(type_0 == typeof(sbyte));
										if (flag8)
										{
											bool flag9 = !(type_0 == typeof(bool));
											if (flag9)
											{
												bool flag10 = !(type_0 == typeof(long));
												if (flag10)
												{
													bool flag11 = !(type_0 == typeof(ulong));
													if (flag11)
													{
														bool flag12 = type_0 == typeof(char);
														if (flag12)
														{
															result = (char)this.struct1_0.int_0;
														}
														else
														{
															bool flag13 = type_0 == typeof(IntPtr);
															if (flag13)
															{
																result = new IntPtr(this.struct1_0.int_0);
															}
															else
															{
																bool flag14 = !(type_0 == typeof(UIntPtr));
																if (flag14)
																{
																	bool flag15 = !type_0.IsEnum;
																	if (flag15)
																	{
																		throw new Class11.Exception1();
																	}
																	result = this.method_6(type_0);
																}
																else
																{
																	result = new UIntPtr(this.struct1_0.uint_0);
																}
															}
														}
													}
													else
													{
														result = (ulong)this.struct1_0.uint_0;
													}
												}
												else
												{
													result = (long)this.struct1_0.int_0;
												}
											}
											else
											{
												result = !this.vmethod_11();
											}
										}
										else
										{
											result = this.struct1_0.sbyte_0;
										}
									}
								}
								else
								{
									result = this.struct1_0.ushort_0;
								}
							}
							else
							{
								result = this.struct1_0.short_0;
							}
						}
						else
						{
							result = this.struct1_0.uint_0;
						}
					}
				}
				else
				{
					Class11.Enum1 @enum = this.enum1_0;
					if (!true)
					{
					}
					object obj;
					switch (@enum)
					{
					case (Class11.Enum1)1:
						obj = this.struct1_0.sbyte_0;
						goto IL_40B;
					case (Class11.Enum1)2:
						obj = this.struct1_0.byte_0;
						goto IL_40B;
					case (Class11.Enum1)3:
						obj = this.struct1_0.short_0;
						goto IL_40B;
					case (Class11.Enum1)4:
						obj = this.struct1_0.ushort_0;
						goto IL_40B;
					case (Class11.Enum1)5:
						obj = this.struct1_0.int_0;
						goto IL_40B;
					case (Class11.Enum1)6:
						obj = this.struct1_0.uint_0;
						goto IL_40B;
					case (Class11.Enum1)7:
						obj = (long)this.struct1_0.int_0;
						goto IL_40B;
					case (Class11.Enum1)8:
						obj = (ulong)this.struct1_0.uint_0;
						goto IL_40B;
					case (Class11.Enum1)11:
						obj = this.vmethod_12();
						goto IL_40B;
					case (Class11.Enum1)15:
						obj = (char)this.struct1_0.int_0;
						goto IL_40B;
					}
					obj = this.struct1_0.int_0;
					IL_40B:
					if (!true)
					{
					}
					object obj2 = obj;
					result = obj2;
				}
				return result;
			}
			internal object method_6(Type type_0)
			{
				Type underlyingType = Enum.GetUnderlyingType(type_0);
				bool flag = underlyingType == typeof(int);
				object result;
				if (flag)
				{
					result = Enum.ToObject(type_0, this.struct1_0.int_0);
				}
				else
				{
					bool flag2 = underlyingType == typeof(uint);
					if (flag2)
					{
						result = Enum.ToObject(type_0, this.struct1_0.uint_0);
					}
					else
					{
						bool flag3 = !(underlyingType == typeof(short));
						if (flag3)
						{
							bool flag4 = underlyingType == typeof(ushort);
							if (flag4)
							{
								result = Enum.ToObject(type_0, this.struct1_0.ushort_0);
							}
							else
							{
								bool flag5 = underlyingType == typeof(byte);
								if (flag5)
								{
									result = Enum.ToObject(type_0, this.struct1_0.byte_0);
								}
								else
								{
									bool flag6 = underlyingType == typeof(sbyte);
									if (flag6)
									{
										result = Enum.ToObject(type_0, this.struct1_0.sbyte_0);
									}
									else
									{
										bool flag7 = underlyingType == typeof(long);
										if (flag7)
										{
											result = Enum.ToObject(type_0, (long)this.struct1_0.int_0);
										}
										else
										{
											bool flag8 = underlyingType == typeof(ulong);
											if (flag8)
											{
												result = Enum.ToObject(type_0, (ulong)this.struct1_0.uint_0);
											}
											else
											{
												bool flag9 = underlyingType == typeof(char);
												if (flag9)
												{
													result = Enum.ToObject(type_0, (ushort)this.struct1_0.int_0);
												}
												else
												{
													result = Enum.ToObject(type_0, this.struct1_0.int_0);
												}
											}
										}
									}
								}
							}
						}
						else
						{
							result = Enum.ToObject(type_0, this.struct1_0.short_0);
						}
					}
				}
				return result;
			}
			public override Class11.Class24 vmethod_14()
			{
				return new Class11.Class24((!this.vmethod_11()) ? 1 : 0);
			}
			internal override bool vmethod_7()
			{
				return this.vmethod_12();
			}
			public override Class11.Class24 vmethod_15()
			{
				return new Class11.Class24((int)this.struct1_0.sbyte_0, (Class11.Enum1)1);
			}
			public Class11.Class24 method_7()
			{
				return new Class11.Class24(this.struct1_0.int_0, (Class11.Enum1)15);
			}
			public override Class11.Class24 vmethod_16()
			{
				return new Class11.Class24((uint)this.struct1_0.byte_0, (Class11.Enum1)2);
			}
			public override Class11.Class24 vmethod_17()
			{
				return new Class11.Class24((int)this.struct1_0.short_0, (Class11.Enum1)3);
			}
			public override Class11.Class24 vmethod_18()
			{
				return new Class11.Class24((uint)this.struct1_0.ushort_0, (Class11.Enum1)4);
			}
			public override Class11.Class24 vmethod_19()
			{
				return new Class11.Class24(this.struct1_0.int_0, (Class11.Enum1)5);
			}
			public override Class11.Class24 vmethod_20()
			{
				return new Class11.Class24(this.struct1_0.uint_0, (Class11.Enum1)6);
			}
			public override Class11.Class25 vmethod_21()
			{
				return new Class11.Class25((long)this.struct1_0.int_0, (Class11.Enum1)7);
			}
			public override Class11.Class25 vmethod_22()
			{
				return new Class11.Class25((ulong)this.struct1_0.uint_0, (Class11.Enum1)8);
			}
			public override Class11.Class24 vmethod_23()
			{
				return this.vmethod_15();
			}
			public override Class11.Class24 vmethod_24()
			{
				return this.vmethod_17();
			}
			public override Class11.Class24 vmethod_25()
			{
				return this.vmethod_19();
			}
			public override Class11.Class25 vmethod_26()
			{
				return this.vmethod_21();
			}
			public override Class11.Class24 vmethod_27()
			{
				return this.vmethod_16();
			}
			public override Class11.Class24 vmethod_28()
			{
				return this.vmethod_18();
			}
			public override Class11.Class24 vmethod_29()
			{
				return this.vmethod_20();
			}
			public override Class11.Class25 vmethod_30()
			{
				return this.vmethod_22();
			}
			public override Class11.Class24 vmethod_31()
			{
				return new Class11.Class24((int)(checked((sbyte)this.struct1_0.int_0)), (Class11.Enum1)1);
			}
			public override Class11.Class24 vmethod_32()
			{
				return new Class11.Class24((int)(checked((sbyte)this.struct1_0.uint_0)), (Class11.Enum1)1);
			}
			public override Class11.Class24 vmethod_33()
			{
				return new Class11.Class24((int)(checked((short)this.struct1_0.int_0)), (Class11.Enum1)3);
			}
			public override Class11.Class24 vmethod_34()
			{
				return new Class11.Class24((int)(checked((short)this.struct1_0.uint_0)), (Class11.Enum1)3);
			}
			public override Class11.Class24 vmethod_35()
			{
				return new Class11.Class24(this.struct1_0.int_0, (Class11.Enum1)5);
			}
			public override Class11.Class24 vmethod_36()
			{
				return new Class11.Class24(checked((int)this.struct1_0.uint_0), (Class11.Enum1)5);
			}
			public override Class11.Class25 vmethod_37()
			{
				return new Class11.Class25((long)this.struct1_0.int_0, (Class11.Enum1)7);
			}
			public override Class11.Class25 vmethod_38()
			{
				return new Class11.Class25((long)((ulong)this.struct1_0.uint_0), (Class11.Enum1)7);
			}
			public override Class11.Class24 vmethod_39()
			{
				return new Class11.Class24((int)(checked((byte)this.struct1_0.int_0)), (Class11.Enum1)2);
			}
			public override Class11.Class24 vmethod_40()
			{
				return new Class11.Class24((int)(checked((byte)this.struct1_0.uint_0)), (Class11.Enum1)2);
			}
			public override Class11.Class24 vmethod_41()
			{
				return new Class11.Class24((int)(checked((ushort)this.struct1_0.int_0)), (Class11.Enum1)4);
			}
			public override Class11.Class24 vmethod_42()
			{
				return new Class11.Class24((int)(checked((ushort)this.struct1_0.uint_0)), (Class11.Enum1)4);
			}
			public override Class11.Class24 vmethod_43()
			{
				return new Class11.Class24(checked((uint)this.struct1_0.int_0), (Class11.Enum1)6);
			}
			public override Class11.Class24 vmethod_44()
			{
				return new Class11.Class24(this.struct1_0.uint_0, (Class11.Enum1)6);
			}
			public override Class11.Class25 vmethod_45()
			{
				return new Class11.Class25(checked((ulong)this.struct1_0.int_0), (Class11.Enum1)8);
			}
			public override Class11.Class25 vmethod_46()
			{
				return new Class11.Class25((ulong)this.struct1_0.uint_0, (Class11.Enum1)8);
			}
			public override Class11.Class27 vmethod_47()
			{
				return new Class11.Class27((float)this.struct1_0.int_0);
			}
			public override Class11.Class27 vmethod_48()
			{
				return new Class11.Class27((double)this.struct1_0.int_0);
			}
			public override Class11.Class27 vmethod_49()
			{
				return new Class11.Class27(this.struct1_0.uint_0);
			}
			public override Class11.Class26 vmethod_50()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_26().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_25().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_51()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_30().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_29().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_52()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_37().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_35().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_53()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_45().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_43().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_54()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_38().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_36().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_55()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_46().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_44().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_56()
			{
				Class11.Enum1 @enum = this.enum1_0;
				Class11.Enum1 enum2 = @enum;
				switch (enum2)
				{
				case (Class11.Enum1)1:
				case (Class11.Enum1)3:
				case (Class11.Enum1)5:
					goto IL_4E;
				case (Class11.Enum1)2:
				case (Class11.Enum1)4:
					break;
				default:
					if (enum2 == (Class11.Enum1)11 || enum2 == (Class11.Enum1)15)
					{
						goto IL_4E;
					}
					break;
				}
				return new Class11.Class24((int)(0UL - (ulong)this.struct1_0.uint_0));
				IL_4E:
				return new Class11.Class24(-this.struct1_0.int_0);
			}
			public override Class11.Class22 vmethod_57(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_57(this);
				}
				else
				{
					result = new Class11.Class24(this.struct1_0.int_0 + ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_58(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_58(this);
				}
				else
				{
					result = new Class11.Class24(checked(this.struct1_0.int_0 + ((Class11.Class24)class22_0).struct1_0.int_0));
				}
				return result;
			}
			public override Class11.Class22 vmethod_59(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_59(this);
				}
				else
				{
					result = new Class11.Class24(checked(this.struct1_0.uint_0 + ((Class11.Class24)class22_0).struct1_0.uint_0));
				}
				return result;
			}
			public override Class11.Class22 vmethod_60(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					result = new Class11.Class24(this.struct1_0.int_0 - ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				else
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).method_8(this);
				}
				return result;
			}
			public override Class11.Class22 vmethod_61(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).method_9(this);
				}
				else
				{
					result = new Class11.Class24(checked(this.struct1_0.int_0 - ((Class11.Class24)class22_0).struct1_0.int_0));
				}
				return result;
			}
			public override Class11.Class22 vmethod_62(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).method_10(this);
				}
				else
				{
					result = new Class11.Class24(checked(this.struct1_0.uint_0 - ((Class11.Class24)class22_0).struct1_0.uint_0));
				}
				return result;
			}
			public override Class11.Class22 vmethod_63(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					result = new Class11.Class24(this.struct1_0.int_0 * ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				else
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_63(this);
				}
				return result;
			}
			public override Class11.Class22 vmethod_64(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					result = new Class11.Class24(checked(this.struct1_0.int_0 * ((Class11.Class24)class22_0).struct1_0.int_0));
				}
				else
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_64(this);
				}
				return result;
			}
			public override Class11.Class22 vmethod_65(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_65(this);
				}
				else
				{
					result = new Class11.Class24(checked(this.struct1_0.uint_0 * ((Class11.Class24)class22_0).struct1_0.uint_0));
				}
				return result;
			}
			public override Class11.Class22 vmethod_66(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).method_11(this);
				}
				else
				{
					result = new Class11.Class24(this.struct1_0.int_0 / ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_67(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					result = new Class11.Class24(this.struct1_0.uint_0 / ((Class11.Class24)class22_0).struct1_0.uint_0);
				}
				else
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).method_12(this);
				}
				return result;
			}
			public override Class11.Class22 vmethod_68(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					result = new Class11.Class24(this.struct1_0.int_0 % ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				else
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).method_13(this);
				}
				return result;
			}
			public override Class11.Class22 vmethod_69(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).method_14(this);
				}
				else
				{
					result = new Class11.Class24(this.struct1_0.uint_0 % ((Class11.Class24)class22_0).struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_70(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					result = new Class11.Class24(this.struct1_0.int_0 & ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				else
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_70(this);
				}
				return result;
			}
			public override Class11.Class22 vmethod_71(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_71(this);
				}
				else
				{
					result = new Class11.Class24(this.struct1_0.int_0 | ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_72()
			{
				return new Class11.Class24(~this.struct1_0.int_0);
			}
			public override Class11.Class22 vmethod_73(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_73(this);
				}
				else
				{
					result = new Class11.Class24(this.struct1_0.int_0 ^ ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_75(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).method_17(this);
				}
				else
				{
					result = new Class11.Class24(this.struct1_0.int_0 << ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_76(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					result = new Class11.Class24(this.struct1_0.int_0 >> ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				else
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).method_16(this);
				}
				return result;
			}
			public override Class11.Class22 vmethod_77(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					result = new Class11.Class24(this.struct1_0.uint_0 >> ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				else
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).method_15(this);
				}
				return result;
			}
			public override string ToString()
			{
				Class11.Enum1 @enum = this.enum1_0;
				Class11.Enum1 enum2 = @enum;
				switch (enum2)
				{
				case (Class11.Enum1)1:
				case (Class11.Enum1)3:
				case (Class11.Enum1)5:
					goto IL_42;
				case (Class11.Enum1)2:
				case (Class11.Enum1)4:
					break;
				default:
					if (enum2 == (Class11.Enum1)11)
					{
						goto IL_42;
					}
					break;
				}
				return this.struct1_0.uint_0.ToString();
				IL_42:
				return this.struct1_0.int_0.ToString();
			}
			internal override Class11.Class22 vmethod_8()
			{
				return this;
			}
			internal override bool vmethod_9()
			{
				return true;
			}
			internal override bool vmethod_5(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.method_0();
				bool result;
				if (flag)
				{
					bool flag2 = class22_0.vmethod_0();
					if (flag2)
					{
						result = ((Class11.Class28)class22_0).vmethod_5(this);
					}
					else
					{
						Class11.Class22 @class = class22_0.vmethod_8();
						bool flag3 = @class.vmethod_9();
						if (flag3)
						{
							bool flag4 = @class.method_3();
							if (flag4)
							{
								result = false;
							}
							else
							{
								bool flag5 = !@class.method_1();
								if (flag5)
								{
									result = ((Class11.Class26)@class).vmethod_5(this);
								}
								else
								{
									result = (this.struct1_0.int_0 == ((Class11.Class24)@class).struct1_0.int_0);
								}
							}
						}
						else
						{
							result = false;
						}
					}
				}
				else
				{
					result = ((Class11.Class34)class22_0).vmethod_5(this);
				}
				return result;
			}
			private static Class11.Class23 smethod_4(Class11.Class22 class22_0)
			{
				Class11.Class23 @class = class22_0 as Class11.Class23;
				bool flag = @class == null && class22_0.vmethod_0();
				if (flag)
				{
					@class = (class22_0.vmethod_8() as Class11.Class23);
				}
				return @class;
			}
			internal override bool vmethod_6(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.method_0();
				bool result;
				if (flag)
				{
					bool flag2 = class22_0.vmethod_0();
					if (flag2)
					{
						result = ((Class11.Class28)class22_0).vmethod_6(this);
					}
					else
					{
						Class11.Class22 @class = class22_0.vmethod_8();
						bool flag3 = @class.vmethod_9();
						if (flag3)
						{
							bool flag4 = !@class.method_3();
							if (flag4)
							{
								bool flag5 = @class.method_1();
								if (flag5)
								{
									result = (this.struct1_0.uint_0 != ((Class11.Class24)@class).struct1_0.uint_0);
								}
								else
								{
									result = ((Class11.Class26)@class).vmethod_6(this);
								}
							}
							else
							{
								result = false;
							}
						}
						else
						{
							result = false;
						}
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			public override bool vmethod_78(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_82(this);
				}
				else
				{
					result = (this.struct1_0.int_0 >= ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				return result;
			}
			public override bool vmethod_79(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_83(this);
				}
				else
				{
					result = (this.struct1_0.uint_0 >= ((Class11.Class24)class22_0).struct1_0.uint_0);
				}
				return result;
			}
			public override bool vmethod_80(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_84(this);
				}
				else
				{
					result = (this.struct1_0.int_0 > ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				return result;
			}
			public override bool vmethod_81(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_85(this);
				}
				else
				{
					result = (this.struct1_0.uint_0 > ((Class11.Class24)class22_0).struct1_0.uint_0);
				}
				return result;
			}
			public override bool vmethod_82(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_78(this);
				}
				else
				{
					result = (this.struct1_0.int_0 <= ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				return result;
			}
			public override bool vmethod_83(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				bool result;
				if (flag2)
				{
					result = (this.struct1_0.uint_0 <= ((Class11.Class24)class22_0).struct1_0.uint_0);
				}
				else
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_79(this);
				}
				return result;
			}
			public override bool vmethod_84(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				bool result;
				if (flag2)
				{
					result = (this.struct1_0.int_0 < ((Class11.Class24)class22_0).struct1_0.int_0);
				}
				else
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_80(this);
				}
				return result;
			}
			public override bool vmethod_85(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					result = ((Class11.Class26)class22_0).vmethod_81(this);
				}
				else
				{
					result = (this.struct1_0.uint_0 < ((Class11.Class24)class22_0).struct1_0.uint_0);
				}
				return result;
			}
			public Class11.Struct1 struct1_0;
			public Class11.Enum1 enum1_0;
		}

		
		[StructLayout(LayoutKind.Explicit)]
		private struct Struct2
		{			[FieldOffset(0)]
			public byte byte_0;
			[FieldOffset(0)]
			public sbyte sbyte_0;
			[FieldOffset(0)]
			public ushort ushort_0;
			[FieldOffset(0)]
			public short short_0;
			[FieldOffset(0)]
			public uint uint_0;
			[FieldOffset(0)]
			public int int_0;
			[FieldOffset(0)]
			public ulong ulong_0;
			[FieldOffset(0)]
			public long long_0;
		}

		
		private class Class25 : Class11.Class23
		{			internal override void vmethod_10(Class11.Class22 class22_0)
			{
				this.struct2_0 = ((Class11.Class25)class22_0).struct2_0;
				this.enum1_0 = ((Class11.Class25)class22_0).enum1_0;
			}
			internal override void vmethod_2(Class11.Class22 class22_0)
			{
				this.vmethod_10(class22_0);
			}
			public Class25(long long_0)
			{
				this.enum4_0 = (Class11.Enum4)2;
				this.struct2_0.long_0 = long_0;
				this.enum1_0 = (Class11.Enum1)7;
			}
			public Class25(Class11.Class25 class25_0)
			{
				this.enum4_0 = class25_0.enum4_0;
				this.struct2_0.long_0 = class25_0.struct2_0.long_0;
				this.enum1_0 = class25_0.enum1_0;
			}
			public override Class11.Class23 vmethod_74()
			{
				return new Class11.Class25(this);
			}
			public Class25(long long_0, Class11.Enum1 enum1_1)
			{
				this.enum4_0 = (Class11.Enum4)2;
				this.struct2_0.long_0 = long_0;
				this.enum1_0 = enum1_1;
			}
			public Class25(ulong ulong_0)
			{
				this.enum4_0 = (Class11.Enum4)2;
				this.struct2_0.ulong_0 = ulong_0;
				this.enum1_0 = (Class11.Enum1)8;
			}
			public Class25(ulong ulong_0, Class11.Enum1 enum1_1)
			{
				this.enum4_0 = (Class11.Enum4)2;
				this.struct2_0.ulong_0 = ulong_0;
				this.enum1_0 = enum1_1;
			}
			public override bool vmethod_11()
			{
				bool flag = this.enum1_0 == (Class11.Enum1)7;
				bool result;
				if (flag)
				{
					result = (this.struct2_0.long_0 == 0L);
				}
				else
				{
					result = (this.struct2_0.ulong_0 == 0UL);
				}
				return result;
			}
			public override bool vmethod_12()
			{
				return !this.vmethod_11();
			}
			public override Class11.Class22 vmethod_13(Class11.Enum1 enum1_1)
			{
				if (!true)
				{
				}
				Class11.Class23 result;
				switch (enum1_1)
				{
				case (Class11.Enum1)1:
					result = this.vmethod_15();
					goto IL_CB;
				case (Class11.Enum1)2:
					result = this.vmethod_16();
					goto IL_CB;
				case (Class11.Enum1)3:
					result = this.vmethod_17();
					goto IL_CB;
				case (Class11.Enum1)4:
					result = this.vmethod_18();
					goto IL_CB;
				case (Class11.Enum1)5:
					result = this.vmethod_19();
					goto IL_CB;
				case (Class11.Enum1)6:
					result = this.vmethod_20();
					goto IL_CB;
				case (Class11.Enum1)7:
					result = this.vmethod_21();
					goto IL_CB;
				case (Class11.Enum1)8:
					result = this.vmethod_22();
					goto IL_CB;
				case (Class11.Enum1)11:
					result = this.vmethod_14();
					goto IL_CB;
				case (Class11.Enum1)15:
					result = this.method_7();
					goto IL_CB;
				case (Class11.Enum1)16:
					result = this.vmethod_74();
					goto IL_CB;
				}
				throw new Exception(((Class11.Enum5)4).ToString());
				IL_CB:
				if (!true)
				{
				}
				return result;
			}
			internal override object vmethod_4(Type type_0)
			{
				bool flag = type_0 != null && type_0.IsByRef;
				if (flag)
				{
					type_0 = type_0.GetElementType();
				}
				bool flag2 = !(type_0 == null) && !(type_0 == typeof(object));
				object result;
				if (flag2)
				{
					bool flag3 = type_0 == typeof(int);
					if (flag3)
					{
						result = this.struct2_0.int_0;
					}
					else
					{
						bool flag4 = !(type_0 == typeof(uint));
						if (flag4)
						{
							bool flag5 = !(type_0 == typeof(short));
							if (flag5)
							{
								bool flag6 = !(type_0 == typeof(ushort));
								if (flag6)
								{
									bool flag7 = !(type_0 == typeof(byte));
									if (flag7)
									{
										bool flag8 = !(type_0 == typeof(sbyte));
										if (flag8)
										{
											bool flag9 = !(type_0 == typeof(bool));
											if (flag9)
											{
												bool flag10 = !(type_0 == typeof(long));
												if (flag10)
												{
													bool flag11 = !(type_0 == typeof(ulong));
													if (flag11)
													{
														bool flag12 = type_0 == typeof(char);
														if (flag12)
														{
															result = (char)this.struct2_0.long_0;
														}
														else
														{
															bool flag13 = !type_0.IsEnum;
															if (flag13)
															{
																throw new Class11.Exception1();
															}
															result = this.method_6(type_0);
														}
													}
													else
													{
														result = this.struct2_0.ulong_0;
													}
												}
												else
												{
													result = this.struct2_0.long_0;
												}
											}
											else
											{
												result = !this.vmethod_11();
											}
										}
										else
										{
											result = this.struct2_0.sbyte_0;
										}
									}
									else
									{
										result = this.struct2_0.byte_0;
									}
								}
								else
								{
									result = this.struct2_0.ushort_0;
								}
							}
							else
							{
								result = this.struct2_0.short_0;
							}
						}
						else
						{
							result = this.struct2_0.uint_0;
						}
					}
				}
				else
				{
					Class11.Enum1 @enum = this.enum1_0;
					if (!true)
					{
					}
					object obj;
					switch (@enum)
					{
					case (Class11.Enum1)1:
						obj = this.struct2_0.sbyte_0;
						goto IL_3A1;
					case (Class11.Enum1)2:
						obj = this.struct2_0.byte_0;
						goto IL_3A1;
					case (Class11.Enum1)3:
						obj = this.struct2_0.short_0;
						goto IL_3A1;
					case (Class11.Enum1)4:
						obj = this.struct2_0.ushort_0;
						goto IL_3A1;
					case (Class11.Enum1)5:
						obj = this.struct2_0.int_0;
						goto IL_3A1;
					case (Class11.Enum1)6:
						obj = this.struct2_0.uint_0;
						goto IL_3A1;
					case (Class11.Enum1)7:
						obj = this.struct2_0.long_0;
						goto IL_3A1;
					case (Class11.Enum1)8:
						obj = this.struct2_0.ulong_0;
						goto IL_3A1;
					case (Class11.Enum1)11:
						obj = this.vmethod_12();
						goto IL_3A1;
					case (Class11.Enum1)15:
						obj = (char)this.struct2_0.int_0;
						goto IL_3A1;
					}
					obj = this.struct2_0.long_0;
					IL_3A1:
					if (!true)
					{
					}
					object obj2 = obj;
					result = obj2;
				}
				return result;
			}
			internal object method_6(Type type_0)
			{
				Type underlyingType = Enum.GetUnderlyingType(type_0);
				bool flag = !(underlyingType == typeof(int));
				object result;
				if (flag)
				{
					bool flag2 = !(underlyingType == typeof(uint));
					if (flag2)
					{
						bool flag3 = !(underlyingType == typeof(short));
						if (flag3)
						{
							bool flag4 = underlyingType == typeof(ushort);
							if (flag4)
							{
								result = Enum.ToObject(type_0, this.struct2_0.ushort_0);
							}
							else
							{
								bool flag5 = !(underlyingType == typeof(byte));
								if (flag5)
								{
									bool flag6 = !(underlyingType == typeof(sbyte));
									if (flag6)
									{
										bool flag7 = !(underlyingType == typeof(long));
										if (flag7)
										{
											bool flag8 = !(underlyingType == typeof(ulong));
											if (flag8)
											{
												bool flag9 = !(underlyingType == typeof(char));
												if (flag9)
												{
													result = Enum.ToObject(type_0, this.struct2_0.long_0);
												}
												else
												{
													result = Enum.ToObject(type_0, (ushort)this.struct2_0.int_0);
												}
											}
											else
											{
												result = Enum.ToObject(type_0, this.struct2_0.ulong_0);
											}
										}
										else
										{
											result = Enum.ToObject(type_0, this.struct2_0.long_0);
										}
									}
									else
									{
										result = Enum.ToObject(type_0, this.struct2_0.sbyte_0);
									}
								}
								else
								{
									result = Enum.ToObject(type_0, this.struct2_0.byte_0);
								}
							}
						}
						else
						{
							result = Enum.ToObject(type_0, this.struct2_0.short_0);
						}
					}
					else
					{
						result = Enum.ToObject(type_0, this.struct2_0.uint_0);
					}
				}
				else
				{
					result = Enum.ToObject(type_0, this.struct2_0.int_0);
				}
				return result;
			}
			public override Class11.Class24 vmethod_14()
			{
				return new Class11.Class24((!this.vmethod_11()) ? 1 : 0);
			}
			internal override bool vmethod_7()
			{
				return this.vmethod_12();
			}
			public Class11.Class24 method_7()
			{
				return new Class11.Class24((int)this.struct2_0.sbyte_0, (Class11.Enum1)15);
			}
			public override Class11.Class24 vmethod_15()
			{
				return new Class11.Class24((int)this.struct2_0.sbyte_0, (Class11.Enum1)1);
			}
			public override Class11.Class24 vmethod_16()
			{
				return new Class11.Class24((uint)this.struct2_0.byte_0, (Class11.Enum1)2);
			}
			public override Class11.Class24 vmethod_17()
			{
				return new Class11.Class24((int)this.struct2_0.short_0, (Class11.Enum1)3);
			}
			public override Class11.Class24 vmethod_18()
			{
				return new Class11.Class24((uint)this.struct2_0.ushort_0, (Class11.Enum1)4);
			}
			public override Class11.Class24 vmethod_19()
			{
				return new Class11.Class24(this.struct2_0.int_0, (Class11.Enum1)5);
			}
			public override Class11.Class24 vmethod_20()
			{
				return new Class11.Class24(this.struct2_0.uint_0, (Class11.Enum1)6);
			}
			public override Class11.Class25 vmethod_21()
			{
				return new Class11.Class25(this.struct2_0.long_0, (Class11.Enum1)7);
			}
			public override Class11.Class25 vmethod_22()
			{
				return new Class11.Class25(this.struct2_0.ulong_0, (Class11.Enum1)8);
			}
			public override Class11.Class24 vmethod_23()
			{
				return this.vmethod_15();
			}
			public override Class11.Class24 vmethod_24()
			{
				return this.vmethod_17();
			}
			public override Class11.Class24 vmethod_25()
			{
				return this.vmethod_19();
			}
			public override Class11.Class25 vmethod_26()
			{
				return this.vmethod_21();
			}
			public override Class11.Class24 vmethod_27()
			{
				return this.vmethod_16();
			}
			public override Class11.Class24 vmethod_28()
			{
				return this.vmethod_18();
			}
			public override Class11.Class24 vmethod_29()
			{
				return this.vmethod_20();
			}
			public override Class11.Class25 vmethod_30()
			{
				return this.vmethod_22();
			}
			public override Class11.Class24 vmethod_31()
			{
				return new Class11.Class24((int)(checked((sbyte)this.struct2_0.long_0)), (Class11.Enum1)1);
			}
			public override Class11.Class24 vmethod_32()
			{
				return new Class11.Class24((int)(checked((sbyte)this.struct2_0.ulong_0)), (Class11.Enum1)1);
			}
			public override Class11.Class24 vmethod_33()
			{
				return new Class11.Class24((int)(checked((short)this.struct2_0.long_0)), (Class11.Enum1)3);
			}
			public override Class11.Class24 vmethod_34()
			{
				return new Class11.Class24((int)(checked((short)this.struct2_0.ulong_0)), (Class11.Enum1)3);
			}
			public override Class11.Class24 vmethod_35()
			{
				return new Class11.Class24(checked((int)this.struct2_0.long_0), (Class11.Enum1)5);
			}
			public override Class11.Class24 vmethod_36()
			{
				return new Class11.Class24(checked((int)this.struct2_0.ulong_0), (Class11.Enum1)5);
			}
			public override Class11.Class25 vmethod_37()
			{
				return new Class11.Class25(this.struct2_0.long_0, (Class11.Enum1)7);
			}
			public override Class11.Class25 vmethod_38()
			{
				return new Class11.Class25(checked((long)this.struct2_0.ulong_0), (Class11.Enum1)7);
			}
			public override Class11.Class24 vmethod_39()
			{
				return new Class11.Class24((int)(checked((byte)this.struct2_0.long_0)), (Class11.Enum1)2);
			}
			public override Class11.Class24 vmethod_40()
			{
				return new Class11.Class24((int)(checked((byte)this.struct2_0.ulong_0)), (Class11.Enum1)2);
			}
			public override Class11.Class24 vmethod_41()
			{
				return new Class11.Class24((int)(checked((ushort)this.struct2_0.long_0)), (Class11.Enum1)4);
			}
			public override Class11.Class24 vmethod_42()
			{
				return new Class11.Class24((int)(checked((ushort)this.struct2_0.ulong_0)), (Class11.Enum1)4);
			}
			public override Class11.Class24 vmethod_43()
			{
				return new Class11.Class24(checked((uint)this.struct2_0.long_0), (Class11.Enum1)6);
			}
			public override Class11.Class24 vmethod_44()
			{
				return new Class11.Class24(checked((uint)this.struct2_0.ulong_0), (Class11.Enum1)6);
			}
			public override Class11.Class25 vmethod_45()
			{
				return new Class11.Class25(checked((ulong)this.struct2_0.long_0), (Class11.Enum1)8);
			}
			public override Class11.Class25 vmethod_46()
			{
				return new Class11.Class25(this.struct2_0.ulong_0, (Class11.Enum1)8);
			}
			public override Class11.Class27 vmethod_47()
			{
				return new Class11.Class27((float)this.struct2_0.long_0);
			}
			public override Class11.Class27 vmethod_48()
			{
				return new Class11.Class27((double)this.struct2_0.long_0);
			}
			public override Class11.Class27 vmethod_49()
			{
				return new Class11.Class27(this.struct2_0.ulong_0);
			}
			public override Class11.Class26 vmethod_50()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_26().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_25().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_51()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_30().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_29().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_52()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_37().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_35().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_53()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_45().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_43().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_54()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_38().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_36().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_55()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)(checked((uint)this.struct2_0.ulong_0)));
				}
				return result;
			}
			public override Class11.Class22 vmethod_56()
			{
				return new Class11.Class25(-this.struct2_0.long_0);
			}
			public override Class11.Class22 vmethod_57(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(this.struct2_0.long_0 + ((Class11.Class25)class22_0).struct2_0.long_0);
			}
			public override Class11.Class22 vmethod_58(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(checked(this.struct2_0.long_0 + ((Class11.Class25)class22_0).struct2_0.long_0));
			}
			public override Class11.Class22 vmethod_59(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(checked(this.struct2_0.ulong_0 + ((Class11.Class25)class22_0).struct2_0.ulong_0));
			}
			public override Class11.Class22 vmethod_60(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(this.struct2_0.long_0 - ((Class11.Class25)class22_0).struct2_0.long_0);
			}
			public override Class11.Class22 vmethod_61(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(checked(this.struct2_0.long_0 - ((Class11.Class25)class22_0).struct2_0.long_0));
			}
			public override Class11.Class22 vmethod_62(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(checked(this.struct2_0.ulong_0 - ((Class11.Class25)class22_0).struct2_0.ulong_0));
			}
			public override Class11.Class22 vmethod_63(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(this.struct2_0.long_0 * ((Class11.Class25)class22_0).struct2_0.long_0);
			}
			public override Class11.Class22 vmethod_64(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(checked(this.struct2_0.long_0 * ((Class11.Class25)class22_0).struct2_0.long_0));
			}
			public override Class11.Class22 vmethod_65(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(checked(this.struct2_0.ulong_0 * ((Class11.Class25)class22_0).struct2_0.ulong_0));
			}
			public override Class11.Class22 vmethod_66(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(this.struct2_0.long_0 / ((Class11.Class25)class22_0).struct2_0.long_0);
			}
			public override Class11.Class22 vmethod_67(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(this.struct2_0.ulong_0 / ((Class11.Class25)class22_0).struct2_0.ulong_0);
			}
			public override Class11.Class22 vmethod_68(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(this.struct2_0.long_0 % ((Class11.Class25)class22_0).struct2_0.long_0);
			}
			public override Class11.Class22 vmethod_69(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(this.struct2_0.ulong_0 % ((Class11.Class25)class22_0).struct2_0.ulong_0);
			}
			public override Class11.Class22 vmethod_70(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(this.struct2_0.long_0 & ((Class11.Class25)class22_0).struct2_0.long_0);
			}
			public override Class11.Class22 vmethod_71(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(this.struct2_0.long_0 | ((Class11.Class25)class22_0).struct2_0.long_0);
			}
			public override Class11.Class22 vmethod_72()
			{
				return new Class11.Class25(~this.struct2_0.long_0);
			}
			public override Class11.Class22 vmethod_73(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class25(this.struct2_0.long_0 ^ ((Class11.Class25)class22_0).struct2_0.long_0);
			}
			public override Class11.Class22 vmethod_75(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.vmethod_3();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = new Class11.Class25(this.struct2_0.long_0 << ((Class11.Class23)class22_0).vmethod_19().struct1_0.int_0);
				}
				else
				{
					result = new Class11.Class25(this.struct2_0.long_0 << ((Class11.Class25)class22_0).struct2_0.int_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_76(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_3();
				Class11.Class22 result;
				if (flag2)
				{
					result = new Class11.Class25(this.struct2_0.long_0 >> ((Class11.Class25)class22_0).struct2_0.int_0);
				}
				else
				{
					bool flag3 = !class22_0.vmethod_3();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					result = new Class11.Class25(this.struct2_0.long_0 >> ((Class11.Class23)class22_0).vmethod_19().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_77(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.vmethod_3();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					result = new Class11.Class25(this.struct2_0.ulong_0 >> ((Class11.Class23)class22_0).vmethod_19().struct1_0.int_0);
				}
				else
				{
					result = new Class11.Class25(this.struct2_0.ulong_0 >> ((Class11.Class25)class22_0).struct2_0.int_0);
				}
				return result;
			}
			public override string ToString()
			{
				bool flag = this.enum1_0 == (Class11.Enum1)7;
				string result;
				if (flag)
				{
					result = this.struct2_0.long_0.ToString();
				}
				else
				{
					result = this.struct2_0.ulong_0.ToString();
				}
				return result;
			}
			internal override Class11.Class22 vmethod_8()
			{
				return this;
			}
			internal override bool vmethod_9()
			{
				return true;
			}
			internal override bool vmethod_5(Class11.Class22 class22_0)
			{
				bool flag = class22_0.method_0();
				bool result;
				if (flag)
				{
					result = ((Class11.Class34)class22_0).vmethod_5(this);
				}
				else
				{
					bool flag2 = class22_0.vmethod_0();
					if (flag2)
					{
						result = ((Class11.Class28)class22_0).vmethod_5(this);
					}
					else
					{
						Class11.Class22 @class = class22_0.vmethod_8();
						bool flag3 = !@class.method_3();
						result = (!flag3 && this.struct2_0.long_0 == ((Class11.Class25)@class).struct2_0.long_0);
					}
				}
				return result;
			}
			private static Class11.Class23 smethod_4(Class11.Class22 class22_0)
			{
				Class11.Class23 @class = class22_0 as Class11.Class23;
				bool flag = @class == null && class22_0.vmethod_0();
				if (flag)
				{
					@class = (class22_0.vmethod_8() as Class11.Class23);
				}
				return @class;
			}
			internal override bool vmethod_6(Class11.Class22 class22_0)
			{
				bool flag = class22_0.method_0();
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = !class22_0.vmethod_0();
					if (flag2)
					{
						Class11.Class22 @class = class22_0.vmethod_8();
						bool flag3 = @class.method_3();
						result = (flag3 && this.struct2_0.ulong_0 != ((Class11.Class25)@class).struct2_0.ulong_0);
					}
					else
					{
						result = ((Class11.Class28)class22_0).vmethod_6(this);
					}
				}
				return result;
			}
			public override bool vmethod_78(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.struct2_0.long_0 >= ((Class11.Class25)class22_0).struct2_0.long_0;
			}
			public override bool vmethod_79(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.struct2_0.ulong_0 >= ((Class11.Class25)class22_0).struct2_0.ulong_0;
			}
			public override bool vmethod_80(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.struct2_0.long_0 > ((Class11.Class25)class22_0).struct2_0.long_0;
			}
			public override bool vmethod_81(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.struct2_0.ulong_0 > ((Class11.Class25)class22_0).struct2_0.ulong_0;
			}
			public override bool vmethod_82(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.struct2_0.long_0 <= ((Class11.Class25)class22_0).struct2_0.long_0;
			}
			public override bool vmethod_83(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.struct2_0.ulong_0 <= ((Class11.Class25)class22_0).struct2_0.ulong_0;
			}
			public override bool vmethod_84(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.struct2_0.long_0 < ((Class11.Class25)class22_0).struct2_0.long_0;
			}
			public override bool vmethod_85(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_3();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.struct2_0.ulong_0 < ((Class11.Class25)class22_0).struct2_0.ulong_0;
			}
			public Class11.Struct2 struct2_0;
			public Class11.Enum1 enum1_0;
		}

		
		private class Class26 : Class11.Class23
		{			internal void method_6(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.method_2();
				if (flag)
				{
					this.vmethod_10(class22_0);
				}
				else
				{
					this.object_0 = ((Class11.Class26)class22_0).object_0;
					this.enum1_0 = ((Class11.Class26)class22_0).enum1_0;
				}
			}
			internal unsafe override void vmethod_10(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.method_2();
				if (flag)
				{
					object obj = class22_0.vmethod_4(null);
					bool flag2 = obj == null;
					if (!flag2)
					{
						IntPtr value = (IntPtr.Size != 8) ? new IntPtr(((Class11.Class24)this.object_0).struct1_0.int_0) : new IntPtr(((Class11.Class25)this.object_0).struct2_0.long_0);
						Type type = obj.GetType();
						bool flag3 = type == typeof(string);
						if (!flag3)
						{
							bool flag4 = type == typeof(byte);
							if (flag4)
							{
								*(byte*)((void*)value) = (byte)obj;
							}
							else
							{
								bool flag5 = type == typeof(sbyte);
								if (flag5)
								{
									*(byte*)((void*)value) = (byte)((sbyte)obj);
								}
								else
								{
									bool flag6 = !(type == typeof(short));
									if (flag6)
									{
										bool flag7 = type == typeof(ushort);
										if (flag7)
										{
											*(short*)((void*)value) = (short)((ushort)obj);
										}
										else
										{
											bool flag8 = type == typeof(int);
											if (flag8)
											{
												*(int*)((void*)value) = (int)obj;
											}
											else
											{
												bool flag9 = type == typeof(uint);
												if (flag9)
												{
													*(int*)((void*)value) = (int)((uint)obj);
												}
												else
												{
													bool flag10 = type == typeof(long);
													if (flag10)
													{
														*(long*)((void*)value) = (long)obj;
													}
													else
													{
														bool flag11 = !(type == typeof(ulong));
														if (flag11)
														{
															bool flag12 = !(type == typeof(float));
															if (flag12)
															{
																bool flag13 = !(type == typeof(double));
																if (flag13)
																{
																	bool flag14 = !(type == typeof(bool));
																	if (flag14)
																	{
																		bool flag15 = !(type == typeof(IntPtr));
																		if (flag15)
																		{
																			bool flag16 = !(type == typeof(UIntPtr));
																			if (flag16)
																			{
																				bool flag17 = !(type == typeof(char));
																				if (flag17)
																				{
																					throw new Class11.Exception1();
																				}
																				*(short*)((void*)value) = (short)((char)obj);
																			}
																			else
																			{
																				*(IntPtr*)((void*)value) = new IntPtr((long)((UIntPtr)obj).ToUInt64());
																			}
																		}
																		else
																		{
																			*(IntPtr*)((void*)value) = (IntPtr)obj;
																		}
																	}
																	else
																	{
																		*(byte*)((void*)value) = (byte)(((bool)obj) ? 1 : 0);
																	}
																}
																else
																{
																	*(double*)((void*)value) = (double)obj;
																}
															}
															else
															{
																*(float*)((void*)value) = (float)obj;
															}
														}
														else
														{
															*(long*)((void*)value) = (long)((ulong)obj);
														}
													}
												}
											}
										}
									}
									else
									{
										*(short*)((void*)value) = (short)obj;
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag18 = IntPtr.Size == 8;
					if (flag18)
					{
						IntPtr value2 = new IntPtr(((Class11.Class25)this.object_0).struct2_0.long_0);
						IntPtr intPtr = new IntPtr(((Class11.Class25)((Class11.Class26)class22_0).object_0).struct2_0.long_0);
						*(long*)((void*)value2) = intPtr.ToInt64();
					}
					else
					{
						IntPtr value3 = new IntPtr(((Class11.Class24)this.object_0).struct1_0.int_0);
						IntPtr intPtr2 = new IntPtr(((Class11.Class24)((Class11.Class26)class22_0).object_0).struct1_0.int_0);
						*(int*)((void*)value3) = intPtr2.ToInt32();
					}
				}
			}
			internal override void vmethod_2(Class11.Class22 class22_0)
			{
				this.vmethod_10(class22_0);
			}
			public Class26(IntPtr intptr_0)
			{
				this.enum4_0 = (Class11.Enum4)3;
				bool flag = IntPtr.Size == 8;
				if (flag)
				{
					this.object_0 = new Class11.Class25(intptr_0.ToInt64());
					this.enum1_0 = (Class11.Enum1)12;
				}
				else
				{
					this.object_0 = new Class11.Class24(intptr_0.ToInt32());
					this.enum1_0 = (Class11.Enum1)12;
				}
			}
			public Class26(UIntPtr uintptr_0)
			{
				this.enum4_0 = (Class11.Enum4)3;
				bool flag = IntPtr.Size == 8;
				if (flag)
				{
					this.object_0 = new Class11.Class25(uintptr_0.ToUInt64());
					this.enum1_0 = (Class11.Enum1)12;
				}
				else
				{
					this.object_0 = new Class11.Class24(uintptr_0.ToUInt32());
					this.enum1_0 = (Class11.Enum1)12;
				}
			}
			public Class26()
			{
				this.enum4_0 = (Class11.Enum4)3;
				bool flag = IntPtr.Size == 8;
				if (flag)
				{
					this.object_0 = new Class11.Class25(0L);
					this.enum1_0 = (Class11.Enum1)12;
				}
				else
				{
					this.object_0 = new Class11.Class24(0);
					this.enum1_0 = (Class11.Enum1)12;
				}
			}
			public override Class11.Class23 vmethod_74()
			{
				return new Class11.Class26
				{
					object_0 = ((Class11.Class23)this.object_0).vmethod_74(),
					enum1_0 = this.enum1_0
				};
			}
			public Class26(long long_0)
			{
				this.enum4_0 = (Class11.Enum4)3;
				bool flag = IntPtr.Size == 8;
				if (flag)
				{
					this.object_0 = new Class11.Class25(long_0);
					this.enum1_0 = (Class11.Enum1)12;
				}
				else
				{
					this.object_0 = new Class11.Class24((int)long_0);
					this.enum1_0 = (Class11.Enum1)12;
				}
			}
			public Class26(long long_0, Class11.Enum1 enum1_1)
			{
				this.enum4_0 = (Class11.Enum4)3;
				bool flag = IntPtr.Size == 8;
				if (flag)
				{
					this.object_0 = new Class11.Class25(long_0);
					this.enum1_0 = enum1_1;
				}
				else
				{
					this.object_0 = new Class11.Class24((int)long_0);
					this.enum1_0 = enum1_1;
				}
			}
			public Class26(ulong ulong_0)
			{
				this.enum4_0 = (Class11.Enum4)4;
				bool flag = IntPtr.Size == 8;
				if (flag)
				{
					this.object_0 = new Class11.Class25(ulong_0);
					this.enum1_0 = (Class11.Enum1)13;
				}
				else
				{
					this.object_0 = new Class11.Class24((uint)ulong_0);
					this.enum1_0 = (Class11.Enum1)13;
				}
			}
			public Class26(ulong ulong_0, Class11.Enum1 enum1_1)
			{
				this.enum4_0 = (Class11.Enum4)4;
				bool flag = IntPtr.Size == 8;
				if (flag)
				{
					this.object_0 = new Class11.Class25(ulong_0);
					this.enum1_0 = enum1_1;
				}
				else
				{
					this.object_0 = new Class11.Class24((uint)ulong_0);
					this.enum1_0 = enum1_1;
				}
			}
			public override bool vmethod_11()
			{
				return ((Class11.Class23)this.object_0).vmethod_11();
			}
			public override bool vmethod_12()
			{
				return !this.vmethod_11();
			}
			internal override bool vmethod_7()
			{
				return this.vmethod_12();
			}
			internal override bool vmethod_1()
			{
				return true;
			}
			public override Class11.Class22 vmethod_13(Class11.Enum1 enum1_1)
			{
				if (!true)
				{
				}
				Class11.Class23 result;
				switch (enum1_1)
				{
				case (Class11.Enum1)1:
					result = this.vmethod_15();
					goto IL_CA;
				case (Class11.Enum1)2:
					result = this.vmethod_16();
					goto IL_CA;
				case (Class11.Enum1)3:
					result = this.vmethod_17();
					goto IL_CA;
				case (Class11.Enum1)4:
					result = this.vmethod_18();
					goto IL_CA;
				case (Class11.Enum1)5:
					result = this.vmethod_19();
					goto IL_CA;
				case (Class11.Enum1)6:
					result = this.vmethod_20();
					goto IL_CA;
				case (Class11.Enum1)7:
					result = this.vmethod_21();
					goto IL_CA;
				case (Class11.Enum1)8:
					result = this.vmethod_22();
					goto IL_CA;
				case (Class11.Enum1)11:
					result = this.vmethod_14();
					goto IL_CA;
				case (Class11.Enum1)12:
					result = this;
					goto IL_CA;
				case (Class11.Enum1)13:
					result = this;
					goto IL_CA;
				case (Class11.Enum1)16:
					result = this.vmethod_74();
					goto IL_CA;
				}
				throw new Exception(((Class11.Enum5)4).ToString());
				IL_CA:
				if (!true)
				{
				}
				return result;
			}
			internal IntPtr method_7()
			{
				bool flag = IntPtr.Size == 8;
				IntPtr result;
				if (flag)
				{
					result = new IntPtr(((Class11.Class25)this.object_0).struct2_0.long_0);
				}
				else
				{
					result = new IntPtr(((Class11.Class24)this.object_0).struct1_0.int_0);
				}
				return result;
			}
			internal override object vmethod_4(Type type_0)
			{
				bool flag = type_0 != null && type_0.IsByRef;
				if (flag)
				{
					type_0 = type_0.GetElementType();
				}
				bool flag2 = type_0 == typeof(IntPtr);
				object result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new IntPtr(((Class11.Class25)this.object_0).struct2_0.long_0);
					}
					else
					{
						result = new IntPtr(((Class11.Class24)this.object_0).struct1_0.int_0);
					}
				}
				else
				{
					bool flag4 = !(type_0 == typeof(UIntPtr));
					if (flag4)
					{
						bool flag5 = !(type_0 == null) && !(type_0 == typeof(object));
						if (flag5)
						{
							throw new Class11.Exception1();
						}
						bool flag6 = IntPtr.Size == 8;
						if (flag6)
						{
							bool flag7 = this.enum1_0 == (Class11.Enum1)12;
							if (flag7)
							{
								result = new IntPtr(((Class11.Class25)this.object_0).struct2_0.long_0);
							}
							else
							{
								result = new UIntPtr(((Class11.Class25)this.object_0).struct2_0.ulong_0);
							}
						}
						else
						{
							bool flag8 = this.enum1_0 == (Class11.Enum1)12;
							if (flag8)
							{
								result = new IntPtr(((Class11.Class25)this.object_0).struct2_0.int_0);
							}
							else
							{
								result = new UIntPtr(((Class11.Class24)this.object_0).struct1_0.uint_0);
							}
						}
					}
					else
					{
						bool flag9 = IntPtr.Size == 8;
						if (flag9)
						{
							result = new UIntPtr(((Class11.Class25)this.object_0).struct2_0.ulong_0);
						}
						else
						{
							result = new UIntPtr(((Class11.Class24)this.object_0).struct1_0.uint_0);
						}
					}
				}
				return result;
			}
			public override Class11.Class24 vmethod_14()
			{
				return ((Class11.Class23)this.object_0).vmethod_14();
			}
			public override Class11.Class24 vmethod_15()
			{
				return ((Class11.Class23)this.object_0).vmethod_15();
			}
			public override Class11.Class24 vmethod_16()
			{
				return ((Class11.Class23)this.object_0).vmethod_16();
			}
			public override Class11.Class24 vmethod_17()
			{
				return ((Class11.Class23)this.object_0).vmethod_17();
			}
			public override Class11.Class24 vmethod_18()
			{
				return ((Class11.Class23)this.object_0).vmethod_18();
			}
			public override Class11.Class24 vmethod_19()
			{
				return ((Class11.Class23)this.object_0).vmethod_19();
			}
			public override Class11.Class24 vmethod_20()
			{
				return ((Class11.Class23)this.object_0).vmethod_20();
			}
			public override Class11.Class25 vmethod_21()
			{
				return ((Class11.Class23)this.object_0).vmethod_21();
			}
			public override Class11.Class25 vmethod_22()
			{
				return ((Class11.Class23)this.object_0).vmethod_22();
			}
			public override Class11.Class24 vmethod_23()
			{
				return this.vmethod_15();
			}
			public override Class11.Class24 vmethod_24()
			{
				return this.vmethod_17();
			}
			public override Class11.Class24 vmethod_25()
			{
				return this.vmethod_19();
			}
			public override Class11.Class25 vmethod_26()
			{
				return this.vmethod_21();
			}
			public override Class11.Class24 vmethod_27()
			{
				return this.vmethod_16();
			}
			public override Class11.Class24 vmethod_28()
			{
				return this.vmethod_18();
			}
			public override Class11.Class24 vmethod_29()
			{
				return this.vmethod_20();
			}
			public override Class11.Class25 vmethod_30()
			{
				return this.vmethod_22();
			}
			public override Class11.Class24 vmethod_31()
			{
				return ((Class11.Class23)this.object_0).vmethod_31();
			}
			public override Class11.Class24 vmethod_32()
			{
				return ((Class11.Class23)this.object_0).vmethod_32();
			}
			public override Class11.Class24 vmethod_33()
			{
				return ((Class11.Class23)this.object_0).vmethod_33();
			}
			public override Class11.Class24 vmethod_34()
			{
				return ((Class11.Class23)this.object_0).vmethod_34();
			}
			public override Class11.Class24 vmethod_35()
			{
				return ((Class11.Class23)this.object_0).vmethod_35();
			}
			public override Class11.Class24 vmethod_36()
			{
				return ((Class11.Class23)this.object_0).vmethod_36();
			}
			public override Class11.Class25 vmethod_37()
			{
				return ((Class11.Class23)this.object_0).vmethod_37();
			}
			public override Class11.Class25 vmethod_38()
			{
				return ((Class11.Class23)this.object_0).vmethod_38();
			}
			public override Class11.Class24 vmethod_39()
			{
				return ((Class11.Class23)this.object_0).vmethod_39();
			}
			public override Class11.Class24 vmethod_40()
			{
				return ((Class11.Class23)this.object_0).vmethod_40();
			}
			public override Class11.Class24 vmethod_41()
			{
				return ((Class11.Class23)this.object_0).vmethod_41();
			}
			public override Class11.Class24 vmethod_42()
			{
				return ((Class11.Class23)this.object_0).vmethod_42();
			}
			public override Class11.Class24 vmethod_43()
			{
				return ((Class11.Class23)this.object_0).vmethod_43();
			}
			public override Class11.Class24 vmethod_44()
			{
				return ((Class11.Class23)this.object_0).vmethod_44();
			}
			public override Class11.Class25 vmethod_45()
			{
				return ((Class11.Class23)this.object_0).vmethod_45();
			}
			public override Class11.Class25 vmethod_46()
			{
				return ((Class11.Class23)this.object_0).vmethod_46();
			}
			public override Class11.Class27 vmethod_47()
			{
				return ((Class11.Class23)this.object_0).vmethod_47();
			}
			public override Class11.Class27 vmethod_48()
			{
				return ((Class11.Class23)this.object_0).vmethod_48();
			}
			public override Class11.Class27 vmethod_49()
			{
				return ((Class11.Class23)this.object_0).vmethod_49();
			}
			public override Class11.Class26 vmethod_50()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_26().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_25().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_51()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_30().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_29().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_52()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_37().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_35().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_53()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_45().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_43().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_54()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_38().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_36().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_55()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_46().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_44().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_56()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class22 result;
				if (flag)
				{
					result = new Class11.Class26(-((Class11.Class25)this.object_0).struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)(-(long)((Class11.Class24)this.object_0).struct1_0.int_0));
				}
				return result;
			}
			public override Class11.Class22 vmethod_57(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 + ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 + ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 + ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 + ((Class11.Class24)class22_0).struct1_0.int_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_58(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.long_0 + ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0));
					}
					else
					{
						result = new Class11.Class26((long)(checked(this.vmethod_19().struct1_0.int_0 + ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0)));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.long_0 + ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0));
					}
					else
					{
						result = new Class11.Class26((long)(checked(this.vmethod_19().struct1_0.int_0 + ((Class11.Class24)class22_0).struct1_0.int_0)));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_59(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.ulong_0 + unchecked((ulong)((Class11.Class24)class22_0).struct1_0.uint_0)));
					}
					else
					{
						result = new Class11.Class26((long)((ulong)(checked(this.vmethod_19().struct1_0.uint_0 + ((Class11.Class24)class22_0).struct1_0.uint_0))));
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.ulong_0 + ((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0));
					}
					else
					{
						result = new Class11.Class26((ulong)(checked(this.vmethod_19().struct1_0.uint_0 + ((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0)));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_60(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 - ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 - ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 - ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 - ((Class11.Class24)class22_0).struct1_0.int_0));
					}
				}
				return result;
			}
			public Class11.Class22 method_8(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0 - this.vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0 - this.vmethod_19().struct1_0.int_0));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0 - this.vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(((Class11.Class24)class22_0).struct1_0.int_0 - this.vmethod_19().struct1_0.int_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_61(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.long_0 - ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0));
					}
					else
					{
						result = new Class11.Class26((long)(checked(this.vmethod_19().struct1_0.int_0 - ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0)));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.long_0 - ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0));
					}
					else
					{
						result = new Class11.Class26((long)(checked(this.vmethod_19().struct1_0.int_0 - ((Class11.Class24)class22_0).struct1_0.int_0)));
					}
				}
				return result;
			}
			public Class11.Class22 method_9(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(checked(((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0 - this.vmethod_21().struct2_0.long_0));
					}
					else
					{
						result = new Class11.Class26((long)(checked(((Class11.Class24)class22_0).struct1_0.int_0 - this.vmethod_19().struct1_0.int_0)));
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(checked(((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0 - this.vmethod_21().struct2_0.long_0));
					}
					else
					{
						result = new Class11.Class26((long)(checked(((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0 - this.vmethod_19().struct1_0.int_0)));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_62(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.ulong_0 - ((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0));
					}
					else
					{
						result = new Class11.Class26((ulong)(checked(this.vmethod_19().struct1_0.uint_0 - ((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0)));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.ulong_0 - unchecked((ulong)((Class11.Class24)class22_0).struct1_0.uint_0)));
					}
					else
					{
						result = new Class11.Class26((long)((ulong)(checked(this.vmethod_19().struct1_0.uint_0 - ((Class11.Class24)class22_0).struct1_0.uint_0))));
					}
				}
				return result;
			}
			public Class11.Class22 method_10(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(checked(((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0 - this.vmethod_21().struct2_0.ulong_0));
					}
					else
					{
						result = new Class11.Class26((ulong)(checked(((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0 - this.vmethod_19().struct1_0.uint_0)));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(checked(unchecked((ulong)((Class11.Class24)class22_0).struct1_0.uint_0) - this.vmethod_21().struct2_0.ulong_0));
					}
					else
					{
						result = new Class11.Class26((long)((ulong)(checked(((Class11.Class24)class22_0).struct1_0.uint_0 - this.vmethod_19().struct1_0.uint_0))));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_63(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 * ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 * ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 * ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 * ((Class11.Class24)class22_0).struct1_0.int_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_64(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.long_0 * ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0));
					}
					else
					{
						result = new Class11.Class26((long)(checked(this.vmethod_19().struct1_0.int_0 * ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0)));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.long_0 * ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0));
					}
					else
					{
						result = new Class11.Class26((long)(checked(this.vmethod_19().struct1_0.int_0 * ((Class11.Class24)class22_0).struct1_0.int_0)));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_65(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.ulong_0 * unchecked((ulong)((Class11.Class24)class22_0).struct1_0.uint_0)));
					}
					else
					{
						result = new Class11.Class26((long)((ulong)(checked(this.vmethod_19().struct1_0.uint_0 * ((Class11.Class24)class22_0).struct1_0.uint_0))));
					}
				}
				else
				{
					bool flag4 = !class22_0.method_2();
					if (flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(checked(this.vmethod_21().struct2_0.ulong_0 * ((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0));
					}
					else
					{
						result = new Class11.Class26((ulong)(checked(this.vmethod_19().struct1_0.uint_0 * ((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0)));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_66(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 / ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 / ((Class11.Class24)class22_0).struct1_0.int_0));
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 / ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 / ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0));
					}
				}
				return result;
			}
			public Class11.Class22 method_11(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0 / this.vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0 / this.vmethod_19().struct1_0.int_0));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0 / this.vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(((Class11.Class24)class22_0).struct1_0.int_0 / this.vmethod_19().struct1_0.int_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_67(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.ulong_0 / ((Class11.Class24)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = new Class11.Class26((long)((ulong)(this.vmethod_19().struct1_0.uint_0 / ((Class11.Class24)class22_0).struct1_0.uint_0)));
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.ulong_0 / ((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = new Class11.Class26((ulong)(this.vmethod_19().struct1_0.uint_0 / ((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0));
					}
				}
				return result;
			}
			public Class11.Class22 method_12(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0 / this.vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = new Class11.Class26((ulong)(((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0 / this.vmethod_19().struct1_0.uint_0));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(((Class11.Class24)class22_0).vmethod_21().struct2_0.ulong_0 / this.vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = new Class11.Class26((long)((ulong)(((Class11.Class24)class22_0).struct1_0.uint_0 / this.vmethod_19().struct1_0.uint_0)));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_68(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 % ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 % ((Class11.Class24)class22_0).struct1_0.int_0));
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 % ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 % ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0));
					}
				}
				return result;
			}
			public Class11.Class22 method_13(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0 % this.vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0 % this.vmethod_19().struct1_0.int_0));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0 % this.vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(((Class11.Class24)class22_0).struct1_0.int_0 % this.vmethod_19().struct1_0.int_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_69(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.ulong_0 % ((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = new Class11.Class26((ulong)(this.vmethod_19().struct1_0.uint_0 % ((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.ulong_0 % ((Class11.Class24)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = new Class11.Class26((long)((ulong)(this.vmethod_19().struct1_0.uint_0 % ((Class11.Class24)class22_0).struct1_0.uint_0)));
					}
				}
				return result;
			}
			public Class11.Class22 method_14(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(((Class11.Class24)class22_0).vmethod_21().struct2_0.ulong_0 % this.vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = new Class11.Class26((long)((ulong)(((Class11.Class24)class22_0).struct1_0.uint_0 % this.vmethod_19().struct1_0.uint_0)));
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0 % this.vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = new Class11.Class26((ulong)(((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0 % this.vmethod_19().struct1_0.uint_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_70(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 & ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 & ((Class11.Class24)class22_0).struct1_0.int_0));
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 & ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 & ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_71(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 | ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 | ((Class11.Class24)class22_0).struct1_0.int_0));
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 | ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 | ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_72()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class22 result;
				if (flag)
				{
					result = new Class11.Class26(~this.vmethod_21().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)(~(long)this.vmethod_19().struct1_0.int_0));
				}
				return result;
			}
			public override Class11.Class22 vmethod_73(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 ^ ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 ^ ((Class11.Class24)class22_0).struct1_0.int_0));
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 ^ ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 ^ ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_75(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 << ((Class11.Class26)class22_0).vmethod_21().struct2_0.int_0);
					}
					else
					{
						result = new Class11.Class26((long)((long)this.vmethod_19().struct1_0.int_0 << ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 << ((Class11.Class24)class22_0).struct1_0.int_0);
					}
					else
					{
						result = new Class11.Class26((long)((long)this.vmethod_19().struct1_0.int_0 << ((Class11.Class24)class22_0).struct1_0.int_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_76(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 >> ((Class11.Class24)class22_0).struct1_0.int_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 >> ((Class11.Class24)class22_0).struct1_0.int_0));
					}
				}
				else
				{
					bool flag4 = !class22_0.method_2();
					if (flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.long_0 >> ((Class11.Class26)class22_0).vmethod_21().struct2_0.int_0);
					}
					else
					{
						result = new Class11.Class26((long)(this.vmethod_19().struct1_0.int_0 >> ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0));
					}
				}
				return result;
			}
			public override Class11.Class22 vmethod_77(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.ulong_0 >> ((Class11.Class26)class22_0).vmethod_21().struct2_0.int_0);
					}
					else
					{
						result = new Class11.Class26((long)((ulong)(this.vmethod_19().struct1_0.uint_0 >> ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0)));
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = new Class11.Class26(this.vmethod_21().struct2_0.ulong_0 >> ((Class11.Class24)class22_0).struct1_0.int_0);
					}
					else
					{
						result = new Class11.Class26((long)((ulong)(this.vmethod_19().struct1_0.uint_0 >> ((Class11.Class24)class22_0).struct1_0.int_0)));
					}
				}
				return result;
			}
			public Class11.Class22 method_15(Class11.Class24 class24_0)
			{
				return new Class11.Class26((long)((ulong)(class24_0.struct1_0.uint_0 >> this.vmethod_19().struct1_0.int_0)));
			}
			public Class11.Class22 method_16(Class11.Class24 class24_0)
			{
				return new Class11.Class26((long)(class24_0.struct1_0.int_0 >> this.vmethod_21().struct2_0.int_0));
			}
			public Class11.Class22 method_17(Class11.Class24 class24_0)
			{
				return new Class11.Class26((long)((long)class24_0.struct1_0.int_0 << this.vmethod_21().struct2_0.int_0));
			}
			public override string ToString()
			{
				return this.object_0.ToString();
			}
			internal override Class11.Class22 vmethod_8()
			{
				return this;
			}
			internal override bool vmethod_9()
			{
				return true;
			}
			internal override bool vmethod_5(Class11.Class22 class22_0)
			{
				bool flag = class22_0.method_0();
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = !class22_0.vmethod_0();
					if (flag2)
					{
						Class11.Class22 @class = class22_0.vmethod_8();
						bool flag3 = !@class.vmethod_9();
						if (flag3)
						{
							result = false;
						}
						else
						{
							bool flag4 = @class.method_1();
							if (flag4)
							{
								bool flag5 = IntPtr.Size == 8;
								if (flag5)
								{
									result = (this.vmethod_21().struct2_0.long_0 == ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
								}
								else
								{
									result = (this.vmethod_19().struct1_0.int_0 == ((Class11.Class24)class22_0).struct1_0.int_0);
								}
							}
							else
							{
								bool flag6 = @class.method_2();
								if (flag6)
								{
									int size = IntPtr.Size;
									result = (this.vmethod_21().struct2_0.long_0 == ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
								}
								else
								{
									result = false;
								}
							}
						}
					}
					else
					{
						result = ((Class11.Class28)class22_0).vmethod_5(this);
					}
				}
				return result;
			}
			internal override bool vmethod_6(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.method_0();
				bool result;
				if (flag)
				{
					bool flag2 = class22_0.vmethod_0();
					if (flag2)
					{
						result = ((Class11.Class28)class22_0).vmethod_6(this);
					}
					else
					{
						Class11.Class22 @class = class22_0.vmethod_8();
						bool flag3 = !@class.vmethod_9();
						if (flag3)
						{
							result = false;
						}
						else
						{
							bool flag4 = @class.method_1();
							if (flag4)
							{
								bool flag5 = IntPtr.Size == 8;
								if (flag5)
								{
									result = (this.vmethod_21().struct2_0.ulong_0 != ((Class11.Class24)class22_0).vmethod_21().struct2_0.ulong_0);
								}
								else
								{
									result = (this.vmethod_19().struct1_0.uint_0 != ((Class11.Class24)class22_0).struct1_0.uint_0);
								}
							}
							else
							{
								bool flag6 = !@class.method_2();
								if (flag6)
								{
									result = false;
								}
								else
								{
									int size = IntPtr.Size;
									result = (this.vmethod_21().struct2_0.ulong_0 != ((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0);
								}
							}
						}
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			public override bool vmethod_78(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = (this.vmethod_21().struct2_0.long_0 >= ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.int_0 >= ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0);
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = (this.vmethod_21().struct2_0.long_0 >= ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.int_0 >= ((Class11.Class24)class22_0).struct1_0.int_0);
					}
				}
				return result;
			}
			public override bool vmethod_79(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = (this.vmethod_21().struct2_0.ulong_0 >= ((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.uint_0 >= ((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0);
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = (this.vmethod_21().struct2_0.ulong_0 >= ((Class11.Class24)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.uint_0 >= ((Class11.Class24)class22_0).struct1_0.uint_0);
					}
				}
				return result;
			}
			public override bool vmethod_80(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = (this.vmethod_21().struct2_0.long_0 > ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.int_0 > ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0);
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = (this.vmethod_21().struct2_0.long_0 > ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.int_0 > ((Class11.Class24)class22_0).struct1_0.int_0);
					}
				}
				return result;
			}
			public override bool vmethod_81(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = (this.vmethod_21().struct2_0.ulong_0 > ((Class11.Class24)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.uint_0 > ((Class11.Class24)class22_0).struct1_0.uint_0);
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = (this.vmethod_21().struct2_0.ulong_0 > ((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.uint_0 > ((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0);
					}
				}
				return result;
			}
			public override bool vmethod_82(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = (this.vmethod_21().struct2_0.long_0 <= ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.int_0 <= ((Class11.Class24)class22_0).struct1_0.int_0);
					}
				}
				else
				{
					bool flag4 = !class22_0.method_2();
					if (flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = (this.vmethod_21().struct2_0.long_0 <= ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.int_0 <= ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0);
					}
				}
				return result;
			}
			public override bool vmethod_83(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = !class22_0.method_2();
					if (flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = (this.vmethod_21().struct2_0.ulong_0 <= ((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.uint_0 <= ((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0);
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = (this.vmethod_21().struct2_0.ulong_0 <= ((Class11.Class24)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.uint_0 <= ((Class11.Class24)class22_0).struct1_0.uint_0);
					}
				}
				return result;
			}
			public override bool vmethod_84(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = IntPtr.Size == 8;
					if (flag3)
					{
						result = (this.vmethod_21().struct2_0.long_0 < ((Class11.Class24)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.int_0 < ((Class11.Class24)class22_0).struct1_0.int_0);
					}
				}
				else
				{
					bool flag4 = class22_0.method_2();
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = (this.vmethod_21().struct2_0.long_0 < ((Class11.Class26)class22_0).vmethod_21().struct2_0.long_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.int_0 < ((Class11.Class26)class22_0).vmethod_19().struct1_0.int_0);
					}
				}
				return result;
			}
			public override bool vmethod_85(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_1();
				bool result;
				if (flag2)
				{
					bool flag3 = class22_0.method_2();
					if (!flag3)
					{
						throw new Class11.Exception1();
					}
					bool flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						result = (this.vmethod_21().struct2_0.ulong_0 < ((Class11.Class26)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.uint_0 < ((Class11.Class26)class22_0).vmethod_19().struct1_0.uint_0);
					}
				}
				else
				{
					bool flag5 = IntPtr.Size == 8;
					if (flag5)
					{
						result = (this.vmethod_21().struct2_0.ulong_0 < ((Class11.Class24)class22_0).vmethod_21().struct2_0.ulong_0);
					}
					else
					{
						result = (this.vmethod_19().struct1_0.uint_0 < ((Class11.Class24)class22_0).struct1_0.uint_0);
					}
				}
				return result;
			}
			public object object_0;
			public Class11.Enum1 enum1_0;
		}

		
		private abstract class Class23 : Class11.Class22
		{			public abstract bool vmethod_11();
			public abstract bool vmethod_12();
			public abstract Class11.Class22 vmethod_13(Class11.Enum1 enum1_0);
			public abstract Class11.Class24 vmethod_14();
			public abstract Class11.Class24 vmethod_15();
			public abstract Class11.Class24 vmethod_16();
			public abstract Class11.Class24 vmethod_17();
			public abstract Class11.Class24 vmethod_18();
			public abstract Class11.Class24 vmethod_19();
			public abstract Class11.Class24 vmethod_20();
			public abstract Class11.Class25 vmethod_21();
			public abstract Class11.Class25 vmethod_22();
			public abstract Class11.Class24 vmethod_23();
			public abstract Class11.Class24 vmethod_24();
			public abstract Class11.Class24 vmethod_25();
			public abstract Class11.Class25 vmethod_26();
			public abstract Class11.Class24 vmethod_27();
			public abstract Class11.Class24 vmethod_28();
			public abstract Class11.Class24 vmethod_29();
			public abstract Class11.Class25 vmethod_30();
			public abstract Class11.Class24 vmethod_31();
			public abstract Class11.Class24 vmethod_32();
			public abstract Class11.Class24 vmethod_33();
			public abstract Class11.Class24 vmethod_34();
			public abstract Class11.Class24 vmethod_35();
			public abstract Class11.Class24 vmethod_36();
			public abstract Class11.Class25 vmethod_37();
			public abstract Class11.Class25 vmethod_38();
			public abstract Class11.Class24 vmethod_39();
			public abstract Class11.Class24 vmethod_40();
			public abstract Class11.Class24 vmethod_41();
			public abstract Class11.Class24 vmethod_42();
			public abstract Class11.Class24 vmethod_43();
			public abstract Class11.Class24 vmethod_44();
			public abstract Class11.Class25 vmethod_45();
			public abstract Class11.Class25 vmethod_46();
			public abstract Class11.Class27 vmethod_47();
			public abstract Class11.Class27 vmethod_48();
			public abstract Class11.Class27 vmethod_49();
			public abstract Class11.Class26 vmethod_50();
			public abstract Class11.Class26 vmethod_51();
			public abstract Class11.Class26 vmethod_52();
			public abstract Class11.Class26 vmethod_53();
			public abstract Class11.Class26 vmethod_54();
			public abstract Class11.Class26 vmethod_55();
			public abstract Class11.Class22 vmethod_56();
			public abstract Class11.Class22 vmethod_57(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_58(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_59(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_60(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_61(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_62(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_63(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_64(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_65(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_66(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_67(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_68(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_69(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_70(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_71(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_72();
			public abstract Class11.Class22 vmethod_73(Class11.Class22 class22_0);
			public abstract Class11.Class23 vmethod_74();
			public abstract Class11.Class22 vmethod_75(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_76(Class11.Class22 class22_0);
			public abstract Class11.Class22 vmethod_77(Class11.Class22 class22_0);
			public abstract bool vmethod_78(Class11.Class22 class22_0);
			public abstract bool vmethod_79(Class11.Class22 class22_0);
			public abstract bool vmethod_80(Class11.Class22 class22_0);
			public abstract bool vmethod_81(Class11.Class22 class22_0);
			public abstract bool vmethod_82(Class11.Class22 class22_0);
			public abstract bool vmethod_83(Class11.Class22 class22_0);
			public abstract bool vmethod_84(Class11.Class22 class22_0);
			public abstract bool vmethod_85(Class11.Class22 class22_0);
			internal override bool vmethod_3()
			{
				return true;
			}
		}

		
		private class Class27 : Class11.Class23
		{			internal override void vmethod_10(Class11.Class22 class22_0)
			{
				this.double_0 = ((Class11.Class27)class22_0).double_0;
				this.enum1_0 = ((Class11.Class27)class22_0).enum1_0;
			}
			internal override void vmethod_2(Class11.Class22 class22_0)
			{
				this.vmethod_10(class22_0);
			}
			public Class27(double double_1)
			{
				this.enum4_0 = (Class11.Enum4)5;
				this.enum1_0 = (Class11.Enum1)10;
				this.double_0 = double_1;
			}
			public Class27(Class11.Class27 class27_0)
			{
				this.enum4_0 = class27_0.enum4_0;
				this.enum1_0 = class27_0.enum1_0;
				this.double_0 = class27_0.double_0;
			}
			public override Class11.Class23 vmethod_74()
			{
				return new Class11.Class27(this);
			}
			public Class27(double double_1, Class11.Enum1 enum1_1)
			{
				this.enum4_0 = (Class11.Enum4)5;
				this.double_0 = double_1;
				this.enum1_0 = enum1_1;
			}
			public Class27(float float_0)
			{
				this.enum4_0 = (Class11.Enum4)5;
				this.double_0 = (double)float_0;
				this.enum1_0 = (Class11.Enum1)9;
			}
			public Class27(float float_0, Class11.Enum1 enum1_1)
			{
				this.enum4_0 = (Class11.Enum4)5;
				this.double_0 = (double)float_0;
				this.enum1_0 = enum1_1;
			}
			public override bool vmethod_11()
			{
				return this.double_0 == 0.0;
			}
			public override bool vmethod_12()
			{
				return !this.vmethod_11();
			}
			public override string ToString()
			{
				return this.double_0.ToString();
			}
			public override Class11.Class22 vmethod_13(Class11.Enum1 enum1_1)
			{
				if (!true)
				{
				}
				Class11.Class22 result;
				switch (enum1_1)
				{
				case (Class11.Enum1)1:
					result = this.vmethod_15();
					break;
				case (Class11.Enum1)2:
					result = this.vmethod_16();
					break;
				case (Class11.Enum1)3:
					result = this.vmethod_17();
					break;
				case (Class11.Enum1)4:
					result = this.vmethod_18();
					break;
				case (Class11.Enum1)5:
					result = this.vmethod_19();
					break;
				case (Class11.Enum1)6:
					result = this.vmethod_20();
					break;
				case (Class11.Enum1)7:
					result = this.vmethod_21();
					break;
				case (Class11.Enum1)8:
					result = this.vmethod_22();
					break;
				case (Class11.Enum1)9:
					result = this.vmethod_47();
					break;
				case (Class11.Enum1)10:
					result = this.vmethod_48();
					break;
				case (Class11.Enum1)11:
					result = this.vmethod_14();
					break;
				default:
					throw new Exception(((Class11.Enum5)4).ToString());
				}
				if (!true)
				{
				}
				return result;
			}
			internal override object vmethod_4(Type type_0)
			{
				bool flag = type_0 != null && type_0.IsByRef;
				if (flag)
				{
					type_0 = type_0.GetElementType();
				}
				bool flag2 = !(type_0 == typeof(float));
				object result;
				if (flag2)
				{
					bool flag3 = type_0 == typeof(double);
					if (flag3)
					{
						result = this.double_0;
					}
					else
					{
						bool flag4 = (type_0 == null || type_0 == typeof(object)) && this.enum1_0 == (Class11.Enum1)9;
						if (flag4)
						{
							result = (float)this.double_0;
						}
						else
						{
							result = this.double_0;
						}
					}
				}
				else
				{
					result = (float)this.double_0;
				}
				return result;
			}
			public override Class11.Class24 vmethod_14()
			{
				return new Class11.Class24(this.vmethod_11() ? 1 : 0);
			}
			internal override bool vmethod_7()
			{
				return this.vmethod_12();
			}
			public override Class11.Class24 vmethod_15()
			{
				return new Class11.Class24((int)((sbyte)this.double_0), (Class11.Enum1)1);
			}
			public override Class11.Class24 vmethod_16()
			{
				return new Class11.Class24((uint)((byte)this.double_0), (Class11.Enum1)2);
			}
			public override Class11.Class24 vmethod_17()
			{
				return new Class11.Class24((int)((short)this.double_0), (Class11.Enum1)3);
			}
			public override Class11.Class24 vmethod_18()
			{
				return new Class11.Class24((uint)((ushort)this.double_0), (Class11.Enum1)4);
			}
			public override Class11.Class24 vmethod_19()
			{
				return new Class11.Class24((int)this.double_0, (Class11.Enum1)5);
			}
			public override Class11.Class24 vmethod_20()
			{
				return new Class11.Class24((uint)this.double_0, (Class11.Enum1)6);
			}
			public override Class11.Class25 vmethod_21()
			{
				return new Class11.Class25((long)this.double_0, (Class11.Enum1)7);
			}
			public override Class11.Class25 vmethod_22()
			{
				return new Class11.Class25((ulong)this.double_0, (Class11.Enum1)8);
			}
			public override Class11.Class24 vmethod_23()
			{
				return this.vmethod_15();
			}
			public override Class11.Class24 vmethod_24()
			{
				return this.vmethod_17();
			}
			public override Class11.Class24 vmethod_25()
			{
				return this.vmethod_19();
			}
			public override Class11.Class25 vmethod_26()
			{
				return this.vmethod_21();
			}
			public override Class11.Class24 vmethod_27()
			{
				return this.vmethod_16();
			}
			public override Class11.Class24 vmethod_28()
			{
				return this.vmethod_18();
			}
			public override Class11.Class24 vmethod_29()
			{
				return this.vmethod_20();
			}
			public override Class11.Class25 vmethod_30()
			{
				return this.vmethod_22();
			}
			public override Class11.Class24 vmethod_31()
			{
				return new Class11.Class24((int)(checked((sbyte)this.double_0)), (Class11.Enum1)1);
			}
			public override Class11.Class24 vmethod_32()
			{
				return new Class11.Class24((int)(checked((sbyte)this.double_0)), (Class11.Enum1)1);
			}
			public override Class11.Class24 vmethod_33()
			{
				return new Class11.Class24((int)(checked((short)this.double_0)), (Class11.Enum1)3);
			}
			public override Class11.Class24 vmethod_34()
			{
				return new Class11.Class24((int)(checked((short)this.double_0)), (Class11.Enum1)3);
			}
			public override Class11.Class24 vmethod_35()
			{
				return new Class11.Class24(checked((int)this.double_0), (Class11.Enum1)5);
			}
			public override Class11.Class24 vmethod_36()
			{
				return new Class11.Class24(checked((int)this.double_0), (Class11.Enum1)5);
			}
			public override Class11.Class25 vmethod_37()
			{
				return new Class11.Class25(checked((long)this.double_0), (Class11.Enum1)7);
			}
			public override Class11.Class25 vmethod_38()
			{
				return new Class11.Class25(checked((long)this.double_0), (Class11.Enum1)7);
			}
			public override Class11.Class24 vmethod_39()
			{
				return new Class11.Class24((int)(checked((byte)this.double_0)), (Class11.Enum1)2);
			}
			public override Class11.Class24 vmethod_40()
			{
				return new Class11.Class24((int)(checked((byte)this.double_0)), (Class11.Enum1)2);
			}
			public override Class11.Class24 vmethod_41()
			{
				return new Class11.Class24((int)(checked((ushort)this.double_0)), (Class11.Enum1)4);
			}
			public override Class11.Class24 vmethod_42()
			{
				return new Class11.Class24((int)(checked((ushort)this.double_0)), (Class11.Enum1)4);
			}
			public override Class11.Class24 vmethod_43()
			{
				return new Class11.Class24(checked((uint)this.double_0), (Class11.Enum1)6);
			}
			public override Class11.Class24 vmethod_44()
			{
				return new Class11.Class24(checked((uint)this.double_0), (Class11.Enum1)6);
			}
			public override Class11.Class25 vmethod_45()
			{
				return new Class11.Class25(checked((ulong)this.double_0), (Class11.Enum1)8);
			}
			public override Class11.Class25 vmethod_46()
			{
				return new Class11.Class25(checked((ulong)this.double_0), (Class11.Enum1)8);
			}
			public override Class11.Class27 vmethod_47()
			{
				return new Class11.Class27((float)this.double_0, (Class11.Enum1)9);
			}
			public override Class11.Class27 vmethod_48()
			{
				return new Class11.Class27(this.double_0, (Class11.Enum1)10);
			}
			public override Class11.Class27 vmethod_49()
			{
				return new Class11.Class27(this.double_0);
			}
			public override Class11.Class26 vmethod_50()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_26().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_25().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_51()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_30().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_29().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_52()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_37().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_35().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_53()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_45().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_43().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_54()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_38().struct2_0.long_0);
				}
				else
				{
					result = new Class11.Class26((long)this.vmethod_36().struct1_0.int_0);
				}
				return result;
			}
			public override Class11.Class26 vmethod_55()
			{
				bool flag = IntPtr.Size == 8;
				Class11.Class26 result;
				if (flag)
				{
					result = new Class11.Class26(this.vmethod_46().struct2_0.ulong_0);
				}
				else
				{
					result = new Class11.Class26((ulong)this.vmethod_44().struct1_0.uint_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_56()
			{
				bool flag = this.enum1_0 == (Class11.Enum1)9;
				Class11.Class22 result;
				if (flag)
				{
					result = new Class11.Class27((float)(0.0 - this.double_0));
				}
				else
				{
					result = new Class11.Class27(0.0 - this.double_0);
				}
				return result;
			}
			public override Class11.Class22 vmethod_57(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 + ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_58(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 + ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_59(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 + ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_60(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 - ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_61(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 - ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_62(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 - ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_63(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4() || !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 * ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_64(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 * ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_65(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 * ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_66(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 / ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_67(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 / ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_68(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 % ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_69(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return new Class11.Class27(this.double_0 % ((Class11.Class27)class22_0).double_0);
			}
			public override Class11.Class22 vmethod_70(Class11.Class22 class22_0)
			{
				throw new Class11.Exception1();
			}
			public override Class11.Class22 vmethod_71(Class11.Class22 class22_0)
			{
				throw new Class11.Exception1();
			}
			public override Class11.Class22 vmethod_72()
			{
				throw new Class11.Exception1();
			}
			public override Class11.Class22 vmethod_73(Class11.Class22 class22_0)
			{
				throw new Class11.Exception1();
			}
			public override Class11.Class22 vmethod_75(Class11.Class22 class22_0)
			{
				throw new Class11.Exception1();
			}
			public override Class11.Class22 vmethod_76(Class11.Class22 class22_0)
			{
				throw new Class11.Exception1();
			}
			public override Class11.Class22 vmethod_77(Class11.Class22 class22_0)
			{
				throw new Class11.Exception1();
			}
			internal override Class11.Class22 vmethod_8()
			{
				return this;
			}
			internal override bool vmethod_5(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.method_0();
				bool result;
				if (flag)
				{
					bool flag2 = !class22_0.vmethod_0();
					if (flag2)
					{
						Class11.Class22 @class = class22_0.vmethod_8();
						bool flag3 = !@class.method_4();
						result = (!flag3 && this.double_0 == ((Class11.Class27)@class).double_0);
					}
					else
					{
						result = ((Class11.Class28)class22_0).vmethod_5(this);
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			internal override bool vmethod_6(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.method_0();
				bool result;
				if (flag)
				{
					bool flag2 = !class22_0.vmethod_0();
					if (flag2)
					{
						Class11.Class22 @class = class22_0.vmethod_8();
						bool flag3 = !@class.method_4();
						result = (!flag3 && this.double_0 != ((Class11.Class27)@class).double_0);
					}
					else
					{
						result = ((Class11.Class28)class22_0).vmethod_6(this);
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			public override bool vmethod_78(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.double_0 >= ((Class11.Class27)class22_0).double_0;
			}
			public override bool vmethod_79(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.double_0 >= ((Class11.Class27)class22_0).double_0;
			}
			public override bool vmethod_80(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.double_0 > ((Class11.Class27)class22_0).double_0;
			}
			public override bool vmethod_81(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.double_0 > ((Class11.Class27)class22_0).double_0;
			}
			public override bool vmethod_82(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.double_0 <= ((Class11.Class27)class22_0).double_0;
			}
			public override bool vmethod_83(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.double_0 <= ((Class11.Class27)class22_0).double_0;
			}
			public override bool vmethod_84(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.double_0 < ((Class11.Class27)class22_0).double_0;
			}
			public override bool vmethod_85(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				if (flag)
				{
					class22_0 = class22_0.vmethod_8();
				}
				bool flag2 = !class22_0.method_4();
				if (flag2)
				{
					throw new Class11.Exception1();
				}
				return this.double_0 < ((Class11.Class27)class22_0).double_0;
			}
			public double double_0;
			public Class11.Enum1 enum1_0;
		}

		
		internal enum Enum1 : byte
		{

		}

		
		internal enum Enum2 : byte
		{

		}

		
		private class Exception0 : Exception
		{			public Exception0(string string_0) : base(string_0)
			{
			}
		}

		
		private class Exception1 : Exception
		{			public Exception1()
			{
			}
			public Exception1(string string_0) : base(string_0)
			{
			}
		}

		
		internal class Class12
		{			public override string ToString()
			{
				object obj = this.enum3_0;
				bool flag = this.object_0 == null;
				string result;
				if (flag)
				{
					result = obj.ToString();
				}
				else
				{
					result = obj.ToString() + "H" + this.object_0.ToString();
				}
				return result;
			}
			internal Class11.Enum3 enum3_0 = (Class11.Enum3)126;
			internal object object_0;
		}

		
		internal abstract class Class28 : Class11.Class22
		{			public Class28()
			{
			}
			internal override bool vmethod_0()
			{
				return true;
			}
			internal abstract IntPtr vmethod_11();
			internal abstract void vmethod_12(Class11.Class22 class22_0);
			internal override bool vmethod_1()
			{
				return true;
			}
		}

		
		internal class Class29 : Class11.Class28
		{			public Class29(int int_1, Class11.Class20 class20_1)
			{
				this.class20_0 = class20_1;
				this.int_0 = int_1;
				this.enum4_0 = (Class11.Enum4)7;
			}
			internal override void vmethod_10(Class11.Class22 class22_0)
			{
				bool flag = !(class22_0 is Class11.Class29);
				if (flag)
				{
					Class11.Class14 @class = this.class20_0.class17_0.list_1[this.int_0];
					bool flag2 = class22_0 is Class11.Class28 && (@class.enum1_0 & (Class11.Enum1)226) > (Class11.Enum1)0;
					if (flag2)
					{
						Class11.Class22 class22_ = (class22_0 as Class11.Class28).vmethod_8();
						this.vmethod_12(class22_);
					}
					else
					{
						this.vmethod_12(class22_0);
					}
				}
				else
				{
					this.class20_0 = ((Class11.Class29)class22_0).class20_0;
					this.int_0 = ((Class11.Class29)class22_0).int_0;
				}
			}
			internal override void vmethod_2(Class11.Class22 class22_0)
			{
				this.vmethod_12(class22_0);
			}
			internal override IntPtr vmethod_11()
			{
				throw new NotImplementedException();
			}
			internal override void vmethod_12(Class11.Class22 class22_0)
			{
				this.class20_0.class22_1[this.int_0] = class22_0;
			}
			internal override object vmethod_4(Type type_0)
			{
				bool flag = this.class20_0.class22_1[this.int_0] != null;
				object result;
				if (flag)
				{
					result = this.vmethod_8().vmethod_4(type_0);
				}
				else
				{
					result = null;
				}
				return result;
			}
			internal override Class11.Class22 vmethod_8()
			{
				bool flag = this.class20_0.class22_1[this.int_0] == null;
				Class11.Class22 result;
				if (flag)
				{
					result = new Class11.Class34(null);
				}
				else
				{
					result = this.class20_0.class22_1[this.int_0].vmethod_8();
				}
				return result;
			}
			internal override bool vmethod_9()
			{
				return this.vmethod_8().vmethod_9();
			}
			internal override bool vmethod_5(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				bool result;
				if (flag)
				{
					bool flag2 = !(class22_0 is Class11.Class29);
					if (flag2)
					{
						result = false;
					}
					else
					{
						bool flag3 = ((Class11.Class29)class22_0).int_0 == this.int_0;
						result = flag3;
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			internal override bool vmethod_6(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.vmethod_0();
				bool result;
				if (flag)
				{
					result = true;
				}
				else
				{
					bool flag2 = class22_0 is Class11.Class29;
					if (flag2)
					{
						bool flag3 = ((Class11.Class29)class22_0).int_0 != this.int_0;
						result = flag3;
					}
					else
					{
						result = true;
					}
				}
				return result;
			}
			internal override bool vmethod_7()
			{
				return this.vmethod_8().vmethod_7();
			}
			private Class11.Class20 class20_0;
			internal int int_0;
		}

		
		internal class Class30 : Class11.Class28
		{			public Class30(int int_1, Array array_1)
			{
				this.array_0 = array_1;
				this.int_0 = int_1;
				this.enum4_0 = (Class11.Enum4)7;
			}
			internal override IntPtr vmethod_11()
			{
				throw new NotImplementedException();
			}
			internal override void vmethod_10(Class11.Class22 class22_0)
			{
				bool flag = !(class22_0 is Class11.Class30);
				if (flag)
				{
					this.vmethod_12(class22_0);
				}
				else
				{
					this.array_0 = ((Class11.Class30)class22_0).array_0;
					this.int_0 = ((Class11.Class30)class22_0).int_0;
				}
			}
			internal override void vmethod_2(Class11.Class22 class22_0)
			{
				this.vmethod_12(class22_0);
			}
			internal override void vmethod_12(Class11.Class22 class22_0)
			{
				this.array_0.SetValue(class22_0.vmethod_4(null), this.int_0);
			}
			internal override object vmethod_4(Type type_0)
			{
				return this.vmethod_8().vmethod_4(type_0);
			}
			internal override Class11.Class22 vmethod_8()
			{
				return Class11.Class22.smethod_1(this.array_0.GetType().GetElementType(), this.array_0.GetValue(this.int_0));
			}
			internal override bool vmethod_9()
			{
				return this.vmethod_8().vmethod_9();
			}
			internal override bool vmethod_5(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.vmethod_0();
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = !(class22_0 is Class11.Class30);
					if (flag2)
					{
						result = false;
					}
					else
					{
						Class11.Class30 @class = (Class11.Class30)class22_0;
						bool flag3 = @class.int_0 != this.int_0;
						if (flag3)
						{
							result = false;
						}
						else
						{
							bool flag4 = @class.array_0 != this.array_0;
							result = !flag4;
						}
					}
				}
				return result;
			}
			internal override bool vmethod_6(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.vmethod_0();
				bool result;
				if (flag)
				{
					result = true;
				}
				else
				{
					bool flag2 = !(class22_0 is Class11.Class30);
					if (flag2)
					{
						result = true;
					}
					else
					{
						Class11.Class30 @class = (Class11.Class30)class22_0;
						bool flag3 = @class.int_0 != this.int_0;
						if (flag3)
						{
							result = true;
						}
						else
						{
							bool flag4 = @class.array_0 != this.array_0;
							result = flag4;
						}
					}
				}
				return result;
			}
			internal override bool vmethod_7()
			{
				return this.vmethod_8().vmethod_7();
			}
			private Array array_0;
			internal int int_0;
		}

		
		internal class Class31 : Class11.Class28
		{			public Class31(FieldInfo fieldInfo_1, object object_1)
			{
				this.fieldInfo_0 = fieldInfo_1;
				this.object_0 = object_1;
				this.enum4_0 = (Class11.Enum4)7;
			}
			internal override IntPtr vmethod_11()
			{
				throw new NotImplementedException();
			}
			internal override void vmethod_12(Class11.Class22 class22_0)
			{
				bool flag = this.object_0 != null && this.object_0 is Class11.Class22;
				if (flag)
				{
					this.fieldInfo_0.SetValue(((Class11.Class22)this.object_0).vmethod_4(null), class22_0.vmethod_4(null));
				}
				else
				{
					this.fieldInfo_0.SetValue(this.object_0, class22_0.vmethod_4(null));
				}
			}
			internal override void vmethod_10(Class11.Class22 class22_0)
			{
				bool flag = class22_0 is Class11.Class31;
				if (flag)
				{
					this.fieldInfo_0 = ((Class11.Class31)class22_0).fieldInfo_0;
					this.object_0 = ((Class11.Class31)class22_0).object_0;
				}
				else
				{
					this.vmethod_12(class22_0);
				}
			}
			internal override void vmethod_2(Class11.Class22 class22_0)
			{
				this.vmethod_12(class22_0);
			}
			internal override object vmethod_4(Type type_0)
			{
				return this.vmethod_8().vmethod_4(type_0);
			}
			internal override Class11.Class22 vmethod_8()
			{
				bool flag = this.object_0 != null && this.object_0 is Class11.Class22;
				Class11.Class22 result;
				if (flag)
				{
					result = Class11.Class22.smethod_1(this.fieldInfo_0.FieldType, this.fieldInfo_0.GetValue(((Class11.Class22)this.object_0).vmethod_4(null)));
				}
				else
				{
					result = Class11.Class22.smethod_1(this.fieldInfo_0.FieldType, this.fieldInfo_0.GetValue(this.object_0));
				}
				return result;
			}
			internal override bool vmethod_9()
			{
				return this.vmethod_8().vmethod_9();
			}
			internal override bool vmethod_5(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				bool result;
				if (flag)
				{
					bool flag2 = !(class22_0 is Class11.Class31);
					if (flag2)
					{
						result = false;
					}
					else
					{
						Class11.Class31 @class = (Class11.Class31)class22_0;
						bool flag3 = !(@class.fieldInfo_0 != this.fieldInfo_0);
						if (flag3)
						{
							bool flag4 = @class.object_0 != this.object_0;
							result = !flag4;
						}
						else
						{
							result = false;
						}
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			internal override bool vmethod_6(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.vmethod_0();
				bool result;
				if (flag)
				{
					result = true;
				}
				else
				{
					bool flag2 = class22_0 is Class11.Class31;
					if (flag2)
					{
						Class11.Class31 @class = (Class11.Class31)class22_0;
						bool flag3 = !(@class.fieldInfo_0 != this.fieldInfo_0);
						if (flag3)
						{
							bool flag4 = @class.object_0 != this.object_0;
							result = flag4;
						}
						else
						{
							result = true;
						}
					}
					else
					{
						result = true;
					}
				}
				return result;
			}
			internal override bool vmethod_7()
			{
				return this.vmethod_8().vmethod_7();
			}
			internal FieldInfo fieldInfo_0;
			internal object object_0;
		}

		
		internal class Class32 : Class11.Class28
		{			public Class32(int int_1, Class11.Class20 class20_1)
			{
				this.class20_0 = class20_1;
				this.int_0 = int_1;
				this.enum4_0 = (Class11.Enum4)7;
			}
			internal override IntPtr vmethod_11()
			{
				throw new NotImplementedException();
			}
			internal override void vmethod_10(Class11.Class22 class22_0)
			{
				bool flag = !(class22_0 is Class11.Class32);
				if (flag)
				{
					this.vmethod_12(class22_0);
				}
				else
				{
					this.class20_0 = ((Class11.Class32)class22_0).class20_0;
					this.int_0 = ((Class11.Class32)class22_0).int_0;
				}
			}
			internal override void vmethod_2(Class11.Class22 class22_0)
			{
				this.vmethod_12(class22_0);
			}
			internal override void vmethod_12(Class11.Class22 class22_0)
			{
				this.class20_0.class22_0[this.int_0] = class22_0;
			}
			internal override object vmethod_4(Type type_0)
			{
				bool flag = this.class20_0.class22_0[this.int_0] != null;
				object result;
				if (flag)
				{
					result = this.vmethod_8().vmethod_4(type_0);
				}
				else
				{
					result = null;
				}
				return result;
			}
			internal override Class11.Class22 vmethod_8()
			{
				bool flag = this.class20_0.class22_0[this.int_0] != null;
				Class11.Class22 result;
				if (flag)
				{
					result = this.class20_0.class22_0[this.int_0].vmethod_8();
				}
				else
				{
					result = new Class11.Class34(null);
				}
				return result;
			}
			internal override bool vmethod_9()
			{
				return this.vmethod_8().vmethod_9();
			}
			internal override bool vmethod_5(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				bool result;
				if (flag)
				{
					bool flag2 = !(class22_0 is Class11.Class32);
					result = (!flag2 && ((Class11.Class32)class22_0).int_0 == this.int_0);
				}
				else
				{
					result = false;
				}
				return result;
			}
			internal override bool vmethod_6(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				bool result;
				if (flag)
				{
					bool flag2 = class22_0 is Class11.Class32;
					result = (!flag2 || ((Class11.Class32)class22_0).int_0 != this.int_0);
				}
				else
				{
					result = true;
				}
				return result;
			}
			internal override bool vmethod_7()
			{
				return this.vmethod_8().vmethod_7();
			}
			private Class11.Class20 class20_0;
			internal int int_0;
		}

		
		internal class Class33 : Class11.Class28
		{			public Class33(Class11.Class22 class22_1, Type type_1)
			{
				this.class22_0 = class22_1;
				this.type_0 = type_1;
				this.enum4_0 = (Class11.Enum4)7;
			}
			internal override IntPtr vmethod_11()
			{
				throw new NotImplementedException();
			}
			internal override void vmethod_10(Class11.Class22 class22_1)
			{
				bool flag = !(class22_1 is Class11.Class33);
				if (flag)
				{
					this.class22_0.vmethod_10(class22_1);
				}
				else
				{
					this.type_0 = ((Class11.Class33)class22_1).type_0;
					this.class22_0 = ((Class11.Class33)class22_1).class22_0;
				}
			}
			internal override void vmethod_2(Class11.Class22 class22_1)
			{
				this.vmethod_12(class22_1);
			}
			internal override void vmethod_12(Class11.Class22 class22_1)
			{
				this.class22_0 = class22_1;
			}
			internal override object vmethod_4(Type type_1)
			{
				bool flag = this.class22_0 == null;
				object result;
				if (flag)
				{
					result = new Class11.Class34(null);
				}
				else
				{
					bool flag2 = !(type_1 == null) && !(type_1 == typeof(object));
					if (flag2)
					{
						result = this.class22_0.vmethod_4(type_1);
					}
					else
					{
						result = this.class22_0.vmethod_4(this.type_0);
					}
				}
				return result;
			}
			internal override Class11.Class22 vmethod_8()
			{
				bool flag = this.class22_0 != null;
				Class11.Class22 result;
				if (flag)
				{
					result = this.class22_0.vmethod_8();
				}
				else
				{
					result = new Class11.Class34(null);
				}
				return result;
			}
			internal override bool vmethod_9()
			{
				return this.vmethod_8().vmethod_9();
			}
			internal override bool vmethod_5(Class11.Class22 class22_1)
			{
				bool flag = class22_1.vmethod_0();
				bool result;
				if (flag)
				{
					bool flag2 = !(class22_1 is Class11.Class33);
					if (flag2)
					{
						result = false;
					}
					else
					{
						Class11.Class33 @class = (Class11.Class33)class22_1;
						bool flag3 = @class.type_0 != this.type_0;
						if (flag3)
						{
							result = false;
						}
						else
						{
							bool flag4 = this.class22_0 != null;
							if (flag4)
							{
								result = this.class22_0.vmethod_5(@class.class22_0);
							}
							else
							{
								bool flag5 = @class.class22_0 != null;
								result = !flag5;
							}
						}
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			internal override bool vmethod_6(Class11.Class22 class22_1)
			{
				bool flag = class22_1.vmethod_0();
				bool result;
				if (flag)
				{
					bool flag2 = !(class22_1 is Class11.Class33);
					if (flag2)
					{
						result = true;
					}
					else
					{
						Class11.Class33 @class = (Class11.Class33)class22_1;
						bool flag3 = !(@class.type_0 != this.type_0);
						if (flag3)
						{
							bool flag4 = this.class22_0 == null;
							if (flag4)
							{
								bool flag5 = @class.class22_0 == null;
								result = !flag5;
							}
							else
							{
								result = this.class22_0.vmethod_6(@class.class22_0);
							}
						}
						else
						{
							result = true;
						}
					}
				}
				else
				{
					result = true;
				}
				return result;
			}
			internal override bool vmethod_7()
			{
				return this.vmethod_8().vmethod_7();
			}
			private Class11.Class22 class22_0;
			private Type type_0;
		}

		
		internal class Class13
		{			public int int_0;
			public bool bool_0;
			public Class11.Enum1 enum1_0;
		}

		
		internal class Class14
		{			public int int_0;
			public Class11.Enum1 enum1_0;
			public bool bool_0;
			public Type type_0 = typeof(object);
		}

		
		internal class Class15
		{			public int int_0;
			public int int_1;
			public Class11.Class16 class16_0;
		}

		
		internal class Class16
		{			public int int_0;
			public int int_1;
			public byte byte_0;
			public Type type_0;
			public int int_2;
			public int int_3;
		}

		
		internal class Class17
		{			internal object object_0;
			internal List<Class11.Class12> list_0;
			internal Class11.Class13[] class13_0;
			internal List<Class11.Class14> list_1;
			internal List<Class11.Class15> list_2;
		}

		
		private class Class18
		{			public Class18(FieldInfo fieldInfo_0, int int_1)
			{
				this.object_0 = fieldInfo_0;
				this.int_0 = int_1;
			}
			internal object object_0;
			internal int int_0;
		}

		
		private class Class19
		{			public Class19(MethodBase methodBase_1, List<Class11.Class18> list_1)
			{
				this.list_0 = list_1;
				this.methodBase_0 = methodBase_1;
			}
			public Class19(MethodBase methodBase_1, Class11.Class18[] class18_0)
			{
				this.list_0.AddRange(class18_0);
			}
			public override bool Equals(object obj)
			{
				Class11.Class19 @class = obj as Class11.Class19;
				bool flag = obj == null;
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = !(this.methodBase_0 != @class.methodBase_0);
					if (flag2)
					{
						bool flag3 = this.list_0.Count != @class.list_0.Count;
						if (flag3)
						{
							result = false;
						}
						else
						{
							int num = 0;
							for (;;)
							{
								bool flag4 = num < this.list_0.Count;
								if (!flag4)
								{
									goto IL_F1;
								}
								bool flag5 = (FieldInfo)this.list_0[num].object_0 != (FieldInfo)@class.list_0[num].object_0;
								if (flag5)
								{
									break;
								}
								bool flag6 = this.list_0[num].int_0 == @class.list_0[num].int_0;
								if (!flag6)
								{
									goto IL_ED;
								}
								num++;
							}
							return false;
							IL_ED:
							return false;
							IL_F1:
							result = true;
						}
					}
					else
					{
						result = false;
					}
				}
				return result;
			}
			public override int GetHashCode()
			{
				int num = this.methodBase_0.GetHashCode();
				foreach (Class11.Class18 @class in this.list_0)
				{
					int num2 = @class.object_0.GetHashCode() + @class.int_0;
					num = (num ^ num2) + num2;
				}
				return num;
			}
			public Class11.Class18 method_0(int int_0)
			{
				foreach (Class11.Class18 @class in this.list_0)
				{
					bool flag = @class.int_0 == int_0;
					if (flag)
					{
						return @class;
					}
				}
				return null;
			}
			public bool method_1(int int_0)
			{
				foreach (Class11.Class18 @class in this.list_0)
				{
					bool flag = @class.int_0 == int_0;
					if (flag)
					{
						return true;
					}
				}
				return false;
			}
			private List<Class11.Class18> list_0 = new List<Class11.Class18>();
			private MethodBase methodBase_0;
		}

		private delegate object Delegate10(object target, object[] paramters);
				private delegate object Delegate11(object target);
		private delegate void Delegate12(IntPtr a, byte b, int c);
		private delegate void Delegate13(IntPtr s, IntPtr t, uint c);

		
		internal class Class20
		{			internal void method_0()
			{
				bool flag = false;
				this.method_2(ref flag);
			}
			internal void method_1()
			{
				this.class36_0.method_1();
				this.class22_1 = null;
				bool flag = this.list_0 == null;
				if (!flag)
				{
					foreach (IntPtr hglobal in this.list_0)
					{
						try
						{
							Marshal.FreeHGlobal(hglobal);
						}
						catch
						{
						}
					}
					this.list_0.Clear();
					this.list_0 = null;
				}
			}
			internal void method_2(ref bool bool_4)
			{
				while (this.int_0 > -2)
				{
					bool flag = this.bool_0;
					if (flag)
					{
						this.bool_0 = false;
						int num = this.int_1;
						int num2 = this.int_0;
						this.method_4(this.int_1, this.int_0);
						this.int_0 = num2;
						this.int_1 = num;
					}
					bool flag2 = !this.bool_2;
					if (flag2)
					{
						bool flag3 = !this.bool_1;
						if (flag3)
						{
							this.int_1 = this.int_0;
							Class11.Class12 @class = this.class17_0.list_0[this.int_0];
							this.object_0 = @class.object_0;
							try
							{
								this.method_7(@class);
							}
							catch (Exception innerException)
							{
								bool flag4 = innerException is TargetInvocationException;
								if (flag4)
								{
									TargetInvocationException ex = (TargetInvocationException)innerException;
									bool flag5 = ex.InnerException != null;
									if (flag5)
									{
										innerException = ex.InnerException;
									}
								}
								this.exception_0 = innerException;
								bool_4 = true;
								this.class36_0.method_1();
								int int_ = this.int_1;
								Class11.Class15 class2 = this.method_5(int_, innerException);
								List<Class11.Class15> list = this.method_6(int_, false);
								List<Class11.Class15> list2 = new List<Class11.Class15>();
								bool flag6 = class2 != null;
								if (flag6)
								{
									list2.Add(class2);
								}
								bool flag7 = list != null && list.Count > 0;
								if (flag7)
								{
									list2.AddRange(list);
								}
								list2.Sort((Class11.Class15 x, Class11.Class15 y) => x.class16_0.int_0.CompareTo(y.class16_0.int_0));
								Class11.Class15 class3 = null;
								foreach (Class11.Class15 class4 in list2)
								{
									bool flag8 = class4.class16_0.int_3 != 0;
									if (!flag8)
									{
										class3 = class4;
										break;
									}
									this.class36_0.method_2(new Class11.Class34(innerException));
									this.int_1 = class4.class16_0.int_2;
									this.int_0 = this.int_1;
									this.method_0();
									bool flag9 = this.bool_3;
									if (flag9)
									{
										this.bool_3 = false;
										class3 = class4;
										break;
									}
								}
								bool flag10 = class3 == null;
								if (flag10)
								{
									throw innerException;
								}
								this.int_2 = class3.class16_0.int_0;
								this.method_3(int_, class3.class16_0.int_0);
								bool flag11 = this.int_2 >= 0;
								if (flag11)
								{
									this.class36_0.method_2(new Class11.Class34(innerException));
									this.int_1 = this.int_2;
									this.int_0 = this.int_1;
									this.int_2 = -1;
									this.method_0();
								}
								return;
							}
							this.int_0++;
							continue;
						}
						this.bool_1 = false;
					}
					else
					{
						this.bool_2 = false;
					}
					return;
				}
				this.class36_0.method_1();
			}
			internal void method_3(int int_3, int int_4)
			{
				bool flag = this.class17_0.list_2 == null;
				if (!flag)
				{
					foreach (Class11.Class15 @class in this.class17_0.list_2)
					{
						bool flag2 = (@class.class16_0.int_3 == 4 || @class.class16_0.int_3 == 2) && @class.class16_0.int_0 >= int_3 && @class.class16_0.int_1 <= int_4;
						if (flag2)
						{
							this.int_1 = @class.class16_0.int_0;
							this.int_0 = this.int_1;
							bool flag3 = false;
							this.method_2(ref flag3);
							bool flag4 = flag3;
							if (flag4)
							{
								break;
							}
						}
					}
				}
			}
			internal void method_4(int int_3, int int_4)
			{
				bool flag = this.class17_0.list_2 == null;
				if (!flag)
				{
					foreach (Class11.Class15 @class in this.class17_0.list_2)
					{
						bool flag2 = @class.class16_0.int_3 == 2 && @class.class16_0.int_0 >= int_3 && @class.class16_0.int_1 <= int_4;
						if (flag2)
						{
							this.int_1 = @class.class16_0.int_0;
							this.int_0 = this.int_1;
							bool flag3 = false;
							this.method_2(ref flag3);
							bool flag4 = flag3;
							if (flag4)
							{
								break;
							}
						}
					}
				}
			}
			internal Class11.Class15 method_5(int int_3, Exception exception_1)
			{
				Class11.Class15 @class = null;
				bool flag = this.class17_0.list_2 != null;
				if (flag)
				{
					foreach (Class11.Class15 class2 in this.class17_0.list_2)
					{
						bool flag2 = class2.class16_0.int_3 == 0 && (class2.class16_0.type_0 == exception_1.GetType() || (class2.class16_0.type_0 != null && (class2.class16_0.type_0.FullName == exception_1.GetType().FullName || class2.class16_0.type_0.FullName == typeof(object).FullName || class2.class16_0.type_0.FullName == typeof(Exception).FullName))) && int_3 >= class2.int_0 && int_3 <= class2.int_1;
						if (flag2)
						{
							bool flag3 = @class == null;
							if (flag3)
							{
								@class = class2;
							}
							else
							{
								bool flag4 = class2.class16_0.int_0 < @class.class16_0.int_0;
								if (flag4)
								{
									@class = class2;
								}
							}
						}
					}
				}
				return @class;
			}
			internal List<Class11.Class15> method_6(int int_3, bool bool_4)
			{
				bool flag = this.class17_0.list_2 == null;
				List<Class11.Class15> result;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Class11.Class15> list = new List<Class11.Class15>();
					foreach (Class11.Class15 @class in this.class17_0.list_2)
					{
						bool flag2 = (@class.class16_0.int_3 & 1) == 1 && int_3 >= @class.int_0 && int_3 <= @class.int_1;
						if (flag2)
						{
							list.Add(@class);
						}
					}
					bool flag3 = list.Count == 0;
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = list;
					}
				}
				return result;
			}
			private unsafe void method_7(Class11.Class12 class12_0)
			{
				switch (class12_0.enum3_0)
				{
				case (Class11.Enum3)0:
				{
					Class11.Class22 @class = this.class36_0.method_4();
					bool flag = Class11.Class20.smethod_1(this.class36_0.method_4()).vmethod_85(@class);
					if (flag)
					{
						this.class36_0.method_2(new Class11.Class24(1));
					}
					else
					{
						this.class36_0.method_2(new Class11.Class24(0));
					}
					break;
				}
				case (Class11.Enum3)1:
				case (Class11.Enum3)121:
				{
					int metadataToken = (int)this.object_0;
					Type type = typeof(Class11).Module.ResolveType(metadataToken);
					Class11.Class22 class2 = this.class36_0.method_4();
					object obj = class2.vmethod_4(type);
					bool flag2 = obj != null;
					if (flag2)
					{
						bool isValueType = type.IsValueType;
						if (isValueType)
						{
							obj = Class11.Class20.smethod_9(obj);
						}
						class2 = Class11.Class22.smethod_1(type, obj);
					}
					else
					{
						bool isValueType2 = type.IsValueType;
						if (isValueType2)
						{
							obj = Activator.CreateInstance(type);
							class2 = Class11.Class22.smethod_1(type, obj);
						}
						else
						{
							class2 = new Class11.Class34(null);
						}
					}
					Class11.Class28 class3 = this.class36_0.method_4() as Class11.Class28;
					if (class3 == null)
					{
						throw new Class11.Exception1();
					}
					class3.vmethod_10(class2);
					break;
				}
				case (Class11.Enum3)2:
				{
					Class11.Class22 class4 = this.class36_0.method_4();
					Class11.Class22 class5 = this.class36_0.method_4();
					bool flag3 = class4.vmethod_5(class5);
					if (flag3)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)3:
					this.bool_3 = (bool)this.class36_0.method_4().vmethod_4(typeof(bool));
					this.bool_1 = true;
					break;
				case (Class11.Enum3)4:
				{
					Class11.Class23 class6 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag4 = class6 != null;
					if (!flag4)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class6.vmethod_36());
					break;
				}
				case (Class11.Enum3)5:
				{
					int metadataToken2 = (int)this.object_0;
					Type type2 = typeof(Class11).Module.ResolveType(metadataToken2);
					Class11.Class22 class7 = this.class36_0.method_4();
					object obj2 = class7.vmethod_4(null);
					bool flag5 = obj2 != null;
					if (flag5)
					{
						bool flag6 = type2.IsAssignableFrom(obj2.GetType());
						if (flag6)
						{
							this.class36_0.method_2(class7);
						}
						else
						{
							this.class36_0.method_2(new Class11.Class34(null));
						}
					}
					else
					{
						this.class36_0.method_2(new Class11.Class34(null));
					}
					break;
				}
				case (Class11.Enum3)6:
					this.class36_0.method_2(this.class36_0.method_3());
					break;
				case (Class11.Enum3)7:
				{
					Class11.Class22 class8 = this.class36_0.method_4();
					bool flag7 = class8.vmethod_3();
					if (flag7)
					{
						class8 = ((Class11.Class23)class8).vmethod_23();
					}
					this.class36_0.method_4().vmethod_2(class8);
					break;
				}
				case (Class11.Enum3)8:
				{
					Class11.Class23 class9 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag8 = class9 != null;
					if (!flag8)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class9.vmethod_41());
					break;
				}
				case (Class11.Enum3)9:
				{
					Class11.Class23 class10 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag9 = class10 != null;
					if (!flag9)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class10.vmethod_29());
					break;
				}
				case (Class11.Enum3)10:
				{
					Class11.Class22 class11 = this.class36_0.method_4();
					bool flag10 = class11.vmethod_3();
					if (flag10)
					{
						class11 = ((Class11.Class23)class11).vmethod_24();
					}
					this.class36_0.method_4().vmethod_2(class11);
					break;
				}
				case (Class11.Enum3)11:
				{
					Class11.Class23 class12 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class12.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(typeof(int), value));
					break;
				}
				case (Class11.Enum3)12:
				{
					Class11.Class23 class13 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag11 = class13 != null;
					if (!flag11)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class13.vmethod_46());
					break;
				}
				case (Class11.Enum3)13:
				{
					Class11.Class22 class14 = this.class36_0.method_4();
					this.class36_0.method_4().vmethod_2(class14);
					break;
				}
				case (Class11.Enum3)14:
				{
					Class11.Class22 class15 = Class11.Class20.smethod_6(this.class36_0.method_4());
					Class11.Class22 class16 = Class11.Class20.smethod_6(this.class36_0.method_4());
					bool flag12 = !class15.vmethod_5(class16);
					if (flag12)
					{
						this.class36_0.method_2(new Class11.Class24(0));
					}
					else
					{
						this.class36_0.method_2(new Class11.Class24(1));
					}
					break;
				}
				case (Class11.Enum3)15:
				{
					Class11.Class22 class17 = this.class36_0.method_4();
					bool flag13 = Class11.Class20.smethod_1(this.class36_0.method_4()).vmethod_84(class17);
					if (flag13)
					{
						this.class36_0.method_2(new Class11.Class24(1));
					}
					else
					{
						this.class36_0.method_2(new Class11.Class24(0));
					}
					break;
				}
				case (Class11.Enum3)16:
				{
					Class11.Class23 class18 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag14 = class18 != null;
					if (!flag14)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class18.vmethod_44());
					break;
				}
				case (Class11.Enum3)17:
				{
					Class11.Class23 class19 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value2 = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class19.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(typeof(float), value2));
					break;
				}
				case (Class11.Enum3)18:
				{
					Class11.Class22 class20 = this.class36_0.method_4();
					Class11.Class23 class21 = Class11.Class20.smethod_1(class20);
					Class11.Class22 class22_ = this.class36_0.method_4();
					Class11.Class23 class22 = Class11.Class20.smethod_1(class22_);
					bool flag15 = class22 != null && class21 != null;
					if (flag15)
					{
						bool flag16 = class22.vmethod_81(class20);
						if (flag16)
						{
							this.int_0 = (int)this.object_0 - 1;
						}
					}
					else
					{
						bool flag17 = class20.vmethod_6(class22_);
						if (flag17)
						{
							this.int_0 = (int)this.object_0 - 1;
						}
					}
					break;
				}
				case (Class11.Enum3)19:
				{
					Class11.Class23 class23 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag18 = class23 != null;
					if (!flag18)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class23.vmethod_49());
					break;
				}
				case (Class11.Enum3)20:
				{
					int metadataToken3 = (int)this.object_0;
					Type type3 = typeof(Class11).Module.ResolveType(metadataToken3);
					Class11.Class22 class24 = this.class36_0.method_4();
					Class11.Class28 class25 = class24 as Class11.Class28;
					bool flag19 = class25 != null;
					if (!flag19)
					{
						throw new Class11.Exception1();
					}
					bool isValueType3 = type3.IsValueType;
					if (isValueType3)
					{
						object obj3 = Activator.CreateInstance(type3);
						class25.vmethod_12(Class11.Class22.smethod_1(type3, obj3));
					}
					else
					{
						class25.vmethod_12(new Class11.Class34(null));
					}
					break;
				}
				case (Class11.Enum3)21:
				{
					Class11.Class23 class26 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class27 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag20 = class27 != null && class26 != null;
					if (!flag20)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class27.vmethod_69(class26));
					break;
				}
				case (Class11.Enum3)22:
				{
					int metadataToken4 = (int)this.object_0;
					Type type4 = typeof(Class11).Module.ResolveType(metadataToken4);
					Class11.Class28 class28 = this.class36_0.method_4() as Class11.Class28;
					if (class28 == null)
					{
						throw new Class11.Exception1();
					}
					object obj4 = class28.vmethod_4(type4);
					bool flag21 = obj4 != null;
					Class11.Class22 class29;
					if (flag21)
					{
						bool isValueType4 = type4.IsValueType;
						if (isValueType4)
						{
							obj4 = Class11.Class20.smethod_9(obj4);
						}
						class29 = Class11.Class22.smethod_1(type4, obj4);
					}
					else
					{
						bool flag22 = !type4.IsValueType;
						if (flag22)
						{
							class29 = new Class11.Class34(null);
						}
						else
						{
							obj4 = Activator.CreateInstance(type4);
							class29 = Class11.Class22.smethod_1(type4, obj4);
						}
					}
					this.class36_0.method_2(class29);
					break;
				}
				case (Class11.Enum3)23:
				{
					int metadataToken5 = (int)this.object_0;
					uint uint_ = (uint)Class11.Class20.smethod_0(typeof(Class11).Module.ResolveType(metadataToken5));
					this.class36_0.method_2(new Class11.Class24(uint_, (Class11.Enum1)6));
					break;
				}
				case (Class11.Enum3)24:
				{
					Class11.Class23 class30 = Class11.Class20.smethod_1(this.class36_0.method_3());
					if (class30 == null)
					{
						throw new ArithmeticException(((Class11.Enum5)0).ToString());
					}
					Class11.Class23 class31 = class30;
					Class11.Class27 class32 = class31 as Class11.Class27;
					bool flag23 = class32 != null;
					if (flag23)
					{
						bool flag24 = double.IsNaN(class32.double_0);
						if (flag24)
						{
							throw new OverflowException(((Class11.Enum5)2).ToString());
						}
						bool flag25 = double.IsInfinity(class32.double_0);
						if (flag25)
						{
							throw new OverflowException(((Class11.Enum5)1).ToString());
						}
					}
					break;
				}
				case (Class11.Enum3)25:
					this.method_12(false);
					break;
				case (Class11.Enum3)26:
				{
					Class11.Class23 class33 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class34 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag26 = class34 != null && class33 != null;
					if (!flag26)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class34.vmethod_76(class33));
					break;
				}
				case (Class11.Enum3)27:
				case (Class11.Enum3)29:
				case (Class11.Enum3)70:
				case (Class11.Enum3)88:
				case (Class11.Enum3)107:
				case (Class11.Enum3)152:
					throw new Class11.Exception1();
				case (Class11.Enum3)28:
				{
					IntPtr intPtr = Marshal.AllocHGlobal((this.class36_0.method_4() as Class11.Class23).vmethod_19().struct1_0.int_0);
					bool flag27 = this.list_0 == null;
					if (flag27)
					{
						this.list_0 = new List<IntPtr>();
					}
					this.list_0.Add(intPtr);
					this.class36_0.method_2(new Class11.Class26(intPtr));
					break;
				}
				case (Class11.Enum3)30:
				case (Class11.Enum3)34:
				case (Class11.Enum3)51:
				case (Class11.Enum3)61:
				case (Class11.Enum3)65:
				case (Class11.Enum3)104:
				case (Class11.Enum3)143:
				case (Class11.Enum3)144:
				{
					Class11.Class22 class35 = this.class36_0.method_4();
					Class11.Class23 class36 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Array array = (Array)this.class36_0.method_4().vmethod_4(null);
					Type elementType = array.GetType().GetElementType();
					array.SetValue(class35.vmethod_4(elementType), class36.vmethod_19().struct1_0.int_0);
					break;
				}
				case (Class11.Enum3)31:
				{
					int[] array2 = (int[])this.object_0;
					Class11.Class23 class37 = Class11.Class20.smethod_1(this.class36_0.method_4());
					long num = class37.vmethod_21().struct2_0.long_0;
					bool flag28 = (num < 0L || class37.method_4()) && IntPtr.Size == 4;
					if (flag28)
					{
						num = (long)((int)num);
					}
					bool flag29 = class37.method_1();
					if (flag29)
					{
						Class11.Class24 class38 = (Class11.Class24)class37;
						bool flag30 = class38.enum1_0 == (Class11.Enum1)6;
						if (flag30)
						{
							num = (long)((ulong)class38.struct1_0.uint_0);
						}
					}
					bool flag31 = num < (long)array2.Length && num >= 0L;
					if (flag31)
					{
						this.int_0 = array2[(int)(checked((IntPtr)num))] - 1;
					}
					break;
				}
				case (Class11.Enum3)32:
				{
					int metadataToken6 = (int)this.object_0;
					Type elementType2 = typeof(Class11).Module.ResolveType(metadataToken6);
					Class11.Class23 class39 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Array array3 = Array.CreateInstance(elementType2, class39.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(new Class11.Class34(array3));
					break;
				}
				case (Class11.Enum3)33:
				{
					Class11.Class23 class40 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class41 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag32 = class41 != null && class40 != null;
					if (!flag32)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class41.vmethod_75(class40));
					break;
				}
				case (Class11.Enum3)35:
				{
					Class11.Class22 class42 = this.class36_0.method_4();
					bool flag33 = Class11.Class20.smethod_1(this.class36_0.method_4()).vmethod_80(class42);
					if (flag33)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)36:
				case (Class11.Enum3)62:
				{
					int metadataToken7 = (int)this.object_0;
					Module module = typeof(Class11).Module;
					this.class36_0.method_2(new Class11.Class26(module.ResolveMethod(metadataToken7).MethodHandle.GetFunctionPointer()));
					break;
				}
				case (Class11.Enum3)37:
				{
					Class11.Class22 class43 = this.class36_0.method_4();
					Class11.Class23 class44 = Class11.Class20.smethod_1(class43);
					Class11.Class22 class22_2 = this.class36_0.method_4();
					Class11.Class23 class45 = Class11.Class20.smethod_1(class22_2);
					bool flag34 = class45 != null && class44 != null;
					if (flag34)
					{
						bool flag35 = !class45.vmethod_81(class43);
						if (flag35)
						{
							this.class36_0.method_2(new Class11.Class24(0));
						}
						else
						{
							this.class36_0.method_2(new Class11.Class24(1));
						}
					}
					else
					{
						bool flag36 = !class43.vmethod_6(class22_2);
						if (flag36)
						{
							this.class36_0.method_2(new Class11.Class24(0));
						}
						else
						{
							this.class36_0.method_2(new Class11.Class24(1));
						}
					}
					break;
				}
				case (Class11.Enum3)38:
				{
					int metadataToken8 = (int)this.object_0;
					FieldInfo fieldInfo_ = typeof(Class11).Module.ResolveField(metadataToken8);
					this.class36_0.method_2(new Class11.Class31(fieldInfo_, null));
					break;
				}
				case (Class11.Enum3)39:
				{
					Class11.Class23 class46 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag37 = class46 != null;
					if (!flag37)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class46.vmethod_35());
					break;
				}
				case (Class11.Enum3)40:
				{
					int metadataToken9 = (int)this.object_0;
					FieldInfo fieldInfo = typeof(Class11).Module.ResolveField(metadataToken9);
					object value3 = this.class36_0.method_4().vmethod_4(fieldInfo.FieldType);
					Class11.Class22 class47 = this.class36_0.method_4();
					object obj5 = class47.vmethod_4(null);
					bool flag38 = obj5 == null;
					if (flag38)
					{
						Type type5 = fieldInfo.DeclaringType;
						bool isByRef = type5.IsByRef;
						if (isByRef)
						{
							type5 = type5.GetElementType();
						}
						bool flag39 = !type5.IsValueType;
						if (flag39)
						{
							throw new NullReferenceException();
						}
						obj5 = Activator.CreateInstance(type5);
						bool flag40 = class47 is Class11.Class29;
						if (flag40)
						{
							((Class11.Class28)class47).vmethod_12(Class11.Class22.smethod_1(type5, obj5));
						}
					}
					fieldInfo.SetValue(obj5, value3);
					break;
				}
				case (Class11.Enum3)41:
				{
					this.int_0 = -3;
					bool flag41 = this.class36_0.method_0() > 0;
					if (flag41)
					{
						this.class22_2 = this.class36_0.method_4();
					}
					break;
				}
				case (Class11.Enum3)42:
				{
					int metadataToken10 = (int)this.object_0;
					FieldInfo fieldInfo2 = typeof(Class11).Module.ResolveField(metadataToken10);
					this.class36_0.method_2(Class11.Class22.smethod_1(fieldInfo2.FieldType, fieldInfo2.GetValue(null)));
					break;
				}
				case (Class11.Enum3)43:
					this.bool_2 = true;
					break;
				case (Class11.Enum3)44:
				{
					bool flag42 = Class11.list_0.Count != 0;
					if (flag42)
					{
						this.class36_0.method_2(new Class11.Class35(Class11.list_0[(int)this.object_0]));
					}
					else
					{
						Module module2 = typeof(Class11).Module;
						this.class36_0.method_2(new Class11.Class35(module2.ResolveString((int)this.object_0 | 1879048192)));
					}
					break;
				}
				case (Class11.Enum3)45:
				{
					int metadataToken11 = (int)this.object_0;
					typeof(Class11).Module.ResolveType(metadataToken11);
					Class11.Class23 class48 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Array array_ = (Array)this.class36_0.method_4().vmethod_4(null);
					this.class36_0.method_2(new Class11.Class30(class48.vmethod_19().struct1_0.int_0, array_));
					break;
				}
				case (Class11.Enum3)46:
				{
					Class11.Class23 class49 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value4 = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class49.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(typeof(byte), value4));
					break;
				}
				case (Class11.Enum3)47:
				{
					Class11.Class22 class50 = this.class36_0.method_4();
					Class11.Class23 class51 = Class11.Class20.smethod_1(class50);
					bool flag43 = class50 != null && class50.vmethod_0() && class51 != null;
					if (flag43)
					{
						this.class36_0.method_2(class51.vmethod_25());
					}
					else
					{
						bool flag44 = class51 != null && class51.method_2();
						if (!flag44)
						{
							throw new Class11.Exception1();
						}
						IntPtr value5 = ((Class11.Class26)class51).method_7();
						this.class36_0.method_2(new Class11.Class24(*(int*)((void*)value5), (Class11.Enum1)5));
					}
					break;
				}
				case (Class11.Enum3)48:
				{
					Class11.Class23 class52 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class53 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag45 = class53 != null && class52 != null;
					if (!flag45)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class53.vmethod_57(class52));
					break;
				}
				case (Class11.Enum3)49:
				{
					Class11.Class22 class54 = this.class36_0.method_4();
					bool flag46 = class54.vmethod_3();
					if (flag46)
					{
						class54 = ((Class11.Class23)class54).vmethod_50();
					}
					this.class36_0.method_4().vmethod_2(class54);
					break;
				}
				case (Class11.Enum3)50:
				{
					Class11.Class23 class55 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag47 = class55 != null;
					if (!flag47)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class55.vmethod_51());
					break;
				}
				case (Class11.Enum3)52:
				{
					Class11.Class23 class56 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class57 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag48 = class57 != null && class56 != null;
					if (!flag48)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class57.vmethod_67(class56));
					break;
				}
				case (Class11.Enum3)53:
				{
					Class11.Class23 class58 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class59 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag49 = class59 != null && class58 != null;
					if (!flag49)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class59.vmethod_77(class58));
					break;
				}
				case (Class11.Enum3)54:
				{
					Array array4 = (Array)this.class36_0.method_4().vmethod_4(null);
					this.class36_0.method_2(new Class11.Class24(array4.Length, (Class11.Enum1)5));
					break;
				}
				case (Class11.Enum3)55:
					this.int_0 = (int)this.object_0 - 1;
					this.bool_0 = true;
					break;
				case (Class11.Enum3)56:
				{
					bool flag50 = this.class36_0.method_4().vmethod_6(this.class36_0.method_4());
					if (flag50)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)57:
				{
					Class11.Class23 class60 = this.class36_0.method_4() as Class11.Class23;
					Class11.Class23 class61 = this.class36_0.method_4() as Class11.Class23;
					IntPtr intPtr2 = Class11.Class20.smethod_8(this.class36_0.method_4());
					bool flag51 = intPtr2 != IntPtr.Zero;
					if (flag51)
					{
						byte byte_ = class61.vmethod_16().struct1_0.byte_0;
						uint uint_2 = class60.vmethod_20().struct1_0.uint_0;
						Class11.Class20.smethod_10(intPtr2, byte_, (int)uint_2);
					}
					break;
				}
				case (Class11.Enum3)58:
				{
					Class11.Class23 class62 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value6 = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class62.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(typeof(sbyte), value6));
					break;
				}
				case (Class11.Enum3)59:
					this.class36_0.method_2(((Class11.Class23)this.class36_0.method_4()).vmethod_56());
					break;
				case (Class11.Enum3)60:
				{
					Class11.Class22 class63 = this.class36_0.method_4();
					Class11.Class23 class64 = Class11.Class20.smethod_1(class63);
					bool flag52 = class63 != null && class63.vmethod_0() && class64 != null;
					if (flag52)
					{
						this.class36_0.method_2(class64.vmethod_28());
					}
					else
					{
						bool flag53 = class64 != null && class64.method_2();
						if (!flag53)
						{
							throw new Class11.Exception1();
						}
						IntPtr value7 = ((Class11.Class26)class64).method_7();
						this.class36_0.method_2(new Class11.Class24((int)(*(ushort*)((void*)value7)), (Class11.Enum1)4));
					}
					break;
				}
				case (Class11.Enum3)63:
				{
					Class11.Class23 class65 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag54 = class65 != null;
					if (!flag54)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class65.vmethod_39());
					break;
				}
				case (Class11.Enum3)64:
				{
					Class11.Class23 class66 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value8 = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class66.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(typeof(long), value8));
					break;
				}
				case (Class11.Enum3)66:
				{
					Class11.Class23 class67 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value9 = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class67.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(typeof(ushort), value9));
					break;
				}
				case (Class11.Enum3)68:
				{
					Class11.Class23 class68 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value10 = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class68.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(typeof(uint), value10));
					break;
				}
				case (Class11.Enum3)69:
				{
					Class11.Class23 class69 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class70 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag55 = class70 != null && class69 != null;
					if (!flag55)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class70.vmethod_64(class69));
					break;
				}
				case (Class11.Enum3)71:
					this.class36_0.method_2(this.class22_0[(int)this.object_0]);
					break;
				case (Class11.Enum3)72:
					this.class36_0.method_2(this.class36_0.method_4().vmethod_8());
					break;
				case (Class11.Enum3)73:
				{
					Class11.Class23 class71 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag56 = class71 != null;
					if (!flag56)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class71.vmethod_47());
					break;
				}
				case (Class11.Enum3)74:
				{
					int metadataToken12 = (int)this.object_0;
					Type type_ = typeof(Class11).Module.ResolveType(metadataToken12);
					Class11.Class23 class72 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value11 = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class72.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(type_, value11));
					break;
				}
				case (Class11.Enum3)75:
				{
					Class11.Class23 class73 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class74 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag57 = class73 != null && class74 != null;
					if (!flag57)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class73.vmethod_70(class74));
					break;
				}
				case (Class11.Enum3)76:
					throw this.exception_0;
				case (Class11.Enum3)77:
				{
					int metadataToken13 = (int)this.object_0;
					FieldInfo fieldInfo3 = typeof(Class11).Module.ResolveField(metadataToken13);
					object value12 = this.class36_0.method_4().vmethod_4(fieldInfo3.FieldType);
					fieldInfo3.SetValue(null, value12);
					break;
				}
				case (Class11.Enum3)78:
					this.class36_0.method_2(new Class11.Class29((int)this.object_0, this));
					break;
				case (Class11.Enum3)79:
				{
					Class11.Class23 class75 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class76 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag58 = class76 != null && class75 != null;
					if (!flag58)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class76.vmethod_68(class75));
					break;
				}
				case (Class11.Enum3)80:
				{
					Class11.Class23 class77 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class78 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag59 = class78 != null && class77 != null;
					if (!flag59)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class78.vmethod_66(class77));
					break;
				}
				case (Class11.Enum3)81:
				{
					Class11.Class22 value13 = this.class36_0.method_4();
					object key = this.class36_0.method_4().vmethod_4(null);
					Class11.Class20.dictionary_1[key] = value13;
					break;
				}
				case (Class11.Enum3)82:
				{
					Class11.Class23 class79 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag60 = class79 == null;
					if (flag60)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class79.vmethod_50());
					break;
				}
				case (Class11.Enum3)83:
					this.class36_0.method_2(new Class11.Class32((int)this.object_0, this));
					break;
				case (Class11.Enum3)84:
				{
					Class11.Class23 class80 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag61 = class80 != null;
					if (!flag61)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class80.vmethod_72());
					break;
				}
				case (Class11.Enum3)85:
				{
					Class11.Class22 class81 = this.class36_0.method_4();
					bool flag62 = class81 != null && class81.vmethod_7();
					if (flag62)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)86:
					this.class36_0.method_2(new Class11.Class27((float)this.object_0));
					break;
				case (Class11.Enum3)87:
				{
					Class11.Class23 class82 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag63 = class82 == null;
					if (flag63)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class82.vmethod_32());
					break;
				}
				case (Class11.Enum3)89:
				{
					Class11.Class23 class83 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag64 = class83 == null;
					if (flag64)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class83.vmethod_54());
					break;
				}
				case (Class11.Enum3)90:
					this.class36_0.method_2(new Class11.Class27((double)this.object_0));
					break;
				case (Class11.Enum3)91:
				{
					Class11.Class23 class84 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class85 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag65 = class85 != null && class84 != null;
					if (!flag65)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class85.vmethod_65(class84));
					break;
				}
				case (Class11.Enum3)92:
				{
					Class11.Class23 class86 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag66 = class86 != null;
					if (!flag66)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class86.vmethod_30());
					break;
				}
				case (Class11.Enum3)93:
				{
					Class11.Class22 class87 = this.class22_1[(int)this.object_0];
					this.class36_0.method_2(class87);
					break;
				}
				case (Class11.Enum3)94:
				{
					Class11.Class23 class88 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class89 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag67 = class89 != null && class88 != null;
					if (!flag67)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class89.vmethod_63(class88));
					break;
				}
				case (Class11.Enum3)95:
					this.class36_0.method_2(new Class11.Class34(null));
					break;
				case (Class11.Enum3)96:
				{
					Class11.Class22 class90 = this.class36_0.method_4();
					bool flag68 = Class11.Class20.smethod_1(this.class36_0.method_4()).vmethod_78(class90);
					if (flag68)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)97:
				{
					int metadataToken14 = (int)this.object_0;
					FieldInfo fieldInfo_2 = typeof(Class11).Module.ResolveField(metadataToken14);
					Class11.Class22 class91 = this.class36_0.method_4();
					class91.vmethod_8();
					object object_ = class91.vmethod_4(null);
					this.class36_0.method_2(new Class11.Class31(fieldInfo_2, object_));
					break;
				}
				case (Class11.Enum3)98:
				{
					Class11.Class23 class92 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class93 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag69 = class92 != null && class93 != null;
					if (!flag69)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class92.vmethod_73(class93));
					break;
				}
				case (Class11.Enum3)99:
				{
					Class11.Class23 class94 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag70 = class94 != null;
					if (!flag70)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class94.vmethod_23());
					break;
				}
				case (Class11.Enum3)100:
				{
					Class11.Class23 class95 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag71 = class95 != null;
					if (!flag71)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class95.vmethod_26());
					break;
				}
				case (Class11.Enum3)102:
				{
					Class11.Class23 class96 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Array array5 = (Array)this.class36_0.method_4().vmethod_4(null);
					object value14 = array5.GetValue(class96.vmethod_19().struct1_0.int_0);
					Type elementType3 = array5.GetType().GetElementType();
					this.class36_0.method_2(Class11.Class22.smethod_1(elementType3, value14));
					break;
				}
				case (Class11.Enum3)103:
				{
					Class11.Class22 class97 = this.class36_0.method_4();
					Class11.Class23 class98 = Class11.Class20.smethod_1(class97);
					bool flag72 = class97 != null && class97.vmethod_0() && class98 != null;
					if (flag72)
					{
						this.class36_0.method_2(class98.vmethod_47());
					}
					else
					{
						bool flag73 = class98 != null && class98.method_2();
						if (!flag73)
						{
							throw new Class11.Exception1();
						}
						IntPtr value15 = ((Class11.Class26)class98).method_7();
						this.class36_0.method_2(new Class11.Class27(*(float*)((void*)value15), (Class11.Enum1)9));
					}
					break;
				}
				case (Class11.Enum3)105:
					this.class36_0.method_4();
					break;
				case (Class11.Enum3)106:
				{
					Class11.Class23 class99 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag74 = class99 == null;
					if (flag74)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class99.vmethod_40());
					break;
				}
				case (Class11.Enum3)108:
				{
					Class11.Class22 class100 = this.class36_0.method_4();
					Class11.Class23 class101 = Class11.Class20.smethod_1(class100);
					bool flag75 = class100 != null && class100.vmethod_0() && class101 != null;
					if (flag75)
					{
						this.class36_0.method_2(class101.vmethod_27());
					}
					else
					{
						bool flag76 = class101 != null && class101.method_2();
						if (!flag76)
						{
							throw new Class11.Exception1();
						}
						IntPtr value16 = ((Class11.Class26)class101).method_7();
						this.class36_0.method_2(new Class11.Class24((int)(*(byte*)((void*)value16)), (Class11.Enum1)2));
					}
					break;
				}
				case (Class11.Enum3)110:
					this.class36_0.method_2(new Class11.Class25((long)this.object_0));
					break;
				case (Class11.Enum3)111:
				{
					Class11.Class23 class102 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag77 = class102 != null;
					if (!flag77)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class102.vmethod_42());
					break;
				}
				case (Class11.Enum3)112:
				{
					Class11.Class22 class103 = this.class36_0.method_4();
					bool flag78 = class103 == null || !class103.vmethod_7();
					if (flag78)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)113:
				{
					Class11.Class23 class104 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class105 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag79 = class104 != null && class105 != null;
					if (!flag79)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class104.vmethod_71(class105));
					break;
				}
				case (Class11.Enum3)114:
				{
					Class11.Class22 class106 = this.class36_0.method_4();
					Class11.Class23 class107 = Class11.Class20.smethod_1(class106);
					bool flag80 = class106 != null && class106.vmethod_0() && class107 != null;
					if (flag80)
					{
						this.class36_0.method_2(class107.vmethod_48());
					}
					else
					{
						bool flag81 = class107 != null && class107.method_2();
						if (!flag81)
						{
							throw new Class11.Exception1();
						}
						IntPtr value17 = ((Class11.Class26)class107).method_7();
						this.class36_0.method_2(new Class11.Class27(*(double*)((void*)value17), (Class11.Enum1)10));
					}
					break;
				}
				case (Class11.Enum3)115:
					throw (Exception)this.class36_0.method_4().vmethod_4(null);
				case (Class11.Enum3)116:
				{
					Class11.Class22 class108 = this.class36_0.method_4();
					Class11.Class23 class109 = Class11.Class20.smethod_1(class108);
					bool flag82 = class108 != null && class108.vmethod_0() && class109 != null;
					if (flag82)
					{
						this.class36_0.method_2(class109.vmethod_29());
					}
					else
					{
						bool flag83 = class109 != null && class109.method_2();
						if (!flag83)
						{
							throw new Class11.Exception1();
						}
						IntPtr value18 = ((Class11.Class26)class109).method_7();
						this.class36_0.method_2(new Class11.Class24(*(uint*)((void*)value18), (Class11.Enum1)6));
					}
					break;
				}
				case (Class11.Enum3)117:
				{
					Class11.Class23 class110 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag84 = class110 == null;
					if (flag84)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class110.vmethod_28());
					break;
				}
				case (Class11.Enum3)118:
				{
					Class11.Class23 class111 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class112 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag85 = class112 != null && class111 != null;
					if (!flag85)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class112.vmethod_60(class111));
					break;
				}
				case (Class11.Enum3)119:
				{
					Class11.Class23 class113 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class114 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag86 = class114 != null && class113 != null;
					if (!flag86)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class114.vmethod_59(class113));
					break;
				}
				case (Class11.Enum3)120:
				{
					Class11.Class23 class115 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag87 = class115 != null;
					if (!flag87)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class115.vmethod_48());
					break;
				}
				case (Class11.Enum3)122:
				{
					Class11.Class23 class116 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value19 = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class116.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(typeof(IntPtr), value19));
					break;
				}
				case (Class11.Enum3)123:
				{
					int metadataToken15 = (int)this.object_0;
					Type type_2 = typeof(Class11).Module.ResolveType(metadataToken15);
					Class11.Class22 class117 = this.class36_0.method_4();
					Class11.Class23 class118 = Class11.Class20.smethod_1(this.class36_0.method_4());
					((Array)this.class36_0.method_4().vmethod_4(null)).SetValue(class117.vmethod_4(type_2), class118.vmethod_19().struct1_0.int_0);
					break;
				}
				case (Class11.Enum3)124:
				{
					Class11.Class23 class119 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class120 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag88 = class120 != null && class119 != null;
					if (!flag88)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class120.vmethod_58(class119));
					break;
				}
				case (Class11.Enum3)125:
				{
					Class11.Class23 class121 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag89 = class121 != null;
					if (!flag89)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class121.vmethod_53());
					break;
				}
				case (Class11.Enum3)126:
				{
					int metadataToken16 = (int)this.object_0;
					Type type_3 = typeof(Class11).Module.ResolveType(metadataToken16);
					object obj6 = this.class36_0.method_4().vmethod_8().vmethod_4(type_3);
					Class11.Class22 class122 = Class11.Class22.smethod_1(type_3, obj6);
					this.class36_0.method_2(class122);
					break;
				}
				case (Class11.Enum3)128:
				{
					Class11.Class22 class123 = this.class36_0.method_4();
					Class11.Class23 class124 = Class11.Class20.smethod_1(class123);
					bool flag90 = class123 != null && class123.vmethod_0() && class124 != null;
					if (flag90)
					{
						this.class36_0.method_2(class124.vmethod_24());
					}
					else
					{
						bool flag91 = class124 != null && class124.method_2();
						if (!flag91)
						{
							throw new Class11.Exception1();
						}
						IntPtr value20 = ((Class11.Class26)class124).method_7();
						this.class36_0.method_2(new Class11.Class24((int)(*(short*)((void*)value20)), (Class11.Enum1)3));
					}
					break;
				}
				case (Class11.Enum3)129:
				{
					Class11.Class23 class125 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value21 = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class125.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(typeof(short), value21));
					break;
				}
				case (Class11.Enum3)130:
				{
					Class11.Class22 class126 = this.class36_0.method_4();
					bool flag92 = Class11.Class20.smethod_1(this.class36_0.method_4()).vmethod_85(class126);
					bool flag93 = !flag92;
					if (flag93)
					{
						this.class36_0.method_2(new Class11.Class24(0));
					}
					else
					{
						this.class36_0.method_2(new Class11.Class24(1));
					}
					bool flag94 = flag92;
					if (flag94)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)131:
				{
					int metadataToken17 = (int)this.object_0;
					FieldInfo fieldInfo4 = typeof(Class11).Module.ResolveField(metadataToken17);
					object obj7 = this.class36_0.method_4().vmethod_4(null);
					this.class36_0.method_2(Class11.Class22.smethod_1(fieldInfo4.FieldType, fieldInfo4.GetValue(obj7)));
					break;
				}
				case (Class11.Enum3)132:
				{
					int metadataToken18 = (int)this.object_0;
					ConstructorInfo constructorInfo = (ConstructorInfo)typeof(Class11).Module.ResolveMethod(metadataToken18);
					ParameterInfo[] parameters = constructorInfo.GetParameters();
					object[] array6 = new object[parameters.Length];
					Class11.Class22[] array7 = new Class11.Class22[parameters.Length];
					List<Class11.Class18> list = null;
					Class11.Class19 class127 = null;
					int num2;
					for (int i = 0; i < parameters.Length; i = num2 + 1)
					{
						Class11.Class22 class128 = this.class36_0.method_4();
						Type type6 = parameters[parameters.Length - 1 - i].ParameterType;
						object obj8 = null;
					bool flag95 = false;
					Class11.Class31 class129 = null;
					bool flag96;
						if (type6.IsByRef)
						{
							class129 = (class128 as Class11.Class31);
							flag96 = (class129 != null);
						}
						else
						{
							flag96 = false;
						}
						bool flag97 = flag96;
						if (flag97)
						{
							bool flag98 = list == null;
							if (flag98)
							{
								list = new List<Class11.Class18>();
							}
							list.Add(new Class11.Class18(class129.fieldInfo_0, i));
							obj8 = class129.object_0;
							bool flag99 = !(obj8 is Class11.Class22);
							if (flag99)
							{
								flag95 = true;
							}
							else
							{
								class128 = (obj8 as Class11.Class22);
							}
						}
						bool flag100 = !flag95;
						if (flag100)
						{
							bool flag101 = class128 != null;
							if (flag101)
							{
								obj8 = class128.vmethod_4(type6);
							}
							bool flag102 = obj8 == null;
							if (flag102)
							{
								bool isByRef2 = type6.IsByRef;
								if (isByRef2)
								{
									type6 = type6.GetElementType();
								}
								bool isValueType5 = type6.IsValueType;
								if (isValueType5)
								{
									obj8 = Activator.CreateInstance(type6);
									bool flag103 = class128 is Class11.Class29;
									if (flag103)
									{
										((Class11.Class28)class128).vmethod_12(Class11.Class22.smethod_1(type6, obj8));
									}
								}
							}
						}
						array7[array6.Length - 1 - i] = class128;
						array6[array6.Length - 1 - i] = obj8;
						num2 = i;
					}
					Class11.Delegate10 @delegate = null;
					bool flag104 = list != null;
					if (flag104)
					{
						class127 = new Class11.Class19(constructorInfo, list);
						@delegate = Class11.Class20.smethod_4(constructorInfo, true, class127);
					}
					object obj9 = (@delegate == null) ? constructorInfo.Invoke(array6) : @delegate(null, array6);
					for (int j = 0; j < parameters.Length; j = num2 + 1)
					{
						bool flag105 = parameters[j].ParameterType.IsByRef && (class127 == null || !class127.method_1(j));
						if (flag105)
						{
							bool flag106 = array7[j].method_2();
							if (flag106)
							{
								((Class11.Class26)array7[j]).method_6(Class11.Class22.smethod_1(parameters[j].ParameterType, array6[j]));
							}
							else
							{
								bool flag107 = array7[j] is Class11.Class29;
								if (flag107)
								{
									array7[j].vmethod_10(Class11.Class22.smethod_1(parameters[j].ParameterType.GetElementType(), array6[j]));
								}
								else
								{
									array7[j].vmethod_10(Class11.Class22.smethod_1(parameters[j].ParameterType, array6[j]));
								}
							}
						}
						num2 = j;
					}
					this.class36_0.method_2(Class11.Class22.smethod_1(constructorInfo.DeclaringType, obj9));
					break;
				}
				case (Class11.Enum3)133:
				{
					Class11.Class22 class130 = this.class36_0.method_4();
					Class11.Class23 class131 = Class11.Class20.smethod_1(class130);
					bool flag108 = class130 != null && class130.vmethod_0() && class131 != null;
					if (flag108)
					{
						this.class36_0.method_2(class131.vmethod_26());
					}
					else
					{
						bool flag109 = class131 != null && class131.method_2();
						if (!flag109)
						{
							throw new Class11.Exception1();
						}
						IntPtr value22 = ((Class11.Class26)class131).method_7();
						this.class36_0.method_2(new Class11.Class25(*(long*)((void*)value22), (Class11.Enum1)7));
					}
					break;
				}
				case (Class11.Enum3)134:
					this.method_12(true);
					break;
				case (Class11.Enum3)136:
				{
					Class11.Class23 class132 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag110 = class132 != null;
					if (!flag110)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class132.vmethod_33());
					break;
				}
				case (Class11.Enum3)138:
				{
					Class11.Class22 class133 = this.class36_0.method_4();
					bool flag111 = class133.vmethod_3();
					if (flag111)
					{
						class133 = ((Class11.Class23)class133).vmethod_48();
					}
					this.class36_0.method_4().vmethod_2(class133);
					break;
				}
				case (Class11.Enum3)140:
					this.int_0 = (int)this.object_0 - 1;
					break;
				case (Class11.Enum3)141:
				{
					Class11.Class23 class134 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class135 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag112 = class135 != null && class134 != null;
					if (!flag112)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class135.vmethod_62(class134));
					break;
				}
				case (Class11.Enum3)142:
				{
					Class11.Class22 class136 = this.class36_0.method_4();
					bool flag113 = Class11.Class20.smethod_1(this.class36_0.method_4()).vmethod_80(class136);
					if (flag113)
					{
						this.class36_0.method_2(new Class11.Class24(1));
					}
					else
					{
						this.class36_0.method_2(new Class11.Class24(0));
					}
					break;
				}
				case (Class11.Enum3)145:
				{
					Class11.Class23 class137 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag114 = class137 != null;
					if (!flag114)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class137.vmethod_55());
					break;
				}
				case (Class11.Enum3)146:
				{
					Class11.Class22 class138 = this.class36_0.method_4();
					bool flag115 = class138.vmethod_0();
					if (!flag115)
					{
						throw new Class11.Exception1();
					}
					object obj10 = class138.vmethod_4(null);
					class138 = ((obj10 != null) ? Class11.Class22.smethod_1(obj10.GetType(), obj10) : new Class11.Class34(null));
					this.class36_0.method_2(class138);
					break;
				}
				case (Class11.Enum3)147:
				{
					Class11.Class22 class139 = this.class36_0.method_4();
					bool flag116 = class139.vmethod_3();
					if (flag116)
					{
						class139 = ((Class11.Class23)class139).vmethod_25();
					}
					this.class36_0.method_4().vmethod_2(class139);
					break;
				}
				case (Class11.Enum3)148:
				{
					object key2 = this.class36_0.method_4().vmethod_4(null);
					Class11.Class22 class140 = null;
					bool flag117 = Class11.Class20.dictionary_1.TryGetValue(key2, out class140);
					if (flag117)
					{
						this.class36_0.method_2(class140);
					}
					else
					{
						this.class36_0.method_2(new Class11.Class34(null));
					}
					break;
				}
				case (Class11.Enum3)149:
				{
					Class11.Class23 class141 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag118 = class141 != null;
					if (!flag118)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class141.vmethod_24());
					break;
				}
				case (Class11.Enum3)150:
				{
					Class11.Class23 class142 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag119 = class142 != null;
					if (!flag119)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class142.vmethod_45());
					break;
				}
				case (Class11.Enum3)151:
				{
					Class11.Class23 class143 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag120 = class143 == null;
					if (flag120)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class143.vmethod_37());
					break;
				}
				case (Class11.Enum3)153:
				{
					int num3 = (int)this.object_0;
					this.class22_1[num3] = this.method_8(this.class36_0.method_4(), this.class17_0.list_1[num3].enum1_0, this.class17_0.list_1[num3].bool_0);
					break;
				}
				case (Class11.Enum3)154:
				{
					Type type7 = typeof(Class11).Module.ResolveType((int)this.object_0);
					object obj11 = this.class36_0.method_4().vmethod_4(type7);
					bool flag121 = obj11 == null;
					if (flag121)
					{
						obj11 = Activator.CreateInstance(type7);
					}
					this.class36_0.method_2(new Class11.Class34(Class11.Class22.smethod_1(type7, Class11.Class20.smethod_9(obj11))));
					break;
				}
				case (Class11.Enum3)155:
					this.class36_0.method_2(new Class11.Class24((int)this.object_0));
					break;
				case (Class11.Enum3)156:
				{
					Class11.Class22 class144 = this.class36_0.method_4();
					bool flag122 = class144.vmethod_3();
					if (flag122)
					{
						class144 = ((Class11.Class23)class144).vmethod_47();
					}
					this.class36_0.method_4().vmethod_2(class144);
					break;
				}
				case (Class11.Enum3)157:
				{
					Class11.Class23 class145 = Class11.Class20.smethod_1(this.class36_0.method_4());
					Class11.Class23 class146 = (Class11.Class23)this.class36_0.method_4();
					bool flag123 = class146 != null && class145 != null;
					if (!flag123)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class146.vmethod_61(class145));
					break;
				}
				case (Class11.Enum3)158:
				{
					int num4 = (int)this.object_0;
					bool flag124 = !((MethodBase)this.class17_0.object_0).IsStatic;
					if (flag124)
					{
						this.class22_0[num4] = this.method_8(this.class36_0.method_4(), this.class17_0.class13_0[num4 - 1].enum1_0, false);
					}
					else
					{
						this.class22_0[num4] = this.method_8(this.class36_0.method_4(), this.class17_0.class13_0[num4].enum1_0, false);
					}
					break;
				}
				case (Class11.Enum3)159:
				{
					Class11.Class23 class147 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag125 = class147 == null;
					if (flag125)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class147.vmethod_25());
					break;
				}
				case (Class11.Enum3)160:
				{
					Class11.Class23 class148 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag126 = class148 != null;
					if (!flag126)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class148.vmethod_31());
					break;
				}
				case (Class11.Enum3)161:
				{
					Class11.Class23 class149 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag127 = class149 == null;
					if (flag127)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class149.vmethod_43());
					break;
				}
				case (Class11.Enum3)162:
				{
					Class11.Class22 class150 = this.class36_0.method_4();
					Class11.Class23 class151 = Class11.Class20.smethod_1(class150);
					bool flag128 = class150 != null && class150.vmethod_0() && class151 != null;
					if (flag128)
					{
						this.class36_0.method_2(class151.vmethod_23());
					}
					else
					{
						bool flag129 = class151 != null && class151.method_2();
						if (!flag129)
						{
							throw new Class11.Exception1();
						}
						IntPtr value23 = ((Class11.Class26)class151).method_7();
						this.class36_0.method_2(new Class11.Class24((int)(*(sbyte*)((void*)value23)), (Class11.Enum1)1));
					}
					break;
				}
				case (Class11.Enum3)163:
				{
					Class11.Class23 class152 = this.class36_0.method_4() as Class11.Class23;
					IntPtr intPtr3 = Class11.Class20.smethod_8(this.class36_0.method_4());
					IntPtr intPtr4 = Class11.Class20.smethod_8(this.class36_0.method_4());
					bool flag130 = intPtr3 != IntPtr.Zero && intPtr4 != IntPtr.Zero;
					if (flag130)
					{
						uint uint_3 = class152.vmethod_20().struct1_0.uint_0;
						Class11.Class20.smethod_11(intPtr4, intPtr3, uint_3);
					}
					break;
				}
				case (Class11.Enum3)164:
				{
					Class11.Class22 class153 = this.class36_0.method_4();
					bool flag131 = Class11.Class20.smethod_1(this.class36_0.method_4()).vmethod_83(class153);
					if (flag131)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)165:
				{
					Class11.Class23 class154 = Class11.Class20.smethod_1(this.class36_0.method_4());
					object value24 = ((Array)this.class36_0.method_4().vmethod_4(null)).GetValue(class154.vmethod_19().struct1_0.int_0);
					this.class36_0.method_2(Class11.Class22.smethod_1(typeof(double), value24));
					break;
				}
				case (Class11.Enum3)166:
				{
					Class11.Class22 class155 = this.class36_0.method_4();
					bool flag132 = Class11.Class20.smethod_1(this.class36_0.method_4()).vmethod_82(class155);
					if (flag132)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)167:
				{
					Class11.Class22 class156 = this.class36_0.method_4();
					bool flag133 = Class11.Class20.smethod_1(this.class36_0.method_4()).vmethod_79(class156);
					if (flag133)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)168:
				{
					Class11.Class23 class157 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag134 = class157 != null;
					if (!flag134)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class157.vmethod_38());
					break;
				}
				case (Class11.Enum3)169:
				{
					Class11.Class22 class158 = this.class36_0.method_4();
					bool flag135 = Class11.Class20.smethod_1(this.class36_0.method_4()).vmethod_84(class158);
					if (flag135)
					{
						this.int_0 = (int)this.object_0 - 1;
					}
					break;
				}
				case (Class11.Enum3)170:
				{
					Class11.Class22 class159 = this.class36_0.method_4();
					Class11.Class23 class160 = Class11.Class20.smethod_1(class159);
					bool flag136 = class159 != null && class159.vmethod_0() && class160 != null;
					if (flag136)
					{
						this.class36_0.method_2(class160.vmethod_50());
					}
					else
					{
						bool flag137 = class160 != null && class160.method_2();
						if (!flag137)
						{
							throw new Class11.Exception1();
						}
						IntPtr value25 = ((Class11.Class26)class160).method_7();
						bool flag138 = IntPtr.Size == 8;
						if (flag138)
						{
							long long_ = *(long*)((void*)value25);
							this.class36_0.method_2(new Class11.Class26(long_, (Class11.Enum1)12));
						}
						else
						{
							int num5 = *(int*)((void*)value25);
							this.class36_0.method_2(new Class11.Class26((long)num5, (Class11.Enum1)12));
						}
					}
					break;
				}
				case (Class11.Enum3)171:
				{
					Class11.Class23 class161 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag139 = class161 == null;
					if (flag139)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class161.vmethod_34());
					break;
				}
				case (Class11.Enum3)172:
				{
					int metadataToken19 = (int)this.object_0;
					Module module3 = typeof(Class11).Module;
					object obj12 = null;
					try
					{
						obj12 = module3.ResolveType(metadataToken19);
					}
					catch
					{
						try
						{
							obj12 = module3.ResolveMethod(metadataToken19);
						}
						catch
						{
							try
							{
								obj12 = module3.ResolveField(metadataToken19);
							}
							catch
							{
								obj12 = module3.ResolveMember(metadataToken19);
							}
						}
					}
					this.class36_0.method_2(new Class11.Class34(obj12));
					break;
				}
				case (Class11.Enum3)173:
				{
					Class11.Class23 class162 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag140 = class162 != null;
					if (!flag140)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class162.vmethod_27());
					break;
				}
				case (Class11.Enum3)174:
				{
					Class11.Class23 class163 = Class11.Class20.smethod_1(this.class36_0.method_4());
					bool flag141 = class163 == null;
					if (flag141)
					{
						throw new Class11.Exception1();
					}
					this.class36_0.method_2(class163.vmethod_52());
					break;
				}
				case (Class11.Enum3)175:
				{
					Class11.Class22 class164 = this.class36_0.method_4();
					bool flag142 = class164.vmethod_3();
					if (flag142)
					{
						class164 = ((Class11.Class23)class164).vmethod_26();
					}
					this.class36_0.method_4().vmethod_2(class164);
					break;
				}
				}
			}
			private Class11.Class22 method_8(Class11.Class22 class22_3, Class11.Enum1 enum1_0, bool bool_4 = false)
			{
				bool flag = !bool_4 && class22_3.vmethod_0();
				if (flag)
				{
					class22_3 = class22_3.vmethod_8();
				}
				bool flag2 = !class22_3.method_1();
				Class11.Class22 result;
				if (flag2)
				{
					bool flag3 = !class22_3.method_3();
					if (flag3)
					{
						bool flag4 = !class22_3.method_4();
						if (flag4)
						{
							bool flag5 = !class22_3.method_2();
							if (flag5)
							{
								result = class22_3;
							}
							else
							{
								result = ((Class11.Class26)class22_3).vmethod_13(enum1_0);
							}
						}
						else
						{
							result = ((Class11.Class27)class22_3).vmethod_13(enum1_0);
						}
					}
					else
					{
						result = ((Class11.Class25)class22_3).vmethod_13(enum1_0);
					}
				}
				else
				{
					result = ((Class11.Class24)class22_3).vmethod_13(enum1_0);
				}
				return result;
			}
			private Class11.Class22 method_9(int int_3)
			{
				return this.class22_1[int_3];
			}
			private void method_10(int int_3)
			{
				this.method_11(int_3, this.class36_0.method_4());
			}
			private static int smethod_0(Type type_0)
			{
				bool flag = Class11.Class20.dictionary_0 == null;
				if (flag)
				{
					Class11.Class20.dictionary_0 = new Dictionary<Type, int>();
				}
				int result;
				try
				{
					int num = 0;
					bool flag2 = !Class11.Class20.dictionary_0.TryGetValue(type_0, out num);
					if (flag2)
					{
						DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(int), Type.EmptyTypes, true);
						ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
						ilgenerator.Emit(OpCodes.Sizeof, type_0);
						ilgenerator.Emit(OpCodes.Ret);
						num = (int)dynamicMethod.Invoke(null, null);
						Class11.Class20.dictionary_0[type_0] = num;
						result = num;
					}
					else
					{
						result = num;
					}
				}
				catch
				{
					result = 0;
				}
				return result;
			}
			private void method_11(int int_3, Class11.Class22 class22_3)
			{
				this.class22_1[int_3] = this.method_8(class22_3, this.class17_0.list_1[int_3].enum1_0, this.class17_0.list_1[int_3].bool_0);
			}
			private static Class11.Class23 smethod_1(Class11.Class22 class22_3)
			{
				Class11.Class23 @class = class22_3 as Class11.Class23;
				bool flag = @class == null && class22_3.vmethod_0();
				if (flag)
				{
					@class = (class22_3.vmethod_8() as Class11.Class23);
				}
				return @class;
			}
			private void method_12(bool bool_4)
			{
				int metadataToken = (int)this.object_0;
				MethodBase methodBase = typeof(Class11).Module.ResolveMethod(metadataToken);
				MethodInfo methodInfo = methodBase as MethodInfo;
				ParameterInfo[] parameters = methodBase.GetParameters();
				object[] array = new object[parameters.Length];
				Class11.Class22[] array2 = new Class11.Class22[parameters.Length];
				List<Class11.Class18> list = null;
				Class11.Class19 @class = null;
				for (int i = 0; i < parameters.Length; i++)
				{
					Class11.Class22 class2 = this.class36_0.method_4();
					Type type = parameters[parameters.Length - 1 - i].ParameterType;
					object obj = null;
					bool flag = false;
					Class11.Class31 class3 = default;
					bool flag2;
					if (type.IsByRef)
					{
						class3 = (class2 as Class11.Class31);
						flag2 = (class3 != null);
					}
					else
					{
						flag2 = false;
					}
					bool flag3 = flag2;
					if (flag3)
					{
						bool flag4 = list == null;
						if (flag4)
						{
							list = new List<Class11.Class18>();
						}
						list.Add(new Class11.Class18(class3.fieldInfo_0, i));
						obj = class3.object_0;
						bool flag5 = obj is Class11.Class22;
						if (flag5)
						{
							class2 = (obj as Class11.Class22);
						}
						else
						{
							flag = true;
						}
					}
					bool flag6 = !flag;
					if (flag6)
					{
						bool flag7 = class2 != null;
						if (flag7)
						{
							obj = class2.vmethod_4(type);
						}
						bool flag8 = obj == null;
						if (flag8)
						{
							bool isByRef = type.IsByRef;
							if (isByRef)
							{
								type = type.GetElementType();
							}
							bool isValueType = type.IsValueType;
							if (isValueType)
							{
								obj = Activator.CreateInstance(type);
								bool flag9 = class2 is Class11.Class29;
								if (flag9)
								{
									((Class11.Class28)class2).vmethod_12(Class11.Class22.smethod_1(type, obj));
								}
							}
						}
					}
					array2[array.Length - 1 - i] = class2;
					array[array.Length - 1 - i] = obj;
				}
				Class11.Delegate10 @delegate = null;
				bool flag10 = list == null;
				if (flag10)
				{
					bool flag11 = methodInfo != null && methodInfo.ReturnType.IsByRef;
					if (flag11)
					{
						@delegate = Class11.Class20.smethod_2(methodBase, bool_4);
					}
				}
				else
				{
					@class = new Class11.Class19(methodBase, list);
					@delegate = Class11.Class20.smethod_3(methodBase, bool_4, @class);
				}
				object obj2 = null;
				bool flag12 = !methodBase.IsStatic;
				if (flag12)
				{
					Class11.Class22 class4 = this.class36_0.method_4();
					bool flag13 = class4 != null;
					if (flag13)
					{
						obj2 = class4.vmethod_4(methodBase.DeclaringType);
					}
					bool flag14 = obj2 == null;
					if (flag14)
					{
						Type type2 = methodBase.DeclaringType;
						bool isByRef2 = type2.IsByRef;
						if (isByRef2)
						{
							type2 = type2.GetElementType();
						}
						bool flag15 = !type2.IsValueType;
						if (flag15)
						{
							throw new NullReferenceException();
						}
						obj2 = Activator.CreateInstance(type2);
						bool flag16 = class4 is Class11.Class29;
						if (flag16)
						{
							((Class11.Class28)class4).vmethod_12(Class11.Class22.smethod_1(type2, obj2));
						}
					}
				}
				object obj3 = (@delegate != null) ? @delegate(obj2, array) : methodBase.Invoke(obj2, array);
				for (int j = 0; j < parameters.Length; j++)
				{
					bool flag17 = parameters[j].ParameterType.IsByRef && (@class == null || !@class.method_1(j));
					if (flag17)
					{
						bool flag18 = array2[j].method_2();
						if (flag18)
						{
							((Class11.Class26)array2[j]).method_6(Class11.Class22.smethod_1(parameters[j].ParameterType, array[j]));
						}
						else
						{
							bool flag19 = array2[j] is Class11.Class29;
							if (flag19)
							{
								array2[j].vmethod_10(Class11.Class22.smethod_1(parameters[j].ParameterType.GetElementType(), array[j]));
							}
							else
							{
								array2[j].vmethod_10(Class11.Class22.smethod_1(parameters[j].ParameterType, array[j]));
							}
						}
					}
				}
				bool flag20 = methodInfo != null && methodInfo.ReturnType != typeof(void);
				if (flag20)
				{
					this.class36_0.method_2(Class11.Class22.smethod_1(methodInfo.ReturnType, obj3));
				}
			}
			private static Class11.Delegate10 smethod_2(object object_1, bool bool_4)
			{
				Class11.Delegate10 result = null;
				if (bool_4)
				{
					bool flag = Class11.Class20.dictionary_2.TryGetValue((MethodBase)object_1, out result);
					if (flag)
					{
						return result;
					}
				}
				else
				{
					bool flag2 = Class11.Class20.dictionary_3.TryGetValue((MethodBase)object_1, out result);
					if (flag2)
					{
						return result;
					}
				}
				MethodInfo methodInfo = object_1 as MethodInfo;
				DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[]
				{
					typeof(object),
					typeof(object[])
				}, true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ParameterInfo[] parameters = ((MethodBase)object_1).GetParameters();
				Type[] array = new Type[parameters.Length];
				for (int i = 0; i < array.Length; i++)
				{
					bool flag3 = !parameters[i].ParameterType.IsByRef;
					if (flag3)
					{
						array[i] = parameters[i].ParameterType;
					}
					else
					{
						array[i] = parameters[i].ParameterType.GetElementType();
					}
				}
				int num = array.Length;
				bool isValueType = ((MemberInfo)object_1).DeclaringType.IsValueType;
				if (isValueType)
				{
					num++;
				}
				LocalBuilder[] array2 = new LocalBuilder[num];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = ilgenerator.DeclareLocal(array[j]);
				}
				bool isValueType2 = ((MemberInfo)object_1).DeclaringType.IsValueType;
				if (isValueType2)
				{
					array2[array2.Length - 1] = ilgenerator.DeclareLocal(((MemberInfo)object_1).DeclaringType.MakeByRefType());
				}
				for (int k = 0; k < array.Length; k++)
				{
					ilgenerator.Emit(OpCodes.Ldarg_1);
					Class11.Class20.smethod_5(ilgenerator, k);
					ilgenerator.Emit(OpCodes.Ldelem_Ref);
					bool isValueType3 = array[k].IsValueType;
					if (isValueType3)
					{
						ilgenerator.Emit(OpCodes.Unbox_Any, array[k]);
					}
					else
					{
						bool flag4 = array[k] != typeof(object);
						if (flag4)
						{
							ilgenerator.Emit(OpCodes.Castclass, array[k]);
						}
					}
					ilgenerator.Emit(OpCodes.Stloc, array2[k]);
				}
				bool flag5 = !((MethodBase)object_1).IsStatic;
				if (flag5)
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
					bool isValueType4 = ((MemberInfo)object_1).DeclaringType.IsValueType;
					if (isValueType4)
					{
						ilgenerator.Emit(OpCodes.Unbox, ((MemberInfo)object_1).DeclaringType);
						ilgenerator.Emit(OpCodes.Stloc, array2[array2.Length - 1]);
						ilgenerator.Emit(OpCodes.Ldloc_S, array2[array2.Length - 1]);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Castclass, ((MemberInfo)object_1).DeclaringType);
					}
				}
				for (int l = 0; l < array.Length; l++)
				{
					bool flag6 = !parameters[l].ParameterType.IsByRef;
					if (flag6)
					{
						ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Ldloca_S, array2[l]);
					}
				}
				if (bool_4)
				{
					bool flag7 = !(methodInfo != null);
					if (flag7)
					{
						ilgenerator.Emit(OpCodes.Call, object_1 as ConstructorInfo);
					}
					else
					{
						ilgenerator.EmitCall(OpCodes.Call, methodInfo, null);
					}
				}
				else
				{
					bool flag8 = methodInfo != null;
					if (flag8)
					{
						ilgenerator.EmitCall(OpCodes.Callvirt, methodInfo, null);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Callvirt, object_1 as ConstructorInfo);
					}
				}
				bool flag9 = !(methodInfo == null) && !(methodInfo.ReturnType == typeof(void));
				if (flag9)
				{
					bool flag10 = !methodInfo.ReturnType.IsByRef;
					if (flag10)
					{
						bool isValueType5 = methodInfo.ReturnType.IsValueType;
						if (isValueType5)
						{
							ilgenerator.Emit(OpCodes.Box, methodInfo.ReturnType);
						}
					}
					else
					{
						Type elementType = methodInfo.ReturnType.GetElementType();
						bool isValueType6 = elementType.IsValueType;
						if (isValueType6)
						{
							ilgenerator.Emit(OpCodes.Ldobj, elementType);
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldind_Ref, elementType);
						}
						bool isValueType7 = elementType.IsValueType;
						if (isValueType7)
						{
							ilgenerator.Emit(OpCodes.Box, elementType);
						}
					}
				}
				else
				{
					ilgenerator.Emit(OpCodes.Ldnull);
				}
				for (int m = 0; m < array.Length; m++)
				{
					bool isByRef = parameters[m].ParameterType.IsByRef;
					if (isByRef)
					{
						ilgenerator.Emit(OpCodes.Ldarg_1);
						Class11.Class20.smethod_5(ilgenerator, m);
						ilgenerator.Emit(OpCodes.Ldloc, array2[m]);
						bool isValueType8 = array2[m].LocalType.IsValueType;
						if (isValueType8)
						{
							ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
						}
						ilgenerator.Emit(OpCodes.Stelem_Ref);
					}
				}
				ilgenerator.Emit(OpCodes.Ret);
				Class11.Delegate10 @delegate = (Class11.Delegate10)dynamicMethod.CreateDelegate(typeof(Class11.Delegate10));
				bool flag11 = !bool_4;
				if (flag11)
				{
					Class11.Class20.dictionary_3.Add((MethodBase)object_1, @delegate);
				}
				else
				{
					Class11.Class20.dictionary_2.Add((MethodBase)object_1, @delegate);
				}
				return @delegate;
			}
			private static Class11.Delegate10 smethod_3(object object_1, bool bool_4, Class11.Class19 class19_0)
			{
				Class11.Delegate10 result = null;
				if (bool_4)
				{
					bool flag = Class11.Class20.dictionary_4.TryGetValue(class19_0, out result);
					if (flag)
					{
						return result;
					}
				}
				else
				{
					bool flag2 = Class11.Class20.dictionary_5.TryGetValue(class19_0, out result);
					if (flag2)
					{
						return result;
					}
				}
				MethodInfo methodInfo = object_1 as MethodInfo;
				DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[]
				{
					typeof(object),
					typeof(object[])
				}, typeof(Class11), true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ParameterInfo[] parameters = ((MethodBase)object_1).GetParameters();
				Type[] array = new Type[parameters.Length];
				for (int i = 0; i < array.Length; i++)
				{
					bool isByRef = parameters[i].ParameterType.IsByRef;
					if (isByRef)
					{
						array[i] = parameters[i].ParameterType.GetElementType();
					}
					else
					{
						array[i] = parameters[i].ParameterType;
					}
				}
				int num = array.Length;
				bool isValueType = ((MemberInfo)object_1).DeclaringType.IsValueType;
				if (isValueType)
				{
					num++;
				}
				LocalBuilder[] array2 = new LocalBuilder[num];
				for (int j = 0; j < array.Length; j++)
				{
					bool flag3 = !class19_0.method_1(j);
					if (flag3)
					{
						array2[j] = ilgenerator.DeclareLocal(array[j]);
					}
					else
					{
						array2[j] = ilgenerator.DeclareLocal(typeof(object));
					}
				}
				bool isValueType2 = ((MemberInfo)object_1).DeclaringType.IsValueType;
				if (isValueType2)
				{
					array2[array2.Length - 1] = ilgenerator.DeclareLocal(((MemberInfo)object_1).DeclaringType.MakeByRefType());
				}
				for (int k = 0; k < array.Length; k++)
				{
					ilgenerator.Emit(OpCodes.Ldarg_1);
					Class11.Class20.smethod_5(ilgenerator, k);
					ilgenerator.Emit(OpCodes.Ldelem_Ref);
					bool flag4 = !class19_0.method_1(k);
					if (flag4)
					{
						bool isValueType3 = array[k].IsValueType;
						if (isValueType3)
						{
							ilgenerator.Emit(OpCodes.Unbox_Any, array[k]);
						}
						else
						{
							bool flag5 = array[k] != typeof(object);
							if (flag5)
							{
								ilgenerator.Emit(OpCodes.Castclass, array[k]);
							}
						}
					}
					ilgenerator.Emit(OpCodes.Stloc, array2[k]);
				}
				bool flag6 = !((MethodBase)object_1).IsStatic;
				if (flag6)
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
					bool isValueType4 = ((MemberInfo)object_1).DeclaringType.IsValueType;
					if (isValueType4)
					{
						ilgenerator.Emit(OpCodes.Unbox, ((MemberInfo)object_1).DeclaringType);
						ilgenerator.Emit(OpCodes.Stloc, array2[array2.Length - 1]);
						ilgenerator.Emit(OpCodes.Ldloc_S, array2[array2.Length - 1]);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Castclass, ((MemberInfo)object_1).DeclaringType);
					}
				}
				for (int l = 0; l < array.Length; l++)
				{
					bool flag7 = class19_0.method_1(l);
					if (flag7)
					{
						Class11.Class18 @class = class19_0.method_0(l);
						bool flag8 = !((FieldInfo)@class.object_0).IsStatic;
						if (flag8)
						{
							bool isValueType5 = ((MemberInfo)@class.object_0).DeclaringType.IsValueType;
							if (isValueType5)
							{
								ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
								ilgenerator.Emit(OpCodes.Unbox, ((MemberInfo)@class.object_0).DeclaringType);
								ilgenerator.Emit(OpCodes.Ldflda, (FieldInfo)@class.object_0);
							}
							else
							{
								ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
								ilgenerator.Emit(OpCodes.Castclass, ((MemberInfo)@class.object_0).DeclaringType);
								ilgenerator.Emit(OpCodes.Ldflda, (FieldInfo)@class.object_0);
							}
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldsflda, (FieldInfo)@class.object_0);
						}
					}
					else
					{
						bool flag9 = !parameters[l].ParameterType.IsByRef;
						if (flag9)
						{
							ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldloca_S, array2[l]);
						}
					}
				}
				if (bool_4)
				{
					bool flag10 = methodInfo != null;
					if (flag10)
					{
						ilgenerator.EmitCall(OpCodes.Call, methodInfo, null);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Call, object_1 as ConstructorInfo);
					}
				}
				else
				{
					bool flag11 = !(methodInfo != null);
					if (flag11)
					{
						ilgenerator.Emit(OpCodes.Callvirt, object_1 as ConstructorInfo);
					}
					else
					{
						ilgenerator.EmitCall(OpCodes.Callvirt, methodInfo, null);
					}
				}
				bool flag12 = !(methodInfo == null) && !(methodInfo.ReturnType == typeof(void));
				if (flag12)
				{
					bool flag13 = !methodInfo.ReturnType.IsByRef;
					if (flag13)
					{
						bool isValueType6 = methodInfo.ReturnType.IsValueType;
						if (isValueType6)
						{
							ilgenerator.Emit(OpCodes.Box, methodInfo.ReturnType);
						}
					}
					else
					{
						Type elementType = methodInfo.ReturnType.GetElementType();
						bool flag14 = !elementType.IsValueType;
						if (flag14)
						{
							ilgenerator.Emit(OpCodes.Ldind_Ref, elementType);
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldobj, elementType);
						}
						bool isValueType7 = elementType.IsValueType;
						if (isValueType7)
						{
							ilgenerator.Emit(OpCodes.Box, elementType);
						}
					}
				}
				else
				{
					ilgenerator.Emit(OpCodes.Ldnull);
				}
				for (int m = 0; m < array.Length; m++)
				{
					bool flag15 = !parameters[m].ParameterType.IsByRef;
					if (!flag15)
					{
						bool flag16 = !class19_0.method_1(m);
						if (flag16)
						{
							ilgenerator.Emit(OpCodes.Ldarg_1);
							Class11.Class20.smethod_5(ilgenerator, m);
							ilgenerator.Emit(OpCodes.Ldloc, array2[m]);
							bool isValueType8 = array2[m].LocalType.IsValueType;
							if (isValueType8)
							{
								ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
							}
							ilgenerator.Emit(OpCodes.Stelem_Ref);
						}
						else
						{
							Class11.Class18 class2 = class19_0.method_0(m);
							bool isStatic = ((FieldInfo)class2.object_0).IsStatic;
							if (isStatic)
							{
								ilgenerator.Emit(OpCodes.Ldarg_1);
								Class11.Class20.smethod_5(ilgenerator, m);
								ilgenerator.Emit(OpCodes.Ldsfld, (FieldInfo)class2.object_0);
								bool isValueType9 = ((FieldInfo)class2.object_0).FieldType.IsValueType;
								if (isValueType9)
								{
									ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
								}
								ilgenerator.Emit(OpCodes.Stelem_Ref);
							}
							else
							{
								ilgenerator.Emit(OpCodes.Ldarg_1);
								Class11.Class20.smethod_5(ilgenerator, m);
								ilgenerator.Emit(OpCodes.Ldloc, array2[m]);
								bool isValueType10 = array2[m].LocalType.IsValueType;
								if (isValueType10)
								{
									ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
								}
								ilgenerator.Emit(OpCodes.Stelem_Ref);
							}
						}
					}
				}
				ilgenerator.Emit(OpCodes.Ret);
				Class11.Delegate10 @delegate = (Class11.Delegate10)dynamicMethod.CreateDelegate(typeof(Class11.Delegate10));
				if (bool_4)
				{
					Class11.Class20.dictionary_4.Add(class19_0, @delegate);
				}
				else
				{
					Class11.Class20.dictionary_5.Add(class19_0, @delegate);
				}
				return @delegate;
			}
			private static Class11.Delegate10 smethod_4(object object_1, bool bool_4, Class11.Class19 class19_0)
			{
				Class11.Delegate10 @delegate = null;
				bool flag = Class11.Class20.dictionary_6.TryGetValue(class19_0, out @delegate);
				Class11.Delegate10 result;
				if (flag)
				{
					result = @delegate;
				}
				else
				{
					ConstructorInfo constructorInfo = object_1 as ConstructorInfo;
					DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[]
					{
						typeof(object),
						typeof(object[])
					}, typeof(Class11), true);
					ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
					ParameterInfo[] parameters = ((MethodBase)object_1).GetParameters();
					Type[] array = new Type[parameters.Length];
					for (int i = 0; i < array.Length; i++)
					{
						bool flag2 = !parameters[i].ParameterType.IsByRef;
						if (flag2)
						{
							array[i] = parameters[i].ParameterType;
						}
						else
						{
							array[i] = parameters[i].ParameterType.GetElementType();
						}
					}
					int num = array.Length;
					bool isValueType = ((MemberInfo)object_1).DeclaringType.IsValueType;
					if (isValueType)
					{
						num++;
					}
					LocalBuilder[] array2 = new LocalBuilder[num];
					for (int j = 0; j < array.Length; j++)
					{
						bool flag3 = class19_0.method_1(j);
						if (flag3)
						{
							array2[j] = ilgenerator.DeclareLocal(typeof(object));
						}
						else
						{
							array2[j] = ilgenerator.DeclareLocal(array[j]);
						}
					}
					bool isValueType2 = ((MemberInfo)object_1).DeclaringType.IsValueType;
					if (isValueType2)
					{
						array2[array2.Length - 1] = ilgenerator.DeclareLocal(((MemberInfo)object_1).DeclaringType.MakeByRefType());
					}
					for (int k = 0; k < array.Length; k++)
					{
						ilgenerator.Emit(OpCodes.Ldarg_1);
						Class11.Class20.smethod_5(ilgenerator, k);
						ilgenerator.Emit(OpCodes.Ldelem_Ref);
						bool flag4 = !class19_0.method_1(k);
						if (flag4)
						{
							bool flag5 = !array[k].IsValueType;
							if (flag5)
							{
								bool flag6 = array[k] != typeof(object);
								if (flag6)
								{
									ilgenerator.Emit(OpCodes.Castclass, array[k]);
								}
							}
							else
							{
								ilgenerator.Emit(OpCodes.Unbox_Any, array[k]);
							}
						}
						ilgenerator.Emit(OpCodes.Stloc, array2[k]);
					}
					for (int l = 0; l < array.Length; l++)
					{
						bool flag7 = class19_0.method_1(l);
						if (flag7)
						{
							Class11.Class18 @class = class19_0.method_0(l);
							bool isStatic = ((FieldInfo)@class.object_0).IsStatic;
							if (isStatic)
							{
								ilgenerator.Emit(OpCodes.Ldsflda, (FieldInfo)@class.object_0);
							}
							else
							{
								bool isValueType3 = ((MemberInfo)@class.object_0).DeclaringType.IsValueType;
								if (isValueType3)
								{
									ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
									ilgenerator.Emit(OpCodes.Unbox, ((MemberInfo)@class.object_0).DeclaringType);
									ilgenerator.Emit(OpCodes.Ldflda, (FieldInfo)@class.object_0);
								}
								else
								{
									ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
									ilgenerator.Emit(OpCodes.Castclass, ((MemberInfo)@class.object_0).DeclaringType);
									ilgenerator.Emit(OpCodes.Ldflda, (FieldInfo)@class.object_0);
								}
							}
						}
						else
						{
							bool flag8 = !parameters[l].ParameterType.IsByRef;
							if (flag8)
							{
								ilgenerator.Emit(OpCodes.Ldloc, array2[l]);
							}
							else
							{
								ilgenerator.Emit(OpCodes.Ldloca_S, array2[l]);
							}
						}
					}
					ilgenerator.Emit(OpCodes.Newobj, object_1 as ConstructorInfo);
					bool isValueType4 = constructorInfo.DeclaringType.IsValueType;
					if (isValueType4)
					{
						ilgenerator.Emit(OpCodes.Box, constructorInfo.DeclaringType);
					}
					for (int m = 0; m < array.Length; m++)
					{
						bool flag9 = !parameters[m].ParameterType.IsByRef;
						if (!flag9)
						{
							bool flag10 = class19_0.method_1(m);
							if (flag10)
							{
								Class11.Class18 class2 = class19_0.method_0(m);
								bool isStatic2 = ((FieldInfo)class2.object_0).IsStatic;
								if (isStatic2)
								{
									ilgenerator.Emit(OpCodes.Ldarg_1);
									Class11.Class20.smethod_5(ilgenerator, m);
									ilgenerator.Emit(OpCodes.Ldsfld, (FieldInfo)class2.object_0);
									bool isValueType5 = ((FieldInfo)class2.object_0).FieldType.IsValueType;
									if (isValueType5)
									{
										ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
									}
									ilgenerator.Emit(OpCodes.Stelem_Ref);
								}
								else
								{
									ilgenerator.Emit(OpCodes.Ldarg_1);
									Class11.Class20.smethod_5(ilgenerator, m);
									ilgenerator.Emit(OpCodes.Ldloc, array2[m]);
									bool isValueType6 = array2[m].LocalType.IsValueType;
									if (isValueType6)
									{
										ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
									}
									ilgenerator.Emit(OpCodes.Stelem_Ref);
								}
							}
							else
							{
								ilgenerator.Emit(OpCodes.Ldarg_1);
								Class11.Class20.smethod_5(ilgenerator, m);
								ilgenerator.Emit(OpCodes.Ldloc, array2[m]);
								bool isValueType7 = array2[m].LocalType.IsValueType;
								if (isValueType7)
								{
									ilgenerator.Emit(OpCodes.Box, array2[m].LocalType);
								}
								ilgenerator.Emit(OpCodes.Stelem_Ref);
							}
						}
					}
					ilgenerator.Emit(OpCodes.Ret);
					Class11.Delegate10 delegate2 = (Class11.Delegate10)dynamicMethod.CreateDelegate(typeof(Class11.Delegate10));
					Class11.Class20.dictionary_6.Add(class19_0, delegate2);
					result = delegate2;
				}
				return result;
			}
			private static void smethod_5(ILGenerator ilgenerator_0, int int_3)
			{
				switch (int_3)
				{
				case -1:
					ilgenerator_0.Emit(OpCodes.Ldc_I4_M1);
					break;
				case 0:
					ilgenerator_0.Emit(OpCodes.Ldc_I4_0);
					break;
				case 1:
					ilgenerator_0.Emit(OpCodes.Ldc_I4_1);
					break;
				case 2:
					ilgenerator_0.Emit(OpCodes.Ldc_I4_2);
					break;
				case 3:
					ilgenerator_0.Emit(OpCodes.Ldc_I4_3);
					break;
				case 4:
					ilgenerator_0.Emit(OpCodes.Ldc_I4_4);
					break;
				case 5:
					ilgenerator_0.Emit(OpCodes.Ldc_I4_5);
					break;
				case 6:
					ilgenerator_0.Emit(OpCodes.Ldc_I4_6);
					break;
				case 7:
					ilgenerator_0.Emit(OpCodes.Ldc_I4_7);
					break;
				case 8:
					ilgenerator_0.Emit(OpCodes.Ldc_I4_8);
					break;
				default:
				{
					bool flag = int_3 > -129 && int_3 < 128;
					if (flag)
					{
						ilgenerator_0.Emit(OpCodes.Ldc_I4_S, (sbyte)int_3);
					}
					else
					{
						ilgenerator_0.Emit(OpCodes.Ldc_I4, int_3);
					}
					break;
				}
				}
			}
			private static Class11.Class22 smethod_6(Class11.Class22 class22_3)
			{
				bool flag = class22_3.vmethod_8().method_0();
				if (flag)
				{
					object obj = class22_3.vmethod_4(null);
					bool flag2 = obj != null && obj.GetType().IsEnum;
					if (flag2)
					{
						Type underlyingType = Enum.GetUnderlyingType(obj.GetType());
						object obj2 = Convert.ChangeType(obj, underlyingType);
						Class11.Class22 @class = Class11.Class20.smethod_7(Class11.Class22.smethod_1(underlyingType, obj2));
						bool flag3 = @class != null;
						if (flag3)
						{
							return @class as Class11.Class23;
						}
					}
				}
				return class22_3;
			}
			private static Class11.Class23 smethod_7(Class11.Class22 class22_3)
			{
				Class11.Class23 @class = class22_3 as Class11.Class23;
				bool flag = @class == null && class22_3.vmethod_0();
				if (flag)
				{
					@class = (class22_3.vmethod_8() as Class11.Class23);
				}
				return @class;
			}
			private static IntPtr smethod_8(object object_1)
			{
				bool flag = object_1 == null;
				IntPtr result;
				if (flag)
				{
					result = IntPtr.Zero;
				}
				else
				{
					bool flag2 = ((Class11.Class22)object_1).method_2();
					if (flag2)
					{
						result = ((Class11.Class26)object_1).method_7();
					}
					else
					{
						bool flag3 = ((Class11.Class22)object_1).vmethod_0();
						if (flag3)
						{
							Class11.Class28 @class = (Class11.Class28)object_1;
							try
							{
								return @class.vmethod_11();
							}
							catch
							{
							}
						}
						object obj = ((Class11.Class22)object_1).vmethod_4(typeof(IntPtr));
						bool flag4 = obj == null || !(obj.GetType() == typeof(IntPtr));
						if (flag4)
						{
							throw new Class11.Exception1();
						}
						result = (IntPtr)obj;
					}
				}
				return result;
			}
			private static object smethod_9(object object_1)
			{
				bool flag = Class11.Class20.dictionary_7 == null;
				if (flag)
				{
					Class11.Class20.dictionary_7 = new Dictionary<Type, Class11.Delegate11>();
				}
				bool flag2 = object_1 == null;
				object result;
				if (flag2)
				{
					result = null;
				}
				else
				{
					try
					{
						Type type = object_1.GetType();
						Class11.Delegate11 @delegate;
						bool flag3 = Class11.Class20.dictionary_7.TryGetValue(type, out @delegate);
						if (flag3)
						{
							result = @delegate(object_1);
						}
						else
						{
							DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[]
							{
								typeof(object)
							}, true);
							ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
							ilgenerator.Emit(OpCodes.Ldarg_0);
							ilgenerator.Emit(OpCodes.Unbox_Any, type);
							ilgenerator.Emit(OpCodes.Box, type);
							ilgenerator.Emit(OpCodes.Ret);
							Class11.Delegate11 delegate2 = (Class11.Delegate11)dynamicMethod.CreateDelegate(typeof(Class11.Delegate11));
							Class11.Class20.dictionary_7.Add(type, delegate2);
							result = delegate2(object_1);
						}
					}
					catch
					{
						result = null;
					}
				}
				return result;
			}
			private static void smethod_10(IntPtr intptr_0, byte byte_0, int int_3)
			{
				bool flag = Class11.Class20.delegate12_0 == null;
				if (flag)
				{
					DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(void), new Type[]
					{
						typeof(IntPtr),
						typeof(byte),
						typeof(int)
					}, typeof(Class11), true);
					ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldarg_1);
					ilgenerator.Emit(OpCodes.Ldarg_2);
					ilgenerator.Emit(OpCodes.Initblk);
					ilgenerator.Emit(OpCodes.Ret);
					Class11.Class20.delegate12_0 = (Class11.Delegate12)dynamicMethod.CreateDelegate(typeof(Class11.Delegate12));
				}
				Class11.Class20.delegate12_0(intptr_0, byte_0, int_3);
			}
			private static void smethod_11(IntPtr intptr_0, IntPtr intptr_1, uint uint_0)
			{
				bool flag = Class11.Class20.delegate13_0 == null;
				if (flag)
				{
					DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(void), new Type[]
					{
						typeof(IntPtr),
						typeof(IntPtr),
						typeof(uint)
					}, typeof(Class11), true);
					ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldarg_1);
					ilgenerator.Emit(OpCodes.Ldarg_2);
					ilgenerator.Emit(OpCodes.Cpblk);
					ilgenerator.Emit(OpCodes.Ret);
					Class11.Class20.delegate13_0 = (Class11.Delegate13)dynamicMethod.CreateDelegate(typeof(Class11.Delegate13));
				}
				Class11.Class20.delegate13_0(intptr_0, intptr_1, uint_0);
			}
			internal Class11.Class17 class17_0;
			internal Class11.Class22[] class22_0 = new Class11.Class22[0];
			internal Class11.Class22[] class22_1 = new Class11.Class22[0];
			internal Class11.Class36 class36_0 = new Class11.Class36();
			internal Class11.Class22 class22_2;
			internal Exception exception_0;
			internal List<IntPtr> list_0;
			private int int_0;
			private int int_1;
			private int int_2 = -1;
			private object object_0;
			private bool bool_0;
			private bool bool_1;
			private bool bool_2;
			private bool bool_3;
			private static Dictionary<Type, int> dictionary_0;
			private static Dictionary<object, Class11.Class22> dictionary_1 = new Dictionary<object, Class11.Class22>();
			private static Dictionary<MethodBase, Class11.Delegate10> dictionary_2 = new Dictionary<MethodBase, Class11.Delegate10>();
			private static Dictionary<MethodBase, Class11.Delegate10> dictionary_3 = new Dictionary<MethodBase, Class11.Delegate10>();
			private static Dictionary<Class11.Class19, Class11.Delegate10> dictionary_4 = new Dictionary<Class11.Class19, Class11.Delegate10>();
			private static Dictionary<Class11.Class19, Class11.Delegate10> dictionary_5 = new Dictionary<Class11.Class19, Class11.Delegate10>();
			private static Dictionary<Class11.Class19, Class11.Delegate10> dictionary_6 = new Dictionary<Class11.Class19, Class11.Delegate10>();
			private static Dictionary<Type, Class11.Delegate11> dictionary_7;
			private static Class11.Delegate12 delegate12_0;
			private static Class11.Delegate13 delegate13_0;
			[CompilerGenerated]
			[Serializable]
			private sealed class Class21
			{
				
				internal int method_0(Class11.Class15 x, Class11.Class15 y)
				{
					return x.class16_0.int_0.CompareTo(y.class16_0.int_0);
				}

				
				public static readonly Class11.Class20.Class21 _003C_003E9 = new Class11.Class20.Class21();

				
				public static Comparison<Class11.Class15> _003C_003E9__12_0;
			}
		}

		
		internal enum Enum3 : byte
		{

		}

		
		internal enum Enum4 : byte
		{

		}

		
		internal abstract class Class22
		{			public Class22()
			{
			}
			internal bool method_0()
			{
				return this.enum4_0 == (Class11.Enum4)0;
			}
			internal bool method_1()
			{
				return this.enum4_0 == (Class11.Enum4)1;
			}
			internal bool method_2()
			{
				bool flag = this.enum4_0 != (Class11.Enum4)3;
				return !flag || this.enum4_0 == (Class11.Enum4)4;
			}
			internal bool method_3()
			{
				return this.enum4_0 == (Class11.Enum4)2;
			}
			internal bool method_4()
			{
				return this.enum4_0 == (Class11.Enum4)5;
			}
			internal bool method_5()
			{
				return this.enum4_0 == (Class11.Enum4)6;
			}
			internal virtual bool vmethod_0()
			{
				return false;
			}
			internal virtual bool vmethod_1()
			{
				return false;
			}
			internal abstract void vmethod_2(Class11.Class22 class22_0);
			internal virtual bool vmethod_3()
			{
				return false;
			}
			internal Class22(Class11.Enum4 enum4_1)
			{
				this.enum4_0 = enum4_1;
			}
			internal abstract object vmethod_4(Type type_0);
			internal abstract bool vmethod_5(Class11.Class22 class22_0);
			internal abstract bool vmethod_6(Class11.Class22 class22_0);
			internal abstract bool vmethod_7();
			internal abstract Class11.Class22 vmethod_8();
			internal virtual bool vmethod_9()
			{
				return false;
			}
			internal abstract void vmethod_10(Class11.Class22 class22_0);
			internal static Class11.Enum2 smethod_0(Type type_0)
			{
				Type type = type_0;
				bool flag = type != null;
				Class11.Enum2 result;
				if (flag)
				{
					bool isByRef = type.IsByRef;
					if (isByRef)
					{
						type = type.GetElementType();
					}
					bool flag2 = !(type == typeof(string));
					if (flag2)
					{
						bool flag3 = !(type == typeof(byte));
						if (flag3)
						{
							bool flag4 = type == typeof(sbyte);
							if (flag4)
							{
								result = (Class11.Enum2)1;
							}
							else
							{
								bool flag5 = !(type == typeof(short));
								if (flag5)
								{
									bool flag6 = !(type == typeof(ushort));
									if (flag6)
									{
										bool flag7 = !(type == typeof(int));
										if (flag7)
										{
											bool flag8 = !(type == typeof(uint));
											if (flag8)
											{
												bool flag9 = !(type == typeof(long));
												if (flag9)
												{
													bool flag10 = !(type == typeof(ulong));
													if (flag10)
													{
														bool flag11 = !(type == typeof(float));
														if (flag11)
														{
															bool flag12 = type == typeof(double);
															if (flag12)
															{
																result = (Class11.Enum2)10;
															}
															else
															{
																bool flag13 = type == typeof(bool);
																if (flag13)
																{
																	result = (Class11.Enum2)11;
																}
																else
																{
																	bool flag14 = !(type == typeof(IntPtr));
																	if (flag14)
																	{
																		bool flag15 = !(type == typeof(UIntPtr));
																		if (flag15)
																		{
																			bool flag16 = !(type == typeof(char));
																			if (flag16)
																			{
																				bool flag17 = !(type == typeof(object));
																				if (flag17)
																				{
																					bool isEnum = type.IsEnum;
																					if (isEnum)
																					{
																						result = (Class11.Enum2)16;
																					}
																					else
																					{
																						result = (Class11.Enum2)17;
																					}
																				}
																				else
																				{
																					result = (Class11.Enum2)0;
																				}
																			}
																			else
																			{
																				result = (Class11.Enum2)15;
																			}
																		}
																		else
																		{
																			result = (Class11.Enum2)13;
																		}
																	}
																	else
																	{
																		result = (Class11.Enum2)12;
																	}
																}
															}
														}
														else
														{
															result = (Class11.Enum2)9;
														}
													}
													else
													{
														result = (Class11.Enum2)8;
													}
												}
												else
												{
													result = (Class11.Enum2)7;
												}
											}
											else
											{
												result = (Class11.Enum2)6;
											}
										}
										else
										{
											result = (Class11.Enum2)5;
										}
									}
									else
									{
										result = (Class11.Enum2)4;
									}
								}
								else
								{
									result = (Class11.Enum2)3;
								}
							}
						}
						else
						{
							result = (Class11.Enum2)2;
						}
					}
					else
					{
						result = (Class11.Enum2)14;
					}
				}
				else
				{
					result = (Class11.Enum2)18;
				}
				return result;
			}
			internal static Class11.Class22 smethod_1(Type type_0, object object_0)
			{
				Class11.Enum2 @enum = Class11.Class22.smethod_0(type_0);
				Class11.Enum2 enum2 = (Class11.Enum2)18;
				bool flag = object_0 != null;
				if (flag)
				{
					enum2 = Class11.Class22.smethod_0(object_0.GetType());
				}
				Class11.Class22 @class = null;
				switch (@enum)
				{
				case (Class11.Enum2)0:
					@class = ((enum2 != (Class11.Enum2)15) ? Class11.Class22.smethod_2(object_0) : new Class11.Class34(object_0));
					break;
				case (Class11.Enum2)1:
				{
					if (!true)
					{
					}
					Class11.Class24 class2;
					if (enum2 <= (Class11.Enum2)2)
					{
						if (enum2 == (Class11.Enum2)1)
						{
							class2 = new Class11.Class24((int)((sbyte)object_0), (Class11.Enum1)1);
							goto IL_117;
						}
						if (enum2 == (Class11.Enum2)2)
						{
							class2 = new Class11.Class24((int)((sbyte)((byte)object_0)), (Class11.Enum1)1);
							goto IL_117;
						}
					}
					else
					{
						if (enum2 == (Class11.Enum2)11)
						{
							class2 = ((!(bool)object_0) ? new Class11.Class24(0, (Class11.Enum1)1) : new Class11.Class24(1, (Class11.Enum1)1));
							goto IL_117;
						}
						if (enum2 == (Class11.Enum2)15)
						{
							class2 = new Class11.Class24((int)((sbyte)((char)object_0)), (Class11.Enum1)1);
							goto IL_117;
						}
					}
					throw new InvalidCastException();
					IL_117:
					if (!true)
					{
					}
					Class11.Class24 class3 = class2;
					@class = class3;
					break;
				}
				case (Class11.Enum2)2:
				{
					if (!true)
					{
					}
					Class11.Class24 class2;
					if (enum2 <= (Class11.Enum2)2)
					{
						if (enum2 == (Class11.Enum2)1)
						{
							class2 = new Class11.Class24((int)((byte)((sbyte)object_0)), (Class11.Enum1)2);
							goto IL_1A8;
						}
						if (enum2 == (Class11.Enum2)2)
						{
							class2 = new Class11.Class24((int)((byte)object_0), (Class11.Enum1)2);
							goto IL_1A8;
						}
					}
					else
					{
						if (enum2 == (Class11.Enum2)11)
						{
							class2 = ((!(bool)object_0) ? new Class11.Class24(0, (Class11.Enum1)2) : new Class11.Class24(1, (Class11.Enum1)2));
							goto IL_1A8;
						}
						if (enum2 == (Class11.Enum2)15)
						{
							class2 = new Class11.Class24((int)((byte)((char)object_0)), (Class11.Enum1)2);
							goto IL_1A8;
						}
					}
					throw new InvalidCastException();
					IL_1A8:
					if (!true)
					{
					}
					Class11.Class24 class4 = class2;
					@class = class4;
					break;
				}
				case (Class11.Enum2)3:
				{
					if (!true)
					{
					}
					Class11.Class24 class2;
					if (enum2 != (Class11.Enum2)3)
					{
						if (enum2 != (Class11.Enum2)11)
						{
							if (enum2 != (Class11.Enum2)15)
							{
								throw new InvalidCastException();
							}
							class2 = new Class11.Class24((int)((short)((char)object_0)), (Class11.Enum1)3);
						}
						else
						{
							class2 = (((bool)object_0) ? new Class11.Class24(1, (Class11.Enum1)3) : new Class11.Class24(0, (Class11.Enum1)3));
						}
					}
					else
					{
						class2 = new Class11.Class24((int)((short)object_0), (Class11.Enum1)3);
					}
					if (!true)
					{
					}
					Class11.Class24 class5 = class2;
					@class = class5;
					break;
				}
				case (Class11.Enum2)4:
				{
					if (!true)
					{
					}
					Class11.Class24 class2;
					if (enum2 != (Class11.Enum2)4)
					{
						if (enum2 != (Class11.Enum2)11)
						{
							if (enum2 != (Class11.Enum2)15)
							{
								throw new InvalidCastException();
							}
							class2 = new Class11.Class24((int)((char)object_0), (Class11.Enum1)4);
						}
						else
						{
							class2 = ((!(bool)object_0) ? new Class11.Class24(0, (Class11.Enum1)4) : new Class11.Class24(1, (Class11.Enum1)4));
						}
					}
					else
					{
						class2 = new Class11.Class24((int)((ushort)object_0), (Class11.Enum1)4);
					}
					if (!true)
					{
					}
					Class11.Class24 class6 = class2;
					@class = class6;
					break;
				}
				case (Class11.Enum2)5:
				{
					if (!true)
					{
					}
					Class11.Class24 class2;
					if (enum2 != (Class11.Enum2)5)
					{
						if (enum2 != (Class11.Enum2)11)
						{
							if (enum2 != (Class11.Enum2)15)
							{
								throw new InvalidCastException();
							}
							class2 = new Class11.Class24((int)((char)object_0), (Class11.Enum1)5);
						}
						else
						{
							class2 = ((!(bool)object_0) ? new Class11.Class24(0, (Class11.Enum1)5) : new Class11.Class24(1, (Class11.Enum1)5));
						}
					}
					else
					{
						class2 = new Class11.Class24((int)object_0, (Class11.Enum1)5);
					}
					if (!true)
					{
					}
					Class11.Class24 class7 = class2;
					@class = class7;
					break;
				}
				case (Class11.Enum2)6:
				{
					if (!true)
					{
					}
					Class11.Class24 class2;
					if (enum2 != (Class11.Enum2)6)
					{
						if (enum2 != (Class11.Enum2)11)
						{
							if (enum2 != (Class11.Enum2)15)
							{
								throw new InvalidCastException();
							}
							class2 = new Class11.Class24((uint)((char)object_0), (Class11.Enum1)6);
						}
						else
						{
							class2 = (((bool)object_0) ? new Class11.Class24(1U, (Class11.Enum1)6) : new Class11.Class24(0U, (Class11.Enum1)6));
						}
					}
					else
					{
						class2 = new Class11.Class24((uint)object_0, (Class11.Enum1)6);
					}
					if (!true)
					{
					}
					Class11.Class24 class8 = class2;
					@class = class8;
					break;
				}
				case (Class11.Enum2)7:
				{
					if (!true)
					{
					}
					Class11.Class25 class9;
					if (enum2 != (Class11.Enum2)7)
					{
						if (enum2 != (Class11.Enum2)11)
						{
							if (enum2 != (Class11.Enum2)15)
							{
								throw new InvalidCastException();
							}
							class9 = new Class11.Class25((long)((ulong)((char)object_0)), (Class11.Enum1)7);
						}
						else
						{
							class9 = (((bool)object_0) ? new Class11.Class25(1L, (Class11.Enum1)7) : new Class11.Class25(0L, (Class11.Enum1)7));
						}
					}
					else
					{
						class9 = new Class11.Class25((long)object_0, (Class11.Enum1)7);
					}
					if (!true)
					{
					}
					Class11.Class25 class10 = class9;
					@class = class10;
					break;
				}
				case (Class11.Enum2)8:
				{
					if (!true)
					{
					}
					Class11.Class25 class9;
					if (enum2 != (Class11.Enum2)8)
					{
						if (enum2 != (Class11.Enum2)11)
						{
							if (enum2 != (Class11.Enum2)15)
							{
								throw new InvalidCastException();
							}
							class9 = new Class11.Class25((ulong)((char)object_0), (Class11.Enum1)8);
						}
						else
						{
							class9 = (((bool)object_0) ? new Class11.Class25(1UL, (Class11.Enum1)8) : new Class11.Class25(0UL, (Class11.Enum1)8));
						}
					}
					else
					{
						class9 = new Class11.Class25((ulong)object_0, (Class11.Enum1)8);
					}
					if (!true)
					{
					}
					Class11.Class25 class11 = class9;
					@class = class11;
					break;
				}
				case (Class11.Enum2)9:
				{
					bool flag2 = enum2 == (Class11.Enum2)9;
					if (!flag2)
					{
						throw new InvalidCastException();
					}
					@class = new Class11.Class27((float)object_0);
					break;
				}
				case (Class11.Enum2)10:
				{
					bool flag3 = enum2 == (Class11.Enum2)10;
					if (!flag3)
					{
						throw new InvalidCastException();
					}
					@class = new Class11.Class27((double)object_0);
					break;
				}
				case (Class11.Enum2)11:
					switch (enum2)
					{
					case (Class11.Enum2)1:
						@class = new Class11.Class24((sbyte)object_0 != 0);
						goto IL_5E1;
					case (Class11.Enum2)2:
						@class = new Class11.Class24((byte)object_0 > 0);
						goto IL_5E1;
					case (Class11.Enum2)3:
						@class = new Class11.Class24((short)object_0 != 0);
						goto IL_5E1;
					case (Class11.Enum2)4:
						@class = new Class11.Class24((ushort)object_0 > 0);
						goto IL_5E1;
					case (Class11.Enum2)5:
						@class = new Class11.Class24((int)object_0 != 0);
						goto IL_5E1;
					case (Class11.Enum2)6:
						@class = new Class11.Class24((uint)object_0 > 0U);
						goto IL_5E1;
					case (Class11.Enum2)7:
						@class = new Class11.Class24((long)object_0 != 0L);
						goto IL_5E1;
					case (Class11.Enum2)8:
						@class = new Class11.Class24((ulong)object_0 > 0UL);
						goto IL_5E1;
					case (Class11.Enum2)9:
					case (Class11.Enum2)10:
					case (Class11.Enum2)12:
					case (Class11.Enum2)13:
					case (Class11.Enum2)14:
					case (Class11.Enum2)15:
					case (Class11.Enum2)16:
						throw new InvalidCastException();
					case (Class11.Enum2)11:
						@class = new Class11.Class24((bool)object_0);
						goto IL_5E1;
					case (Class11.Enum2)18:
						@class = new Class11.Class24(false);
						goto IL_5E1;
					}
					@class = new Class11.Class24(object_0 != null);
					IL_5E1:
					break;
				case (Class11.Enum2)12:
				{
					bool flag4 = enum2 == (Class11.Enum2)12;
					if (!flag4)
					{
						throw new InvalidCastException();
					}
					@class = new Class11.Class26((IntPtr)object_0);
					break;
				}
				case (Class11.Enum2)13:
				{
					bool flag5 = enum2 == (Class11.Enum2)13;
					if (!flag5)
					{
						throw new InvalidCastException();
					}
					@class = new Class11.Class26((UIntPtr)object_0);
					break;
				}
				case (Class11.Enum2)14:
					@class = new Class11.Class35(object_0 as string);
					break;
				case (Class11.Enum2)15:
				{
					if (!true)
					{
					}
					Class11.Class24 class2;
					switch (enum2)
					{
					case (Class11.Enum2)1:
						class2 = new Class11.Class24((int)((sbyte)object_0), (Class11.Enum1)15);
						break;
					case (Class11.Enum2)2:
						class2 = new Class11.Class24((int)((byte)object_0), (Class11.Enum1)15);
						break;
					case (Class11.Enum2)3:
						class2 = new Class11.Class24((int)((short)object_0), (Class11.Enum1)15);
						break;
					case (Class11.Enum2)4:
						class2 = new Class11.Class24((int)((ushort)object_0), (Class11.Enum1)15);
						break;
					case (Class11.Enum2)5:
						class2 = new Class11.Class24((int)object_0, (Class11.Enum1)15);
						break;
					case (Class11.Enum2)6:
						class2 = new Class11.Class24((int)((uint)object_0), (Class11.Enum1)15);
						break;
					default:
						if (enum2 != (Class11.Enum2)15)
						{
							throw new InvalidCastException();
						}
						class2 = new Class11.Class24((int)((char)object_0), (Class11.Enum1)15);
						break;
					}
					if (!true)
					{
					}
					Class11.Class24 class12 = class2;
					@class = class12;
					break;
				}
				case (Class11.Enum2)16:
				case (Class11.Enum2)17:
					@class = Class11.Class22.smethod_2(object_0);
					break;
				case (Class11.Enum2)18:
					throw new InvalidCastException();
				}
				bool isByRef = type_0.IsByRef;
				if (isByRef)
				{
					@class = new Class11.Class33(@class, type_0.GetElementType());
				}
				return @class;
			}
			private static Class11.Class22 smethod_2(object object_0)
			{
				bool flag = object_0 != null && object_0.GetType().IsEnum;
				if (flag)
				{
					Type underlyingType = Enum.GetUnderlyingType(object_0.GetType());
					object object_ = Convert.ChangeType(object_0, underlyingType);
					Class11.Class22 @class = Class11.Class22.smethod_3(Class11.Class22.smethod_1(underlyingType, object_));
					bool flag2 = @class != null;
					if (flag2)
					{
						return @class as Class11.Class23;
					}
				}
				return new Class11.Class34(object_0);
			}
			private static Class11.Class23 smethod_3(Class11.Class22 class22_0)
			{
				Class11.Class23 @class = class22_0 as Class11.Class23;
				bool flag = @class == null && class22_0.vmethod_0();
				if (flag)
				{
					@class = (class22_0.vmethod_8() as Class11.Class23);
				}
				return @class;
			}
			internal Class11.Enum4 enum4_0;
		}

		
		private class Class34 : Class11.Class22
		{			public Class34() : this(null)
			{
			}
			internal override void vmethod_10(Class11.Class22 class22_1)
			{
				bool flag = class22_1 is Class11.Class34;
				if (flag)
				{
					this.class22_0 = ((Class11.Class34)class22_1).class22_0;
					this.type_0 = ((Class11.Class34)class22_1).type_0;
				}
				else
				{
					this.class22_0 = class22_1.vmethod_8();
				}
			}
			internal override void vmethod_2(Class11.Class22 class22_1)
			{
				this.vmethod_10(class22_1);
			}
			public Class34(object object_0) : base((Class11.Enum4)0)
			{
				this.class22_0 = (Class11.Class22)object_0;
				this.type_0 = null;
			}
			public Class34(object object_0, Type type_1) : base((Class11.Enum4)0)
			{
				this.class22_0 = (Class11.Class22)object_0;
				this.type_0 = type_1;
			}
			public override string ToString()
			{
				bool flag = this.class22_0 == null;
				string result;
				if (flag)
				{
					result = ((Class11.Enum5)5).ToString();
				}
				else
				{
					result = this.class22_0.ToString();
				}
				return result;
			}
			internal override object vmethod_4(Type type_1)
			{
				bool flag = this.class22_0 != null;
				object result;
				if (flag)
				{
					bool flag2 = type_1 != null && type_1.IsByRef;
					if (flag2)
					{
						type_1 = type_1.GetElementType();
					}
					bool flag3 = this.class22_0 != null;
					if (flag3)
					{
						bool flag4 = !(this.type_0 != null);
						if (flag4)
						{
							object obj = this.class22_0.vmethod_4(type_1);
							bool flag5 = obj != null && type_1 != null && obj.GetType() != type_1;
							if (flag5)
							{
								bool flag6 = type_1 == typeof(RuntimeFieldHandle) && obj is FieldInfo;
								if (flag6)
								{
									obj = ((FieldInfo)obj).FieldHandle;
								}
								else
								{
									bool flag7 = type_1 == typeof(RuntimeTypeHandle) && obj is Type;
									if (flag7)
									{
										obj = ((Type)obj).TypeHandle;
									}
									else
									{
										bool flag8 = type_1 == typeof(RuntimeMethodHandle) && obj is MethodBase;
										if (flag8)
										{
											obj = ((MethodBase)obj).MethodHandle;
										}
									}
								}
							}
							result = obj;
						}
						else
						{
							result = this.class22_0.vmethod_4(this.type_0);
						}
					}
					else
					{
						object obj2 = this.class22_0;
						bool flag9 = obj2 != null && type_1 != null && obj2.GetType() != type_1;
						if (flag9)
						{
							bool flag10 = type_1 == typeof(RuntimeFieldHandle) && obj2 is FieldInfo;
							if (flag10)
							{
								obj2 = ((FieldInfo)obj2).FieldHandle;
							}
							else
							{
								bool flag11 = type_1 == typeof(RuntimeTypeHandle) && obj2 is Type;
								if (flag11)
								{
									obj2 = ((Type)obj2).TypeHandle;
								}
								else
								{
									bool flag12 = type_1 == typeof(RuntimeMethodHandle) && obj2 is MethodBase;
									if (flag12)
									{
										obj2 = ((MethodBase)obj2).MethodHandle;
									}
								}
							}
						}
						result = obj2;
					}
				}
				else
				{
					result = null;
				}
				return result;
			}
			internal override bool vmethod_5(Class11.Class22 class22_1)
			{
				bool flag = class22_1.vmethod_0();
				bool result;
				if (flag)
				{
					result = ((Class11.Class28)class22_1).vmethod_5(this);
				}
				else
				{
					object obj = this.vmethod_4(null);
					object obj2 = class22_1.vmethod_4(null);
					result = (obj == obj2);
				}
				return result;
			}
			internal override bool vmethod_6(Class11.Class22 class22_1)
			{
				bool flag = !class22_1.vmethod_0();
				bool result;
				if (flag)
				{
					object obj = this.vmethod_4(null);
					object obj2 = class22_1.vmethod_4(null);
					result = (obj != obj2);
				}
				else
				{
					result = ((Class11.Class28)class22_1).vmethod_6(this);
				}
				return result;
			}
			internal override Class11.Class22 vmethod_8()
			{
				Class11.Class22 @class = this.class22_0;
				bool flag = @class != null;
				Class11.Class22 result;
				if (flag)
				{
					result = @class.vmethod_8();
				}
				else
				{
					result = this;
				}
				return result;
			}
			internal override bool vmethod_7()
			{
				bool flag = this.class22_0 != null;
				bool result;
				if (flag)
				{
					Class11.Class22 @class = this.class22_0;
					bool flag2 = @class != null;
					if (flag2)
					{
						bool flag3 = @class.vmethod_4(null) == null;
						result = !flag3;
					}
					else
					{
						result = true;
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			public Class11.Class22 class22_0;
			public Type type_0;
		}

		
		private class Class35 : Class11.Class22
		{			public Class35(string string_1) : base((Class11.Enum4)6)
			{
				this.string_0 = string_1;
			}
			internal override void vmethod_10(Class11.Class22 class22_0)
			{
				this.string_0 = ((Class11.Class35)class22_0).string_0;
			}
			internal override void vmethod_2(Class11.Class22 class22_0)
			{
				this.vmethod_10(class22_0);
			}
			public override string ToString()
			{
				bool flag = this.string_0 == null;
				string result;
				if (flag)
				{
					result = ((Class11.Enum5)5).ToString();
				}
				else
				{
					result = "*" + this.string_0 + "*";
				}
				return result;
			}
			internal override bool vmethod_7()
			{
				return this.string_0 != null;
			}
			internal override object vmethod_4(Type type_0)
			{
				return this.string_0;
			}
			internal override bool vmethod_5(Class11.Class22 class22_0)
			{
				bool flag = class22_0.vmethod_0();
				bool result;
				if (flag)
				{
					result = ((Class11.Class28)class22_0).vmethod_5(this);
				}
				else
				{
					string text = this.string_0;
					object obj = class22_0.vmethod_4(null);
					result = (text == obj);
				}
				return result;
			}
			internal override bool vmethod_6(Class11.Class22 class22_0)
			{
				bool flag = !class22_0.vmethod_0();
				bool result;
				if (flag)
				{
					string text = this.string_0;
					object obj = class22_0.vmethod_4(null);
					result = (text != obj);
				}
				else
				{
					result = ((Class11.Class28)class22_0).vmethod_6(this);
				}
				return result;
			}
			internal override Class11.Class22 vmethod_8()
			{
				return this;
			}
			public string string_0;
		}

		
		internal class Class36
		{			public int method_0()
			{
				return this.list_0.Count;
			}
			public void method_1()
			{
				this.list_0.Clear();
			}
			public void method_2(Class11.Class22 class22_0)
			{
				this.list_0.Add(class22_0);
			}
			public Class11.Class22 method_3()
			{
				return this.list_0[this.list_0.Count - 1];
			}
			public Class11.Class22 method_4()
			{
				Class11.Class22 result = this.method_3();
				bool flag = this.list_0.Count != 0;
				if (flag)
				{
					this.list_0.RemoveAt(this.list_0.Count - 1);
				}
				return result;
			}
			private List<Class11.Class22> list_0 = new List<Class11.Class22>();
		}
		internal enum Enum5
		{

		}
		[CompilerGenerated]
		[Serializable]
		private sealed class Class37<T>
		{			internal int method_0(Class11.Class15 x, Class11.Class15 y)
			{
				return x.class16_0.int_0.CompareTo(y.class16_0.int_0);
			}
			internal static bool smethod_0()
			{
				return Class11.Class37<T>.object_0 == null;
			}
			internal static object smethod_1()
			{
				return Class11.Class37<T>.object_0;
			}
			public static readonly Class11.Class37<T> _003C_003E9 = new Class11.Class37<T>();
			public static Comparison<Class11.Class15> _003C_003E9__45_0;
			internal static object object_0;
		}
	}
}
