using Roommates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true; TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Delete a room"):
                        List<Room> roomOptionsForDelete = roomRepo.GetAll();
                        foreach (Room r in roomOptionsForDelete)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name}");
                        }
                        Console.Write("Which room would you like to delete? ");
                        int selectedRoomForDeleteId = int.Parse(Console.ReadLine());
                        roomRepo.Delete(selectedRoomForDeleteId);
                        Console.WriteLine("Room deleted successfully.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}.");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for chore"):
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(choreId);

                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string choreName = Console.ReadLine();

                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName,
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show unassigned chores"):
                        List<Chore> unassignedChores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in unassignedChores)
                        {
                            Console.WriteLine($"{c.Name}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all roommates"):
                        List<Roommate> roommates = roommateRepo.GetAll();
                        foreach (Roommate r in roommates)
                        {
                            Console.WriteLine($"{r.FirstName} {r.LastName} has an Id of {r.Id}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for roommate"):
                        Console.Write("Roommate Id: ");
                        int roommateId = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetById(roommateId);

                        Console.WriteLine($"{roommate.Id} - {roommate.FirstName} {roommate.LastName}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search roommates by room id"):
                        Console.Write("Room Id: ");
                        int roomId = int.Parse(Console.ReadLine());

                        List<Roommate> roommateByRoom = roommateRepo.GetRoommatesByRoomId(roomId);
                        foreach (Roommate r in roommateByRoom)
                        {
                            Console.WriteLine($"{r.Id} - {r.FirstName} {r.LastName}");
                            Console.Write("Press any key to continue");
                        }
                            Console.ReadKey();
                        break;
                    case ("Add a roommate"):
                        Console.Write("Roommate first name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Roommate last name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Roommate rent portion: ");
                        string rentPortion = Console.ReadLine();
                        Console.Write("Roommate move in date: ");
                        string moveInDate = Console.ReadLine();
                        Console.Write("Roommate room id: ");
                        string roommateRoomId = Console.ReadLine();
                        Room roommateRoom = roomRepo.GetById(Convert.ToInt32(roommateRoomId));

                        Roommate roommateToAdd = new Roommate()
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            RentPortion = Convert.ToInt32(rentPortion),
                            MovedInDate = Convert.ToDateTime(moveInDate),
                            Room = roommateRoom
                        };

                        roommateRepo.Insert(roommateToAdd);

                        Console.WriteLine($"{roommateToAdd.FirstName} {roommateToAdd.LastName} has been added and assigned an Id of {roommateToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a roommate"):
                        List<Roommate> roommateOptions = roommateRepo.GetAll();
                        foreach (Roommate r in roommateOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.FirstName} {r.LastName}");
                        }
                        Console.Write("Which roommate would you like to update? ");
                        int selectedRoommateId = int.Parse(Console.ReadLine());
                        Roommate selectedRoommate = roommateOptions.FirstOrDefault(r => r.Id == selectedRoommateId);
                        Console.Write("Roommate first name: ");
                        selectedRoommate.FirstName = Console.ReadLine();
                        Console.Write("Roommate last name: ");
                        selectedRoommate.LastName = Console.ReadLine();
                        Console.Write("Roommate rent portion: ");
                        selectedRoommate.RentPortion = int.Parse(Console.ReadLine());
                        Console.Write("Roommate move in date: ");
                        selectedRoommate.MovedInDate = Convert.ToDateTime(Console.ReadLine());
                        Console.Write("Roommate room id: ");
                        int selectedRoommateRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoommateRoom = roomRepo.GetById(selectedRoommateRoomId);
                        selectedRoommate.Room = selectedRoommateRoom;
                        roommateRepo.Update(selectedRoommate);
                        Console.WriteLine($"{selectedRoommate.FirstName} {selectedRoommate.LastName} has been updated.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Delete a roommate"):
                        List<Roommate> roommateOptionsForDelete = roommateRepo.GetAll();
                        foreach (Roommate r in roommateOptionsForDelete)
                        {
                            Console.WriteLine($"{r.Id} - {r.FirstName} {r.LastName}");
                        }
                        Console.Write("Which roommate would you like to delete? ");
                        int selectedRoommateForDeleteId = int.Parse(Console.ReadLine());
                        roommateRepo.Delete(selectedRoommateForDeleteId);
                        Console.WriteLine("Roommate deleted successfully.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Update a room",
                "Delete a room",
                "Show all chores",
                "Search for chore",
                "Add a chore",
                "Show unassigned chores",
                "Show all roommates",
                "Search for roommate",
                "Search roommates by room id",
                "Add a roommate",
                "Update a roommate",
                "Delete a roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}