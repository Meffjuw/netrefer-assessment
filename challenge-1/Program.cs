using System;
using System.Collections.Generic;

public class Person
{
    public int Height { get; set; }
    public int Weight { get; set; }

    /// <summary>
    /// Returns true if the other Person can stack on top of the current Person.
    /// </summary>
    /// <param name="other">The other Person instance to check against.</param>
    /// <returns></returns>
    public bool CanStack(Person other)
    {
        return other.Height < Height && other.Weight < Weight;
    }

    /// <summary>
    /// Overridden to print details at the end.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Weight},{Height}";
    }
}

/// <summary>
/// Comparer class for sorting the Person Lists.
/// </summary>
public class PersonComparer : IComparer<Person>
{
    /// <summary>
    /// Attempts to compare two people by their weight, or by their height if weight is the same.
    /// </summary>
    /// <param name="a">A Person instance.</param>
    /// <param name="b">A Person instance.</param>
    /// <returns></returns>
    public int Compare(Person a, Person b)
    {
        if (a.Weight != b.Weight)
        {
            return a.Weight - b.Weight;
        }
        else
        {
            return a.Height - b.Height;
        }
    }
}

/// <summary>
/// This Program will calculate the maximum number of people that can satisfy the circus tower condition.
/// The concept is to first sort the List by one of the properties (i.e. Weight), so we only need to deal with the other variable (i.e. Height).
/// Once the List is sorted, apply the longest increasing subsequence approach to determine the largest result.
/// </summary>
class Program
{
    /// <summary>
    /// Attempts to determine the longest increasing subsequence for the people at this specific index.
    /// </summary>
    /// <param name="people">The sorted people List.</param>
    /// <param name="results">The current results so far.</param>
    /// <param name="index">The current index.</param>
    /// <returns></returns>
    private static List<Person> GetBestResultsForIndex(List<Person> people, List<List<Person>> results, int index)
    {
        // Get the current person to compare with.
        Person person = people[index];

        // Declare empty List.
        List<Person> currentBest = new List<Person>();

        for (int i = 0; i < index; i++)
        {
            // Get the current best solution previously calculated for the old indices.
            List<Person> result = results[i];

            // Get the last person from the array of the solution.
            Person previousPerson = result[^1];

            // Check if the new person satisfies the condition for being added at the bottom of the tower.
            if (result.Count == 0 || person.CanStack(previousPerson))
            {
                // If yes, check if the currently proposed solution for this index is larger than the previous result, and set accordingly.
                currentBest = result.Count > currentBest.Count ? result : currentBest;
            }
        }

        // Clone the current result, and add the new person.
        List<Person> temp = new List<Person>(currentBest)
        {
            person
        };

        return temp;
    }

    static void Main(string[] args)
    {
        // Initialise the List of people.
        List<Person> people = new List<Person>
        {
            new Person
            {
                Height = 120,
                Weight = 64
            },
            new Person
            {
                Height = 100,
                Weight = 65
            },
            new Person
            {
                Height = 150,
                Weight = 70
            },
            new Person
            {
                Height = 90,
                Weight = 56
            },
            new Person
            {
                Height = 190,
                Weight = 75
            },
            new Person
            {
                Height = 95,
                Weight = 60
            },
            new Person
            {
                Height = 110,
                Weight = 68
            }
        };

        // Sort the List using the PersonComparer.
        people.Sort(new PersonComparer());

        // Declare empty answer List.
        List<Person> answer = new List<Person>();

        // Store all the possible results in a 2D List of Lists.
        List<List<Person>> results = new List<List<Person>>();

        // Attempt to find the longest possible solution by utilizing the person at a given index.
        for (int i = 0; i < people.Count; i++)
        {
            List<Person> result = GetBestResultsForIndex(people, results, i);

            // Add this result to the list of other results.
            results.Add(result);

            // Compare the new result to the current best contender, and set accordingly.
            answer = result.Count > answer.Count ? result : answer;
        }

        // Loop through the List and print the Person details.
        foreach (Person person in answer)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine($"\nThe tallest tower would include {answer.Count} people.");
    }
}

