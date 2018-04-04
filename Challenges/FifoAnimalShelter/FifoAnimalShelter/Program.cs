﻿using System;
using StackAndQueue;

namespace FifoAnimalShelter
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// Enqueues an Animal object of type Cat or type Dog onto the appropriate
        /// queue, assigns that Animal object a serial number, and increments the
        /// lastSerialNumber through a ref parameter
        /// </summary>
        /// <param name="catQueue">Queue of Cat objects in the animal shelter</param>
        /// <param name="dogQueue">Queue of Dog objects in the animal shelter</param>
        /// <param name="lastSerialNumber">The last assigned serial number</param>
        /// <param name="newAnimal">The new Animal object of type Cat or type
        /// Dog</param>
        /// <exception cref="ArgumentException"><paramref name="newAnimal"/> was not
        /// of type Cat or type Dog</exception>
        public static void Enqueue(MyQueue<Cat> catQueue, MyQueue<Dog> dogQueue,
            ref long lastSerialNumber, Animal newAnimal)
        {
            newAnimal.SerialNumber = lastSerialNumber++;

            // Enqueue to the correct queue depending on the type of the Animal
            if (newAnimal is Dog)
            {
                dogQueue.Enqueue((Dog)newAnimal);
            }
            else if (newAnimal is Cat)
            {
                catQueue.Enqueue((Cat)newAnimal);
            }
            else
            {
                // I'm not too crazy about calling dogs and cats objects, but such is software
                throw new ArgumentException(
                    "This animal shelter only takes Dog and Cat objects", nameof(newAnimal));
            }
        }

        /// <summary>
        /// If <paramref name="preferredAnimalType"/> is provided and is "cat"
        /// or "dog" then return the Cat or Dog (respectively) object that has been
        /// waiting the longest; otherwise, return the Animal object that has
        /// been waiting the longest regardless of its derived type
        /// </summary>
        /// <param name="catQueue">Queue of Cat objects in the animal shelter</param>
        /// <param name="dogQueue">Queue of Dog objects in the animal shelter</param>
        /// <param name="preferredAnimalType">The type of Animal object to return.
        /// If "cat" then return the Cat object that has been waiting the longest.
        /// If "dog" then return the Dog object that has been waiting the longest.
        /// Otherwise, return the longest waiting Animal regardless of its derived
        /// type.</param>
        /// <returns>The Animal object of the specifed derived type in 
        /// <paramref name="preferredAnimalType"/> or either derived type that has
        /// been waiting the longest based on its serial number.</returns>
        public static Animal Dequeue(MyQueue<Cat> catQueue, MyQueue<Dog> dogQueue,
            string preferredAnimalType)
        {
            // I would probably use System.Type rather than a string in real code...
            if (preferredAnimalType.ToLower() == "cat")
            {
                return catQueue.Dequeue();
            }
            else if (preferredAnimalType.ToLower() == "dog")
            {
                return dogQueue.Dequeue();
            }
            else
            {
                // We don't care which type of Animal object we are returning, so
                // compare the longest waiting Cat and Dog object through Peek() and
                // dequeue the one with the lowest serial number (added before the other)
                return catQueue.Peek().SerialNumber < dogQueue.Peek().SerialNumber ? 
                    (Animal)catQueue.Dequeue() : dogQueue.Peek();
            }
        }
    }
}
