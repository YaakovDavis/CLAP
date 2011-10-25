﻿using System;
using System.IO;
using System.Reflection;

namespace CLAP.Validation
{
    /// <summary>
    /// Directory exists validation
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public sealed class DirectoryExistsAttribute : ValidationAttribute
    {
        public DirectoryExistsAttribute()
        {

        }

        public override IValueValidator GetValidator()
        {
            return new DirectoryExistsValidator();
        }

        public override string Description
        {
            get
            {
                return "Directory exists";
            }
        }

        private class DirectoryExistsValidator : IValueValidator
        {
            public void Validate(ValueInfo info)
            {
                string path = string.Empty;

                if (info.Value is Uri)
                {
                    path = ((Uri)info.Value).LocalPath;
                }
                else
                {
                    path = Environment.ExpandEnvironmentVariables((string)info.Value);
                }

                if (!Directory.Exists(path))
                {
                    throw new ValidationException("{0}: '{1}' is not an existing directory".FormatWith(
                        info.Name,
                        path));
                }
            }
        }
    }
}