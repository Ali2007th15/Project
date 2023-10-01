using System;

Vocabulary program = new();
program.start();
class Vocabulary
{
    private Dictionary<string, Dictionary<string, List<string>>> dictionaries;
    private string current;
    private bool num;

    public Vocabulary()
    {
        dictionaries = new();
        current = null;
        num = true;
    }

    public void start()
    {
        while (num)
        {
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Create a dictionary");
            Console.WriteLine("2. Select a dictionary");
            Console.WriteLine("3. Add a word and translation");
            Console.WriteLine("4. Replace a word or translation");
            Console.WriteLine("5. Delete a word or translation");
            Console.WriteLine("6. Find a translation");
            Console.WriteLine("7. Export the dictionary");
            Console.WriteLine("8. Exit the application");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Create();
                    break;
                case "2":
                    Select();
                    break;
                case "3":
                    AddWord();
                    break;
                case "4":
                    ReplaceWord();
                    break;
                case "5":
                    DeleteWord();
                    break;
                case "6":
                    Search();
                    break;
                case "7":
                    SaveInFile();
                    break;
                case "8":
                    num = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    private void Create()
    {
        Console.WriteLine("Enter the name of the new dictionary:");
        string name = Console.ReadLine();

        if (!dictionaries.ContainsKey(name))
        {
            dictionaries[name] = new Dictionary<string, List<string>>();
            Console.WriteLine($"Dictionary {name} created");
        }
        else
        {
            Console.WriteLine($"Dictionary with the name {name} already exists");
        }
    }

    private void Select()
    {
        Console.WriteLine("Choose a dictionary:");
        int i = 1;

        foreach (var name in dictionaries.Keys)
        {
            Console.WriteLine($"{i}. {name}");
            i++;
        }

        int select = int.Parse(Console.ReadLine()) - 1;
        var names = new List<string>(dictionaries.Keys);

        if (select >= 0 && select < names.Count)
        {
            current = names[select];
            Console.WriteLine($"Selected dictionary: {current}");
        }
        else
        {
            Console.WriteLine("Invalid dictionary selection");
        }
    }

    private void AddWord()
    {
        if (current == null)
        {
            Console.WriteLine("Please select a dictionary");
            return;
        }

        Console.WriteLine("Enter a word:");
        string word = Console.ReadLine().ToLower();

        if (!dictionaries[current].ContainsKey(word))
        {
            dictionaries[current][word] = new List<string>();
        }

        Console.WriteLine("Enter a translation (or 'D' to finish):");

        while (true)
        {
            string translation = Console.ReadLine().ToUpper();

            if (translation == "D")
                break;

            dictionaries[current][word].Add(translation);
        }

        Console.WriteLine("Word and translations added");
    }

    private void ReplaceWord()
    {
        if (current == null)
        {
            Console.WriteLine("Please select a dictionary");
            return;
        }

        Console.WriteLine("Enter the word you want to replace:");
        string replace = Console.ReadLine().ToLower();

        if (dictionaries[current].ContainsKey(replace))
        {
            Console.WriteLine("Enter the new word:");
            string word2 = Console.ReadLine().ToLower();

            dictionaries[current][word2] = new List<string>(dictionaries[current][replace]);
            dictionaries[current].Remove(replace);

            Console.WriteLine($"Word {replace} replaced with {word2} in the dictionary");
        }
        else
        {
            Console.WriteLine($"Word {replace} not found in the dictionary");
        }
    }

    private void DeleteWord()
    {
        if (current == null)
        {
            Console.WriteLine("Please select a dictionary");
            return;
        }

        Console.WriteLine("Enter the word you want to delete:");
        string delete = Console.ReadLine().ToLower();

        if (dictionaries[current].ContainsKey(delete))
        {
            dictionaries[current].Remove(delete);
            Console.WriteLine($"Word {delete} deleted from the dictionary");
        }
        else
        {
            Console.WriteLine($"Word {delete} not found in the dictionary");
        }
    }

    private void Search()
    {
        if (current == null)
        {
            Console.WriteLine("Please select a dictionary");
            return;
        }

        Console.WriteLine("Enter a word to search for a translation:");
        string search = Console.ReadLine().ToLower();

        if (dictionaries[current].ContainsKey(search))
        {
            Console.WriteLine($"Translations of the word {search}:");

            foreach (var translation in dictionaries[current][search])
            {
                Console.WriteLine(translation);
            }
        }
        else
        {
            Console.WriteLine($"Word {search} not found in the dictionary");
        }
    }

    private void SaveInFile()
    {
        if (current == null)
        {
            Console.WriteLine("Please select a dictionary");
            return;
        }

        Console.WriteLine("Enter the name of file:");
        string file = Console.ReadLine();

        try
        {
            using (FileStream fs = new(file, FileMode.Create))
            using (StreamWriter sw = new(fs))
            {
                var dictionary = dictionaries[current];

                foreach (var note in dictionary)
                {
                    string word = note.Key;
                    List<string> translations = note.Value;

                    sw.WriteLine($"Word: {word}");
                    sw.WriteLine("Translations:");

                    foreach (var translation in translations)
                    {
                        sw.WriteLine($" {translation}");
                    }

                    sw.WriteLine();
                }

                Console.WriteLine($"Dictionary {current} saved to the file {file}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}
