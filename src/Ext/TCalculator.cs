using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// <summary>
/// Localization: clean
/// </summary>

namespace Ext.Utils
{
	// FIXME: ��������� ���� ���, ��������������
	public sealed class TCalculator : IDisposable
	{
        [Serializable]
		public class ECalculate : Exception
		{
			public ECalculate()
			{
			}

			public ECalculate(string message) : base(message)
			{
			}
		}

		// FIXME: �����������, ���������
		public class TNamedVar
		{
			public string Name;
			public double Value;
		}

		private enum TCalcCBType : byte
		{
			ctGetValue,
			ctSetValue,
			ctFunction
		}

		private enum TToken : byte
		{
			tkEOF,
			tkERROR,
			tkASSIGN,
			tkLBRACE,
			tkRBRACE,
			tkNUMBER,
			tkIDENT,
			tkSEMICOLON,
			tkPOW,
			tkINV,
			tkNOT,
			tkMUL,
			tkDIV,
			tkMOD,
			tkPER,
			tkADD,
			tkSUB,
			tkLT,
			tkLE,
			tkEQ,
			tkNE,
			tkGE,
			tkGT,
			tkOR,
			tkXOR,
			tkAND
		}

		private double fvalue;
		private string svalue;
		private string FExpression;
		private int FPtr;
		private TToken FToken;
		private List<TNamedVar> FVars;
		private bool Disposed_;

		/*public double Vars[string Name]
		{
			get { return this.GetVar(Name); }
			set { this.SetVar(Name, Value); }
		}*/

		public TCalculator()
		{
			this.FVars = new List<TNamedVar>();
		}

		public void Dispose()
		{
			if (!this.Disposed_)
			{
				this.ClearVars();
				//this.FVars.Dispose();
				this.Disposed_ = true;
			}
		}

		public void ClearVars()
		{
			this.FVars.Clear();
		}

		private void RaiseError(string Msg)
		{
			throw new TCalculator.ECalculate(Msg);
		}

		private double bfloat(bool B)
		{
			return ((B) ? 1.0 : 0.0);
		}

		private double fmod(double x, double y)
		{
			return (x - Int((x / y)) * y);
		}

		public double Int(double AValue)
		{
			return ((AValue > (double)0f) ? Math.Floor(AValue) : Math.Ceiling(AValue));
		}

		private double Frac(double AValue)
		{
			return (AValue - Int(AValue));
		}

		private bool DefCalcProc(TCalcCBType ctype, [In] string S, ref double V)
		{
			bool Result = true;
			if (ctype != TCalcCBType.ctGetValue)
			{
				if (ctype != TCalcCBType.ctSetValue)
				{
					if (ctype == TCalcCBType.ctFunction)
					{
						if (S == "round")
						{
							V = ((double)checked((long)Math.Round(V)));
						}
						else
						{
							if (S == "trunc")
							{
								V = ((double)Math.Truncate(V));
							}
							else
							{
								if (S == "int")
								{
									V = Int(V);
								}
								else
								{
									if (S == "frac")
									{
										V = Frac(V);
									}
									else
									{
										if (S == "sin")
										{
											V = Math.Sin(V);
										}
										else
										{
											if (S == "cos")
											{
												V = Math.Cos(V);
											}
											else
											{
												if (S == "tan")
												{
													V = Math.Tan(V);
												}
												else
												{
													if (S == "atan")
													{
														V = Math.Atan(V);
													}
													else
													{
														if (S == "ln")
														{
															V = Math.Log(V);
														}
														else
														{
															if (S == "exp")
															{
																V = Math.Exp(V);
															}
															else
															{
																if (S == "sign")
																{
																	if (V > (double)0f)
																	{
																		V = 1.0;
																	}
																	else
																	{
																		if (V < (double)0f)
																		{
																			V = -1.0;
																		}
																	}
																}
																else
																{
																	if (S == "sgn")
																	{
																		if (V > (double)0f)
																		{
																			V = 1.0;
																		}
																		else
																		{
																			if (V < (double)0f)
																			{
																				V = 0.0;
																			}
																		}
																	}
																	else
																	{
																		if (S == "xsgn")
																		{
																			if (V < (double)0f)
																			{
																				V = 0.0;
																			}
																		}
																		else
																		{
																			Result = false;
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					Result = false;
				}
			}
			else
			{
				if (S == "pi")
				{
					V = 3.1415926535897931;
				}
				else
				{
					if (S == "e")
					{
						V = 2.718281828;
					}
					else
					{
						Result = false;
					}
				}
			}
			return Result;
		}

		private bool Callback(TCalcCBType ctype, [In] string Name, ref double Res)
		{
			bool Result = this.DefCalcProc(ctype, Name, ref Res);

			if (!Result)
			{
				Result = true;

				switch (ctype) {
					case TCalcCBType.ctGetValue:
						Res = this.GetVar(Name);
						break;
					case TCalcCBType.ctSetValue:
						this.SetVar(Name, Res);
						break;
					case TCalcCBType.ctFunction:
						Result = false;
						break;
				}
			}

			return Result;
		}

		private double GetVar([In] string Name)
		{
			int num = this.FVars.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				TNamedVar V = (this.FVars[i] as TNamedVar);

				if (string.Compare(V.Name, Name, false) == 0)
				{
					return V.Value;
				}
			}

			throw new TCalculator.ECalculate("Unknown function or variable \"" + Name + "\".");
		}

		private void SetVar([In] string Name, double Value)
		{
			TNamedVar V = null;

			int num = this.FVars.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				TNamedVar nv = (this.FVars[i] as TNamedVar);
				if (string.Compare(nv.Name, Name, false) == 0)
				{
					V = nv;
				}
			}

			if (V == null)
			{
				V = new TNamedVar();
				V.Name = Name;
				this.FVars.Add(V);
			}

			V.Value = Value;
		}

		private void lex()
		{
			while (this.FExpression[this.FPtr - 1] != '\0' && this.FExpression[this.FPtr - 1] <= ' ')
			{
				this.FPtr++;
			}
			this.FToken = TCalculator.TToken.tkEOF;
			if (this.FExpression[this.FPtr - 1] != '\0')
			{
				int s_pos = this.FPtr;
				this.FToken = TCalculator.TToken.tkNUMBER;
				if (this.FExpression[this.FPtr - 1] == '$')
				{
					this.FPtr++;
					while (true)
					{
						char c2 = this.FExpression[this.FPtr - 1];
						if (c2 < '0' || (c2 >= ':' && (c2 < 'A' || (c2 >= 'I' && (c2 < 'a' || c2 >= 'i')))))
						{
							break;
						}
						this.FPtr++;
					}
					if (this._lex_ConvertNumber(s_pos, this.FPtr, 16))
					{
						return;
					}
				}
				else
				{
					char c3 = this.FExpression[this.FPtr - 1];
					if (c3 >= '0' && c3 < ':')
					{
						if (this.FExpression[this.FPtr - 1] == '0')
						{
							this.FPtr++;
							char c4 = this.FExpression[this.FPtr - 1];
							if (c4 == 'X' || c4 == 'x')
							{
								this.FPtr++;
								s_pos = this.FPtr;
								while (true)
								{
									char c5 = this.FExpression[this.FPtr - 1];
									if (c5 < '0' || (c5 >= ':' && (c5 < 'A' || (c5 >= 'I' && (c5 < 'a' || c5 >= 'i')))))
									{
										break;
									}
									this.FPtr++;
								}
								if (this._lex_ConvertNumber(s_pos, this.FPtr, 16))
								{
									return;
								}
								goto Error;
							}
							else
							{
								char c6 = this.FExpression[this.FPtr - 1];
								if (c6 == 'B' || c6 == 'b')
								{
									this.FPtr++;
									s_pos = this.FPtr;
									while (true)
									{
										char c7 = this.FExpression[this.FPtr - 1];
										if (c7 < '0' || c7 >= '2')
										{
											break;
										}
										this.FPtr++;
									}
									if (this._lex_ConvertNumber(s_pos, this.FPtr, 2))
									{
										return;
									}
									goto Error;
								}
							}
						}
						while (true)
						{
							char c8 = this.FExpression[this.FPtr - 1];
							if (c8 < '0' || (c8 >= ':' && (c8 < 'A' || (c8 >= 'G' && (c8 < 'a' || c8 >= 'g')))))
							{
								break;
							}
							this.FPtr++;
						}
						char c9 = this.FExpression[this.FPtr - 1];
						if (c9 == 'H' || c9 == 'h')
						{
							if (this._lex_ConvertNumber(s_pos, this.FPtr, 16))
							{
								this.FPtr++;
								return;
							}
						}
						else
						{
							char c10 = this.FExpression[this.FPtr - 1];
							if (c10 == 'B' || c10 == 'b')
							{
								if (this._lex_ConvertNumber(s_pos, this.FPtr, 2))
								{
									this.FPtr++;
									return;
								}
							}
							else
							{
								if (this._lex_ConvertNumber(s_pos, this.FPtr, 10))
								{
									if (this.FExpression[this.FPtr - 1] == '`')
									{
										this.fvalue = (this.fvalue * 3.1415926535897931 / 180.0);
										this.FPtr++;
										double frac = 0.0;
										while (true)
										{
											char c11 = this.FExpression[this.FPtr - 1];
											if (c11 < '0' || c11 >= ':')
											{
												break;
											}
											frac = (frac * 10.0 + (double)((int)this.FExpression[this.FPtr - 1] - 48));
											this.FPtr++;
										}
										this.fvalue = (this.fvalue + frac * 3.1415926535897931 / 180.0 / 60.0);
										if (this.FExpression[this.FPtr - 1] == '`')
										{
											this.FPtr++;
											frac = 0.0;
											while (true)
											{
												char c12 = this.FExpression[this.FPtr - 1];
												if (c12 < '0' || c12 >= ':')
												{
													break;
												}
												frac = (frac * 10.0 + (double)((int)this.FExpression[this.FPtr - 1] - 48));
												this.FPtr++;
											}
											this.fvalue = (this.fvalue + frac * 3.1415926535897931 / 180.0 / 60.0 / 60.0);
										}
										this.fvalue = this.fmod(this.fvalue, 6.2831853071795862);
										return;
									}
									if (this.FExpression[this.FPtr - 1] == '.')
									{
										this.FPtr++;
										double frac = 1.0;
										while (true)
										{
											char c13 = this.FExpression[this.FPtr - 1];
											if (c13 < '0' || c13 >= ':')
											{
												break;
											}
											frac = (frac / 10.0);
											this.fvalue = (this.fvalue + frac * (double)((int)this.FExpression[this.FPtr - 1] - 48));
											this.FPtr++;
										}
									}
									char c14 = this.FExpression[this.FPtr - 1];
									if (c14 != 'E' && c14 != 'e')
									{
										return;
									}
									this.FPtr++;
									int exp = 0;
									char sign = this.FExpression[this.FPtr - 1];
									char c15 = this.FExpression[this.FPtr - 1];
									if (c15 == '+' || c15 == '-')
									{
										this.FPtr++;
									}
									char c16 = this.FExpression[this.FPtr - 1];
									if (c16 >= '0' && c16 < ':')
									{
										while (true)
										{
											char c17 = this.FExpression[this.FPtr - 1];
											if (c17 < '0' || c17 >= ':')
											{
												break;
											}
											exp = exp * 10 + (int)this.FExpression[this.FPtr - 1] - 48;
											this.FPtr++;
										}
										if (exp == 0)
										{
											this.fvalue = 1.0;
											return;
										}
										if (sign == '-')
										{
											if (exp > 0)
											{
												do
												{
													this.fvalue = (this.fvalue * 10.0);
													exp--;
												}
												while (exp > 0);
												return;
											}
											return;
										}
										else
										{
											if (exp > 0)
											{
												do
												{
													this.fvalue = (this.fvalue / 10.0);
													exp--;
												}
												while (exp > 0);
												return;
											}
											return;
										}
									}
								}
							}
						}
					}
					else
					{
						char c18 = this.FExpression[this.FPtr - 1];
						if (c18 >= 'A' && (c18 < '[' || c18 == '_' || (c18 >= 'a' && c18 < '{')))
						{
							this.svalue = new string(this.FExpression[this.FPtr - 1], 1);
							this.FPtr++;
							while (true)
							{
								char c19 = this.FExpression[this.FPtr - 1];
								if (c19 < '0' || (c19 >= ':' && (c19 < 'A' || (c19 >= '[' && c19 != '_' && (c19 < 'a' || c19 >= '{')))))
								{
									break;
								}
								string text = this.svalue;
								if (((text != null) ? text.Length : 0) >= 32)
								{
									break;
								}
								this.svalue += this.FExpression[this.FPtr - 1];
								this.FPtr++;
							}
							this.FToken = TCalculator.TToken.tkIDENT;
							return;
						}

						char c = this.FExpression[this.FPtr - 1];
						this.FPtr++;

						switch (c)
						{
							case '!': //chk
							{
								this.FToken = TCalculator.TToken.tkNOT;
								if (this.FExpression[this.FPtr - 1] == '=')
								{
									this.FPtr++;
									this.FToken = TCalculator.TToken.tkNE;
									return;
								}
								return;
							}
							case '%': //chk
							{
								this.FToken = TCalculator.TToken.tkMOD;
								if (this.FExpression[this.FPtr - 1] == '%')
								{
									this.FPtr++;
									this.FToken = TCalculator.TToken.tkPER;
									return;
								}
								return;
							}
							case '&': //chk
							{
								this.FToken = TCalculator.TToken.tkAND;
								return;
							}
							case '(': //chk
							{
								this.FToken = TCalculator.TToken.tkLBRACE;
								return;
							}
							case ')': //chk
							{
								this.FToken = TCalculator.TToken.tkRBRACE;
								return;
							}
							case '*': //chk
							{
								this.FToken = TCalculator.TToken.tkMUL;
								if (this.FExpression[this.FPtr - 1] == '*')
								{
									this.FPtr++;
									this.FToken = TCalculator.TToken.tkPOW;
									return;
								}
								return;
							}
							case '+': //chk
							{
								this.FToken = TCalculator.TToken.tkADD;
								return;
							}
							case '-': //chk
							{
								this.FToken = TCalculator.TToken.tkSUB;
								return;
							}
							case '/': //chk
							{
								this.FToken = TCalculator.TToken.tkDIV;
								return;
							}
							case ';': //chk
							{
								this.FToken = TCalculator.TToken.tkSEMICOLON;
								return;
							}
							case '<': //chk
							{
								this.FToken = TCalculator.TToken.tkLT;
								if (this.FExpression[this.FPtr - 1] == '=')
								{
									this.FPtr++;
									this.FToken = TCalculator.TToken.tkLE;
									return;
								}
								if (this.FExpression[this.FPtr - 1] == '>')
								{
									this.FPtr++;
									this.FToken = TCalculator.TToken.tkNE;
									return;
								}
								return;
							}
							case '=': //chk
							{
								this.FToken = TCalculator.TToken.tkASSIGN;
								if (this.FExpression[this.FPtr - 1] == '=')
								{
									this.FPtr++;
									this.FToken = TCalculator.TToken.tkEQ;
									return;
								}
								return;
							}
							case '>': //chk
							{
								this.FToken = TCalculator.TToken.tkGT;
								if (this.FExpression[this.FPtr - 1] == '=')
								{
									this.FPtr++;
									this.FToken = TCalculator.TToken.tkGE;
									return;
								}
								if (this.FExpression[this.FPtr - 1] == '<')
								{
									this.FPtr++;
									this.FToken = TCalculator.TToken.tkNE;
									return;
								}
								return;
							}
							case '^': //chk
							{
								this.FToken = TCalculator.TToken.tkXOR;
								return;
							}
							case '|': //chk
							{
								this.FToken = TCalculator.TToken.tkOR;
								return;
							}
							case '~': //chk
							{
								this.FToken = TCalculator.TToken.tkINV;
								return;
							}
							default: //chk
								{
									this.FToken = TCalculator.TToken.tkERROR;
									this.FPtr--;
									return;
								}
						}
					}
				}

				Error:
				this.FToken = TCalculator.TToken.tkERROR;
			}
		}

		private void term(ref double R)
		{
			TCalculator.TToken fToken = this.FToken;
			if (fToken != TCalculator.TToken.tkLBRACE)
			{
				if (fToken != TCalculator.TToken.tkNUMBER)
				{
					if (fToken != TCalculator.TToken.tkIDENT)
					{
						this.RaiseError("Syntax error.");
					}
					else
					{
						string st = this.svalue.ToLower();
						this.lex();
						if (this.FToken == TCalculator.TToken.tkLBRACE)
						{
							this.lex();
							this.expr6(ref R);
							if (this.FToken == TCalculator.TToken.tkRBRACE)
							{
								this.lex();
							}
							else
							{
								this.RaiseError("Syntax error");
							}
							if (!this.Callback(TCalculator.TCalcCBType.ctFunction, st, ref R))
							{
								this.RaiseError("Unknown function or variable \"" + st + "\".");
							}
						}
						else
						{
							if (this.FToken == TCalculator.TToken.tkASSIGN)
							{
								this.lex();
								this.expr6(ref R);
								if (!this.Callback(TCalculator.TCalcCBType.ctSetValue, st, ref R))
								{
									this.RaiseError("Unknown function or variable \"" + st + "\".");
								}
							}
							else
							{
								if (!this.Callback(TCalculator.TCalcCBType.ctGetValue, st, ref R))
								{
									this.RaiseError("Unknown function or variable \"" + st + "\".");
								}
							}
						}
					}
				}
				else
				{
					R = this.fvalue;
					this.lex();
				}
			}
			else
			{
				this.lex();
				this.expr6(ref R);
				if (this.FToken == TCalculator.TToken.tkRBRACE)
				{
					this.lex();
				}
				else
				{
					this.RaiseError("Syntax error");
				}
			}
		}

		private void expr1(ref double R)
		{
			this.term(ref R);
			if (this.FToken == TCalculator.TToken.tkPOW)
			{
				this.lex();
				double V = 0.0;
				this.term(ref V);
				R = Math.Pow(R, V);
			}
		}

		private void expr2(ref double R)
		{
			TToken fToken = this.FToken;
			if (fToken >= TToken.tkINV && (fToken < TToken.tkMUL || (fToken >= TToken.tkADD && fToken < TToken.tkLT)))
			{
				TToken oldt = this.FToken;
				this.lex();
				this.expr2(ref R);

				switch (oldt) {
					case TToken.tkINV:
						R = ((double)(~SysUtils.Trunc(R)));
						break;
					case TToken.tkNOT:
						R = this.bfloat(SysUtils.Trunc(R) <= 0);
						break;
					case TToken.tkSUB:
						R = (-R);
						break;
				}
			}
			else
			{
				this.expr1(ref R);
			}
		}

		private void expr3(ref double R)
		{
			this.expr2(ref R);
			while (true)
			{
				if (this.FToken < TToken.tkMUL || this.FToken >= TToken.tkADD) break;

				TToken oldt = this.FToken;
				this.lex();
				double V = 0.0;
				this.expr2(ref V);

				switch (oldt) {
					case TToken.tkMUL:
						R = (R * V);
						break;
					case TToken.tkDIV:
						R = (R / V);
						break;
					case TToken.tkMOD:
						R = ((double)(SysUtils.Trunc(R) % SysUtils.Trunc(V)));
						break;
					case TToken.tkPER:
						R = (R * V / 100.0);
						break;
				}
			}
		}

		private void expr4(ref double R)
		{
			this.expr3(ref R);
			while (true)
			{
				if (this.FToken < TToken.tkADD || this.FToken >= TToken.tkLT) break;

				TToken oldt = this.FToken;
				this.lex();
				double V = 0.0;
				this.expr3(ref V);

				switch (oldt) {
					case TToken.tkADD:
						R = (R + V);
						break;
					case TToken.tkSUB:
						R = (R - V);
						break;
				}
			}
		}

		private void expr5(ref double R)
		{
			this.expr4(ref R);
			while (true)
			{
				if (this.FToken < TToken.tkLT || this.FToken >= TToken.tkOR) break;

				TToken oldt = this.FToken;
				this.lex();
				double V = 0.0;
				this.expr4(ref V);

				switch (oldt) {
					case TToken.tkLT:
						R = this.bfloat(R < V);
						break;
					case TToken.tkLE:
						R = this.bfloat(R <= V);
						break;
					case TToken.tkEQ:
						R = this.bfloat(R == V);
						break;
					case TToken.tkNE:
						R = this.bfloat(R != V);
						break;
					case TToken.tkGE:
						R = this.bfloat(R >= V);
						break;
					case TToken.tkGT:
						R = this.bfloat(R > V);
						break;
				}
			}
		}

		private void expr6(ref double R)
		{
			this.expr5(ref R);
			while (true)
			{
				if (this.FToken < TToken.tkOR || this.FToken >= (TToken)26) break;

				TToken oldt = this.FToken;
				this.lex();
				double V = 0.0;
				this.expr5(ref V);

				switch (oldt) {
					case TToken.tkOR:
						R = ((double)(SysUtils.Trunc(R) | SysUtils.Trunc(V)));
						break;
					case TToken.tkXOR:
						R = ((double)(SysUtils.Trunc(R) ^ SysUtils.Trunc(V)));
						break;
					case TToken.tkAND:
						R = ((double)(SysUtils.Trunc(R) & SysUtils.Trunc(V)));
						break;
				}
			}
		}

		private void start(ref double R)
		{
			this.expr6(ref R);

			while (this.FToken == TToken.tkSEMICOLON)
			{
				this.lex();
				this.expr6(ref R);
			}

			if (this.FToken != TToken.tkEOF)
			{
				this.RaiseError("Syntax error");
			}
		}

		public double Calc(string aExpression)
		{
			this.FExpression = aExpression + "\0";
			this.FPtr = 1;
			this.lex();
			double Result = 0.0;
			this.start(ref Result);
			return Result;
		}

		private bool _lex_ConvertNumber(int first, int last, ushort @base)
		{
			this.fvalue = 0.0;
			if (first < last)
			{
				do
				{
					byte c = (byte)((int)this.FExpression[first - 1] - 48);
					if (c > 9)
					{
						c -= 7;
						if (c > 15)
						{
							c -= 32;
						}
					}
					if ((ushort)c >= @base)
					{
						break;
					}
					this.fvalue = (this.fvalue * @base + c);
					first++;
				}
				while (first < last);
			}
			return first == last;
		}
	}
}
