using System;

namespace UkrainianHashTable
{
    class Program
    {
        const int DEFAULT_HASH_TABLE_SIZE = 11;
        static Node[] hashTable;
        static int hashTableSize;

        // Вузол для списку
        class Node
        {
            public string data;
            public Node next;
        }

        // Хеш-функція
        static int myhash(string data)
        {
            int hash = 0;
            foreach (char c in data)
                hash = (31 * hash + c) % hashTableSize;
            return hash;
        }

        // Додавання слова
        static void insertNode(string data)
        {
            int bucket = myhash(data);
            Node p = new Node();
            p.data = data;
            p.next = hashTable[bucket];
            hashTable[bucket] = p;
        }

        // Основна програма
        static void Main(string[] args)
        {
            string[] words = {
                "вiкно", "вишня", "горiх", "дуб", "зiрка", "лампа",
                "лiто", "мрiя", "нiч", "осiнь", "пiсня", "ручка",
                "сад", "свiтло", "сонце", "трава", "усмiшка", "хмара"
            };

            Console.Write("Введiть розмiр хеш-таблицi: ");
            if (!int.TryParse(Console.ReadLine(), out hashTableSize) || hashTableSize <= 0)
            {
                Console.WriteLine("Невiрне значення. Використано стандартне: " + DEFAULT_HASH_TABLE_SIZE);
                hashTableSize = DEFAULT_HASH_TABLE_SIZE;
            }

            // Ініціалізація
            hashTable = new Node[hashTableSize];

            foreach (string word in words)
                insertNode(word);

            // Виведення таблиці
            Console.WriteLine("\nВмiст хеш-таблицi:");
            for (int i = 0; i < hashTableSize; i++)
            {
                Console.Write($"{i}: ");
                Node temp = hashTable[i];
                while (temp != null)
                {
                    Console.Write($"{temp.data} -> ");
                    temp = temp.next;
                }
                Console.WriteLine("null");
            }

            // Аналіз
            int maxChain = 0, minChain = int.MaxValue, empty = 0;

            for (int i = 0; i < hashTableSize; i++)
            {
                int count = 0;
                Node temp = hashTable[i];
                while (temp != null)
                {
                    count++;
                    temp = temp.next;
                }

                if (count == 0) empty++;
                if (count > maxChain) maxChain = count;
                if (count > 0 && count < minChain) minChain = count;
            }

            Console.WriteLine($"\nМакс. довжина ланцюжка: {maxChain}");
            Console.WriteLine($"Мiн. довжина (ненульова): {minChain}");
            Console.WriteLine($"Кiлькiсть порожнiх комiрок: {empty}");

            Console.ReadKey();
        }
    }
}
