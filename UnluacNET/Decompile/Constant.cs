﻿// Copyright (c) 2020-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: see LICENSE for more details.

namespace Elskom.Generic.Libs.UnluacNET
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Constant
    {
        public const int CONST_NIL = 0;
        public const int CONST_BOOL = 1;
        public const int CONST_NUMBER = 2;
        public const int CONST_STRING = 3;
        private static readonly HashSet<string> m_reservedWords =
            new()
            {
                "and",
                "and",
                "break",
                "do",
                "else",
                "elseif",
                "end",
                "false",
                "for",
                "function",
                "if",
                "in",
                "local",
                "nil",
                "not",
                "or",
                "repeat",
                "return",
                "then",
                "true",
                "until",
                "while",
            };

        private readonly int m_type;
        private readonly bool m_bool;
        private readonly LNumber m_number;
        private readonly string m_string;

        public Constant(int constant)
        {
            this.m_type = 2;
            this.m_bool = false;
            this.m_number = LNumber.MakeInteger(constant);
            this.m_string = null;
        }

        public Constant(LObject constant)
        {
            if (constant is LNil)
            {
                this.m_type = 0;
                this.m_bool = false;
                this.m_number = null;
                this.m_string = null;
            }
            else if (constant is LBoolean lboolean)
            {
                this.m_type = 1;
                this.m_bool = lboolean == LBoolean.LTRUE;
                this.m_number = null;
                this.m_string = null;
            }
            else if (constant is LNumber number)
            {
                this.m_type = 2;
                this.m_bool = false;
                this.m_number = number;
                this.m_string = null;
            }
            else if (constant is LString lstring)
            {
                this.m_type = 3;
                this.m_bool = false;
                this.m_number = null;
                this.m_string = lstring.DeRef();
            }
            else
            {
                throw new ArgumentException("Illegal constant type: " + constant.ToString());
            }
        }

        public bool IsBoolean => this.m_type == CONST_BOOL;

        public bool IsIdentifier
        {
            get
            {
                if (!this.IsString || this.m_string.Length == 0 || m_reservedWords.Contains(this.m_string))
                {
                    return false;
                }

                var start = this.m_string[0];
                if (start != '_' && !char.IsLetter(start))
                {
                    return false;
                }

                for (var i = 1; i < this.m_string.Length; i++)
                {
                    var next = this.m_string[i];
                    if (char.IsLetterOrDigit(next) || next == '_')
                    {
                        continue;
                    }

                    return false;
                }

                return true;
            }
        }

        public bool IsInteger
        {
            get
            {
                var value = this.m_number.Value;
                return value == Math.Round(value);
            }
        }

        public bool IsNil => this.m_type == CONST_NIL;

        public bool IsNumber => this.m_type == CONST_NUMBER;

        public bool IsString => this.m_type == CONST_STRING;

        public int AsInteger()
            => !this.IsInteger ? throw new InvalidOperationException() : (int)this.m_number.Value;

        public string AsName()
            => !this.IsString ? throw new InvalidOperationException() : this.m_string;

        public void Print(Output output)
        {
            switch (this.m_type)
            {
                case CONST_NIL:
                {
                    output.Print("nil");
                    break;
                }

                case CONST_BOOL:
                {
                    output.Print(this.m_bool ? "true" : "false");
                    break;
                }

                case CONST_NUMBER:
                {
                    output.Print(this.m_number.ToString());
                    break;
                }

                case CONST_STRING:
                {
                    var newLines = 0;
                    var unprinttable = 0;
                    foreach (var c in this.m_string)
                    {
                        if (c == '\n')
                        {
                            newLines++;
                        }
                        else if ((c <= 31 && c != '\t') || c >= 127)
                        {
                            unprinttable++;
                        }
                    }

                    if (unprinttable == 0 && !this.m_string.Contains("[[") &&
                        (newLines > 1 || newLines == 1) && this.m_string.IndexOf('\n') != this.m_string.Length - 1)
                    {
                        var pipe = 0;
                        var pipeString = new StringBuilder();
                        pipeString.Append("]]");
#if !NETFRAMEWORK && !NETCOREAPP2_0
                        while (this.m_string.Contains(pipeString.ToString(), StringComparison.InvariantCulture))
#else
                        while (this.m_string.Contains(pipeString.ToString()))
#endif
                        {
                            pipe++;
                            pipeString.Clear();
                            pipeString.Append(']');
                            var i = pipe;
                            while (i-- > 0)
                            {
                                pipeString.Append('=');
                            }

                            pipeString.Append(']');
                        }

                        output.Print("[");
                        while (pipe-- > 0)
                        {
                            output.Print("=");
                        }

                        output.Print("[");
                        var indent = output.IndentationLevel;
                        output.IndentationLevel = 0;
                        output.PrintLine();
                        output.Print(this.m_string);
                        output.Print(pipeString.ToString());
                        output.IndentationLevel = indent;
                    }
                    else
                    {
                        output.Print("\"");
                        var chars = new[]
                        {
                            "\\a",
                            "\\b",
                            "\\t",
                            "\\n",
                            "\\v",
                            "\\f",
                            "\\r",
                        };
                        foreach (var c in this.m_string)
                        {
                            if (c <= 31 || c >= 127)
                            {
                                var cx = (int)c;
                                if (cx >= 7 && cx <= 13)
                                {
                                    output.Print(chars[cx - 7]);
                                }
                                else
                                {
                                    var dec = cx.ToString();
                                    var len = dec.Length;
                                    output.Print("\\");
                                    while (len++ < 3)
                                    {
                                        output.Print("0");
                                    }

                                    output.Print(dec);
                                }
                            }
                            else if (c == 34)
                            {
                                output.Print("\\\"");
                            }
                            else if (c == 92)
                            {
                                output.Print("\\\\");
                            }
                            else
                            {
                                output.Print(c.ToString());
                            }
                        }

                        output.Print("\"");
                    }

                    break;
                }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
