//using System;
//using System.Linq.Expressions;
//using System.Reflection;
//using System.Linq;
//using System.Collections.Generic;
//using ESS.FW.Common.Utilities;
//using ESS.FW.DataAccess.EF.GraphDiff.Internal.Graph;

//namespace ESS.FW.DataAccess.EF.GraphDiff.Internal
//{
//    /// <summary>
//    /// Reads an IUpdateConfiguration mapping and produces an GraphNode graph.
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    internal class StringConfigurationVisitor<T>
//    {
//        private string _currentMethod = "OwnedCollection";
//        private BindingFlags _bindingAttr = BindingFlags.Public | BindingFlags.Instance;

//        private GraphNode GetGraphNode(GraphNode parent, string includeString)
//        {
//            if (parent == null || parent.Members == null || !parent.Members.Any())
//                return null;

//            foreach (var node in parent.Members)
//            {
//                if (node.IncludeString == includeString)
//                    return node;

//                var childNode = GetGraphNode(node, includeString);

//                if (childNode != null)
//                    return childNode;
//            }

//            return null;
//        }

//        private void AddGraphNode(GraphNode parent, string name)
//        {
//            var childProperty = TypeUtils.GetPureType(parent.Accessor.PropertyType).GetProperty(name, _bindingAttr);

//            var childNote = CreateNewMember(parent, childProperty);

//            parent.Members.Push(childNote);
//        }

//        public GraphNode GetNodes(string[] members)
//        {
//            //eg: "PurPriceDets|PurPriceDets.GuidePriceDets"
//            //members: new string[]{ "PurPriceDets", "PurPriceDets.GuidePriceDets",
//            //                       "PurPriceDets2", "PurPriceDets2.GuidePriceDets" }

//            var detailMembers = members.Where(c => !c.Contains('.'));//����������Ӧ����ϸ���ԣ���������"."��

//            if (!detailMembers.Any())
//                throw new ApplicationException("û��������ϸ����");

//            GraphNode root = new GraphNode();

//            foreach (var detailMember in detailMembers)
//            {
//                //1.���������Ӧ����ϸ��Members
//                PropertyInfo detailProperty = typeof(T).GetProperty(detailMember, _bindingAttr);
//                var detailNote = CreateNewMember(root, detailProperty);
//                root.Members.Push(detailNote);

//                //2.�Ƿ�����ϸ��Ӧ���Ӷ��󣬼�����ϸ����ϸ��
//                var childMembers = members.Where(c => c.StartsWith(detailMember + "."));

//                if (childMembers.Any())
//                {
//                    foreach (var childMember in childMembers)
//                    {
//                        var childPropertyNames = childMember.Split('.');

//                        string parentIncludeString = childPropertyNames[0];//�ϼ��ڵ㣬ȡ����ĵ�һ��Ԫ��ԭ����BillDefine���IncludeChild�ֶ�ָ��

//                        for (int i = 1; i < childPropertyNames.Length; i++)
//                        {
//                            var parent = GetGraphNode(root, parentIncludeString);//��ȡ�ϼ��ڵ�

//                            if (parent == null)
//                                throw new ApplicationException("���ڵ�δ�ҵ�");

//                            var currentIncludeString = parentIncludeString + $".{childPropertyNames[i]}";

//                            var current = GetGraphNode(parent, currentIncludeString);//��鵱ǰ�ڵ��Ƿ��Ѵ���

//                            if (current == null)
//                            {
//                                AddGraphNode(parent, childPropertyNames[i]);
//                            }

//                            parentIncludeString = currentIncludeString;
//                        }
//                    }
//                }
//            }

//            return root;
//        }

//        //protected void VisitMember(string memberExpression)
//        //{
//        //    var accessor = GetMemberAccessor(memberExpression);
//        //    var newMember = CreateNewMember(accessor);

//        //    _currentMember.Members.Push(newMember);
//        //    //_currentMember = newMember;

//        //}

//        private GraphNode CreateNewMember(GraphNode parent, PropertyInfo accessor)
//        {
//            GraphNode newMember;
//            switch (_currentMethod)
//            {
//                case "OwnedEntity":
//                    newMember = new OwnedEntityGraphNode(parent, accessor);
//                    break;
//                case "AssociatedEntity":
//                    newMember = new AssociatedEntityGraphNode(parent, accessor);
//                    break;
//                case "OwnedCollection":
//                    newMember = new CollectionGraphNode(parent, accessor, true);
//                    break;
//                case "AssociatedCollection":
//                    newMember = new CollectionGraphNode(parent, accessor, false);
//                    break;
//                default:
//                    throw new NotSupportedException("The method used in the update mapping is not supported");
//            }
//            return newMember;
//        }

//        //private static PropertyInfo GetMemberAccessor(string propertyName)
//        //{
//        //    PropertyInfo accessor = null;

//        //    var propertyNames = propertyName.Split('.');

//        //    foreach (var name in propertyNames)
//        //    {
//        //        if (accessor == null)//�ڶ���(��ϸ)
//        //            accessor = typeof(T).GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
//        //        else
//        //            accessor = TypeUtils.GetPureType(accessor.PropertyType).GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
//        //    }

//        //    if (accessor == null)
//        //        throw new NotSupportedException("Unknown accessor type found!");

//        //    return accessor;
//        //}
//    }
//}