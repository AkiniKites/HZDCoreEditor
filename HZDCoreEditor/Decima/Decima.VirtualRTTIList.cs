﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Decima
{
    static partial class RTTI
    {
        public class VirtualRTTIList
        {
            public Type ClassType { get; private set; }
            public IReadOnlyList<OrderedFieldInfo.Entry> ResolvedMembers { get { return _ResolvedMembers.AsReadOnly(); } }

            private readonly List<Entry> Members;
            private readonly List<OrderedFieldInfo.Entry> _ResolvedMembers;

            public struct Entry
            {
                public string Type;
                public string Category;
                public string Name;
            }

            public VirtualRTTIList(string className, int capacity = 0)
            {
                ClassType = GetTypeByName(className);
                Members = new List<Entry>(capacity);
                _ResolvedMembers = new List<OrderedFieldInfo.Entry>();
            }

            public void Add(string type, string category, string name)
            {
                Members.Add(new Entry
                {
                    Type = type,
                    Category = category,
                    Name = name,
                });
            }

            public void ResolveMembersToFieldInfo()
            {
                var info = GetOrderedFieldsForClass(ClassType);

                foreach (var virtualMember in Members)
                {
                    var resolvedMember = info.Members
                        .Where(x => MatchField(x.Field, virtualMember.Type, virtualMember.Category, virtualMember.Name))
                        .Single();

                    _ResolvedMembers.Add(resolvedMember);
                }
            }

            private static bool MatchField(FieldInfo field, string type, string category, string name)
            {
                if (GetFieldCategory(field) != category)
                    return false;

                if (GetFieldName(field) != name)
                    return false;

                string ftn = GetTypeNameString(field.FieldType);

                // TODO: Custom int32 type - C# doesn't support typedefs. I can pretend this isn't a problem until I need
                // to write fields.
                if (ftn != "int" && type != "int32")
                {
                    if (!ftn.Equals(type, StringComparison.OrdinalIgnoreCase))
                        return false;
                }

                return true;
            }
        }
    }
}
