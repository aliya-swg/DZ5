using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadachi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
            
        }
        static void Task1()
        { 
        }
        static void Task2()
        {

        }
        static void Task3()
        {

        }
        static void Task4()
        {
            // Граф (словарь: вершина -> список смежных вершин)
            var graph = new Dictionary<string, List<string>>()
            {
                {"A", new List<string>(){"B", "C"}},
                {"B", new List<string>(){"A", "D", "E"}},
                {"C", new List<string>(){"A", "F"}},
                {"D", new List<string>(){"B"}},
                {"E", new List<string>(){"B", "F"}},
                {"F", new List<string>(){"C", "E"}}
            };

            string startNode = "A";
            string endNode = "F";

            Console.WriteLine("Обход в глубину (DFS):");
            if (TryFindPath(graph, startNode, endNode, out var dfsPath, SearchType.DFS))
            {
                PrintPath(dfsPath);
            }
            else
            {
                Console.WriteLine("Путь не найден.");
            }


            Console.WriteLine("\nОбход в ширину (BFS):");
            if (TryFindPath(graph, startNode, endNode, out var bfsPath, SearchType.BFS))
            {
                PrintPath(bfsPath);
            }
            else
            {
                Console.WriteLine("Путь не найден.");
            }
        }

        enum SearchType { DFS, BFS }
        static bool TryFindPath(Dictionary<string, List<string>> graph, string start, string end, out List<string> path, SearchType searchType)
        {
            path = new List<string>();
            var visited = new HashSet<string>();

            if (searchType == SearchType.DFS)
            {
                var stack = new Stack<string>();
                stack.Push(start);

                while (stack.Count > 0)
                {
                    string node = stack.Pop();
                    if (!visited.Contains(node))
                    {
                        visited.Add(node);
                        path.Add(node);

                        if (node == end) return true;

                        // Используем TryGetValue
                        if (graph.TryGetValue(node, out var neighbors))
                        {
                            foreach (string neighbor in neighbors.Where(n => !visited.Contains(n)))
                            {
                                stack.Push(neighbor);
                            }
                        }
                    }
                }
            }
            else // BFS
            {
                var queue = new Queue<List<string>>();
                queue.Enqueue(new List<string> { start });

                while (queue.Count > 0)
                {
                    path = queue.Dequeue();
                    string lastNode = path.Last();
                    visited.Add(lastNode);

                    if (lastNode == end) return true;

                    //  Используем TryGetValue
                    if (graph.TryGetValue(lastNode, out var neighbors))
                    {
                        foreach (string neighbor in neighbors.Where(n => !visited.Contains(n)))
                        {
                            var newPath = new List<string>(path) { neighbor };
                            queue.Enqueue(newPath);
                        }
                    }
                }
            }
            return false;
        }

        static void PrintPath(List<string> path)
        {
            Console.WriteLine(string.Join(" -> ", path));
        }

    }
    
}
