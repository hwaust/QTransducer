using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QTrans.Company.Y2017
{
    class JNode
    {

        public JNode parent;

        public string name;

        public object value;

        private bool closeList = false;

        /// <summary>
        /// the type of variable value.
        /// </summary>
        public JValueType vType = JValueType.Undefined;

        public List<JNode> nodes = new List<JNode>();

        public String getNode(string key)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].name == key)
                {
                    if (nodes[i].vType == JValueType.String || nodes[i].vType == JValueType.Numerial)
                        return nodes[i].value.ToString();
                }
            }
            return null;
        }

        public JNode() { }

        public JNode(string v)
        {
            name = v;
        }


        public static JNode read(String path)
        {
            return parse(System.IO.File.ReadAllText(path));
        }

        public static JNode parse(string data)
        {
            JNode cur = new JNode("root");
            Stack<JNode> stack = new Stack<JNode>();

            for (int i = 0; i < data.Length; i++)
            {
                char c = data[i];

                // remove white space.
                if (isWhiteSpace(c))
                    continue;

                int start = i;

                // process digit number.
                if (char.IsDigit(c) || c == '-')
                {
                    start = i;
                    if (c == '-') i++;
                    while (char.IsDigit(data[i]) || data[i] == '.')
                        i++;

                    cur.setDouble(double.Parse(data.Substring(start, i - start)));

                    i--;
                    continue;
                }


                switch (c)
                {
                    case '{':
                        if (cur.vType == JValueType.NodeList)
                        {
                            if (!cur.closeList)
                            {
                                JNode n = new JNode();
                                n.vType = JValueType.JNode;
                                n.add(new JNode());
                                cur.addNodetoList(n);
                                stack.Push(cur);
                                cur = n.first();
                            }
                        }
                        else if (cur.name != null)
                        {
                            JNode n = new JNode();
                            n.vType = JValueType.JNode;
                            n.add(new JNode());
                            //cur.addNodetoList(n);
                            //stack.Push(cur);
                            cur.value = n;
                            stack.Push(cur);
                            cur = n.first();
                        }
                        break;


                    case '\"':
                        start = i + 1;
                        while (data[++i] != '\"') ;
                        string str = data.Substring(start, i - start);

                        if (cur.vType == JValueType.NodeList)
                        {
                            cur.addStringToList(str);
                        }
                        else if (cur.name == null)
                        {
                            cur.name = str;
                        }
                        else
                        {
                            cur.setString(str);
                        }
                        break;


                    case ':':
                        break;

                    case ',':
                        if (cur.vType == JValueType.NodeList && !cur.closeList)
                            continue;

                        else if (cur.parent != null)
                        {
                            cur.parent.add(new JNode());
                            cur = cur.parent.last();
                        }
                        break;

                    case '}':

                        if (stack.Count == 0)
                        {
                            cur = cur.parent;
                        }
                        else
                        {
                            JNode tmp = stack.Pop();
                            if (cur.parent != null)
                            {
                                cur = tmp;
                            }
                            else if (tmp.vType == JValueType.NodeList)
                            {
                                tmp.addNodetoList(cur);
                            }
                            else if (tmp.vType == JValueType.JNode)
                            {
                                tmp.value = cur;
                            }
                            else
                            {
                                cur = tmp;
                            }
                        }
                        break;

                    case '[':
                        cur.setNewList();
                        break;

                    case ']':
                        if (cur.vType == JValueType.NodeList)
                        {
                            if (!cur.closeList)
                                cur.closeList = true;
                        }
                        break;
                }


            }

            return cur.value as JNode;
        }

        public JNode get(string key)
        {
            foreach (JNode node in nodes)
            {
                if (node.name == key)
                {
                    return node;
                }
            }

            return null;
        }

        private void addNodetoList(JNode node)
        {
            if (value == null)
            {
                value = new List<JNode>();
                vType = JValueType.NodeList;
            }
            (this.value as List<JNode>).Add(node);
        }

        private void addStringToList(string str)
        {
            JNode node = new JNode();
            node.value = str;
            (this.value as List<JNode>).Add(node);
        }

        private void setNewList()
        {
            value = new List<JNode>();
            vType = JValueType.NodeList;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (vType == JValueType.NodeList)
            {
                List<JNode> nodelist = (List<JNode>)value;
                sb.Append(name + " : " + '[');
                foreach (JNode n in nodelist)
                {
                    sb.Append("{" + n.ToString() + "}, ");
                }
                sb.Append(']');
            }
            else if (vType == JValueType.JNode)
            {
                sb.Append(name);
                if (value != null)
                {
                    sb.Append(" : {" + value + "}");
                }
                else
                {
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        sb.Append(nodes[i].ToString());
                    }
                }
            }
            else
                sb.Append(string.Format("\"{0}\" : {1}", name, value));

            return sb.ToString();
        }

        public static void showAll(JNode node)
        {
            Console.WriteLine(node.ToString());
            for (int i = 0; i < node.nodes.Count; i++)
            {
                showAll(node.nodes[i]);
            }
        }



        private JNode first()
        {
            return nodes.Count > 0 ? nodes[0] : null;
        }

        private JNode last()
        {
            return nodes.Count > 0 ? nodes[nodes.Count - 1] : null;
        }

        private void add(JNode node)
        {
            this.nodes.Add(node);
            node.parent = this;
        }

        /// <summary>
        ///  set a double to value
        /// </summary>
        /// <param name="v"></param>
        private void setDouble(double v)
        {
            if (vType == JValueType.NodeList)
            {
                JNode node = new JNode();
                node.value = v;
                node.vType = 0;
                (this.value as List<JNode>).Add(node);
            }
            else
            {
                value = v;
                vType = 0;
            }
        }

        /// <summary>
        /// if value is of type JNode List, return count(list)
        /// </summary>
        /// <returns></returns>
        public int getListCount()
        {
            return value is List<JNode> ? (value as List<JNode>).Count : -1;
        }

        /// <summary>
        ///  if value is of type JNode List, return the p-th element.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public JNode getListNode(int p)
        {
            return (p >= 0 && p < getListCount()) ? (value as List<JNode>)[p] : null;
        }

        public JNode getListNode(string key)
        {
            List<JNode> listnodes = value as List<JNode>;
            if (listnodes == null)
                return null;

            foreach (JNode node in listnodes)
            {
                if (node.name == key)
                    return node;
            }

            return null;
        }

        private void setString(string v)
        {
            value = v;
            vType = JValueType.String;
        }

        public static bool isWhiteSpace(char c)
        {
            return c == ' ' || c == '\r' || c == '\n' || c == '\t';
        }


    }
}
